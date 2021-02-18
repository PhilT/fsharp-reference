(**
---
title: Writing Tests for Reactive Programming
description: Automated testing is a big deal so when working with anything new I always evaluate the available testing tools.
created: 2020-02-18
updated:
keywords: f# reactive testing
---

## Our first reactive test

So far I have a few tests for some of the pure functions in the engine but
nothing on the reactive side.

Let's get familar with `FSharp.Control.Reactive.Testing` which provides some
wrappers to Rx.NET to make reactive tests more functional.

    dotnet paket add FSharp.Control.Reactive.Testing
    dotnet paket generate-load-scripts
 *)

#load @"../../.paket/load/netstandard2.0/Expecto.fsx"
#load @"../../.paket/load/netstandard2.0/FSharp.Control.Reactive.Testing.fsx"

open Expecto
open FSharp.Control.Reactive.Testing

test "Filter more than 10" {
  TestSchedule.usage <| fun scheduler ->
  let stub = Marbles.cold "--2-(30)--(22)-5--(60)-(1)-|" scheduler
  let expected =  "-- -(30)--(22)- --(60)-   -|"

  stub
  |> Observable.filter (fun x -> int x > 10)
  |> Marbles.expectMessages scheduler expected
}
|> runTestsWithCLIArgs [CLIArguments.Version] [||]

(**
`TestSchedule.usage` gives us a virtual scheduler that simulates the passing
of time so tests can be executed quickly.

`Marbles.cold` creates a stream of events specified by the marble diagram string
when subscribed to (i.e. a cold observable).

Here we're just testing the `filter` operator with our predicate. The test should
match our expected marble diagram.

## Marble diagrams

Marble diagrams seem like a great way to express time. Unlike a pure function
that returns a single value, we require zero to many events from Obervables to
apply operators to.

https://www.freecodecamp.org/news/https-medium-com-henry-little-a-reactive-joystick-built-with-rxjs-abfca3668786/
 *)



