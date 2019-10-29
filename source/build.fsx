#r "paket:
source https://api.nuget.org/v3/index.json
source https://ci.appveyor.com/nuget/fsharp-formatting

nuget FSharp.Data 3.3.2
nuget FSharp.Literate 3.1.0 //"

open System.IO
open FSharp.Literate

let source = "../content"
let output = "../output"
let template = File.ReadAllText "template.html"

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

let wrap (content: string) =
  template.Replace("{document}", content.Replace("\r\n", "\n"))

let processFile path =
  let outputPath = toOutputPath path

  Path.GetDirectoryName(outputPath)
  |> Directory.CreateDirectory
  |> ignore

  if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
  then
    File.WriteAllText (outputPath,
      path
      |> File.ReadAllText
      |> parse
      |> format
      |> wrap
    )

    printf "."

processFiles "*.fsx" [source]
|> Seq.iter processFile

printfn ""
