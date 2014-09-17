namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat
open MarkdownForWhat.Model

[<TestFixture>]
type Test() = 
    let makeP (container:MarkdownContainer, value:string) =
        let p = new MarkdownParagraph();
        let t = new MarkdownText();
        t.value <- value
        p.children <- List.append p.children [t]
        container.children <- List.append container.children [p]

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


        (*
    [<Test>]
    member x.em() =
        let s = new HtmlToMarkdown()
        let md = s.Convert "<em>what</em>"
        Assert.AreEqual("*what*", md)
        *)
