namespace MarkdownForWhat

open System
open System.Collections.Generic

type MarkdownContainer() = 
    inherit MarkdownNode()

    let Children : MarkdownNode list = []

   