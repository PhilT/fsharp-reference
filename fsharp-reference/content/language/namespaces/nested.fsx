(**
---
title: Nested namespaces
description: Must be fully qualified, otherwise a new top-level namespace is created.
---
 *)

namespace Outer
type MyClass() =
  member this.X(x) = x + 1

namespace Outer.Inner
type MyClass() =
  member this.Prop1 = "X"
