// # Pattern Matching

// Compare, decompose or extract data using `match`, function arguments,
// lambda expressions, `try...with` exception handlers.
// TODO: ensure we document try...with and lambda expressions


// ## Constant Pattern

// Similar to a case statement in other languages. Any numeric, character,
// or string literal, enumeration constant, or defined literal identifier.

[<Literal>]
let Three = 3

let filter123 x =
  match x with
  | 1 | 2 | Three -> printfn "Found 1, 2, or 3!"
  | a -> printfn "Unknown value %A" a

filter123 3
filter123 4

type Color =
  | Red = 0
  | Green = 1

let printColorName (color: Color) =
  match color with
  | Color.Red -> printfn "Red"
  | Color.Green -> printfn "Green"
  | _ -> printfn "Unknown value"


// ## Identifier Pattern

// A case value of a discriminated union, exception label, active pattern case.
// See relevant types for pattern matching examples.


// ## Variable Pattern

let function1 x =
  match x with
  | (var1, var2) when var1 > var2 -> printfn "%d is more than %d" var1 var2
  | (var1, var2) when var1 < var2 -> printfn "%d is less than %d" var1 var2
  | (var1, var2) -> printfn "%d equals %d" var1 var2


// ## As Pattern

let (var1, var2) as tuple1 = (1, 2)
printfn "%d %d %A" var1 var2 tuple1


// ## Or/And Pattern

let detectZero point =
  match point with
  | (0, 0) -> "Both zero"
  | (0, _) | (_, 0) -> "One value is zero"
  | _ -> "Other value"


// ## Cons(truct) Pattern

let list1 = [1, 2, 3]
let rec printList l =
  match l with
  | head :: tail -> printfn "%d" head; printList tail
  | [] -> printfn ""


// ## List/Array Pattern

// Decompose lists (`[]`) or arrays (`[||]`).

let listLength list =
  match list with
  | [] -> 0
  | [_] -> 1
  | [_; _] -> 2
  | _ -> List.length list


// ## Parenthesized Pattern

// Use to group patterns.

let countValues list value =
  let rec checkList list acc =
    match list with
    | (elem1 & head) :: tail when elem1 = value -> checkList tail (acc + 1)
    // [TODO: Not sure what elem1 will be assigned to]
    | head :: tail -> checkList tail acc
    | [] -> acc
  checkList list 0


// ## Tuple Pattern

let print tuple1 =
  match tuple1 with
  | (a, b) -> printfn "Pair %A %A" a b

// TODO: Pattern matching Tuples in function inputs


// ## Record Pattern

type MyRecord = { Name: string; ID: int }
let IsMatchByName record (name: string) =
  match record with
  | { MyRecord.Name = nameFound; MyRecord.ID = _; } when nameFound = name -> true
  | _ -> false

let recordX = { Name = "Parker"; ID = 10 }


// ## Wildcard Pattern

// Underscore (`_`) matches anything. Like variable pattern but discards
// input. Used in most of the previous examples as the last catch all case.


// ## Type Annotations

let detect x =
  match x with
  | 1 -> "Found a 1"
  | (var1: int) -> sprintf "%d" var1


// ## Type Test Pattern

open System.Windows.Forms
let RegisterControl(control: Control) =
  match control with
  | :? Button as button -> button.Text <- "Registered"
  | :? CheckBox as checkbox -> checkbox.Text <- "Registered"
  | _ -> ()


// ## Null Pattern

// Matches null, mainly for working with .NET interop. `| null -> ...`


// ## Active Patterns

// Named positions that subdivide input data. Max 7 partitions.

let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

let TestNumber input =
  match input with
  | Even -> printfn "%d is even" input
  | Odd -> printfn "%d is odd" input

// TODO: Finish off active patterns
