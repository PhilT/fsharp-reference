(**
Nested namespaces:
 *)

namespace Outer
type MyClass() =
  member this.X(x) = x + 1

namespace Outer.Inner
type MyClass() =
  member this.Prop1 = "X"
