module IndexPage

#load "frontmatter.fsx"
#load "pathutils.fsx"
#load "template.fsx"

open System.IO
open System.Text.RegularExpressions

let indexEntry output index (path: string, fm: Map<string, string>) =
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
    (Frontmatter.item fm "updated" "Updated: " "")
    fm.["description"]
    url

let generate output template (config: Map<string, string>) (frontmatters: Map<string, Map<string, string>>) =
  let indexPath = output + "/index.html"

  if (config.["title"] = "") then
    printfn "No title in config, skipping generation of Index page"
  else
    let dateSort (_, (map: Map<string, string>)) =
      if map.ContainsKey("updated") then map.["updated"]
      else map.["created"]

    frontmatters
    |> Map.toList
    |> List.sortByDescending dateSort
    |> Seq.fold (indexEntry output) ""
    |> (fun content -> Template.wrap template (config, content))
    |> Template.write indexPath
