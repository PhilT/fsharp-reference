(**
---
title: Object Expressions
description: Used to avoid the extra overhead of creating a new named type.
keywords: class type anonymous member
---

Minimize the number of types created in a program.

Object expression that overrides `ToString` method
 *)
let obj1 = { new System.Object() with member x.ToString() = "F#" }
printfn "%A" obj1

// Object expression on an interface
let delimiter(delim1: string, delim2: string, value: string) =
  { new System.IFormattable with
      member x.ToString(format: string, provider: System.IFormatProvider) =
      if format = "D" then
        delim1 + value + delim2
      else
        value
  }

let obj2 = delimiter("{", "}", "Bananas!")
printfn "%A" (System.String.Format("{0:D}", obj2))

type IFirst =
  abstract F: unit -> unit

type ISecond =
  abstract H: unit -> unit

(**
Object expression that implements 2 interfaces
 *)
let implementer() =
  { new ISecond with
      member this.H() = ()
    interface IFirst with
      member this.F() = ()
  }
