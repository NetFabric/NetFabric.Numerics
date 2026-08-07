# Testing as the Behavioral Contract

Tests are the most reliable deterministic signal for whether agent-generated behavior is correct. Treat the full suite as the definition of "done" — the same way `dotnet format` defines style compliance.

## AGENTS.md Instruction Pattern

```markdown
Run the full test suite before finishing any feature. A feature is not complete
until all tests pass. Do not weaken, skip, or delete a test to make it pass.
```

Explicit and machine-checkable beats "make sure tests pass" — it also tells the agent skipping/deleting a failing test isn't an acceptable resolution.

## Test Tiers

| Tier | Describes | Tooling |
|------|-----------|---------|
| Unit | domain rules | xUnit/NUnit/MSTest |
| Integration | how components interact | `WebApplicationFactory`, Testcontainers |
| End-to-end | user flows | [Playwright](https://playwright.dev/) |

## Triaging a Failing Test

When a test fails after an agent change, there are exactly three explanations:

1. The implementation is wrong.
2. The test is outdated.
3. The behavior intentionally changed.

An agent infers which applies from surrounding code, naming, domain rules, and cross-solution behavior consistency — but only if the failure signal itself is deterministic (same input → same failure, every run). Flaky tests destroy this feedback loop; fix or quarantine them before relying on agents to self-correct against the suite.

Without a solid test suite, agents cannot safely refactor or extend a system — they have no way to confirm a change didn't break something elsewhere. With one, they can operate on larger changes with less human review per step.
