namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat
open MarkdownForWhat.Model

[<TestFixture>]
type ConversionTest() = 
    let makeP (container:MarkdownContainer, value:string) =
        let p = new MarkdownParagraph();
        let t = new MarkdownText();
        t.value <- value
        p.children <- List.append p.children [t]
        container.children <- List.append container.children [p]
    let makeStrong (container:MarkdownContainer, value:string) =
        let b = new MarkdownStrong();
        let t = new MarkdownText();
        t.value <- value
        b.children <- List.append b.children [t]
        container.children <- List.append container.children [b] 

    [<Test>]
    member x.paragraph() =
        let c = new MarkdownContainer();
        makeP (c, "what") |> ignore

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual("what", md)

    [<Test>]
    member x.paragraph_double() =
        let c = new MarkdownContainer();
        makeP (c, "for") |> ignore
        makeP (c, "what") |> ignore

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual("for\n\nwhat", md)


    [<Test>]
    member x.strong() =
        let c = new MarkdownContainer();
        makeStrong (c, "what")

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual("**what**", md)
