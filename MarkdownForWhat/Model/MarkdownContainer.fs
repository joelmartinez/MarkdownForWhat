namespace MarkdownForWhat.Model

open System
open System.Collections.Generic

type MarkdownContainer() = 
    inherit MarkdownNode()

    let mutable _children : MarkdownNode list = []

    member this.children
        with get () = _children
        and set (value) = _children <- value

   