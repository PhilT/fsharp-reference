(**
---
title: Class
description: Supports F# as an OO language. Mainly used when working closely with .NET libraries.
keywords: inherit override default abstract member type
---

Consider using discriminated unions or records when working with OO code.

Class definition and primary constructor takes 2 arguments and
defines self for the entire class
 *)
type MyClass(x: int, y: int, data) as self =
  let someData = data
  do printfn "%d %d" x y // run some code on constructor instantiation
  do self.PrintMessage() // example of self being used to call a member function
  new() = MyClass(0, 0, "data") // additional constructors
  member self.PrintMessage() = // define self for this method only
    printfn "Some data: %s" someData

let myClass = MyClass()


(**
## Inheritance
 *)

type MyBase() =
  member this.Func1 x = x + 1

type MyDerived() =
  inherit MyBase()

(**
Private members of the base class such as let bindings and constructor
parameters cannot be accessed from the subclass.

`base` is used to identify the base class. Like `super` in other languages.

Virtual methods and properties are defined as follows:
 *)

type MyClassBase() =
  let mutable z = 0
  abstract member Function1: int -> int
  default u.Function1(a: int) = z <- z + a; z

type MyClassDerived() =
  inherit MyClassBase()
  override u.Function1(a: int) = a + 1

(**
## Abstract Class
 *)

[<AbstractClass>]
type Shape2D(x0: float, y0: float) =
  let mutable x, y = x0, y0
  let mutable rotAngle = 0.0

  // This property cannot be overridden
  member this.CenterX with get() = x and set xval = x <- xval

  // This property is abstract without implementation and must
  // be implemented in a sub-class
  abstract Area: float with get

  // Abstract method with default implementation
  // A virtual method in other .NET languages
  abstract member Rotate: float -> unit
  default this.Rotate(angle) = rotAngle <- rotAngle + angle
