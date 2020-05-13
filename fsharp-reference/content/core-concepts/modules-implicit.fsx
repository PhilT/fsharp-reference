(**
---
title: Implicit Modules
description: Only works for single file apps and interactive FSX files.
keywords:
---

If a file, `implicit.fsx` contains:
*)

let x = 40

(**
Then it has an implicit module declaration of:

    module Implicit
    let x = 40


Note that libraries and multi-file applications require an explicit namespace
or module declaration at the top of the file.
*)
