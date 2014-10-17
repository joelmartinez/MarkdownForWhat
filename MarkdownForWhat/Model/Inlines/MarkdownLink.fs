namespace MarkdownForWhat.Model

open System

type MarkdownLink() = 
    inherit MarkdownSpan()

    member val href:string = null with get, set