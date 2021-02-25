namespace WebsiteBuilder

type Frontmatter = {
  title: string
  description: string
  created: string
  updated: string
  keywords: list<string>
}

module Frontmatter =

  open System.Text.RegularExpressions

  let regexOptions = RegexOptions.Multiline ||| RegexOptions.Singleline

  let initial = {
    title = ""
    description = ""
    created = ""
    updated = ""
    keywords = []
  }

  let splitContent content =
    match Regex.Split(content, "^---\n", regexOptions) |> Array.toList with
      | openComment :: fm :: body :: _ -> (openComment, fm, body)
      | _ -> ("", "", content)


  let parseFrontmatter str : Frontmatter =
    str
    |> Stringx.split "\n"
    |>
      List.fold (fun fm line ->
        match Stringx.split ": " line with
        | ["title"; title] -> { fm with title = title }
        | ["description"; desc] -> { fm with description = desc }
        | ["created"; created] -> { fm with created = created }
        | ["updated"; updated] -> { fm with updated = updated }
        | ["keywords"; keywords] -> { fm with keywords = Stringx.split " " keywords }
        | _ -> fm
      ) initial


  let item prefix value suffix =
    if value <> "" then sprintf "%s%s%s" prefix value suffix
    else ""


  let header (fm: Frontmatter) =
    let keywords = fm.keywords |> String.concat " "

    [
      "<header>\n"
      (item "# " fm.title "\n")
      "<div class='timestamps'>\n"
      (item "<span class='created'>Created: " fm.created "</span>\n")
      (item "<span class='updated'>Updated: " fm.updated "</span>\n")
      (item "<div class='keywords'>keywords: " keywords "</div>\n")
      "</div>\n"
      "</header>\n"
      (item "" fm.description "\n")
    ] |> String.concat "\n"


  let convertToHeading content =
    let (openComment, fmString, body) = splitContent content
    let fm = parseFrontmatter fmString
    (fm, openComment + "\n" + header fm + body)
