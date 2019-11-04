open System.Text.RegularExpressions

let replaceSourcePath (source: string) (output: string) (path: string) =
  Regex.Replace(path, source, output)

let removeOutputPath (output: string) (path: string) =
  Regex.Replace(path, output, "")

let removeDate (path: string) =
  Regex.Replace(path, "\d\d\d\d-\d\d-\d\d-", "")

let replaceExtension (path: string) =
  Regex.Replace(path, ".fsx$|.md$", ".html")

let toOutputPath (source: string) (output: string) (path: string) =
  path
  |> replaceSourcePath source output
  |> removeDate
  |> replaceExtension
