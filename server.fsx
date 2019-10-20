open System
open System.Net
open System.Text
open System.IO

let siteRoot = @"C:\Users\Phil\Sync\phil\code\fsharp-reference\output"
let host = "http://localhost:8080/"

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
    let file = Path.Combine(siteRoot,
                            Uri(host).MakeRelativeUri(req.Url).OriginalString)
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
