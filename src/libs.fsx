#I "../packages/FSharp.Compiler.Service/lib/netstandard2.0"
#I "../packages/FSharp.Literate/lib/netstandard2.0"

#r "FSharp.Compiler.Service.dll"
#r "FSharp.Formatting.Common.dll"
#r "FSharp.Markdown.dll"
#r "FSharp.CodeFormat.dll"
#r "FSharp.Literate.dll"
#r "FSharp.MetadataFormat.dll"

// HACK: force usage of Fsharp.Compiler.Services
// or the indirect reference from FSharp.Literate will fail to load
let dummy (pos: FSharp.Compiler.Range.pos) =
    pos.Column
FSharp.Compiler.Range.mkPos 1 1 |> dummy
