# Language Quickstarts

Install, then send a first prompt (`sendAndWait`) with an "approve all" permission handler. Full streaming/custom-tool progressions live in the [getting-started tutorial](https://github.com/github/copilot-sdk/tree/main/docs/getting-started.md).

## Node.js / TypeScript

```bash
npm install @github/copilot-sdk tsx
```

```typescript
import { CopilotClient } from "@github/copilot-sdk";

const client = new CopilotClient();
const session = await client.createSession({ model: "auto" });
const response = await session.sendAndWait({ prompt: "What is 2 + 2?" });
console.log(response?.data.content);
await client.stop();
```

## Python

```bash
pip install github-copilot-sdk
```

```python
import asyncio
from copilot import CopilotClient
from copilot.session import PermissionHandler

async def main():
    client = CopilotClient()
    await client.start()
    session = await client.create_session(on_permission_request=PermissionHandler.approve_all, model="auto")
    response = await session.send_and_wait("What is 2 + 2?")
    print(response.data.content)
    await client.stop()

asyncio.run(main())
```

## Go

```bash
go get github.com/github/copilot-sdk/go
```

```go
client := copilot.NewClient(nil)
client.Start(ctx)
defer client.Stop()

session, _ := client.CreateSession(ctx, &copilot.SessionConfig{Model: "auto"})
response, _ := session.SendAndWait(ctx, copilot.MessageOptions{Prompt: "What is 2 + 2?"})
if d, ok := response.Data.(*copilot.AssistantMessageData); ok {
    fmt.Println(d.Content)
}
```

## Rust

```bash
cargo add github-copilot-sdk --features derive
cargo add tokio --features rt-multi-thread,macros
```

```rust
use github_copilot_sdk::handler::ApproveAllHandler;
use github_copilot_sdk::{Client, ClientOptions, MessageOptions, SessionConfig};
use std::sync::Arc;

let client = Client::start(ClientOptions::default()).await?;
let session = client
    .create_session(SessionConfig::default().with_permission_handler(Arc::new(ApproveAllHandler)))
    .await?;
let response = session.send_and_wait(MessageOptions::new("What is 2 + 2?")).await?;
```

## .NET

```bash
dotnet add package GitHub.Copilot.SDK
```

```csharp
using GitHub.Copilot;

await using var client = new CopilotClient();
await using var session = await client.CreateSessionAsync(new SessionConfig
{
    Model = "auto",
    OnPermissionRequest = PermissionHandler.ApproveAll
});

var response = await session.SendAndWaitAsync(new MessageOptions { Prompt = "What is 2 + 2?" });
Console.WriteLine(response?.Data.Content);
```

## Java

```xml
<dependency>
    <groupId>com.github</groupId>
    <artifactId>copilot-sdk-java</artifactId>
    <version>${copilot.sdk.version}</version>
</dependency>
```

```java
try (var client = new CopilotClient()) {
    client.start().get();
    var session = client.createSession(
        new SessionConfig().setModel("auto").setOnPermissionRequest(PermissionHandler.APPROVE_ALL)
    ).get();
    var response = session.sendAndWait(new MessageOptions().setPrompt("What is 2 + 2?")).get();
    System.out.println(response.getData().content());
    client.stop().get();
}
```

## CLI prerequisite

Node.js, Python, and .NET auto-manage the `copilot` CLI. Go, Java, and Rust require it installed and authenticated separately — verify with `copilot --version` before running any sample above.
