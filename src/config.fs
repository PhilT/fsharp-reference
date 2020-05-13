namespace WebsiteBuilder

open System.IO

type Links = {
  linkedin: string
  twitter: string
  github: string
  envelope: string
}

type Config = {
  title: string
  description: string
  links: Links
  sections: list<string>
}

module Config =

  let load site =
    let json = File.ReadAllText(__SOURCE_DIRECTORY__ + "/../" + site + "/config.json")
    Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(json)

