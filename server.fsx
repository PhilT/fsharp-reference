open System
open System.Net
open System.Text
open System.IO

let siteRoot = @"output"
let hostFromEnv = Environment.GetEnvironmentVariable("HOST")
let host = match hostFromEnv with
            | null -> "http://localhost:8080/"
            | _ -> hostFromEnv

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

let output (req:HttpListenerRequest) =
    let file = Path.Combine(siteRoot, req.Url.LocalPath.[1..])
    printfn "Requested : '%s'" file
    if (File.Exists file)
    then (File.ReadAllText(file), Path.GetExtension(file))
    else ("File does not exist!", file)

let contentTypeFor ext =
  match ext with
  | ".html" -> "text/html"
  | ".css" -> "text/css"
  | ".js" -> "application/javascript"
  | _ -> "text/plain"

listener (fun req resp ->
    async {
      let (content, ext) = output req
      let txt = Encoding.ASCII.GetBytes(content)
      resp.ContentType <- contentTypeFor ext
      resp.OutputStream.Write(txt, 0, txt.Length)
      resp.OutputStream.Close()
    })
