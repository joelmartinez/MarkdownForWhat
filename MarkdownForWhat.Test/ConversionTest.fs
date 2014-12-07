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
        p.add t |> ignore
        container.add p

    let makeStrong (container:MarkdownContainer, value:string) =
        let b = new MarkdownStrong();
        let t = new MarkdownText();
        t.value <- value
        b.add t |> ignore
        container.add b

    let makeHtml (container:MarkdownContainer, value:string) =
        let h = new MarkdownHtml();
        h.OuterHtml <- value
        container.children <- List.append container.children [h] 
    let makeA (container:MarkdownContainer, href:string, value:string) =
        let h = new MarkdownLink();
        h.href <- href
        let t = new MarkdownText()
        t.value <- value
        //t |> h.add |> container.add
        container.add (h.add t)


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



    [<Test>]
    member x.html() =
        let htmlString = "<somehtml>derp</somehtml>"

        let c = new MarkdownContainer();
        makeHtml (c, htmlString)

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(htmlString, md)

    [<Test>]
    member x.link() =
        let c = new MarkdownContainer()
        makeA (c, "http://for.what", "markdown")
        let expected = @"[markdown](http://for.what)"

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(expected, md)
