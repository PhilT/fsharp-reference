(**
---
title: Install .NET Core 3.0 SDK
description: .NET Core 3.0 SDK includes F# Interactive (fsi.exe). This is a REPL like irb or node. Previous versions of .NET Core did not come with FSI.
---

It can be installed from Chocolatey:

```bash
cinst dotnetcore-sdk
```

Check the correct version is installed

```bash
dotnet --version
```

Run the FSI Shell (quit with `#quit;;` or press `CTRL+D`)

```bash
dotnet fsi
```

Run a saved script [example.fsi](example.html)
```bash
dotnet fsi example.fsi
```

To create a new F# console project:

```bash
mkdir newapp
cd newapp
dotnet new console -lang F#
dotnet run
```
 *)
