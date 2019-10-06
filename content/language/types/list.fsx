// # List

// Ordered, immutable elements of the same type or base type.

let aList = [1; 2; 3]
let anotherList = [
  1
  2
  3
]

type Control() =
  member this.Bla() = ""
type Button() =
  inherit Control()
type CheckBox() =
  inherit Control()
let controlList: Control list = [Button(); CheckBox()]
let rangeList = [1..10]
let emptyList = []
let sequenceList = [for i in 1..10 -> i * i] // List made with Sequence Expression
let list2 = 100 :: aList // -> [100; 1; 2; 3]
let list3 = aList @ anotherList // -> [1; 2; 3; 1; 2; 3]

printfn "%A" aList.IsEmpty // `true` if no elements
printfn "%A" aList.Head // First element
printfn "%A" (aList.Item 1) // Second element
printfn "%A" aList.Length // Number of elements
printfn "%A" aList.Tail.Head // Next element
printfn "%A" List.Empty // New empty list


// ## Recursion

// The following function uses an accumulator to ensure the last call is a call
// to itself (tail recursive).

let sum2 list =
  let rec loop list acc =
    match list with
    | head :: tail -> loop tail (acc + head)
    | [] -> acc
  loop list 0


// ## Module Functions

// Detailed docs of these functions can be found at:
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/collections.list-module-%5bfsharp%5d

let head = List.head [1; 2; 3] // head = 1
let tail = List.tail [1; 2; 3] // tail = [2; 3]

let containsNumber number list = List.exists (fun elem -> elem = number) list
containsNumber 0 [0..3] // returns true as it contains 0

let isAllZeros list = List.forall (fun elem -> elem = 0.0) list
isAllZeros [0.0; 0.0] // returns true

let listEqual l1 l2 = List.forall2 (fun elem1 elem2 -> elem1 = elem2) l1 l2
printfn "listEqual: %A" (listEqual [0; 1] [0; 1] ) // returns true
printfn "listEqual: %A" (listEqual [0; 1] [1; 0]) // returns false

let pickList = [("a", 1); ("b", 2); ("c", 3)]
let resultPick = List.pick (fun elem ->
                  match elem with
                  | (value, 2) -> Some value
                  | _ -> None) pickList

printfn "%A" resultPick // returns "b"
