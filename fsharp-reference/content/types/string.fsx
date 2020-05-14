(**
---
title: String
description: Immutable text as a sequence of unicode characters.
keywords: immutable text escape quote
---

Some examples:

 *)

printfn "Standard string with\n\tsome escape sequences"
printfn "%A" @"Verbatim string escape sequences such as \n are ignored. Quoting can be done like so, ""Quoted text"""
printfn "%A" """With triple quoted strings all escape sequences are ignored and text can be quoted like so, "Quoted text"."""

printfn "This has a line
break"
printfn "This does not have\
a line break"

printfn "Second character in the string is %c" "Hello".[1]
printfn "Substring of Hello is %s" "Hello".[0..2]

(**
ASCII array representation of a string
 *)
let bytearray : byte[] = "abc"B

(**
Concatenate a string
 *)
let string1 = "Hello, " + "World"

(**
## Core.String Module

https://msdn.microsoft.com/visualfsharpdocs/conceptual/core.string-module-%5bfsharp%5d
 *)
