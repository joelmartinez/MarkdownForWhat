namespace MarkdownForWhat.Model

open System

type MarkdownHtml() = 
    inherit MarkdownNode()

    let mutable _html : string = ""

    member this.OuterHtml    
        with get () = _html
        and set (value) = _html <- value
