(**
# Result

Useful for writing error-tolerant code.
 *)

type Details = { Name: string; Email: string }

let validateName details =
  match details.Name with
    | null | "" -> Error "Name not entered"
    | _ -> Ok details

let validateEmail details =
  match details.Email with
    | null | "" -> Error "Email not entered"
    | _ -> Ok details

let validateDetails detailsResult =
  detailsResult
  |> Result.bind validateName
  |> Result.bind validateEmail

let test details =
  match validateDetails (Ok details) with
  | Ok details -> printfn "Valid: %A" details
  | Error e -> printfn "Invalid: %A" e

let validDetails = { Name = "Phil"; Email = "phil@example.com" }
let invalidName = { Name = null; Email = "phil@example.com" }
let invalidEmail = {Name = "Phil"; Email = ""}

test validDetails
test invalidName
test invalidEmail
