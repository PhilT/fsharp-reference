(**
# Fixed Keyword

"Pin" a local onto the stack to prevent it from being collected or moved during GC.
Used for low-level programming.
 *)

open Microsoft.FSharp.NativeInterop

type Point = { mutable X: int; mutable Y: int }
let squareWithPointer (p: nativeptr<int>) =
  let mutable value = NativePtr.get p 0

  value <- value * value
  NativePtr.set p 0 value

let pnt = { X = 1; Y = 2 }
printfn "pnt before - X: %d, Y: %d" pnt.X pnt.Y

let pointerWork() =
  use ptr = fixed &pnt.Y

  squareWithPointer ptr
  printfn "pnt after - X: %d, Y: %d" pnt.X pnt.Y

pointerWork()
