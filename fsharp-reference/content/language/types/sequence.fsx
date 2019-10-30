(**
# Sequence

Lazy computed values. Data types such as lists, arrays, sets, and maps are
implicitly sequences as they all implement `IEnumerable<'T>` which is a
`seq<'T>`.
 *)

Seq.empty // Creates an empty sequence
Seq.singleton 10 // Creates a sequence of one element, 10
seq { 1..5 } // Creates the sequence 1, 2, 3, 4, 5
seq { 0..10..100 } // Creates a sequence 0 to 100 in steps of 10
seq { for i in 1..10 -> i * i } // yields the square for each value in the range 1 to 10

let (height, width) = (10, 10)
seq { for row in 0..width - 1 do
        for col in 0..height - 1 do
          yield (row, col, row * width + col)
    }

let isPrime n = true
seq { for n in 1..100 do if isPrime n then yield n } // if acts as a filter

(**
`yield` generates a single element. Use `yield!` when generating multiple
elements that will be concatenated.

## Seq Module

https://msdn.microsoft.com/visualfsharpdocs/conceptual/collections.seq-module-[fsharp]
 *)
