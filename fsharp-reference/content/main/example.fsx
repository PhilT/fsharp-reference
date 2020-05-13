(**
---
title: Simple Example
description: Simple example
---

Run with `dotnet fsi example.fsx

 *)

let hello str = sprintf "Hello, %s!!" str
printfn "%s" (hello "World")
