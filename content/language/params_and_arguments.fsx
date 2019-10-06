// # Parameters & Arguments

// The term *parameter* is used to describe the names for values that are
// expected to be supplied. The term *argument* is used for the values
// provided for each parameter.

type SomeType =
  member this.SomeMethod(param1, param2) = () // Tuple form

let function1 param1 param2 = () // Curried form

// The Tuple form is generally used when calling methods as this most
// closely resembles .NET calling convention.

// The curried form is generally used when creating functions.

// The two forms can be mixed:

let function2 param1 (param2, param3) param4 = ()

// It's also possible to ignore parameters using the wildcard (`_`) pattern:

let makeList _ = [ for i in 1 .. 100 -> i * i ]
let list1 = makeList 200 // 200 is ignored

// Other patterns that are used in arguments are the `as` pattern and identifier
// patterns associated with discriminated unions and active patterns.


// ## Named Arguments

type SpeedingTicket() =
  member this.GetMPHOver(speed: int, limit: int) = speed - limit

let calculateFine (ticket: SpeedingTicket) =
  // here we assign the values to named arguments
  let delta = ticket.GetMPHOver(limit = 55, speed = 70)
  if delta < 20 then 50.0 else 100.0

let ticket1: SpeedingTicket = SpeedingTicket()
printfn "%f" (calculateFine ticket1)

type Account() =
  let mutable balance = 0.0
  let mutable number = 0
  member this.AccountNumber
    with get() = number
    and set(value) = number <- value
  member this.Balance
    with get() = balance
    and set(value) = balance <- value
  //...

let account1 = Account(AccountNumber=83746, Balance=234.43)


// ## Optional Parameters

// Only on members, not functions. Prefix parameter with `?`. Use option type
// `Some` or `None`.

type DuplexType =
  | Full
  | Half

type Connection(?rate0: int, ?duplex0: DuplexType) =
  let duplex = defaultArg duplex0 Full
  let mutable rate = match rate0 with
                      | Some rate1 -> rate1
                      | None -> match duplex with
                                | Full -> 9600
                                | Half -> 4800

let conn1 = Connection(duplex0 = Full)
let conn2 = Connection(300, Half)
let conn3 = Connection(?duplex0 = None)

// When calling F# from C# the `Optional` attribute can be used.

open System
open System.Runtime.InteropServices
type C =
  static member Foo([<Optional; DefaultParameterValue("Hello world")>] message) =
    printfn "%s" message


// ## Passing by Reference

// * Use `inref<'T>` to read a pointer
// * Use `outref<'T>` to write to a pointer
// * Use `byref<'T>` to read and write a pointer

let exampleInref (x: inref<int>) = printfn "It's %d" x
let exampleOutref (x: outref<int>) = x <- x + 1
let exampleByref (x: byref<int>) =
  printfn "It'd %d" x
  x <- x + 1

let xxx = 1
// Waiting for https://github.com/dotnet/docs/issues/14522 to be resolved:
// `exampleInref &xxx`

let mutable y = 2
exampleOutref &y
exampleByref &y


// ## Parameter Arrays

// Can only be used in methods not functions. Can only be used as the last parameter.

open System

type X() =
  member this.F([<ParamArray>] args: Object[]) =
    for arg in args do
      printfn "%A" arg

[<EntryPoint>]
let main _ =
  Console.WriteLine("a {0} {1} {2} {3} {4}", 1, 10.0, "Hello", 1u, true)

  let xobj = X()
  xobj.F("a", 1, 10.0, "Hello", 1u, true)
  0
