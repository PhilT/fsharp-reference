module WebsiteBuilder.Stringx

open System.Globalization

let split (splitter: string) (str: string) = str.Split(splitter) |> Array.toList
let startsWith (term: string) (str: string) = str.StartsWith(term)

let titleize (str: string) =
  let textinfo = CultureInfo("en", false).TextInfo
  textinfo.ToTitleCase(str.Replace("-", " "))

