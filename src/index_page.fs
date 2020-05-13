module WebsiteBuilder.IndexPage

open System.IO
open System.Text.RegularExpressions

let indexEntry index (path: string, fm: Frontmatter) =
  sprintf "%s
  <article>
  <header>
    <h1><a href='/%s.html'>%s</a></h1>
    <div class='timestamps'>
      <span class='created'>Created: %s</span>
      <span class='updated'>%s</span>
    </div>
  </header>

  <p>
    %s
    <a href='/%s.html'>read more »</a>
  </p>
  </article>
  " index
    path
    fm.title
    fm.created
    (Frontmatter.item "updated" fm.updated "Updated: ")
    fm.description
    path

let generateArticleIndex output (frontmatters: Map<string, Frontmatter>) =
  let indexPath = output + "/index.html"

  let dateSort (_, (fm: Frontmatter)) =
    [fm.updated; fm.created]
    |> Seq.find (fun date -> date <> "")


  let html =
    frontmatters
    |> Map.toList
    |> List.filter (fun (_, fm) -> fm.created <> "")
    |> List.sortByDescending dateSort
    |> Seq.fold indexEntry ""

  printfn "Writing article index to '%s'" indexPath
  File.WriteAllText(indexPath, html)


let generatePageInfoFor section frontmatters =
  frontmatters
  |> Map.filter (fun key _ -> Stringx.startsWith section key)
  |> Map.map (fun path fm ->
    sprintf "{ id: '%s', name: '%s', keywords: [%s] }"
      (Pathutils.removePath (section + @"\\") path)
      fm.title
      (Listx.toString fm.keywords)
  )
  |> Map.toList
  |> List.map (fun (_, v) -> v)
  |> String.concat ",\n"


let generateJsIndex sections output frontmatters =
  let indexPath = output + "/../assets/index.js"

  let sectionsList =
    sections
    |>
      List.map (fun section ->
      let heading = Stringx.titleize section
      let path = section + "/"
      let pages = generatePageInfoFor section frontmatters
      sprintf "{\n\
          heading: '%s',\n\
          path: '%s',\n\
          pages: [\n%s\n\
          ]\n\
        }"
        heading
        path
        pages
    )
    |> String.concat ",\n"


  let js = "window.menu = [\n" + sectionsList + "\n]\n"

  printfn "Writing index to '%s'" indexPath
  File.WriteAllText(indexPath, js)


let generate output (config: Config) frontmatters =
  if (List.isEmpty config.sections) then
    generateArticleIndex output frontmatters
  else
    generateJsIndex config.sections output frontmatters
