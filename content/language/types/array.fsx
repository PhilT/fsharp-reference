// # Array

// Ordered sets of mutable elements of the same type. Based on Array .NET Type.


// ## Creating

let array1 = [| 1; 2; 3 |]

let array2 = [|
      1
      2
      3
    |]

let array3 = [| for i in 1..10 -> i * i |]

let array4: int array = Array.zeroCreate 10 // Array of 10 zeros

let array2d = array2D [ [1; 0]; [0; 1]] // Create Array2D from 2D List with `array2D` operator


// ## Accessing

array1.[0]
array1.[0..2] // Using slice notation to access elements from 0 to 2
array1.[..2] // elements from 0 to 2
array1.[2..] // elements from 2 to end of array


// ## Array Module

// https://msdn.microsoft.com/visualfsharpdocs/conceptual/collections.array-module-%5bfsharp%5d
