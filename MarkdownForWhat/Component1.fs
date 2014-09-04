namespace MarkdownForWhat

open HtmlAgilityPack
open System.Linq
open System.Text

type HtmlToMarkdown() = 
    let builder = new StringBuilder()

    let walk (node:HtmlNode) = 
         builder.Append node.InnerText |> ignore

    member this.Convert (html:string) =
        let doc = new HtmlDocument()
        doc.LoadHtml(html)
        walk doc.DocumentNode
        builder.ToString()
    



