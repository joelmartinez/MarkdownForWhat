namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat
open MarkdownForWhat.Model

[<TestFixture>]
type ConversionTest() = 
    let makeText (container:MarkdownContainer, value:string) =
        let t = new MarkdownText();
        t.value <- value
        container.add t |> ignore

    let makeP (container:MarkdownContainer, value:string) =
        let p = new MarkdownParagraph();
        makeText (p, value)
        container.add p |> ignore

    let makeStrong (container:MarkdownContainer, value:string) =
        let b = new MarkdownStrong();
        makeText (b, value)
        container.add b |> ignore

    let makeHtml (container:MarkdownContainer, value:string) =
        let h = new MarkdownHtml();
        h.OuterHtml <- value
        container.children <- List.append container.children [h] 

    let makeA (container:MarkdownContainer, href:string, value:string) =
        let h = new MarkdownLink();
        h.href <- href
        let t = new MarkdownText()
        t.value <- value
        container.add (h.add t) |> ignore


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
    member x.html_span_should_remain() =
        let htmlString =  @"this <b>is</b> <span class=""someclass"">some</span> text"
        let c = new MarkdownContainer()
        makeText (c, "this ")
        makeStrong (c, "is")
        makeText (c, " ")
        makeHtml (c, @"<span class=""someclass"">some</span>");
        makeText (c, " text")

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(@"this **is** <span class=""someclass"">some</span> text", md)

    [<Test>]
    member x.link() =
        let c = new MarkdownContainer()
        makeA (c, "http://for.what", "markdown")
        let expected = @"[markdown](http://for.what)"

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(expected, md)
