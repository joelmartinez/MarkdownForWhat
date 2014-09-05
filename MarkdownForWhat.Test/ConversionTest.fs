namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat

[<TestFixture>]
type Test() = 

    [<Test>]
    member x.paragraph() =
        let s = new HtmlToMarkdown()
        let md = s.Convert "<p>what</p>"
        Assert.AreEqual("what", md)

    [<Test>]
    member x.paragraph_double() =
        let s = new HtmlToMarkdown()
        let md = s.Convert "<p>for</p>\n<p>what</p>"
        Assert.AreEqual("for\n\nwhat", md)

    [<Test>]
    member x.paragraph_double_extrawhitespace() =
        let s = new HtmlToMarkdown()
        let md = s.Convert "<p>for</p>\n   \n  \n   \n<p>what</p>"
        Assert.AreEqual("for\n\nwhat", md)

    [<Test>]
    member x.em() =
        let s = new HtmlToMarkdown()
        let md = s.Convert "<em>what</em>"
        Assert.AreEqual("*what*", md)

