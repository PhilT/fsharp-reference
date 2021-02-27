module WebsiteBuilder.Main

open FSharp.Data
open FSharp.Literate
open FSharp.Formatting.Literate
open FSharp.Formatting.Markdown
open System.IO

type Hello = One | Two | Three


let processSite site =
  printfn "Website: %s..." site

  let config = Config.load site

  let sourceDir = site + "/content"
  let output = "output/" + site

  let filterDocumentType (file: string) =
    match Path.GetExtension(file) with
    | ".md" | ".fsx" -> true
    | _ -> false


  let parseScript source =
    let doc = Literate.ParseScriptString(source)
    Literate.ToHtml(doc)


  let parse (path: string) source =
    match Path.GetExtension(path) with
    | ".fsx" -> parseScript source
    | _ -> Markdown.ToHtml(Markdown.Parse source)


  let rec processFiles dirs =
    if Seq.isEmpty dirs then Seq.empty else
      seq {
        yield! dirs |> Seq.collect Directory.EnumerateFiles |> Seq.filter filterDocumentType
        yield! dirs |> Seq.collect Directory.EnumerateDirectories |> processFiles
      }


  let processFile (frontmatters: Map<string, Frontmatter>) path =
    let sourceDirRegex = sourceDir + @"(\\|/)"
    let outputPath = Pathutils.toOutputPath sourceDirRegex output path
    let name = Pathutils.toName sourceDirRegex path

    Path.GetDirectoryName(outputPath)
    |> Directory.CreateDirectory
    |> ignore

    let content = File.ReadAllText path
    let (fm, content) = Frontmatter.convertToHeading content

    if File.GetLastWriteTime(path) > File.GetLastWriteTime(outputPath)
    then
      printfn "Processing %s" name

      let html = parse path content |> Template.wrapArticle site |> Template.wrap site

      File.WriteAllText(outputPath, html)
    else
      printfn "Up-to-date %s" name

    Map.add name fm frontmatters


  processFiles [sourceDir]
  |> Seq.fold processFile Map.empty
  |> IndexPage.generate output site

  printfn ""


["electricvisions"; "matter-game"]
|> Seq.iter processSite
