using Core.Enumerations;
using HtmlAgilityPack;
using System;

namespace Core.Web.Helpers.Interfaces
{
    public interface IHtmlParser
    {
        HtmlNode GetHtmlDocumentNode(string url, int retryAttempts = EInteger.Number.One, Exception internalException = null);
    }
}