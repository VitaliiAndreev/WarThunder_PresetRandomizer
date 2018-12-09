using System;

namespace Core.Helpers.Logger.Interfaces
{
    /// <summary> Handles formatting of exception information into a form adequate for logging. </summary>
    public interface IExceptionFormatter
    {
        /// <summary> Re-formats an exception into a more readable form. </summary>
        /// <param name="exception"> An exception to re-format.</param>
        /// <returns></returns>
        string GetFormattedException(Exception exception);
    }
}
