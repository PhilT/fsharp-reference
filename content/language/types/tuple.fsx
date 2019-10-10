(**
# Tuple
 *)

(1, 2)
("one", "two", "three")
// (a, b) // generic types
("one", 2, 3.0) // mixed types
// (a + 1, b + 1) // expressions
struct (1.2025f, 1.5f) // Struct Tuple of floats (Interop for C# Tuples)

(**
## Obtaining Individual Values

Deconstruct with pattern matching:
 *)
let (a, b) = (1, 2)
let struct (c, d) = struct (1, 2)

(**
Just grab the first value from the Tuple:
 *)
let (e, _) = (1, 2)
printfn "e: %A" e

(**
Use a Tuple to disable partial application:
 *)
let sum a b = a + b
let add2 a = sum 2
let sumNoCurry (a, b) = a + b // Does not allow currying

(**
Tuple Type Syntax:
`int * float * string // e.g. (10, 10.0, "ten")`

## Interoperation with C# Tuples

It's not possible to convert between reference Tuples and struct Tuples.
A Tuple must be pattern matched to get the parts and then a struct Tuple
created.
 *)

let (aa, bb) = (1, 2)
let struct (cc, dd) = struct(aa, bb)
