#load "frontmatter.fsx"

// TODO: Try to separate everything in terms of operations
// on files and IO vs the business logic.

open System.IO

let load site =
  if File.Exists(site + "/template.html") then
    File.ReadAllText(site + "/template.html")
  else
    ""

let replaceLineEndings (content: string) =
  content.Replace("\r\n", "\n")

let write outputPath content =
  File.WriteAllText(outputPath, content)

let wrap (template: string) (fm: Map<string, string>, content) =
  content

// TODO: Probably need to remove/rename Template module as we don't need it anymore
  //template
  //  .Replace("{title}", Frontmatter.item fm "title" "" "")
  //  .Replace("{description}", Frontmatter.item fm "description" "" "")
  //  .Replace("{document}", replaceLineEndings content)
