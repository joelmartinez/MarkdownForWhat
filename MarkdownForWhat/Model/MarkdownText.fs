namespace MarkdownForWhat.Model

open System

type MarkdownText() = 
    inherit MarkdownNode()

    let mutable _value : string = "";

    member this.value
        with get () = _value
        and set (value) = _value <- value
