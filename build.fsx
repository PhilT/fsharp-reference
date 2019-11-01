#I "packages/FSharp.Compiler.Service/lib/netstandard2.0"
#I "packages/FSharp.Literate/lib/netstandard2.0"

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

open System.IO
open FSharp.Literate
open System.Text.RegularExpressions

let processSite site =
  printfn "Website: %s..." site

  let source = site + "/content"
  let output = "output/" + site
  let template = File.ReadAllText (site + "/template.html")

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

    Literate.FormatLiterateNodes(doc, OutputKind.Html, "", true, true)
    |> format

  let parse (path: string) (fm, source) =
    match Path.GetExtension(path) with
    | ".fsx" -> (fm, parseScript source)
    | _ -> (fm, FSharp.Markdown.Markdown.TransformHtml source)

  let toOutputPath (path: string) =
    Regex.Replace(path.Replace(source, output), ".fsx$|.md$", ".html")

  let replaceLineEndings (content: string) =
    content.Replace("\r\n", "\n")

  let frontmatterItem (fm: Map<string, string>) key prefix suffix =
    match fm.TryFind(key) with
    | Some value -> sprintf "%s%s%s" prefix value suffix
    | None -> ""

  let wrap (fm: Map<string, string>, content) =
    template
      .Replace("{title}", frontmatterItem fm "title" "" "")
      .Replace("{description}", frontmatterItem fm "description" "" "")
      .Replace("{document}", replaceLineEndings content)

  let writeFile outputPath content =
    File.WriteAllText(outputPath, content)

  let header (fm: Map<string, string>) =
    (frontmatterItem fm "title" "# " "\n\n") +
      (frontmatterItem fm "created" "#### " "\n") +
      (frontmatterItem fm "updated" "#### " "\n") +
      (frontmatterItem fm "categories" "#### " "\n") +
      (frontmatterItem fm "description" "" "\n\n")

  let splitContent content =
    let options = RegexOptions.Multiline ||| RegexOptions.Singleline
    match Regex.Split(content, "^---\n", options) |> Array.toList with
      | comment :: fm :: body :: _ -> (comment, fm, body)
      | _ -> ("", "", content)

  let keyValueTuple = function
    | [|key; value|] -> (key, value)
    | _ -> ("", "")

  let splitFrontmatter (str: string) =
    str.Split "\n"
    |> Array.filter (fun s -> s <> "")
    |> Array.map (fun s -> s.Split ": " |> keyValueTuple)
    |> Map.ofArray

  let convertFrontMatter content =
    let (comment, fm, body) = splitContent content
    let fmMap = splitFrontmatter fm
    (fmMap, comment + "\n" + header fmMap + body)

  let processFile path =
    let outputPath = toOutputPath path

    Path.GetDirectoryName(outputPath)
    |> Directory.CreateDirectory
    |> ignore

    if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
    then
      printfn "Processing %s" path
      path
      |> File.ReadAllText
      |> convertFrontMatter
      |> parse path
      |> wrap
      |> writeFile outputPath

  processFiles [source]
  |> Seq.iter processFile

  printfn ""

let sites = ["electricvisions"; "fsharp-reference"; "matter-game"]
sites |> Seq.iter processSite
