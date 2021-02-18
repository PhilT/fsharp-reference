namespace Matter

type Tests = Expecto.TestsAttribute

module Expectrum =
  let testList = Expecto.Tests.testList
  let testSequenced = Expecto.Tests.testSequenced
  let test = Expecto.Tests.test
  let ptest = Expecto.Tests.ptest
  let ftest = Expecto.Tests.ftest

module Expect =

  open Expecto
  open Expecto.Flip

  let equal expected actual = Expect.equal "" expected actual
  let greaterThan b a = Expect.isGreaterThan "" (b, a)
  let lessThan b a = Expect.isLessThan "" (b, a)
  let isFasterThan a b = Expect.isFasterThan "" a b
  let isTrue actual = Expect.isTrue "" actual

  let floatClose (a: float32) (b: float32) =
    Expect.floatClose "" Accuracy.medium (float a) (float b)
