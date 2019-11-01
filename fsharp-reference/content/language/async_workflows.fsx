(**
---
title: Asynchronous Workflows
description: TODO: Needs more description and examples.
---
 *)

let runProcess i =
  async {
    let delay = System.Random().Next(500)
    // let! or use! for asynchronous equivalents of let or use
    // can be used here when results are needed
    do! Async.Sleep delay
    printfn "Done processing %d after %dms" i delay
  }

[1; 2; 3]
|> Seq.map runProcess
|> Async.Parallel
|> Async.RunSynchronously

printfn "Done."
