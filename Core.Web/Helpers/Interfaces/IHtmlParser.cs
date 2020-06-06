using HtmlAgilityPack;

namespace Core.Web.Helpers.Interfaces
{
    public interface IHtmlParser
    {
        HtmlNode GetHtmlDocumentNode(string url);
    }
}