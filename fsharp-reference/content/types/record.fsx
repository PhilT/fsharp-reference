(**
---
title: Record
description: Aggregate of named values optionally with members.
---

Simpler than classes, they lack interface implementation and inheritance,
constructors and hidden fields. They be reference types (default) or structs.
 *)

open System

type Point = { X: float; Y: float; Z: float }

[<Struct>]
type StructPoint = {
  X: float
  Y: float
  Z: float
}

let myPoint = { X = 1.0; Y = 1.1; Z = 0.0 }
let explicitPoint = { Point.X = 1.0; Point.Y = 1.1; Point.Z = 0.0 }

(**
Records and anonymous records are immutable.
Instead you copy and update them
 *)
let newPoint = { myPoint with Y = 1.2; Z = 2.0 }


(**
## Mutually recursive records

Reference a record before defining it:
 *)

type Person = {
  Name: string
  Age: int
  Address: Address
} and Address = {
  Line1: string
  Line2: string
  PostCode: string
}


(**
## Pattern Matching
 *)

type Point3D = { X: float; Y: float; Z: float }
let evalPoint (point: Point3D) =
 match point with
 | { X = 0.0; Y = 0.0; Z = 0.0 } -> printfn "At origin"
 | { X = xVal; Y = 0.0; Z = 0.0 } -> printfn "At %f along x-axis" xVal
 | { X = xVal; Y = yVal; Z = zVal } -> printfn "At %f, %f, %f" xVal yVal zVal

evalPoint { X = 0.0; Y = 0.0; Z = 0.0 } // "At origin"
evalPoint { X = 1.0; Y = 0.0; Z = 0.0 } // "At 1.0 along x-axis


(**
# Anonymous Record

F# Records that don't need to be declared before use.
 *)

let getCircleStats radius =
  let d = radius * 2.0
  let a = Math.PI * (radius ** 2.0)
  let c = 2.0 * Math.PI * radius

  {| Diameter = d; Area = a; Circumference = c|}
  // And can also be declared as struct records
  // struct {| Diameter = d; Area = a; Circumference = c|}

(**
The struct keyword is not explicitly needed when it's already
been defined, e.g. as a parameter to a function.
 *)
let r = 2.0
let stats = getCircleStats r

printfn "Circle with radius: %f has diameter %f, area %f, circ %f"
  r stats.Diameter stats.Area stats.Circumference

(**
They can also be passed as parameters
 *)

let printCircleStats r (stats: {| Diameter: float; Area: float; Circumference: float |}) =
  printfn "Stats for radius %f: %A" r stats

printCircleStats r stats

// TODO: Finish anonymous records
