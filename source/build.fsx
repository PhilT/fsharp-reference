#r "paket:
source https://api.nuget.org/v3/index.json

nuget FSharp.Data 3.3.2
nuget FSharp.Literate 3.1.0 //"

open System.IO
open FSharp.Literate
open System.Text.RegularExpressions

let source = "../content"
let output = "../output"
let template = File.ReadAllText "template.html"

let filterDocument (file: string) =
  match Path.GetExtension(file) with
  | ".md" | ".fsx" -> true
  | _ -> false

let rec processFiles dirs =
  if Seq.isEmpty dirs then Seq.empty else
    seq {
      yield! dirs |> Seq.collect Directory.EnumerateFiles |> Seq.filter filterDocument
      yield! dirs |> Seq.collect Directory.EnumerateDirectories |> processFiles
    }

let format (doc: LiterateDocument) =
  Formatting.format doc.MarkdownDocument true OutputKind.Html

let parseScript source =
  let doc = Literate.ParseScriptString(source)

  FSharp.Literate.Literate.FormatLiterateNodes(doc, OutputKind.Html, "", true, true)
  |> format

let parse (path: string) source =
  match Path.GetExtension(path) with
  | ".fsx" -> parseScript source
  | _ -> FSharp.Markdown.Markdown.TransformHtml source

let toOutputPath (path: string) =
  Regex.Replace(path.Replace(source, output), ".fsx$|.md$", ".html")

let wrap (content: string) =
  template.Replace("{document}", content.Replace("\r\n", "\n"))

let writeFile outputPath content =
  File.WriteAllText(outputPath, content)

let processFile path =
  let outputPath = toOutputPath path

  Path.GetDirectoryName(outputPath)
  |> Directory.CreateDirectory
  |> ignore

  if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
  then
    path
    |> File.ReadAllText
    |> parse path
    |> wrap
    |> writeFile outputPath

    printf "."

processFiles [source]
|> Seq.iter processFile

printfn ""
