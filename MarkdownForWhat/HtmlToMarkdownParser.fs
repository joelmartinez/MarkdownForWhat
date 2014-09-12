namespace MarkdownForWhat

open System
open HtmlAgilityPack
open MarkdownForWhat.Model

type MarkdownParser() = 
    let blockElements = ["table";
            "p";
            "div";
            "h1"; "h2"; "h3"; "h4"; "h5"; "h6";
            "ol"; "ul";
            "pre"; 
            "address";
            "blockquote";
            "center";
            "dl";
            "fieldset";
            "form";
            "menu";
            "body"]
    
    let rec walk (node:HtmlNode) = 
        let p = new MarkdownText()
        p.value <- node.InnerText
        p

    member this.Parse (html:string) =
        let doc = new HtmlDocument()
        doc.LoadHtml(html)
        let node = walk doc.DocumentNode
        node

    