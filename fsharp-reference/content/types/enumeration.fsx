(**
---
title: Enumeration
description: A set of related labels.
keywords: discriminated union
---
 *)

type Color =
  | Red = 0
  | Green = 1
  | Blue = 2

let col: Color = Color.Red // usage
let colN = int col // colN = 0

//TODO Expand usage
