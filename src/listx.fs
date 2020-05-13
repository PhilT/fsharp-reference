module WebsiteBuilder.Listx

let toString lst =
  lst
  |> List.map (fun str -> sprintf "'%s'" str)
  |> String.concat ","

