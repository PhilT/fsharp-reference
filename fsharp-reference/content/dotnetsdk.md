---
title: .NET Core 3.0 SDK
description: It appears to be the future direction of .NET. It makes sense in order to support multiple operating systems. One of the great features of version 3 is that it ships with F# Interactive, a shell to run F# scripts without first compiling them. Similar to Node's REPL or Ruby's IRB and Pry.
---

It can be installed from Chocolatey:

```powershell
cinst dotnetcore-sdk
```

Check the correct version is installed

```powershell
dotnet --version
```

Run the FSI Shell (quit with `#quit;;` or press `CTRL+D`)

```powershell
dotnet fsi
```

Run a saved script [example.fsi](example.html)
```powershell
dotnet fsi example.fsi
```

To create a new F# console project:

```powershell
mkdir newapp
cd newapp
dotnet new console -lang F#
dotnet run
```
