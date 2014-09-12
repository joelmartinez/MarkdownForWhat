namespace MarkdownForWhat.Test

open System
open NUnit.Framework
open MarkdownForWhat
open MarkdownForWhat.Model

[<TestFixture>]
type DOMTest() = 
    [<Test>]
    member x.paragraph() =
        let s = new MarkdownParser()

        let x:MarkdownText = s.Parse "<p>what</p>"
        Assert.AreEqual("what", x.value)

