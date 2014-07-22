namespace MarkdownForWhat

open HtmlAgilityPack
open System.Linq


type HtmlToMarkdown() = 
    
    let walk (node:HtmlNode) = 
        node.ChildNodes.ToArray()
         

    member this.Convert (html:string) =
        let doc = new HtmlDocument()
        doc.LoadHtml(html)
        //doc.DocumentNode.ChildNodes.ToArray()
        //()
    



