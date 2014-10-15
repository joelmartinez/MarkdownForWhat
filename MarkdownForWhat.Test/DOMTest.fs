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

    [<Test>]
    member x.paragraph_two() =
        let s = new MarkdownParser()

        let x = s.Parse "<p>for</p>\n<p>what</p>"

        let xt:MarkdownContainer = x :?> MarkdownContainer
        let p = xt.children.Head :?> MarkdownParagraph
        let text = p.children.Head :?> MarkdownText
        Assert.AreEqual("for", text.value)

        let p2 = xt.children |> List.reduce (fun _ x -> x)  :?> MarkdownParagraph
        let text2 = p2.children.Head :?> MarkdownText
        Assert.AreEqual("what", text2.value)

