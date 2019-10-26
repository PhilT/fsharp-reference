#r "paket:
source https://api.nuget.org/v3/index.json

nuget Fake.IO.FileSystem
nuget Fake.Core.Trace
nuget FSharp.Data
nuget FSharp.Formatting //"

#load "packages/fsharp.formatting/3.1.0/FSharp.Formatting.fsx"

open FSharp.Formatting.Razor

let source = "../content"
let output = "../output"
let template = "template.html"

RazorLiterate.ProcessDirectory (source, template, output)
