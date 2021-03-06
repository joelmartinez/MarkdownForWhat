﻿namespace MarkdownForWhat.Test

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

    member x.strongTest (element:string) = 
        let s = new MarkdownParser()

        let x = s.Parse (String.Format("<{0}>what</{0}>", element))

        let xt:MarkdownContainer = x :?> MarkdownContainer
        let b = xt.children.Head :?> MarkdownStrong
        let text = b.children.Head :?> MarkdownText
        Assert.AreEqual("what", text.value)

    [<Test>]
    member x.strong_strong() =
        x.strongTest "strong"

    [<Test>]
    member x.strong_bold() =
        x.strongTest "b"

    [<Test>]
    member x.unknown_html_block_should_remain() = 
        let s = new MarkdownParser()

        let x = s.Parse @"<somehtml>what</somehtml>"

        let xt:MarkdownContainer = x :?> MarkdownContainer
        let h = xt.children.Head :?> MarkdownHtml
        Assert.AreEqual("<somehtml>what</somehtml>", h.OuterHtml)

    
    [<Test>]
    member x.spans_in_text_should_remain() = 
        let s = new MarkdownParser()

        let x = s.Parse @"this <b>is</b> <span class=""someclass"">some</span> text"

        let p:MarkdownContainer = x :?> MarkdownContainer

        let thisText = p.children.Head :?> MarkdownText
        let isStrong = p.children.Tail.Head :?> MarkdownStrong
        let aSpace   = p.children.Tail.Tail.Head :?> MarkdownText
        let someHtml = p.children.Tail.Tail.Tail.Head :?> MarkdownHtml
        let lastText = p.children.Tail.Tail.Tail.Tail.Head :?> MarkdownText

        Assert.AreEqual(@"this ", thisText.value)
        Assert.AreEqual("is", (isStrong.children.Head :?> MarkdownText).value)
        Assert.AreEqual(@"<span class=""someclass"">some</span>", someHtml.OuterHtml)
        Assert.AreEqual(" text", lastText.value);

    [<Test>]
    member x.link() =
        let s = new MarkdownParser()

        let x = s.Parse @"<a href=""http://for.com"">what</a>"

        let xt:MarkdownContainer = x :?> MarkdownContainer
        let l = xt.children.Head :?> MarkdownLink
        let t = l.children.Head :?> MarkdownText
        Assert.AreEqual("http://for.com", l.href)
        Assert.AreEqual("what", t.value)
