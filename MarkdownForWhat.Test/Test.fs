namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat

[<TestFixture>]
type Test() = 

    [<Test>]
    member x.TestCase() =
        let s = new HtmlToMarkdown()
        let md = s.Convert "<p>what</p>"
        Assert.AreEqual("what", md)

