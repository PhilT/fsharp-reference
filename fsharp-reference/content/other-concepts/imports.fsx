(**
---
title: Import Declarations
description: Use `open` to avoid having to type the fully qualified name including namespace and module names.
---

*)

open System.IO

(**
Namespaces that are open by default:

* `Microsoft.FSharp.Core` - `int`, `float`, etc
* `Microsoft.FSharp.Core.Operators` - `+`, `*`, etc
* `Microsoft.FSharp.Collections` - `List`, `Array`, etc
* `Microsoft.FSharp.Control` - lazy eval, async workflows
* `Microsoft.FSharp.Text` - `printf`, etc

`AutoOpen` attribute can be applied to an assembly to automatically open a
namespace or module. `AutoOpen` attribute can be applied to a module to
automatically open it when the parent module or namespace is opened.

`RequireQualifiedAccess` attribute can be specified to enforce qualified
name to be used even if the module/namespace is opened.

*)
