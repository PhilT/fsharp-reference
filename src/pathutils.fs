module WebsiteBuilder.Pathutils

open System.Text.RegularExpressions

let replaceSourcePath (source: string) (output: string) (path: string) =
  Regex.Replace(path, source, output)

let removePath source path = replaceSourcePath source "" path

let removeOutputPath (output: string) (path: string) =
  replaceSourcePath output "" path

let removeDate (path: string) =
  Regex.Replace(path, "\d\d\d\d-\d\d-\d\d-", "")

let replaceExtension (extension: string) (path: string) =
  Regex.Replace(path, ".fsx$|.md$", extension)

let toOutputPath (source: string) (output: string) (path: string) =
  path
  |> replaceSourcePath source (output + "/")
  |> removeDate
  |> replaceExtension ".html"

let toName source path =
  path
  |> removePath source
  |> removeDate
  |> replaceExtension ""

