(**
---
title: Reference Cell
description: Storage locations that enable mutable values with reference semantics.
keywords: set get mutable
---

Create a new reference cell
 *)
let refVar = ref 1
refVar := 2 // Value changed to 2

(**
Set and get value (property)
 *)
refVar.Value <- 3
printfn "Value using Value property: %d" refVar.Value

(**
Set and get value (record field) for ML compatibility
 *)
refVar.contents <- 4
printfn "Value using contents: %d" refVar.contents

printfn "Value using `!` dereference operator: %d" !refVar
