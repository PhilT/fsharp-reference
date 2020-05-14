(**
---
title: Local Modules
description: A file, `local.fs` contains a nested local module declaration. To access this from another module the implicit declaration would need to prefix the call.
keywords: nested
---

e.g. `Local.Outer.Inner.xFunc 10`
 *)

module Outer =
  let x = 40

  module Inner =
    let xFunc x = x + 10

