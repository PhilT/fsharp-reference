open System
open System.IO

type MarkdownFile = {
  path: string
  keywords: string
}

let loadKeywords path =
  File.ReadLines(path)
  |> Seq.find (fun line -> line.StartsWith("keywords:") )

let contentDir = "electricvisions/content"

Directory.GetFiles(contentDir)
|> List.ofArray
|> List.map (fun path ->
  { path = path; keywords = loadKeywords(path) }
)
|> List.iter (fun markdownFile ->
  printfn $"{markdownFile.path}: {markdownFile.keywords}"
)
