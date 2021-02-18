---
title: CycleJS Style Game Engine in F# Part 2: Operators
description: Recently I took another look at CycleJS to see if I can take some inspiration for the design of my game engine.
created: 2020-01-10
updated:
keywords: f# reactiveprogramming cyclejs
---




## Comparing F# and CycleJS operators

The number of operators in Rx.NET and consequently FSharp.Control.Reactive is
mind-boggling. I noticed that xstream which is a reactive library used by CycleJS
itself only has a handful of core operators and factories. My use-cases will be
different as I'm writing a game not a web application. Still,

```
FSharp.Control.Reactive   Cyclejs operators
==================================================

Factory functions
--------------------------------------------------
n/a                       create
n/a                       createWithMemory
neverWitness              never
empty                     empty
throw                     throw
toObservable              from
ofSeq                     of
toObservable              fromArray
fromEvent                 fromPromise
n/a                       fromObservable
interval                  periodic
merge                     merge
combineLatest/
  combineLatestSeq        combine

Operators
--------------------------------------------------
subscribe                 addListener
subscription.unsubscribe  removeListener
subscribe                 subscribe
map                       map
mapTo                     mapTo
filter                    filter
take                      take
skip                      drop
last                      last
startWith                 startWith
takeUntilOther            endWhen
scanInit                  fold
onErrorConcat*            replaceError
switch                    flatten
|>                        compose
publish+refCount+
  connect+replay          remember
iter/iterEnd/iterError    debug
Subject.broadcast         imitate
Subject.onNext            shamefullySendNext
Subject.onError           shamefullySendError
Subject.onComplete        shamefullySendComplete
n/a                       setDebugListener

```

The full list of operators from FSharp.Control.Reactive can be found at
(https://fsprojects.github.io/FSharp.Control.Reactive/reference/fsharp-control-reactive-observablemodule.html).

As you can see from the list it's even less than 26. Some of these missing
operators are not applicable to F# or the domain I'm in. Some I've not quite
identified yet so I'll update this list when I discover them.

There are also quite a few operators that are relevant to F# only. I'll turn to
these when the above options aren't suitable but I think this list is a good
place to start.


## References

* https://journal.artfuldev.com/why-you-should-stop-using-state-reducers-in-your-app-to-be-fully-reactive-89a4279b3ece
