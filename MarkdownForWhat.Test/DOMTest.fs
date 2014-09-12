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

        let x = s.Parse "<p>what</p>"

        let xt:MarkdownContainer = x :?> MarkdownContainer
        let p = xt.children.Head :?>MarkdownParagraph
        let text = p.children.Head :?>MarkdownText
        Assert.AreEqual("what", text.value)

