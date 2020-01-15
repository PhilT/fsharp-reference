(**
---
title: Map
description: Immutable maps. Keys are ordered by F# generic comparison.
---

Maps based on generic comparison are efficient for small keys. They are not a
suitable choice if keys are recursive data structures or if keys require
bespoke comparison semantics. All members of this class are thread-safe.
It's recommended to use the module functions as F# can then determine types
implicitly.

## Creating
 *)

let employees =
  Map.empty
  |> Map.add "Joe" 1001
  |> Map.add "Jeff" 1002
  |> Map.add "George" 1003

// Map module usage, preferred
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
printfn "Does France have a capital? %b" (Map.containsKey "France" capitals)

(**
Use `TryFind` to return an Option type with the value if found:
 *)

match Map.tryFind "Joe" employees with
| Some number -> printfn "Employee found, ID: %d" number
| None -> printfn "Employee not found"


(**
## Map Module

Functions that operate on a Map:

* `Map.add key value map` - Return new map with the key/value added
* `Map.containsKey key map` - Return true if the key is in the map
* `Map.count map` - Return the number of items in the map
* `Map.empty map` - Return a new, empty map
* `Map.exists predicate map` - Return true where predicate function `(fun k v -> ...)` returns true
* `Map.filter predicate map` - Return new map where predicate function `(fun k v -> ...)` returns true
* `Map.find key map` - Returns the value found or raises KeyNotFoundException
* `Map.findKey predicate map` - Return the key where the predicate function `(fun k v -> ...)` returns true
* `Map.fold folder state map` - Accumulate or reduce the key/value pairs using a folder function `(fun state k v -> ...)`
* `Map.foldBack folder state map` - Same as above but works in reverse. Useful for doing cons (::) on lists (append to head)
* `Map.forall predicate map` - Returns true if predicate `(fun k v -> ...)` returns true for all items
* `Map.isEmpty map` - Returns true if the map contains no items
* `Map.iter action map` - Apply action function to each item returning `unit`
* `Map.map mapper map` - Apply mapper function `(fun k v -> transformed v)` to each item returning a new map with the mapped items
* `Map.ofArray anArray` - Takes an array of key, value tuples and returns a map
* `Map.ofList aList` - Takes a list of key, value tuples and returns a map
* `Map.ofSeq aSequence` - Takes a sequence of key, value tuples and returns a map
* `Map.partition predicate map` - Return 2 maps as a tuple. The first, where the predicate function `(fun k v -> ...)` returns true and the second where the predicate returns false
* `Map.pick chooser map` - Returns the first value where the chooser returns `Some` or raises KeyNotFoundException
* `Map.remove key map` - Returns a new map with the item (if found) removed
* `Map.toArray map` - Returns an array of key, value tuples
* `Map.toList map` - Returns a list of key, value tuples
* `Map.toSeq map` - Returns a sequence of key, value tuples
* `Map.tryFind key map` - Like `find` but returns `Some` if the value is found and `None` if not
* `Map.tryFindKey predicate map` - Like `findKey` but returns `Some key` if the predicate function returns true or `None` if not
* `Map.tryPick chooser map` - Like `pick` but returns the option type `Some v` or `None` instead of raising

 *)
