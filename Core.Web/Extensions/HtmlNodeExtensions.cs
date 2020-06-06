using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Core.Web.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static IEnumerable<HtmlNode> GetChildNodes(this HtmlNode sourceNode, string childNodeName)
        {
            return sourceNode
                .ChildNodes
                .OfType<HtmlNode>()
                .Where(node => node.Name == childNodeName)
            ;
        }

        public static string GetTrimmedInnerText(this HtmlNode sourceNode)
        {
            return sourceNode.InnerText.Trim();
        }
    }
}