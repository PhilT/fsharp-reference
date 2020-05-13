(**
---
title: Operators
description: Supported operators in the F# language.
keywords:
---

## Arithmetic Operators

* `+` - Addition
* `-` - Subtraction
* `*` - Multiplication
* `/` - Division (`DivideByZeroException` for int types and `+Infinity` or `-Infinity` for float types)
* `%` - Remainder
* `**` - Exponent or power

## Comparison Operators

* `=` - Equality (not assignment)
* `>` - Greater than
* `<` - Less than
* `>=` - Greater than or equal
* `<=` - Less than or equal
* `<>` - Not equal

To customize the comparison functions, override Equals to provide your own
custom equality comparison, and then implement `IComparable`. The
`System.IComparable` interface has a single method, the `CompareTo` method.

## Boolean Operators

* `not` - Negation
* `||` - OR
* `&&` - AND

## Bitwise Operators

* `&&&` - AND
* `|||` - OR
* `^^^` - Exclusive OR
* `~~~` - Negation
* `<<<` - Left shift
* `>>>` - Right shift

## Type Inference

Operators default to `int` type. Add a type annotation to override
 *)

let function1 x y = x + y // inferred as int

let function2 (x: float) y = x + y // override to use different types
