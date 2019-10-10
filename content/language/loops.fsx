(**
# Loops

## for...to Expression

Iterate over a range of values returning unit.
Works like imperative for loop.
 *)

printfn "for...to"

for i = 1 to 10 do
  printf "%d " i
printfn ""

for i = 10 downto 1 do
  printf "%d " i
printfn ""

(**
## for...in Expression

Iterate over values in an enumerable collection.
 *)

printfn "for...in"

let list1 = [1; 5; 100; 450; 788]
for i in list1 do
  printf "%d " i
printfn ""

for i in 10 .. -1 .. 1 do
  printf "%d " i
printfn ""

(**
## while...do Expression

Loop while a condition is true
 *)

open System

let randomGenerator = Random()
let mutable value = randomGenerator.Next(10)
printf "%d " value
while value > 1 do
  printf "%d " value
  value <- randomGenerator.Next(10)
printfn "Done"
