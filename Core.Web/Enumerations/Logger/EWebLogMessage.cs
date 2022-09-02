namespace Core.Web.Enumerations.Logger
{
    public class EWebLogMessage : CoreLogMessage
    {
        public static readonly string FailedToRead = $"{_Failed} {_to} {_read} \"{{0}}\".";
    }
}