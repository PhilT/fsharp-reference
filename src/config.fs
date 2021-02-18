namespace WebsiteBuilder

open System.IO
open System.Text.RegularExpressions

type Config = {
  title: string
  description: string
  linkedin: string
  twitter: string
  github: string
  envelope: string
  css: list<string>
}

module Config =
  let initial = {
    title = ""
    description = ""
    linkedin = ""
    twitter = ""
    github = ""
    envelope = ""
    css = []
  }

  let load site : Config =
    File.ReadLines(__SOURCE_DIRECTORY__ + "/../" + site + "/config.yml")
    |> Seq.fold (fun acc line ->
      let regex = Regex.Split(line, ": ")
      let key, value = regex.[0], regex.[1]
      match key with
      | "title" -> { acc with title = value }
      | "description" -> { acc with description = value }
      | "linkedin" -> { acc with linkedin = value }
      | "twitter" -> { acc with twitter = value }
      | "github" -> { acc with github = value }
      | "envelope" -> { acc with envelope = value }
      | "css" -> { acc with css = value.Split(" ") |> List.ofArray }
      | _ -> acc
    ) initial


