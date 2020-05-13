(**
---
title: Operator Overloading
description: Operators can be overloaded as `member`s of classes or records.
---

They can also be overloaded at the global level using `let`:
 *)

type Vector(x: float, y: float) =
  do printfn "%f %f" x y
  member this.X = x
  member this.Y = y

type SomeType =
  static member (~-) (v: Vector) = Vector(-1.0 * v.X, -1.0 * v.Y)

let inline (+?) (x: int) (y: int) = x + 2 * y

(**
Existing operators can be overloaded but new, custom operators can
also be created using a combination of allowed operator characters.

In order to make an unary operator (as opposed to binary) the operator must
be prefixed with `~` as in the example above, `(~-)`. This overloads the
`-` operator.
 *)
