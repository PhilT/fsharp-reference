(**
# Recursive namespaces

Allow you to reference yet to be defined types:
 *)

namespace rec MutualReferences

exception DontSqueezeTheBananaException of Banana

type Banana() =
  // ...
  member self.SqueezeJuiceOut() = raise (DontSqueezeTheBananaException self)
