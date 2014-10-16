namespace MarkdownForWhat

open MarkdownForWhat.Model

open HtmlAgilityPack
open System
open System.Linq
open System.Text

type HtmlToMarkdown() = 
    let builder = new StringBuilder()


    let handleBlock (node:HtmlNode) =
        builder.Append node.InnerText |> ignore

    let handleElement (node:HtmlNode) =
        match node.Name with
        | "em" -> 
            builder.AppendFormat ("*{0}*", node.InnerText) |> ignore
        | _ -> 
            builder.Append node.OuterHtml |> ignore

    let rec walk (node:MarkdownNode) = 

        match node.GetType() with
        | x when x = typeof<MarkdownText> ->
            let text:MarkdownText = downcast node
            builder.Append text.value |> ignore
        | x when x = typeof<MarkdownStrong> ->
            let b:MarkdownStrong = downcast node
            builder.Append "**" |> ignore
            List.iter (fun n -> walk n) b.children
            builder.Append "**" |> ignore
        | x when x = typeof<MarkdownParagraph> ->
            let p:MarkdownParagraph = downcast node

            builder.Append Environment.NewLine |> ignore
            builder.Append Environment.NewLine |> ignore
            List.iter (fun n -> walk n) p.children
        | x when x = typeof<MarkdownHtml> ->
            let h:MarkdownHtml = downcast node
            builder.Append h.OuterHtml |> ignore
        | x when x = typeof<MarkdownContainer> ->
            let container:MarkdownContainer = downcast node
            List.iter (fun n -> walk n) container.children
        | _ ->
            failwith "unsure how to handle " + node.GetType().ToString() |> ignore


    member this.Convert (md:MarkdownNode) =
        walk md
        builder.ToString().Trim()

    

