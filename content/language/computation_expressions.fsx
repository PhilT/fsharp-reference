(**
# Computation Expressions

[Sequence Expressions](sequences.fsx), [Asynchronous Workflows](async_workflows.fsx) and
[Query Expressions](query_expressions.fsx) are types of computation expressions.

## Logging all assignments
 *)

type LoggingBuilder() =
  let log p = printfn "expression is %A" p
  member this.Bind(x, f) =
    log x
    f x
  member this.Return(x) =
    x

let logger = LoggingBuilder()
let loggedWorkflow =
  logger {
    let! x = 42
    let! y = 43
    let! z = x + y
    return z
  }

(**
`use!` can be applied instead of `let!` when the result
implements IDisposable (same use as `use`).


## Handling divide by zero
Handle a series of divide operations, shortcutting if
any divide by zero
 *)

let divideBy bottom top =
  if bottom = 0
  then None
  else Some(top / bottom)

let (>>=) m f = Option.bind f m
let divideByWorkflow x y w z =
  x |> divideBy y
  >>= divideBy w
  >>= divideBy z

let printResult result =
  match result with
  | Some result -> printfn "Good workflow: %i" result
  | None -> printfn "Bad workflow: Divide by zero happened"

let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

printResult good
printResult bad


(**
## Return early if a call succeeds
 *)

type OrElseBuilder() =
  member this.ReturnFrom(x) = x
  member this.Combine(a, b) =
    match a with
    | Some _ -> a
    | None -> b
  member this.Delay(f) = f()

let orElse = OrElseBuilder()

let map1 = [("1", "One"); ("2", "Two")] |> Map.ofList
let map2 = [("A", "Alice"); ("B", "Bob")] |> Map.ofList
let map3 = [("CA", "California"); ("NY", "New York")] |> Map.ofList

let multiLookup key = orElse {
  return! map1.TryFind key
  return! map2.TryFind key
  return! map3.TryFind key
}

multiLookup "A" |> printfn "Result for A is %A"
multiLookup "CA" |> printfn "Result for CA is %A"
multiLookup "X" |> printfn "Result for X is %A"

(**
## Continuations

Caller decides what to do with a result instead of the function.
 *)

let divide ifZero ifSuccess top bottom =
  if (bottom = 0)
  then ifZero()
  else ifSuccess (top / bottom)

(**
Print out the result
 *)
let ifZeroPrint () = printfn "bad"
let ifSuccessPrint x = printfn "good %i" x

let dividePrint = divide ifZeroPrint ifSuccessPrint

let goodPrint = dividePrint 6 3
let badPrint = dividePrint 6 0

(**
Convert to an option
 *)
let ifZeroOption () = None
let ifSuccessOption x = Some x

let divideOption = divide ifZeroOption ifSuccessOption

let goodOption = divideOption 6 3
let badOption = divideOption 6 0

printfn "End.\n"
