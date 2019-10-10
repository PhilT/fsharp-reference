(**
# Type Abbreviations

Give a type another name. Abbreviations are an F# concept only and
not represented in .NET.
*)

type SizeType = uint32

(**
Can include generic parameters.
`Transform` takes a single argument of a type and returns
a single value of the same type.
*)
type Transform<'a> = 'a -> 'a