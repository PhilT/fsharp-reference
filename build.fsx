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

  let replaceSourcePath (path: string) =
    Regex.Replace(path, source, output)

  let removeOutputPath (path: string) =
    Regex.Replace(path, output, "")

  let removeDate (path: string) =
    Regex.Replace(path, "\d\d\d\d-\d\d-\d\d-", "")

  let replaceExtension (path: string) =
    Regex.Replace(path, ".fsx$|.md$", ".html")

  let toOutputPath (path: string) =
    path
    |> replaceSourcePath
    |> removeDate
    |> replaceExtension

  let replaceLineEndings (content: string) =
    content.Replace("\r\n", "\n")

  let frontmatterItem (fm: Map<string, string>) key prefix suffix =
    match fm.TryFind(key) with
    | Some value -> sprintf "%s%s%s" prefix value suffix
    | None -> ""

  let writeFile outputPath content =
    File.WriteAllText(outputPath, content)

  let header (fm: Map<string, string>) =
    (frontmatterItem fm "title" "# " "\n\n") +
      (frontmatterItem fm "created" "##### Created: " "\n") +
      (frontmatterItem fm "updated" "##### Updated: " "\n") +
      (frontmatterItem fm "categories" "###### categories: " "\n") +
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

  let parse (path: string) (fm, source) =
    match Path.GetExtension(path) with
    | ".fsx" -> (fm, parseScript source)
    | _ -> (fm, FSharp.Markdown.Markdown.TransformHtml source)

  let wrap (fm: Map<string, string>, content) =
    template
      .Replace("{title}", frontmatterItem fm "title" "" "")
      .Replace("{description}", frontmatterItem fm "description" "" "")
      .Replace("{document}", replaceLineEndings content)

  let rec processFiles dirs =
    if Seq.isEmpty dirs then Seq.empty else
      seq {
        yield! dirs |> Seq.collect Directory.EnumerateFiles |> Seq.filter filterDocumentType
        yield! dirs |> Seq.collect Directory.EnumerateDirectories |> processFiles
      }

  let processFile (frontmatters: Map<string, Map<string, string>>) path =
    let outputPath = toOutputPath path

    Path.GetDirectoryName(outputPath)
    |> Directory.CreateDirectory
    |> ignore

    if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
    then
      printfn "Processing %s" path
      let content = File.ReadAllText path
      let (fm, content) = convertFrontMatter content

      (fm, content)
      |> parse path
      |> wrap
      |> writeFile outputPath

      frontmatters.Add(outputPath, fm)
    else
      frontmatters

  let indexEntry index (path: string, fm: Map<string, string>) =
    let url = path |> removeOutputPath
    sprintf "%s
    <article>
    <h1><a href='%s'>%s</a></h1>
    <h5>Created: %s</h5>
    %s

    <p>
      %s
      <a href='%s'>read more »</a>
    </p>
    </article>
    " index
      url
      fm.["title"]
      fm.["created"]
      (frontmatterItem fm "updated" "<h5>Updated: " "</h5>\n")
      fm.["description"]
      url

  let indexFrontmatter =
    Map.empty.
      Add("title", "matter-game.com").
      Add("description", "A blog about the progress of Matter, a new game I'm working on. Topics such as JavaScript, 3D, physics, Real-time strategy (RTS) will be discussed as well as the games industry in general and how I hope to make it a better place.")

  let generateIndexPage (frontmatters: Map<string, Map<string, string>>) =
    let indexPath = output + "/index.html"
    if (File.Exists indexPath) then
      printfn "%s exists - skipping generation" indexPath
    else
      frontmatters
      |> Map.toList
      |> Seq.fold indexEntry ""
      |> (fun content -> wrap (indexFrontmatter, content))
      |> writeFile indexPath

  // Need to add Map.empty to the start of the sequence so reduce has initial value
  processFiles [source]
  |> Seq.fold processFile Map.empty
  |> generateIndexPage

  printfn ""

let sites = ["electricvisions"; "fsharp-reference"; "matter-game"]
sites |> Seq.iter processSite
