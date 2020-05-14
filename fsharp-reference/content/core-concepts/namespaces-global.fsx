(**
---
title: The Global Namespace
description: Refer to it with the keyword `global`
keywords: toplevel
---
Can be useful when needing to define types in an associated module but
don't want a top-level namespace. However, it's recommended to define a
top-level namespace to avoid naming conflicts with other libraries.
 *)

namespace global

type Vector = {
  x: float
  y: float
  z: float
}

module Vector =
  let add a b =
    {
      x = a.x + b.x
      y = a.y + b.y
      z = a.z + b.z
    }
