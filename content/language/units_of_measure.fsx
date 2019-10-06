// # Units of Measure

// Floating point and signed values can have length, volume, mass, etc.
// This helps the compiler ensure types have the correct units.
// Used for compile-time checking only so no run-time penalty.

// New units can be defined as follows:

[<Measure>] type cm
[<Measure>] type ml = cm^3 // Defined in terms of the previous definition

[<Measure>] type miles
[<Measure>] type hour
[<Measure>] type g
[<Measure>] type kg

let length = 1.0f<cm>
printfn "%0.2f" length

55.0<miles/hour>
55.0f<miles/hour> //single-precision float

let gramsToKilos (x: float<g>) = x / 1000.0<g/kg>

let grams = 3452.0<g>
printfn "gramsToKilos %0.2f %0.3f" grams (gramsToKilos grams)

// ## Generic Units

[<Measure>] type m // meters
[<Measure>] type s // seconds

let sumUnits (x: float<'u>) (y: float<'u>) = x + y

let result1 = sumUnits 3.1<m/s> 2.7<m/s>
// `let result2 = sumUnits 3.1<m/s> 2.7<m>` // Compile Error: Unit type mismatch

// ## Aggregate Types with Generic Units

// Define a vector type with generic units
type vector3D<[<Measure>] 'u> = {x: float<'u>; y: float<'u>; z: float<'u>}

// Create 2 vectors with different units
let xvec: vector3D<m> = {x = 0.0<m>; y = 0.0<m>; z = 0.0<m>}
let v1vec: vector3D<m/s> = {x = 0.0<m/s>; y = 0.0<m/s>; z = 0.0<m/s>}

// ## Conversions

let x = float length // Create unitless float from length with units

// For interoperability there are also explicit functions
let height: float<cm> = LanguagePrimitives.FloatWithMeasure x