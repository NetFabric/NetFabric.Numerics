#!/usr/bin/env python3
"""Query local coding-agent logs without sending their contents to a model."""

from __future__ import annotations

import argparse
import json
import os
from pathlib import Path
import platform
import re
import shutil
import sqlite3
import subprocess
import sys
from typing import Any, Iterable, Iterator


TEXT_SUFFIXES = {".json", ".jsonl", ".log", ".txt"}
TOON_CLI_PACKAGE = "@toon-format/cli@4.1.1"
SECRET_PATTERNS = (
    re.compile(r"(?i)([\"']?authorization[\"']?\s*[:=]\s*[\"']?(?:bearer\s+)?)[^\s,;\"']+"),
    re.compile(r"(?i)([\"']?(?:api[_-]?key|access[_-]?token|secret|password)[\"']?\s*[:=]\s*[\"']?)[^\s,;\"']+"),
    re.compile(r"\b(?:gh[opsu]_[A-Za-z0-9_]{20,}|sk-[A-Za-z0-9_-]{20,})\b"),
    re.compile(r"-----BEGIN [A-Z ]*PRIVATE KEY-----.*?-----END [A-Z ]*PRIVATE KEY-----", re.DOTALL),
)


def home() -> Path:
    return Path.home()


def candidate_roots() -> list[tuple[str, Path, str]]:
    """Return documented roots first and explicit discovery fallbacks second."""
    user_home = home()
    roots: list[tuple[str, Path, str]] = [
        ("copilot-cli", Path(os.environ.get("COPILOT_HOME", user_home / ".copilot")), "documented"),
        ("claude-code", user_home / ".claude", "documented"),
        ("codex", Path(os.environ.get("CODEX_HOME", user_home / ".codex")), "documented"),
        ("gemini-cli", Path(os.environ.get("GEMINI_CLI_HOME", user_home)) / ".gemini", "documented"),
    ]

    if platform.system() == "Windows":
        data_home = Path(os.environ.get("USERPROFILE", user_home)) / ".local" / "share"
        app_data = Path(os.environ.get("APPDATA", user_home / "AppData" / "Roaming"))
        roots.extend(
            [
                ("opencode", data_home / "opencode", "documented"),
                ("vscode", app_data / "Code" / "logs", "discovery"),
                ("vscode-insiders", app_data / "Code - Insiders" / "logs", "discovery"),
            ]
        )
    elif platform.system() == "Darwin":
        roots.extend(
            [
                ("opencode", user_home / ".local" / "share" / "opencode", "documented"),
                ("vscode", user_home / "Library" / "Application Support" / "Code" / "logs", "discovery"),
                ("vscode-insiders", user_home / "Library" / "Application Support" / "Code - Insiders" / "logs", "discovery"),
            ]
        )
    else:
        xdg_data = Path(os.environ.get("XDG_DATA_HOME", user_home / ".local" / "share"))
        xdg_config = Path(os.environ.get("XDG_CONFIG_HOME", user_home / ".config"))
        roots.extend(
            [
                ("opencode", xdg_data / "opencode", "documented"),
                ("vscode", xdg_config / "Code" / "logs", "discovery"),
                ("vscode-insiders", xdg_config / "Code - Insiders" / "logs", "discovery"),
            ]
        )
    return roots


def iter_files(root: Path) -> Iterator[Path]:
    if root.is_file():
        yield root
        return
    if not root.is_dir():
        return
    for path in root.rglob("*"):
        if path.is_file():
            yield path


def redact(text: str) -> str:
    for pattern in SECRET_PATTERNS:
        text = pattern.sub(lambda match: f"{match.group(1)}<REDACTED>" if match.lastindex else "<REDACTED>", text)
    return text


def flatten(value: Any, prefix: str = "") -> Iterator[tuple[str, Any]]:
    if isinstance(value, dict):
        for key, child in value.items():
            name = f"{prefix}.{key}" if prefix else str(key)
            yield from flatten(child, name)
    elif isinstance(value, list):
        for index, child in enumerate(value):
            yield from flatten(child, f"{prefix}[{index}]")
    else:
        yield prefix, value


def encode_toon(value: Any) -> str:
    npx = shutil.which("npx")
    if npx is None:
        raise ValueError("--toon requires Node.js and npx; install Node.js or omit --toon for JSON output")
    result = subprocess.run(
        [npx, "--yes", TOON_CLI_PACKAGE, "--encode"],
        input=json.dumps(value),
        check=False,
        capture_output=True,
        text=True,
    )
    if result.returncode:
        detail = result.stderr.strip() or "unknown encoder error"
        raise ValueError(f"TOON encoding failed: {detail}")
    return result.stdout


def emit(value: Any, args: argparse.Namespace, *, json_lines: bool = False) -> None:
    if args.toon:
        encoded = encode_toon(value)
        print(encoded, end="" if encoded.endswith("\n") else "\n")
    elif json_lines:
        for item in value:
            print(json.dumps(item))
    else:
        print(json.dumps(value, sort_keys=True))


def cmd_discover(args: argparse.Namespace) -> int:
    records = [
        {"harness": harness, "path": str(path), "exists": path.exists(), "confidence": confidence}
        for harness, path, confidence in candidate_roots()
    ]
    emit(records, args, json_lines=True)
    return 0


