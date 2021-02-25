open System
open System.Net
open System.Text
open System.IO
open System.Text.RegularExpressions

let website = Array.last fsi.CommandLineArgs
let siteRoot = "output/" + website
let pageRoot = siteRoot + "/index.html"
let port = match website with
            | "electricvisions" -> "9000"
            | "fsharp-reference" -> "9001"
            | "matter-game" -> "9002"
            | _ -> "9003"
let host = "http://+:" + port + "/"

printfn "Serving files from %s" siteRoot
printfn "Host set to %A" host

let listener (handler:(HttpListenerRequest->HttpListenerResponse->Async<unit>)) =
  let hl = new HttpListener()
  hl.Prefixes.Add host
  hl.Start()
  let task = Async.FromBeginEnd(hl.BeginGetContext, hl.EndGetContext)
  async {
    while true do
      let! context = task
      Async.Start(handler context.Request context.Response)
  } |> Async.Start

let contentTypeFor ext =
  match ext with
  | ".html" -> "text/html"
  | ".css" -> "text/css"
  | ".js" -> "text/javascript"
  | ".webp" -> "image/webp"
  | ".svg" -> "image/svg+xml"
  | ".ico" -> "image/x-icon"
  | _ -> "text/plain"

let output (req:HttpListenerRequest) =
  let requestedPath = req.Url.LocalPath.[1..]
  printfn "Requested : '%s'" requestedPath
  let actualPath = if requestedPath = "" then "index.html" else requestedPath
  let actualPath = Path.Combine(siteRoot, actualPath)

  let ext = Path.GetExtension(actualPath)
  let contentType = contentTypeFor ext
  let content = if File.Exists actualPath
                then File.ReadAllBytes(actualPath)
                else File.ReadAllBytes(pageRoot)

  (content, contentType)

listener (fun req resp ->
  async {
    let (content, contentType) = output req
    resp.ContentType <- contentType
    resp.OutputStream.Write(content, 0, content.Length)
    resp.OutputStream.Close()
  })
