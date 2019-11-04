let item (fm: Map<string, string>) key prefix suffix =
  match fm.TryFind(key) with
  | Some value -> sprintf "%s%s%s" prefix value suffix
  | None -> ""
