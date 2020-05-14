(**
---
title: Namespaces
description: A .NET construct to attach a name to a group of program elements to avoid naming conflicts when used with other libraries.
keywords: module type structure organization
---

Can contain modules and types but not functions.

Reasonably sized applications and libraries should consider this approach to
avoid naming conflicts with other libraries and aid organization of modules
and types.

 *)

namespace MyAppName

type Vector = {
  x: float
  y: float
}

module Vector =
  let create () =
    { x = 0.0; y = 0.0 }

