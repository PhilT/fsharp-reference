(**
---
title: Observable Module
description: Understanding Observables by example
---

Obserables allow us to process asynchronous events or observations in a
synchronous way.

* [Observable.add](observable.html#Observable-add)
* [Observable.choose](observable.html#Observable-choose)
* [Observable.filter](observable.html#Observable-filter)
* [Observable.map](observable.html#Observable-map)
* [Observable.merge](observable.html#Observable-merge)
* [Observable.pairwise](observable.html#Observable-pairwise)
* [Observable.partition](observable.html#Observable-partition)
* [Observable.scan](observable.html#Observable-scan)
* [Observable.split](observable.html#Observable-split)
* [Observable.subscribe](observable.html#Observable-subscribe)

Let's look at each of the functions in the `Observable` module.

First, we create a timer that fires events every `interval` milliseconds
for 1 second and then terminates. Now we have something to observe.
*)

[<Literal>]
let DefaultInterval = 100
let DefaultTotalTime = 500

let createTimer interval =
  let timer = new System.Timers.Timer(float interval)
  (
    async {
      timer.Start()
      do! Async.Sleep(DefaultTotalTime)
      timer.Stop()
    },
    timer.Elapsed // returns an Observable
  )

(**
## Observable.subscribe

Starts listening for events/observations on the observable. It returns
a `Dispoable` that can be used to unsubscribe.

`subscribe` or `add` must be the final call on the observable.

### Syntax
```
('a -> unit) -> System.IObservable<'a> -> System.IDisposable
```

### Example
 *)

module Subscribe =
  printfn "# Subscribe"
  let task, source = createTimer DefaultInterval
  let callback = fun _ -> printf "."
  let disposable = source |> Observable.subscribe callback

  Async.RunSynchronously task
  disposable.Dispose()
  printfn "\n"

(**
## Observable.add

Starts listening for events/observations on the observable. This observer
cannot be unsubscribed in contrast with `subscribe`.

`subscribe` or `add` must be the final call on the observable.

### Syntax
```
('a -> unit) -> System.IObservable<'a> -> unit
```

### Example
 *)

module Add =
  printfn "# Add"
  let task, source = createTimer DefaultInterval
  let callback = fun _ -> printf "."
  source |> Observable.add callback // Not disposable

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.scan

Similar to `Seq.reduce`, `scan` accumulates state from previous
events/observations.

### Syntax
```
('a -> 'b -> 'a) -> 'a -> System.IObservable<'b> -> System.IObservable<'a>
```
### Example
 *)
module Scan =
  printfn "# Scan"
  type Counter = { value: string }
  let task, source = createTimer DefaultInterval
  let collector state (x: System.Timers.ElapsedEventArgs) =
    printfn "%A" x.SignalTime.Millisecond
    { value = state.value + "." }
  let state = { value = "" }

  source
  |> Observable.scan collector state
  |> Observable.subscribe (fun state -> printfn "State: %s" state.value)

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.choose

Returns a stream that only includes `Some` values, unwrapping
the option type to provide the raw value.

### Syntax
```
('a -> 'b option) -> System.IObservable<'a> -> System.IObservable<'b>
```

### Example
 *)
module Choose =
  printfn "# Choose"
  let task, source = createTimer DefaultInterval

  let chooser count =
    if count > 1 then Some(sprintf "Count is %d" count) else None

  source
  |> Observable.scan (fun count _ -> count + 1) 0
  |> Observable.choose chooser
  |> Observable.subscribe (fun count ->
    // Note that the string is returned from `choose`
    // rather than the option type
    printfn "Count: %s" count
  )

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.filter

Similar to `Seq.filter`, returns a stream where the predicate was `true`.

### Syntax
```
('a -> bool) -> System.IObservable<'a> -> System.IObservable<'a>
```

### Example
 *)
module Filter =
  printfn "# Filter"
  let task, source = createTimer DefaultInterval

  let predicate count =
    count > 1

  source
  |> Observable.scan (fun count _ -> count + 1) 0
  |> Observable.filter predicate
  |> Observable.subscribe (fun count ->
    printfn "Count: %d" count
  )

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.map

Works like `Seq.map`, returning a stream with each event having had a mapping
function applied.

### Syntax
```
('a -> 'b) -> System.IObservable<'a> -> System.IObservable<'b>
```

### Example
 *)
module Map =
  printfn "# Map"
  let task, source = createTimer DefaultInterval

  let mapper _ =
    "mapped"

  let capitalize (x: string) =
    x.ToUpper()

  source
  |> Observable.map mapper
  |> Observable.map capitalize
  |> Observable.subscribe (fun value -> printfn "Mapped to: %s" value)

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.merge

Merge 2 streams. Not thread safe.

### Syntax
```
System.IObservable<'a> -> System.IObservable<'a> -> System.IObservable<'a>
```
### Example
 *)
module Merge =
  printfn "# Merge"
  let task1, source1 = createTimer DefaultInterval
  let task2, source2 = createTimer (int (float DefaultInterval * 1.5))

  let obs1 = source1 |> Observable.map (fun t ->
    sprintf "Source1: %d" t.SignalTime.Millisecond
  )
  let obs2 = source2 |> Observable.map (fun t ->
    sprintf "Source2: %d" t.SignalTime.Millisecond
  )

  Observable.merge obs1 obs2
  |> Observable.subscribe (fun x -> printfn "Merged: %s" x)

  [task1; task2]
  |> Async.Parallel
  |> Async.RunSynchronously

  printfn "\n"

(**
## Observable.pairwise

Retains the last value and includes a tuple with the last and current value.
Not thread safe.

### Syntax
```
System.IObservable<'a> -> System.IObservable<'a * 'a>
```

### Example
 *)
module Pairwise =
  printfn "# Pairwise"
  let task, source = createTimer DefaultInterval

  source
  |> Observable.pairwise
  |> Observable.subscribe (fun (olderValue, laterValue) ->
    printfn "Pairwise: %d, %d"
      olderValue.SignalTime.Millisecond
      laterValue.SignalTime.Millisecond
  )

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.partition

Returns 2 streams as a tuple, splitting the original stream based on a function
that returns `true` (first stream) or `false` (second stream).

### Syntax
```
('a -> bool) -> System.IObservable<'a> ->
     System.IObservable<'a> * System.IObservable<'a>
```

### Example
 *)
module Partition =
  printfn "# Partition"
  let task, source = createTimer DefaultInterval

  let predicate t = t % 2 = 0

  let obs1, obs2 =
    source
    |> Observable.map (fun e -> e.SignalTime.Millisecond)
    |> Observable.partition predicate

  obs1 |> Observable.subscribe (fun t -> printfn "Even stream: %d" t)
  obs2 |> Observable.subscribe (fun t -> printfn "Odd stream: %d" t)

  Async.RunSynchronously task
  printfn "\n"

(**
## Observable.split

Returns 2 streams as a tuple, splitting the original stream based on a function
that returns either `Choice1Of2` or `Choice2Of2`.

### Syntax
```
('a -> Choice<'b,'c>) -> System.IObservable<'a> ->
     System.IObservable<'b> * System.IObservable<'c>
```
### Example
 *)
module Split =
  printfn "# Split"
  let task, source = createTimer DefaultInterval

  type OddEven =
    | Odd
    | Even

  let splitter count =
    match count % 2 with
    | 0 -> Choice1Of2 Odd
    | _ -> Choice2Of2 Even

  let ob1, ob2 =
    source
    |> Observable.scan (fun count _ -> count + 1 ) 0
    |> Observable.split splitter

  ob1 |> Observable.subscribe (fun x -> printfn "Stream 1: Value: %A" x)
  ob2 |> Observable.subscribe (fun x -> printfn "Stream 2: Value: %A" x)

  Async.RunSynchronously task
  printfn "\n"


