module Matter.ShellTest

open Expectrum


[<Tests>]
let tests =
  testList "Shell" [
    test "it works" {
      Expect.equal 1 1
    }
  ]

