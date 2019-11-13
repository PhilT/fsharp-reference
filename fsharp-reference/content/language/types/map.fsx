(**
---
title: Map
description: Immutable maps. Keys are ordered by F# generic comparison.
---

Maps based on generic comparison are efficient for small keys. They are not a
suitable choice if keys are recursive data structures or if keys require
bespoke comparison semantics. All members of this class are thread-safe

## Creating
 *)

let employees =
  Map.empty.
    Add("Joe", 1001).
    Add("Jeff", 1002).
    Add("George", 1003)

let capitals =
  [ "Argentina", "Buenos Aires";
    "France ", "Paris";
    "Switzerland", "Bern"
  ]
  |> Map.ofList

(**
## Accessing
 *)

printfn "Employee number for Jeff: %d" employees.["Jeff"]
printfn "Capital of France is %s" capitals.["France"]
printfn "Does France have a capital? %b" (capitals.ContainsKey "France")

(**
Use `TryFind` to return an Option type with the value if found:
 *)

match employees.TryFind "Joe" with
| Some number -> printfn "Employee found, ID: %d" number
| None -> printfn "Employee not found"

(**
Other methods include `Count`, `IsEmpty` and `Remove`.
 *)

(**
## Map Module
https://msdn.microsoft.com/visualfsharpdocs/conceptual/collections.map%5b%27key%2c%27value%5d-class-%5bfsharp%5d
 *)
