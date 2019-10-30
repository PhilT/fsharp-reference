(**
# Casting & Conversions

## Arithmetic Types

First `byte` is the type definition, second `byte` is the conversion operator:
 *)

let x: int = 5
let b: byte = byte x

(**
## Conversion Operators

`byte`, `sbyte`, `int16`, `uint16`, `int32` or `int`, `unit32`, `int64`,
`uint64`, `nativeint`, `unativeint`, `float` or `double`, `float32` or `single`
`decimal`, `char`, `enum`.

These operators can also be used with types that implement `op_Explicit` or
`op_Implicit` with appropriate signatures.

TODO: make sure I understand what `op_Explicit` and `op_Implicit` mean

## Enumerated Types

`enum` operator can convert say, an integer to an enum
 *)

type Color =
  | Red = 1
  | Green = 2
  | Blue = 3

(**
This is currently pending an open support request:
https://github.com/dotnet/docs/issues/14508
`let color = enum 1  // Type inference infers Color`
 *)
let color2: Color = enum 1 // type annotation added
let color3 = enum<Color> 3 // Or specified explicitly

(**
The following will fail to compile. Incompatible types
`let color4 : Color = enum 2u`

## Casting Object Types

Used in OO Programming to cast up (upcast) or down (downcast) in a
class hierarchy.
 *)

type Base1() =
  abstract member F : unit -> unit
  default this.F() =
    printfn "F Base1"

type Derived1() =
  inherit Base1()
  override this.F() =
    printfn "F Derived1"

let d1 : Derived1 = Derived1()
let base1 = d1 :> Base1  // Upcast to Base1 or

(**
or
`let base1 = upcast d1 // Upcast to Base1 with type inference`
This is currently pending an open support request:
https://github.com/dotnet/docs/issues/14508
 *)

let derived1 = base1 :?> Derived1 // Downcast to Derived1 or throw if not possible

(**
Prefer Discriminated Unions over Enums as they are easier to control
(enums can be supplied with non specified values), unless int values
are required or are to be used with other .NET languages.
 *)
