module WebsiteBuilder.IndexPage

open System.IO
open System.Text.RegularExpressions

let indexEntry index (path: string, fm: Frontmatter) =
  let keywords = fm.keywords |> String.concat " "
  let updated = Frontmatter.item "<span class='updated'><span class='label'>Updated: </span>" fm.updated "</span>"

  $"{index}
  <article class='summary {keywords}'>
  <header>
    <div class='keywords'>{keywords}</div>
    <h1><a href='/{path}.html'>{fm.title}</a></h1>
    <div class='timestamps'>
      <span class='created'><span class='label'>Created: </span>{fm.created}</span>
      {updated}
    </div>
  </header>

  <p>
    {fm.description}
    <a href='/{path}.html'>read more»</a>
  </p>
  </article>
  "


let generate output site frontmatters =
  let indexPath = output + "/index.html"

  let dateSort (_, (fm: Frontmatter)) = fm.created

  let html =
    frontmatters
    |> Map.toList
    |> List.filter (fun (_, fm) -> fm.created <> "")
    |> List.sortByDescending dateSort
    |> Seq.fold indexEntry ""
    |> Template.wrap site

  printfn "Writing article index to '%s'" indexPath
  File.WriteAllText(indexPath, html)
