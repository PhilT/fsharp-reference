// # Exception

exception MyException of string
exception MyTupleException of string * int

// Use `raise`, `failwith`, `invalidArg` to raise an exception.

let function1 x y =
  try
    if x = y then raise (MyException("x = y"))
    else if x > y then raise (MyTupleException("x", 10))
    else if x > 10 then invalidArg "x" (sprintf "x is %d" x)
    else failwith "x and y cannot do this"
  finally
    printfn "Finished processing"

let caller x =
  try
    function1 x 2
  with
    | MyException(str) -> printfn "MyError: %s" str
    | MyTupleException(str, i) -> printfn "MyTupleError: %s, %d" str i

caller 2

// # Assertions

// Assertion checking when in Debug mode.

let x = 3
assert (x < 5)
