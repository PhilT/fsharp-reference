(**
---
title: Literals
description: Note that hexidecimal characters are not case-sensitive but type identifiers are.
---

Below these literals are casted but this is not necessary. It's just to
illustrate the type and cast keyword that can be used.
 *)

sbyte 86y                     // signed 8-bit integer
byte 86uy                     // unsigned 8-bit integer
int16 86s                     // signed 16-bit integer
uint16 86us                   // unsigned 16-bit integer
int16 86, 86l                 // signed 32-bit integer
uint32 86u, 86ul              // unsigned 32-bit integer
nativeint 123n                // native pointer to a signed number
unativeint 0x0002df           // native pointer to unsigned number
int64 86L                     // signed 64-bit integer
uint64 86UL                   // unsigned 64-bit integer
single 4.14F, float32 4.14f   // 32-bit floating point number
float 4.321, double 4.321     // 64-bit floating point number
999999999999I                 // bitint - for integers not limited to 64-bit
char 'a', '\u0061'            // Unicode character
string "text"                 // Unicode string
@"C:\filename"                // Unescaped unicode string
"""<code>"is cool"!</code>""" // Unescaped unicode string (allow double quotes)
byte 'a'B                     // ASCII character
"text"B                       // (byte[]) ASCII string

(**
## Named literals (Constants)

As all values are immutable by default, constants can be marked additionally
with the `[<Literal>]` attribute.

In pattern matching, identifiers that begin with lowercase characters are always
treated as variables to be bound, so you should always use `PascalCase` when
defining literals.

 *)

[<Literal>]
let MaxSize = 1000
let ReadWrite = System.IO.FileAccess.Read ||| System.IO.FileAccess.Write

(**
## Hexidecimal, Octal and Binary integers

 *)

let numbers = (0x9f, 0o77, 0b10101)

(**
## Use Underscores for readability

 *)

let value = 0xDEAD_BEEF
let exampleSsn = 123_456_7890

