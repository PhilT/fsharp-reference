(**
# Lazy Expressions

Execute (`Force`) only when the result is required.
Helps improve performance.
 *)

let x = 10
let result = lazy (x + 10)
printfn "%d" (result.Force())
