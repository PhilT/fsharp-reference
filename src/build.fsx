#load "libs.fsx"
#load "pathutils.fsx"
#load "index_page.fsx"
#load "frontmatter.fsx"
#load "template.fsx"

open System.IO
open FSharp.Literate
open System.Text.RegularExpressions

let regexOptions = RegexOptions.Multiline ||| RegexOptions.Singleline

let processSite site =
  printfn "Website: %s..." site

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

  let header (fm: Map<string, string>) =
    (Frontmatter.item fm "title" "# " "\n\n") +
      (Frontmatter.item fm "created" "##### Created: " "\n") +
      (Frontmatter.item fm "updated" "##### Updated: " "\n") +
      (Frontmatter.item fm "categories" "###### categories: " "\n") +
      (Frontmatter.item fm "description" "" "\n\n")

  let splitContent content =
    match Regex.Split(content, "^---\n", regexOptions) |> Array.toList with
      | comment :: fm :: body :: _ -> (comment, fm, body)
      | _ -> ("", "", content)

  let keyValueTuple = function
    | [|key; value|] -> (key, value)
    | _ -> ("", "")

  // TODO: Move to frontmatter.fsx
  let splitFrontmatter (str: string) =
    str.Split "\n"
    |> Array.filter (fun s -> s <> "")
    |> Array.map (fun s -> s.Split ": " |> keyValueTuple)
    |> Map.ofArray

  // TODO: Move to frontmatter.fsx
  let convertFrontMatter content =
    let (comment, fm, body) = splitContent content
    let fmMap = splitFrontmatter fm
    (fmMap, comment + "\n" + header fmMap + body)

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
    let (fm, content) = convertFrontMatter content

    if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
    then
      printfn "Processing %s" path

      (fm, content)
      |> parse path
      |> Template.wrap site
      |> Template.write outputPath
    else
      printfn "Up-to-date %s" path

    frontmatters.Add(outputPath, fm)

  // Need to add Map.empty to the start of the sequence so reduce has initial value
  processFiles [source]
  |> Seq.fold processFile Map.empty
  |> IndexPage.generate output site

  printfn ""

let sites = ["electricvisions"; "fsharp-reference"; "matter-game"]
sites |> Seq.iter processSite
