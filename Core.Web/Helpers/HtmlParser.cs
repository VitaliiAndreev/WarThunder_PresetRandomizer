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

        private static readonly int _retryDelay = EInteger.Time.MillisecondsInSecond * EInteger.Number.Five;

        #endregion Constants
        #region Constructors

        public HtmlParser(params IConfiguredLogger[] loggers)
            : base(EWebLogCategory.HtmlParser, loggers)
        {
            LogDebug(ECoreLogMessage.Created.Format(EWebLogCategory.HtmlParser));
        }

        #endregion Constructors

        public HtmlNode GetHtmlDocumentNode(string url, int retryAttempts = EInteger.Number.One, Exception internalException = null)
        {
            if (retryAttempts.IsZero())
                throw new TimeoutException(EWebLogMessage.FailedToRead.Format(url), internalException);

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