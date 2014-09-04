namespace MarkdownForWhat

open HtmlAgilityPack
open System.Linq
open System.Text

type HtmlToMarkdown() = 
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

    let builder = new StringBuilder()


    let handleBlock (node:HtmlNode) =
        builder.Append node.InnerText |> ignore

    let handleElement (node:HtmlNode) =
        match node.Name with
        | "em" -> 
            builder.AppendFormat ("*{0}*", node.InnerText) |> ignore
        | _ -> 
            builder.Append node.OuterHtml |> ignore

    let rec walk (node:HtmlNode) = 
        let ns = node.ChildNodes.ToArray() |> Array.toList

        let isblock = List.exists (fun e -> e = node.Name) blockElements 

        match node.NodeType with
        | HtmlNodeType.Document -> 
            List.iter (fun n -> walk n) ns
        | HtmlNodeType.Element -> 
            match isblock with
            | true -> handleBlock node
            | _ -> handleElement node
        | HtmlNodeType.Text -> 
            builder.Append node.InnerText |> ignore
        | _ -> 
            builder.Append node.OuterHtml |> ignore


    member this.Convert (html:string) =
        let doc = new HtmlDocument()
        doc.LoadHtml(html)
        walk doc.DocumentNode
        builder.ToString()

    



