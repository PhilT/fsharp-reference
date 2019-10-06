// # Attribute

// Allow metadata to be applied to a construct. Attributes are a .NET
// programming construct that enables an object attribute to be
// associated with a type.

open System.Runtime.InteropServices

[<DllImport("kernel32", SetLastError=true)>]
extern bool CloseHandle(nativeint handle)

// Attributes can be applied to functions, methods, assemblies, modules, types,
// constructors, properties, fields, parameters, type parameters and return
// values. Attributes are not allowed on `let` bindings inside classes,
// expressions or workflow expressions.

// They can be defined individually or together:

type Owner(name) =
  inherit System.Attribute()

type Company(name) =
  inherit System.Attribute()

[<Owner("Phil Thompson")>]
[<Company("Electric Visions")>]
type ISomeType3 =
  abstract member SomeMember : unit -> unit

[<Owner("Phil Thompson"); Company("Electric Visions")>]
type ISomeType4 =
  abstract member SomeMember : unit -> unit

// Typical use cases:

open System
[<Obsolete("Do no use. Use newFunction instead.")>]
let obsoleteFunction x y = ()

let newFunction x y = ()

let result = obsoleteFunction 10 100 // produces a warning

open System.Reflection
[<assembly:AssemblyVersionAttribute("1.0.0.0")>]
do
  printfn "Executing..."

// If the target (in this case, `assembly`) is omitted for a `do` binding,
// F# compiler attempts to infer it. Most attributes either apply to
// functions or assemblies and so can be inferred. When both, functions is
// the default and applies to the program's main function.
