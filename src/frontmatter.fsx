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
  (item fm "title" "# " "\n\n") +
    (item fm "created" "##### Created: " "\n") +
    (item fm "updated" "##### Updated: " "\n") +
    (item fm "categories" "###### categories: " "\n") +
    (item fm "description" "" "\n\n")

let convertToHeading content =
  let (openComment, fm, body) = splitContent content
  let fmMap = split fm
  (fmMap, openComment + "\n" + header fmMap + body)
