using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Core.Helpers.Logger
{
    /// <summary> Handles formatting of exception information into a form adequate for logging. </summary>
    public class ExceptionFormatter : IExceptionFormatter
    {
        #region Constants

        /// <summary> A format string supposed to contain an extension type and a message. </summary>
        private const string _formattedExceptionMessage = "\t[{0}]\n\t\t{1}";

        #endregion Constants
        #region Methods

        /// <summary> Re-formats an exception into a more readable form. </summary>
        /// <param name="exception"> An exception to re-format.</param>
        /// <returns></returns>
        public string GetFormattedException(Exception exception)
        {
            if (exception == null)
                return ECoreLogMessage.ExceptionIsNull;

            var lines = new List<string>()
            {
                $"{ECharacter.NewLine}{_formattedExceptionMessage.FormatFluently(exception.GetType(), exception.Message.Flatten())}",
                GetFormattedStackTrace(new StackTrace(exception, true)),
                GetFormattedInnerExceptions(exception),
            };

            return lines
                .Where(line => line.Any())
                .StringJoin(ECharacter.NewLine)
            ;
        }

        /// <summary> Formats inner exceptions of a specified exception into a more readable form. </summary>
        /// <param name="exception"> An exception whose inner exceptions to format. </param>
        /// <returns></returns>
        private string GetFormattedInnerExceptions(Exception exception)
        {
            var lines = new List<string>();
            var innerException = exception.InnerException;

            while (innerException != null)
            {
                lines.Add(_formattedExceptionMessage.FormatFluently(innerException.GetType(), innerException.Message.Flatten()));
                lines.Add(GetFormattedStackTrace(new StackTrace(innerException, true)));

                innerException = innerException.InnerException;
            }

            return lines
                .Where(line => line.Any())
                .StringJoin(ECharacter.NewLine)
            ;
        }

        /// <summary> Formats a stack trace into a more readable form. </summary>
        /// <param name="stackTrace"> A stack trace to format. </param>
        /// <returns></returns>
        private string GetFormattedStackTrace(StackTrace stackTrace)
        {
            var lines = new List<string>();
            var stackFrames = stackTrace?.GetFrames();

            if (stackFrames == null)
                return string.Empty;

            foreach (var stackFrame in stackFrames)
            {
                var stringBuilder = new StringBuilder();
                var fileName = stackFrame.GetFileName();
                var method = stackFrame.GetMethod();
                var lineNumber = stackFrame.GetFileLineNumber();

                stringBuilder.Append($"\t\t\tat ");

                if (fileName != null && fileName.Any())
                    stringBuilder.Append($"{fileName} : ");

                stringBuilder.Append(method);

                if (lineNumber > 0)
                    stringBuilder.Append($" : Line {lineNumber}");

                lines.Add(stringBuilder.ToString());
            }

            return lines.StringJoin(ECharacter.NewLine);
        }

        #endregion Methods
    }
}