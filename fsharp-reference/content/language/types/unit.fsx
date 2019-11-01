(**
---
title: Unit
description: A type used when a return value is of no interest e.g. the return type of `printf` is `unit`.
---

Similar to the `void` type is C# and C++.

A `do` binding or the return value of a top level module must be `unit`.
 *)

let function1 x y = x + y
function1 10 20 |> ignore

(**
Because `function1` returns a value other than `unit` it must be `ignore`d.
 *)
