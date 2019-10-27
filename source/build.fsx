#r "paket:
source https://api.nuget.org/v3/index.json
source https://ci.appveyor.com/nuget/fsharp-formatting

nuget Fake.IO.FileSystem
nuget Fake.Core.Trace
nuget FSharp.Data
nuget Fable.React
nuget FSharp.Literate //"

let source = "../content"
let output = "../output"
let template = "template.html"

open System.IO
open FSharp.Literate

let rec processFiles pattern dirs =
  if Seq.isEmpty dirs then Seq.empty else
    seq {
      yield! dirs |> Seq.collect (fun dir -> Directory.EnumerateFiles(dir, pattern))
      yield! dirs |> Seq.collect Directory.EnumerateDirectories |> processFiles pattern
    }

let parse source =
  let doc =
    let fsharpCoreDir = "-I:" + __SOURCE_DIRECTORY__ + "/../lib"
    let systemRuntime = "-r:System.Runtime"
    Literate.ParseScriptString(
      source,
      compilerOptions = systemRuntime + " " + fsharpCoreDir,
      fsiEvaluator = FSharp.Literate.FsiEvaluator([|fsharpCoreDir|])
    )

  FSharp.Literate.Literate.FormatLiterateNodes(doc, OutputKind.Html, "", true, true)

let format (doc: LiterateDocument) =
  Formatting.format doc.MarkdownDocument true OutputKind.Html

let toOutputPath (path: string) =
  path.Replace(source, output).Replace(".fsx", ".html")

let processFile path =
  let outputPath = toOutputPath path

  File.WriteAllText (outputPath,
    path
    |> File.ReadAllText
    |> parse
    |> format
  )

processFiles "*.fsx" [source]
|> Seq.iter processFile
