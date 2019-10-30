(**
# Type Inference

F# is statically typed. It tries to infer the type from usage. If the
type cannot be inferred then type annotations need to be added.
*)


(**
Parameters `a`, `b` and return type are inferred to be `int`:
 *)

let f a b = a + b + 100

hello=


(**
These can be influenced, for example, by adding a suffix to `100`:
 *)

let f2 a b = a + b + 100u

(**
Or by explicitly setting the parameters:
 *)

let f3 (a: uint32) b = a + b

(**
Or the expression:
 *)

let f4 a b = (a: uint32) + b

(**
Or the return type:
 *)

let f5 a b: uint32 = a + b

(**
Use case, on a parameter where you want to use a member of the object type:
 *)

let replace(str: string) = str.Replace("A", "a")

(**
# Automatic Generalization

If a function does not rely on a specific type, F# can automatically generalize it:
 *)

let makeTuple a b = (a, b)

(**
This is a generalized function of type:
'a -> 'b -> 'a * 'b

The function takes parameters of type `a` and `b` and returns a Tuple of
types `a` and `b`. In this case the 2 parameters can be of different types.
 *)

let max a b = if a > b then a else b

(**
`a -> `a -> `a

In this case, the `>` operator is generic but requires both parameters to
be of the same type and since one of these is returned the return type will
also be `a`.

## Value Restriction

The compiler only performs automatic generalization on complete function
definitions that have explicit arguments and on simple immutable values.

If generalization fails you can:

Constrain a type to be nongeneric by adding a type annotation as previously
discussed.

 *)
// let counter = ref None // From this
let counter: int option ref = ref None // To this

(**
If using function composition or incomplete curried function arguments,
specify the arguments:
 *)

// let maxhash = max << hash // From this
let maxhash obj = (max << hash) obj // To this

(**
This is expression cannot be generalized so make it a function by adding parens:
 *)

// let emptyList = Array.create 10 [] // From this
let emptyList () = Array.create 10 [] // To this

(**
Add type parameters (rare):
 *)

// let arrayOf10Lists = Array.create 10 [] // From this
let arrayOf10Lists<'T> = Array.create 10 ([]:'T list)  // To this

let intLists = arrayOf10Lists<int>
let floatLists = arrayOf10Lists<float>

// TODO: Need more about generics, constraints, statically resolved type params
