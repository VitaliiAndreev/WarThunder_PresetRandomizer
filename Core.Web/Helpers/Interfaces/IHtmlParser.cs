using Core.Enumerations;
using HtmlAgilityPack;
using System;

namespace Core.Web.Helpers.Interfaces
{
    public interface IHtmlParser
    {
        HtmlNode GetHtmlDocumentNode(string url, int retryAttempts = Integer.Number.One, Exception internalException = null);
    }
}