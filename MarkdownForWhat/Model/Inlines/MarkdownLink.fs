namespace MarkdownForWhat.Model

open System

type MarkdownLink() = 
    inherit MarkdownSpan()

    let mutable _href : string = "";

    member this.href
        with get () = _href
        and set (value) = _href <- value