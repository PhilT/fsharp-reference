(**
---
title: Structures
description: More compact and efficient than classes. For types that have a small amount of data and simple behaviour. Structures are passed by value.
keywords: class record readonly discriminated union
---

Structures are useful for frequently accessed data when the overhead of
accessing the heap is a problem.
 *)

type Point3D =
  struct
    val x: float
    val y: float
    val z: float
  end


type Point2D =
  struct
    val x: float
    val y: float
    new(X: float, Y: float) = {x = X; y = Y}

    member this.DistanceFrom(p: Point2D) =
      let dX = (p.x - this.x) ** 2.0
      let dY = (p.y - this.y) ** 2.0

      dX + dY
      |> sqrt
  end

let p1 = Point2D (1.0, 1.0)
let p2 = Point2D (2.0, 2.0)
let d = p2.DistanceFrom p1

printfn "Distance from %0.2f,%0.2f to %0.2f,%0.2f is %0.2f" p1.x p1.y p2.x p2.y d


// TODO: ByRefLike structs - understand usage


(**
## ReadOnly Structs
 *)

open System.Runtime.CompilerServices
[<IsReadOnly; Struct>]
type S(count1: int, count2: int) =
  member x.Count1 = count1
  member x.Count2 = count2

(**
## Struct Records and Discriminated Unions

Records and Discriminated Unions can be represented as Structs with the
`[<Struct>]` attribute.

TODO: Clarify why this is useful
 *)
