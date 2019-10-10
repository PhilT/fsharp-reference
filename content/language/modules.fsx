(**
# Modules

An F# construct for grouping values, types and functions.
*)

#load "modules/implicit.fsx"
let myX = Implicit.x

(**
Local module declarations require indentation. These modules would also be
implicitly grouped in a top-level module `Program` if the file is `program.fs`:
*)

#load "modules/local.fsx"

(**
This can be referenced:
*)

let result1 = Local.Outer.Inner.xFunc 5 // => 15

open Local.Outer.Inner
let result2 = xFunc 5 // => 15

(**
Modules, as with namespaces can be mutually recursive:
*)

module rec MutualReferences =
  exception DontSqueezeTheBananaException of Banana

  type Banana() =
    // ...
    member self.SqueezeJuiceOut() = raise (DontSqueezeTheBananaException self)