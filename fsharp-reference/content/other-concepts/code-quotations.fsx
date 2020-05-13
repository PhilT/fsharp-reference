(**
---
title: Code Quotations
description: Pass around F# code as a dynamic expression. Untyped quotations are a little faster to process.
---

TODO: Mention use cases.
 *)

open Microsoft.FSharp.Quotations

let expr: Expr<int> = <@ 1 + 1 @> // Typed code quotation
let expr2: Expr = <@@ 1 + 1 @@> // Untyped code quotation

<@ let f x = x + 1 in f 20 @>

(**
or
 *)

<@
  let f x = x + 1
  f 20
@>

(**
## Splicing
 *)

<@ 1 + %expr @> // Add typed expression `expr` to quotation
<@@ 1 + %%expr2 @@> // Add untyped expression `expr2` to quotation

// TODO: More concrete example needed
