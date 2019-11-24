open System.Text.RegularExpressions

let regexOptions = RegexOptions.Multiline ||| RegexOptions.Singleline

let splitContent content =
  match Regex.Split(content, "^---\n", regexOptions) |> Array.toList with
    | openComment :: fm :: body :: _ -> (openComment, fm, body)
    | _ -> ("", "", content)

let keyValueTuple = function
  | [|key; value|] -> (key, value)
  | _ -> ("", "")

let split (str: string) =
  str.Split "\n"
  |> Array.filter (fun s -> s <> "")
  |> Array.map (fun s -> s.Split ": " |> keyValueTuple)
  |> Map.ofArray

let item (fm: Map<string, string>) key prefix suffix =
  match fm.TryFind(key) with
  | Some value -> sprintf "%s%s%s" prefix value suffix
  | None -> ""

let header (fm: Map<string, string>) =
  "<header>\n" +
    (item fm "title" "# " "\n") +
    "<div class='timestamps'>\n" +
    (item fm "created" "<span class='created'>Created: " "</span>\n") +
    (item fm "updated" "<span class='updated'>Updated: " "</span>\n") +
    "</div>\n" +
    "</header>\n" +
    (item fm "categories" "<div class='categories'>categories: " "</div>\n\n") +
    (item fm "description" "" "\n\n")

let convertToHeading content =
  let (openComment, fm, body) = splitContent content
  let fmMap = split fm
  (fmMap, openComment + "\n" + header fmMap + body)
