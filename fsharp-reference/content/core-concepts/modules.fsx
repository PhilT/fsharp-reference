(**
---
title: Modules
description: An F# construct for grouping values, types and functions.
---

This is the primrary code organization construct in F#. Libraries and multi-file
applications require all files to start with a namespace or module definition.

One recomendation is to use a top-level namespace followed by the module name.
For example:
*)

module MyApp.MyModule

let publicFuncInMyModule () = 0

(** [modules/implicit.fsx](modules/implicit.html) *)
#load "modules/implicit.fsx"
let myX = Implicit.x

(**
Local module declarations require indentation. These modules would also be
implicitly grouped in a top-level module `Program` if the file is `program.fs`:
*)

(** [modules/local.fsx](modules/local.html) *)
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