def cmd_inventory(args: argparse.Namespace) -> int:
    files = sorted(iter_files(Path(args.path)), key=lambda item: item.stat().st_mtime, reverse=True)
    records = []
    for path in files[: args.limit]:
        stat = path.stat()
        records.append({"path": str(path), "bytes": stat.st_size, "mtime": stat.st_mtime, "suffix": path.suffix.lower()})
    emit(records, args, json_lines=True)
    return 0


def stream_lines(paths: Iterable[Path]) -> Iterator[tuple[Path, int, str]]:
    for path in paths:
        if path.suffix.lower() not in TEXT_SUFFIXES:
            continue
        try:
            with path.open("r", encoding="utf-8", errors="replace") as handle:
                for number, line in enumerate(handle, 1):
                    yield path, number, line.rstrip("\n")
        except OSError as error:
            print(f"warning: {path}: {error}", file=sys.stderr)


def cmd_search(args: argparse.Namespace) -> int:
    pattern = re.compile(args.pattern, re.IGNORECASE if args.ignore_case else 0)
    records = []
    for path, number, line in stream_lines(iter_files(Path(args.path))):
        if pattern.search(line):
            text = redact(line) if args.redact else line
            records.append({"path": str(path), "line": number, "text": text})
            if len(records) >= args.limit:
                break
    emit(records, args, json_lines=True)
    return 0 if records else 1


def cmd_jsonl_summary(args: argparse.Namespace) -> int:
    counts: dict[str, int] = {}
    invalid = 0
    total = 0
    for _, _, line in stream_lines([Path(args.path)]):
        if not line.strip():
            continue
        total += 1
        try:
            record = json.loads(line)
        except json.JSONDecodeError:
            invalid += 1
            continue
        value = dict(flatten(record)).get(args.field, "<missing>")
        key = json.dumps(value, sort_keys=True) if not isinstance(value, str) else value
        counts[key] = counts.get(key, 0) + 1
    emit({"path": args.path, "records": total, "invalid": invalid, "field": args.field, "counts": counts}, args)
    return 0


def cmd_sqlite(args: argparse.Namespace) -> int:
    if not args.query.lstrip().lower().startswith(("select", "pragma", "with", "explain")):
        raise ValueError("only read-only SELECT, PRAGMA, WITH, or EXPLAIN queries are allowed")
    uri = f"file:{Path(args.path).resolve()}?mode=ro"
    records = []
    with sqlite3.connect(uri, uri=True) as connection:
        connection.row_factory = sqlite3.Row
        for index, row in enumerate(connection.execute(args.query)):
            if index >= args.limit:
                break
            records.append(dict(row))
    emit(records, args, json_lines=True)
    return 0


def cmd_redact(args: argparse.Namespace) -> int:
    source = Path(args.path)
    destination = Path(args.output)
    if source.resolve() == destination.resolve():
        raise ValueError("output must differ from input")
    with source.open("r", encoding="utf-8", errors="replace") as reader, destination.open("w", encoding="utf-8") as writer:
        for line in reader:
            writer.write(redact(line))
    return 0


def parser() -> argparse.ArgumentParser:
    root = argparse.ArgumentParser(description=__doc__)
    commands = root.add_subparsers(dest="command", required=True)

    discover = commands.add_parser("discover", help="print candidate harness roots as JSONL")
    discover.add_argument("--toon", action="store_true", help="output TOON instead of JSONL")
    discover.set_defaults(handler=cmd_discover)

    inventory = commands.add_parser("inventory", help="list files by modification time")
    inventory.add_argument("path")
    inventory.add_argument("--limit", type=int, default=100)
    inventory.add_argument("--toon", action="store_true", help="output TOON instead of JSONL")
    inventory.set_defaults(handler=cmd_inventory)

    search = commands.add_parser("search", help="stream a regular expression across text logs")
    search.add_argument("path")
    search.add_argument("pattern")
    search.add_argument("--ignore-case", action="store_true")
    search.add_argument("--redact", action="store_true")
    search.add_argument("--limit", type=int, default=100)
    search.add_argument("--toon", action="store_true", help="output TOON instead of JSONL")
    search.set_defaults(handler=cmd_search)

    summary = commands.add_parser("jsonl-summary", help="count values of a flattened JSONL field")
    summary.add_argument("path")
    summary.add_argument("--field", default="type")
    summary.add_argument("--toon", action="store_true", help="output TOON instead of JSON")
    summary.set_defaults(handler=cmd_jsonl_summary)

    sqlite = commands.add_parser("sqlite", help="run a read-only SQLite query")
    sqlite.add_argument("path")
    sqlite.add_argument("query")
    sqlite.add_argument("--limit", type=int, default=100)
    sqlite.add_argument("--toon", action="store_true", help="output TOON instead of JSONL")
    sqlite.set_defaults(handler=cmd_sqlite)

    redact_command = commands.add_parser("redact", help="write a best-effort redacted text copy")
    redact_command.add_argument("path")
    redact_command.add_argument("output")
    redact_command.set_defaults(handler=cmd_redact)
    return root


def main() -> int:
    try:
        args = parser().parse_args()
        return args.handler(args)
    except (OSError, sqlite3.Error, ValueError) as error:
        print(f"error: {error}", file=sys.stderr)
        return 2


if __name__ == "__main__":
    raise SystemExit(main())