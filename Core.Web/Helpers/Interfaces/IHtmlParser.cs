using HtmlAgilityPack;
using System;

namespace Core.Web.Helpers.Interfaces
{
    public interface IHtmlParser
    {
        HtmlNode GetHtmlDocumentNode(string url, int retryAttempts = 1, Exception internalException = null);
    }
}