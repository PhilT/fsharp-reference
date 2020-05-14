(**
---
title: Nested Namespaces
description: They can be nested multiple levels.
keywords: module
---

Must be fully qualified, otherwise a new top-level namespace is created.
 *)

namespace Outer
type MyClass() =
  member this.X(x) = x + 1

namespace Outer.Inner
type MyClass() =
  member this.Prop1 = "X"
