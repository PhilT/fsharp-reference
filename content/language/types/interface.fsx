(**
# Interface

A set of abstract members that would be implemented in a Class.

Interface
 *)
type IPrintable =
  abstract member Print: unit -> unit

(**
Implementation
 *)
type SomeClass(x: int, y: float) =
  interface IPrintable with
    member this.Print() = printfn "%d %f" x y

(**
Interface methods can only be called through the interface by upcasting (`:>`):
 *)
let x1 = SomeClass(1, 2.0)
(x1:> IPrintable).Print()

(**
Alternatively, declare a method on the object that does the upcast:
 *)
type SomeClass2(x: int, y: float) =
  member this.Print() = (this:> IPrintable).Print()
  interface IPrintable with
    member this.Print() = printfn "%d %f" x y

let x2 = SomeClass2(1, 2.0)
x2.Print()

// TODO: Interfaces using Object Expressions

// TODO: Interface Inheritance
