(**
---
title: Slices
description: A subset of a data type. Like an indexer but for multiple values.
---

Strings, lists, arrays and 2D arrays have built-in support


## Slicing

A list of 100 integers
 *)
let fullList = [1 .. 100]

let smallSlice = fullList.[1..5]
printfn "Small slice: %A" smallSlice

let fromStart = fullList.[..10]
printfn "Small slice from start: %A" fromStart

(**
An array of 100 integers
 *)
let fullArray = [|1..100|]

let smallSlice2 = fullList.[1..5]

printfn "Small slice: %A" smallSlice2

let fromStart2 = fullList.[..10]
printfn "Small slice from start: %A" fromStart2


(**
## Slicing multidimensional arrays

2D arrays can be sliced as follows. For 3D arrays,
`GetSlice` must be defined.

Generate a 3x3 2D matrix
 *)
let A = array2D [[1;2;3];[4;5;6];[7;8;9]]
printfn "Full matrix:\n %A" A

(**
Take the first row
 *)
let row0 = A.[0,*]
printfn "Row 0: %A" row0

(**
Take the first column
 *)
let col0 = A.[*,0]
printfn "Column 0: %A" col0

(**
Take all rows but only two columns
 *)
let subA = A.[*,0..1]
printfn "%A" subA

(**
Take two rows and all columns
 *)
let subA' = A.[0..1,*]
printfn "%A" subA'

(**
Slice a 2x2 matrix out of the full 3x3 matrix
 *)
let twoByTwo = A.[0..1,0..1]
printfn "%A" twoByTwo


(**
## Define a `GetSlice` method
 *)

open System

type ArraySegment<'TItem> with
  member segment.GetSlice(start, finish) =
    let start = defaultArg start 0
    let finish = defaultArg finish segment.Count
    ArraySegment(segment.Array, segment.Offset + start, finish - start)

let arr = ArraySegment [| 1 .. 10 |]
let slice = arr.[2..5] //[ 3; 4; 5]
printfn "slice: %A" slice
printfn "beginning slice: %A" arr.[..3]
