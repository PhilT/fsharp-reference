#r "../../packages/Expecto/lib/netstandard2.0/Expecto"

(**
---
title: F# Testing - Expecto
description: Test or Behaviour Driven Development has been a staple in my workflow for years. Now that I've got some familiarity with F# it's time to get serious.
created: 2019-11-12
updated:
categories: f# testing
---

.NET has a bunch of testing tools such as xUnit, NUNit and MSTest. However,
this is F# so we can do better!

## Expecto Patron...

Err, anyway. It runs tests in parallel and async which is a nice boost. It's
got a test runner built in so no need for additional libraries. I'm a (Neo)Vim
user but there are runners for VSCode and Visual Studio.

It also comes with performance testing which sounds intriguing especially
for game development. I'll take a look at that later.

There is also support for property based testing through FsCheck. I'll not
be using FsCheck to begin with, preferring to stick with what I know but
it's definitely something I want to add later on.

## Installing Expecto

There is a dotnet project template, however, I like to know what is going
into it so we'll set it up manually.

All we need to begin with is Expecto itself.

    dotnet paket add Expecto

## Running our first test

  Time for a "Hello world" example:
 *)

open Expecto

test "A simple test" {
  let subject = "Hello World"
  Expect.equal subject "Hello World" "Should match"
}
|> runTestsWithCLIArgs [] [||]

(**
Nice!

We'll need to know a few more features in order to get started with testing
in my project.

## Grouping tests

You can use `testlist` to group a bunch of tests. `testList` can be nested.
*)

testList "Group of tests" [
  test "a test" {
    Expect.equal (2 + 2) 4 ""
  }

  test "another test" {
    Expect.equal (3 + 3) 6 ""
  }
]
|> runTestsWithCLIArgs [] [||]


(**
## Pending and focused tests.

Prefix `test` with a `p` to mark it as pending and it'll be excluded in test runs.
To only run some tests it's `f` for focused test.
 *)

testList "unfocused test list" [
  test "won't run" { Expect.isTrue false "This should have run" }
  ftest "will run" { Expect.isTrue true "This is meant to run" }
]

(**
Unfortunately, there is currently no way to have Expecto auto-discover tests
and shuffle them. `Test.shuffle` only works on a list of tests. Perhaps this
could be added as an option.

 *)

(**
One thing I don't like is the extra message string. Often, it's obvious what's
going on without needing clarification. Especially with BDD where test cases
generally have a single expectation and the text of that is usually enough.

However, there is a workaround. As suggested by the creator:
*)

module ExpectLite =
  let equal a b = b |> Expect.equal "" a

(**
For testing my project we'll need to run all the tests in the project. This
can be done by marking tests with the `Tests` attribute:
*)

[<Tests>]
testList "test list" [
  test "a test" { Expect.isTrue true "" }
]

(**
And then running with one of the `*InAssembly` functions:
 *)

[<EntryPoint>]
let main args =
  runTestsInAssemblyWithCLIArgs [] args

(**

Integrations with BenchmarkDotNet, FsCheck and Hopac can be added to
`paket.dependencies`:

  ```powershell
    ac paket.dependencies nuget Expecto.BenchmarkDotNet
    ac paket.dependencies nuget Expecto.FsCheck
    ac paket.dependencies nuget Expecto.Hopac

    dotnet paket install
  ```
 *)
