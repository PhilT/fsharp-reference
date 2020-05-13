(**
---
title: Option
description: An Option is an optional value.
keywords: none some search value
---

The Option type supports the following properties and methods:

* `None` `'T option` - Creates an option value of `None`
* `IsNone` `bool` -  Returns `true` if the option has the `None` value
* `IsSome` `bool` - Returns `true` if the option has a value that is not `None`
* `Some` `'T option` - Creates an option that has a value that is not `None`
* `Value` `'T` - Returns the underlying value or throws an Exception if the value is `None`
 *)

let keepIfPositive (a: int) = if a > 0 then Some(a) else None

let exists (x: int option) =
  match x with
  | Some(x) -> true
  | None -> false

(**
Useful when searching:
 *)

let rec tryFindMatch predFunc list =
  match list with
  | head :: tail -> if predFunc(head)
                    then Some(head)
                    else tryFindMatch predFunc tail
  | [] -> None

let found = tryFindMatch (fun elem -> elem = 100) [200; 100; 50; 25]
let notFound = tryFindMatch (fun elem -> elem = 26) [200; 100; 50; 25]

(**
`found` returns `Some (100)` and `notFound` returns `None`.
 *)

(**
Functions that return an Option type are typically prefixed with `try`.
E.g. `Seq.tryFindIndex`.

Useful when handling exceptions:
 *)

open System.IO
let openFile filename =
  try
    let file = File.Open (filename, FileMode.Create)
    Some(file)
  with
    | ex -> eprintf "An exception occurred with message %s" ex.Message
            None

(**
It's possible that something can return `Some(null)` due to reference types
in .NET but should be avoided where possible.

The Option module has helper functions for working with Option types. Some of
these functions are the same as the members of Option type but are useful
when a function is required. Option also has functions for working with
collections such as `Option.map`, `Option.iter`, `Option.fold` etc that
enable a collection for zero or more elements.

Convert to a list or array with `Option.toList` or `Option.toArray`.


# Value Option

Passed by value option type. Being a struct type it's allocated on the
stack which is faster and doesn't need to be garbage collected.
Always profile code when selecting Struct types for performance reasons.
 *)

let tryParseDateTime (s: string) =
  match System.DateTime.TryParse(s) with
    | (true, dt) -> ValueSome dt
    | (false, _) -> ValueNone

let possibleDateString1 = "1990-12-25"
let possibleDateString2 = "not a date"

let result1 = tryParseDateTime possibleDateString1
let result2 = tryParseDateTime possibleDateString2

printfn "%A" result1
printfn "%A" result2
