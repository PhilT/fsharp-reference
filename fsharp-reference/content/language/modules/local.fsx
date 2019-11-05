(**
2 nested local module declarations. To access this from another module the
implicit declaration would need to prefix the call.

e.g. `Local.Outer.Inner.xFunc 10`
 *)

module Outer =
  let x = 40

  module Inner =
    let xFunc x = x + 10

(**
« Back to [Modules](../modules.html)
 *)
