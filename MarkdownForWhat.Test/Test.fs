namespace MarkdownForWhat.Test
open System
open NUnit.Framework
open MarkdownForWhat

[<TestFixture>]
type Test() = 

    [<Test>]
    member x.TestCase() =
        let s = new HtmlToMarkdown()
        None
        //Assert.AreEqual("F#", s.Parse("F#"));

