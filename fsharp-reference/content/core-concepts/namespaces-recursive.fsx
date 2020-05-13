(**
---
title: Recursive Namespaces
description: Allow you to reference yet to be defined types and modules.
---

All types and modules within the namespace will be marked as recursive to
allow them to be referenced before the complete definition.

 *)

namespace rec MutualReferences

exception DontSqueezeTheBananaException of Banana

type Banana() =
  // ...
  member self.SqueezeJuiceOut() = raise (DontSqueezeTheBananaException self)
