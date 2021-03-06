# MarkdownForWhat

This project aims to create an HTML to Markdown parser and converter. It is written with F&#35;.

## Usage

There are currently two steps, the parser, and then the conversion to markdown.

        let parser = new MarkdownParser()
        let dom = parser.Parse "<p>markdown</p>\n<p>for</p>\n<p>what</p>"
        
        let converter = new HtmlToMarkdown()
        let md = converter.Convert dom
        
this outputs the string:

    "markdown
    
    for
    
    what"

## Status
This is still a work-in-progress and is still heavily in development.

## Roadmap

1. [current] - Creating the parser, which creates a markdown object model from a source HTML string.
2. Next step would be to write the Markdown renderer that takes the DOM and generates Markdown.
3. Once that's created, the plan is to write a Markdown parser that conforms to the [CommonMark](http://commonmark.org/) spec that will parse a markdown string to generate the DOM, and then an HTML renderer.

Ultimately, this should provide a two-way conversion to and from markdown and HTML.