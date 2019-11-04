module IndexPage

#load "pathutils.fsx"
#load "frontmatter.fsx"
#load "template.fsx"

open System.IO
open System.Text.RegularExpressions

let regexOptions = RegexOptions.Multiline ||| RegexOptions.Singleline

// FIXME: No Internet to get proper Yaml lib
let loadConfig site =
  let configText = File.ReadAllText (site + "/config.yml")
  let config = Regex.Split (configText, "title: |description: |\n", regexOptions)
  Map.empty.
    Add("title", config.[1]).
    Add("description", config.[3])

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

let generate output site (frontmatters: Map<string, Map<string, string>>) =
  let config = loadConfig site
  let indexPath = output + "/index.html"
  if (config.["title"] = "(none)") then
    printfn "No title in config for %s - skipping generation" site
  else
    frontmatters
    |> Map.toList
    |> Seq.fold (indexEntry output) ""
    |> (fun content -> Template.wrap site (config, content))
    |> Template.write indexPath
