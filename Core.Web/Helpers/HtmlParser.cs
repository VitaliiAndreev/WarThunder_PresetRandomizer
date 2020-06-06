using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Web.Enumerations.Logger;
using Core.Web.Helpers.Interfaces;
using HtmlAgilityPack;

namespace Core.Web.Helpers
{
    public class HtmlParser : LoggerFluency, IHtmlParser
    {
        #region Constructors

        public HtmlParser(params IConfiguredLogger[] loggers)
            : base(EWebLogCategory.HtmlParser, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(EWebLogCategory.HtmlParser));
        }

        #endregion Constructors

        public HtmlNode GetHtmlDocumentNode(string url) =>
            new HtmlWeb()
                .Load(url)
                .DocumentNode
            ;
    }
}