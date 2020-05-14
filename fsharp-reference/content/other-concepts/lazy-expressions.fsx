(**
---
title: Lazy Expressions
description: Execute (`Force`) only when the result is required.
keywords: performance
---

Helps improve performance.
 *)

let x = 10
let result = lazy (x + 10)
printfn "%d" (result.Force())
