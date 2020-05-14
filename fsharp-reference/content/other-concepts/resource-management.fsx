(**
---
title: Resource Management
description: `use` binding instead of `let` automatically calls `Dispose` on .NET types.
keywords: use using dispose
---

The `using` function is another form of `use`. With `use`, `Dispose` is called at the
end of the block. With `using`, `Dispose` is called at the end of the
function. Prefer `use`.
 *)

open System.IO

let writefile filename obj =
  use file = File.CreateText(filename)
  file.WriteLine("{0}", obj.ToString())
  // calls file.Dispose() here

writefile "abc.txt" "Hello File World!"

let writefile2 filename obj =
  using (System.IO.File.CreateText(filename)) (fun file ->
    file.WriteLine("{0}", obj.ToString())
  )

writefile2 "abc.txt" "Hello File World!"

// TODO: Add a note on computation expressions.


