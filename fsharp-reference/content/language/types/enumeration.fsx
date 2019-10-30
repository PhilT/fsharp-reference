(**
# Enumeration

A set of related labels.
 *)

type Color =
  | Red = 0
  | Green = 1
  | Blue = 2

let col: Color = Color.Red // usage
let colN = int col // colN = 0
