using System;
using AngleSharp.Parser.Html;

namespace xTab.Tools.Helpers
{
    public static class AngleSharpParser
    {
        public static string RemoveTags(string html)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(html);

            var result = document.Body.TextContent;

            return result;
        }
    }
}
