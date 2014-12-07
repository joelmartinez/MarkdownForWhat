namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat
open MarkdownForWhat.Model

[<TestFixture>]
type ConversionTest() = 
    let makeText (value:string) =
        let t = new MarkdownText();
        t.value <- value
        t

    let makeP (value:string) =
        let p = new MarkdownParagraph();
        makeText value |> p.add 
        p

    let makeStrong (value:string) =
        let b = new MarkdownStrong();
        makeText value |> b.add
        b

    let makeHtml (value:string) =
        let h = new MarkdownHtml();
        h.OuterHtml <- value
        h

    let makeA (href:string) (value:string) =
        let h = new MarkdownLink();
        h.href <- href
        makeText value |> h.add
        h



    [<Test>]
    member x.paragraph() =
        let c = new MarkdownContainer();
        makeP "what" |> c.add

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual("what", md)

    [<Test>]
    member x.paragraph_double() =
        let c = new MarkdownContainer();
        makeP "for" |> c.add
        makeP "what" |> c.add

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual("for\n\nwhat", md)

    [<Test>]
    member x.strong() =
        let c = new MarkdownContainer();
        makeStrong "what" |> c.add

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual("**what**", md)


       
    [<Test>]
    member x.html() =
        let htmlString = "<somehtml>derp</somehtml>"

        let c = new MarkdownContainer();
        makeHtml htmlString |> c.add

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(htmlString, md)

    [<Test>]
    member x.html_span_should_remain() =
        let htmlString =  @"this <b>is</b> <span class=""someclass"">some</span> text"
        let c = new MarkdownContainer()
        makeText "this " |> c.add
        makeStrong "is" |> c.add
        makeText " " |> c.add
        makeHtml @"<span class=""someclass"">some</span>" |> c.add
        makeText " text" |> c.add

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(@"this **is** <span class=""someclass"">some</span> text", md)

    [<Test>]
    member x.link() =
        let c = new MarkdownContainer()
        makeA "http://for.what" "markdown" |> c.add
        let expected = @"[markdown](http://for.what)"

        let s = new HtmlToMarkdown()
        let md = s.Convert c
        Assert.AreEqual(expected, md)
