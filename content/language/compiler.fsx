// # Compiler Directives

// ## Preprocessor Directives

// For conditional compilation and setting compiler flags.
// Symbols are defined in project settings or compiler options

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


// ## Line Directives

// F# generation code can use this directive to indicate correct line numbers
// from originating code.

// TODO: Need example

// ## Compiler Directives

// Enables lightweight syntax. On by default See Verbose syntax for details.

// When off (not commonly used) uses keywords `begin`, `end`, `in` to indicate blocks
// rather than indentation.
#light "on" // or just `#light`


// # Compiler Options

// Command-line options provided to the compiler. See original language reference for details.
