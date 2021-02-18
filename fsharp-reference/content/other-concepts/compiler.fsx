(**
---
title: Compiler Directives
description: How to set preprocessor, line, compiler directives and options.
keywords: preprocessor directive conditional flag symbol line option define
---

## Preprocessor Directives

For conditional compilation and setting compiler flags.
Symbols are defined using the `OtherFlags` tag in project properties.
 *)

#if MYSYMBOL
let function1 x y : int = x * y
#else
let function1 x y = x + y
#endif

let result = function1 1 2
printfn "Result: %A" result

#if !DEBUG
printfn "DEBUG not set"
#endif

(**
`DEBUG` is automatically defined in Debug builds. Other directives can be set
in the .fsproj file. E.g The following enables the above `MYSYMBOL` directive:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
    <OtherFlags>-d:MYSYMBOL</OtherFlags>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="Program.fs" />
  </ItemGroup>
</Project>

```

## Line Directives

F# generation code can use this directive to indicate correct line numbers
from originating code.

For example, the following indicates that F# code generated at this location
derived from line 25 in a file Script1:
*)

# 25 @"C:\Projects\MyProject\MyProject\Script1"

(*
TODO: Need example

## Compiler Directives

Enables lightweight syntax. On by default See Verbose syntax for details.

When off (not commonly used) uses keywords `begin`, `end`, `in` to indicate blocks
rather than indentation.
 *)

#light "on" // or just `#light`


(**
# Compiler Options

Command-line options provided to the compiler. See original language reference for details.

TODO
 *)
