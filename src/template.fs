module WebsiteBuilder.Template

open System.IO
open System.Text.RegularExpressions

let wrap (site: string) (html: string) =
  let templatePath = Path.Combine(site, "template.html")
  let template = File.ReadAllText(templatePath)
  Regex.Replace(template, "{{content}}", html)

let wrapArticle (site: string) (html: string) =
  $"<article id='full'>{html}</article>"
