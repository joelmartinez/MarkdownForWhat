namespace MarkdownForWhat

open System
open System.Linq
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
        let children = node.ChildNodes.ToArray() |> Array.toList

        let isBlock = List.exists (fun e -> e.Equals(node.Name)) blockElements

        match node.NodeType with
        | HtmlNodeType.Document -> 
            let container = new MarkdownContainer()

            container.children <- List.map (fun n -> walk n) children
            container :> MarkdownNode
        | HtmlNodeType.Element ->
            match isBlock with
            | true -> handleBlock node :> MarkdownNode
            | _ -> handleElement node :> MarkdownNode
        | HtmlNodeType.Text ->
            let text = new MarkdownText();
            text.value <- node.InnerText
            text :> MarkdownNode
        | _ -> new MarkdownNode()

    and handleBlock (node:HtmlNode) = 
        let p = new MarkdownParagraph()

        let inners = node.ChildNodes.ToArray() |> Array.toList
        p.children <- List.map (fun n -> walk n) inners
        p

    and handleElement (node:HtmlNode) =
        let name = node.Name.ToLower()
        let children = node.ChildNodes.ToArray() |> Array.toList

        match name with
        | n when n = "strong" || n = "b" -> 
            let strong = new MarkdownStrong()
            strong.children <- List.map (fun n -> walk n) children
            strong :> MarkdownNode
        | _ ->
            let text = new MarkdownText()
            text.value <- node.InnerText;

            text :> MarkdownNode

    member this.Parse (html:string) =
        let doc = new HtmlDocument()
        doc.LoadHtml(html)
        let node = walk doc.DocumentNode
        node

    