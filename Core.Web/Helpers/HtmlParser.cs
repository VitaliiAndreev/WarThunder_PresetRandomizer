using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Web.Enumerations.Logger;
using Core.Web.Helpers.Interfaces;
using HtmlAgilityPack;
using System;
using System.Threading;

namespace Core.Web.Helpers
{
    public class HtmlParser : LoggerFluency, IHtmlParser
    {
        #region Constants

        private const int _retryDelay = EInteger.Time.MillisecondsInSecond;

        #endregion Constants
        #region Constructors

        public HtmlParser(params IConfiguredLogger[] loggers)
            : base(EWebLogCategory.HtmlParser, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(EWebLogCategory.HtmlParser));
        }

        #endregion Constructors

        public HtmlNode GetHtmlDocumentNode(string url, int retryAttempts = EInteger.Number.One, Exception internalException = null)
        {
            if (retryAttempts.IsZero())
                throw new TimeoutException(EWebLogMessage.FailedToRead.FormatFluently(url), internalException);

            try
            {
                return new HtmlWeb()
                    .Load(url)
                    .DocumentNode
                ;
            }
            catch (Exception exception)
            {
                Thread.Sleep(_retryDelay);
                return GetHtmlDocumentNode(url, --retryAttempts, exception);
            }
        }
    }
}