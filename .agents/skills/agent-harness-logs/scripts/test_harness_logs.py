from __future__ import annotations

import json
from pathlib import Path
import shutil
import sqlite3
import subprocess
import sys
import tempfile
import unittest


SCRIPT = Path(__file__).with_name("harness_logs.py")
TOON_CLI_PACKAGE = "@toon-format/cli@4.1.1"


class HarnessLogsTests(unittest.TestCase):
    def run_cli(self, *arguments: str) -> subprocess.CompletedProcess[str]:
        return subprocess.run(
            [sys.executable, str(SCRIPT), *arguments],
            check=False,
            capture_output=True,
            text=True,
        )

    def decode_toon(self, value: str) -> object:
        npx = shutil.which("npx")
        if npx is None:
            self.skipTest("Node.js and npx are required for TOON output")
        result = subprocess.run(
            [npx, "--yes", TOON_CLI_PACKAGE, "--decode"],
            input=value,
            check=False,
            capture_output=True,
            text=True,
        )
        self.assertEqual(0, result.returncode, result.stderr)
        return json.loads(result.stdout)

    def test_search_streams_and_redacts_text(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            log = Path(directory) / "agent.log"
            log.write_text("ok\nAuthorization: Bearer secret-token\n", encoding="utf-8")

            result = self.run_cli("search", directory, "authorization", "--ignore-case", "--redact")

            self.assertEqual(0, result.returncode)
            record = json.loads(result.stdout)
            self.assertEqual(2, record["line"])
            self.assertIn("<REDACTED>", record["text"])
            self.assertNotIn("secret-token", record["text"])

    def test_jsonl_summary_counts_nested_field_and_invalid_lines(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            log = Path(directory) / "session.jsonl"
            log.write_text(
                '{"event":{"name":"tool"}}\n'
                '{"event":{"name":"tool"}}\n'
                '{"event":{"name":"error"}}\n'
                'not-json\n',
                encoding="utf-8",
            )

            result = self.run_cli("jsonl-summary", str(log), "--field", "event.name")

            self.assertEqual(0, result.returncode)
            summary = json.loads(result.stdout)
            self.assertEqual(4, summary["records"])
            self.assertEqual(1, summary["invalid"])
            self.assertEqual({"error": 1, "tool": 2}, summary["counts"])

    def test_toon_output_round_trips_structured_results(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            log = Path(directory) / "agent.log"
            log.write_text("first error\nsecond error\n", encoding="utf-8")

            result = self.run_cli("search", directory, "error", "--toon")

            self.assertEqual(0, result.returncode, result.stderr)
            records = self.decode_toon(result.stdout)
            self.assertEqual(2, len(records))
            self.assertEqual("first error", records[0]["text"])

    def test_sqlite_query_is_read_only(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            database = Path(directory) / "state.sqlite"
            with sqlite3.connect(database) as connection:
                connection.execute("CREATE TABLE events (kind TEXT)")
                connection.execute("INSERT INTO events VALUES ('tool')")

            result = self.run_cli("sqlite", str(database), "SELECT kind FROM events")
            rejected = self.run_cli("sqlite", str(database), "DELETE FROM events")

            self.assertEqual(0, result.returncode)
            self.assertEqual({"kind": "tool"}, json.loads(result.stdout))
            self.assertEqual(2, rejected.returncode)
            self.assertIn("only read-only", rejected.stderr)

    def test_redact_requires_a_distinct_output(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            log = Path(directory) / "agent.log"
            output = Path(directory) / "redacted.log"
            log.write_text('{"api_key":"secret-value"}\n', encoding="utf-8")

            rejected = self.run_cli("redact", str(log), str(log))
            accepted = self.run_cli("redact", str(log), str(output))

            self.assertEqual(2, rejected.returncode)
            self.assertIn("output must differ", rejected.stderr)
            self.assertEqual(0, accepted.returncode)
            self.assertEqual('{"api_key":"<REDACTED>"}\n', output.read_text(encoding="utf-8"))


if __name__ == "__main__":
    unittest.main()