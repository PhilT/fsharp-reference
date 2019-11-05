#load "libs.fsx"

#load "frontmatter.fsx"
#load "index_page.fsx"
#load "pathutils.fsx"
#load "template.fsx"

open FSharp.Data
open FSharp.Literate
open System.IO
open System.Text.RegularExpressions

let loadConfig site =
  (JsonValue.Load (__SOURCE_DIRECTORY__ + "/../" + site + "/config.json")).Properties()
  |> Array.filter (fun (_, v) -> match v with JsonValue.String _ -> true | _ -> false)
  |> Array.map (fun (k, v) -> (k, v.AsString()))
  |> Map.ofArray

let processSite site =
  printfn "Website: %s..." site

  let config = loadConfig site
  let template = Template.load site

  let source = site + "/content"
  let output = "output/" + site

  let filterDocumentType (file: string) =
    match Path.GetExtension(file) with
    | ".md" | ".fsx" -> true
    | _ -> false

  let format (doc: LiterateDocument) =
    Formatting.format doc.MarkdownDocument true OutputKind.Html

  let parseScript source =
    let doc = Literate.ParseScriptString(source)

    Literate.FormatLiterateNodes(doc, OutputKind.Html, "", true, true)
    |> format

  let parse (path: string) (fm, source) =
    match Path.GetExtension(path) with
    | ".fsx" -> (fm, parseScript source)
    | _ -> (fm, FSharp.Markdown.Markdown.TransformHtml source)

  let rec processFiles dirs =
    if Seq.isEmpty dirs then Seq.empty else
      seq {
        yield! dirs |> Seq.collect Directory.EnumerateFiles |> Seq.filter filterDocumentType
        yield! dirs |> Seq.collect Directory.EnumerateDirectories |> processFiles
      }

  let processFile (frontmatters: Map<string, Map<string, string>>) path =
    let outputPath = Pathutils.toOutputPath source output path

    Path.GetDirectoryName(outputPath)
    |> Directory.CreateDirectory
    |> ignore

    let content = File.ReadAllText path
    let (fm, content) = Frontmatter.convertToHeading content

    if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
    then
      printfn "Processing %s" path

      (fm, content)
      |> parse path
      |> Template.wrap template
      |> Template.write outputPath
    else
      printfn "Up-to-date %s" path

    frontmatters.Add(outputPath, fm)

  processFiles [source]
  |> Seq.fold processFile Map.empty
  |> IndexPage.generate output template config

  printfn ""

let sites = ["electricvisions"; "fsharp-reference"; "matter-game"]
sites |> Seq.iter processSite
