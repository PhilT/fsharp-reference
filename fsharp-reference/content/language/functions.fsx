(**
---
title: Functions - function values, curried, anonymous
description: A core unit of program execution.
---

Functions are defined with `let`
 *)

let inc1 x = x + 1
let inc1Result = inc1 1
printfn "inc1 1: %A" inc1Result

(**
Arguments can have a type specified
 *)
let inc2 (a: int): int = a + 1
printfn "inc2 2: %A" (inc2 2)

(**
Indentation indicates function body
 *)
let cylinderVol radius length =
  let pi = 3.14159
  length * pi * radius * radius

printfn "cylinderVol %A" (cylinderVol 3.0 20.0)

(**
Code can also be executed without being named using a do binding.
Code in a top-level do binding is executed when the module is
initialized. In this case the do keyword is optional.


## Partial Application
 *)

let smallPipeVolume = cylinderVol 2.0
let bigPipeVolume = cylinderVol 3.0

let smallLongPipeVol = smallPipeVolume 5.0
let bigShortPipeVol = bigPipeVolume 2.0

printfn "smallLongPipeVol: %A" smallLongPipeVol
printfn "bigShortPipeVol: %A" bigShortPipeVol


(**
## Function Values (First class functions)
All functions in F# are known as function values.

The transform argument is a function that takes
an int and returns an int.
 *)
let apply1 (transform: int -> int) y = transform y
let transformFunc x = x * 2
printfn "apply1 with transformFunc: %A" (apply1 transformFunc 3)


(**
## Lambda Expression (Anonymous functions?)
 *)

let lambdaResult = apply1(fun x -> x + 1) 100
printfn "lambdaResult: %A" lambdaResult


(**
## Function Composition and Pipelining
Composition operators take 2 functions and return a function
 *)
let addOne x = x + 1
let timesTwo x = x * 2
let compose1 = addOne >> timesTwo
let compose2 = addOne << timesTwo
printfn "compose1: %A" (compose1 100) // (100 + 1) * 2 = 202
printfn "compose2: %A" (compose2 100) // (100 * 2) + 1 = 201

(**
Pipelining operators take a function and an argument and return a value
(100 + 1) * 2 = 202
 *)
printfn "Pipeline result: %A" (100 |> addOne |> timesTwo)


(**
# External Functions

Call native functions (platform invoke).

Assuming the following C++ DLL:
```
#include <stdio.h>
extern "C" void __declspec(dllexport) HelloWorld() {
   printf("Hello world, invoked by F#!\n");
}
```

External functions can be called like:
 *)

open System.Runtime.InteropServices
module InteropWithNative =
  [<DllImport(@"C:\bin\nativedll", CallingConvention = CallingConvention.Cdecl)>]
  extern void HelloWorld()

  (**
No DLL so this is for reference only:
InteropWithNative.HelloWorld()


## Inline Functions

Functions that are integrated directly into the calling code.
Judicious use of inline functions can be used to optimise code.
   *)

let inline increment x = x + 1
type WrapInt32() =
  member inline this.IncrementByOne(x) = x + 1
  static member inline Increment(x) = x + 1


(**
## Recursive Functions
 *)

let rec fib n = if n < 2 then 1 else fib (n - 1) + fib (n - 2)
printfn "fib 5: %A" (fib 5)


(**
## Entry Point

The entry point of an F# application. Must be the last let binding
in the project.

If no entry point is specified, the last top-level binding is used
as the entry point.

NOTE: EntryPoint attributes are ignored in FSI
 *)
[<EntryPoint>]
let main args =
  printfn "Args passed in: %A" args
  0 // Return 0 exit code for success
