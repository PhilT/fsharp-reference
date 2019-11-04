#load "libs.fsx"
#load "pathutils.fsx"

open System.IO
open FSharp.Literate
open System.Text.RegularExpressions

let regexOptions = RegexOptions.Multiline ||| RegexOptions.Singleline

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
    match Regex.Split(content, "^---\n", regexOptions) |> Array.toList with
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
    let outputPath = Pathutils.toOutputPath source output path

    Path.GetDirectoryName(outputPath)
    |> Directory.CreateDirectory
    |> ignore

    let content = File.ReadAllText path
    let (fm, content) = convertFrontMatter content

    if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
    then
      printfn "Processing %s" path

      (fm, content)
      |> parse path
      |> wrap
      |> writeFile outputPath
    else
      printfn "Up-to-date %s" path

    frontmatters.Add(outputPath, fm)

  let indexEntry index (path: string, fm: Map<string, string>) =
    let url = path |> Pathutils.removeOutputPath output
    sprintf "%s
    <article>
    <header>
      <h1><a href='%s'>%s</a></h1>
      <div class='timestamps'>
        <span class='created'>Created: %s</span>
        <span class='updated'>%s</span>
      </div>
    </header>

    <p>
      %s
      <a href='%s'>read more »</a>
    </p>
    </article>
    " index
      url
      fm.["title"]
      fm.["created"]
      (frontmatterItem fm "updated" "Updated: " "")
      fm.["description"]
      url

  // FIXME: No Internet to get proper Yaml lib
  let config =
    let configText = File.ReadAllText (site + "/config.yml")
    let config = Regex.Split (configText, "title: |description: |\n", regexOptions)
    Map.empty.
      Add("title", config.[1]).
      Add("description", config.[3])

  let generateIndexPage (frontmatters: Map<string, Map<string, string>>) =
    let indexPath = output + "/index.html"
    if (config.["title"] = "(none)") then
      printfn "No title in config for %s - skipping generation" site
    else
      frontmatters
      |> Map.toList
      |> Seq.fold indexEntry ""
      |> (fun content -> wrap (config, content))
      |> writeFile indexPath

  // Need to add Map.empty to the start of the sequence so reduce has initial value
  processFiles [source]
  |> Seq.fold processFile Map.empty
  |> generateIndexPage

  printfn ""

let sites = ["electricvisions"; "fsharp-reference"; "matter-game"]
sites |> Seq.iter processSite
