(**
# Conditional Expressions

In F# conditionals are expressions, meaning they return a value.
 *)

let test x y =
  if x = y then "equals"
  elif x < y then "is less than"
  else "is greater than"

printfn "X and Y %s" (test 1 1)
printfn "X and Y %s" (test 1 2)
printfn "X and Y %s" (test 2 1)

(**
The return type here is `unit`
 *)
let printUnderAge age =
  if age < 18
  then printfn "%A is under age" age

printUnderAge 20
printUnderAge 17
