#load "frontmatter.fsx"

// TODO: Try to separate everything in terms of operations
// on files and IO vs the business logic.

open System.IO

let load site = File.ReadAllText (site + "/template.html")

let replaceLineEndings (content: string) =
  content.Replace("\r\n", "\n")

let write outputPath content =
  File.WriteAllText(outputPath, content)

let wrap (template: string) (fm: Map<string, string>, content) =
  template
    .Replace("{title}", Frontmatter.item fm "title" "" "")
    .Replace("{description}", Frontmatter.item fm "description" "" "")
    .Replace("{document}", replaceLineEndings content)
