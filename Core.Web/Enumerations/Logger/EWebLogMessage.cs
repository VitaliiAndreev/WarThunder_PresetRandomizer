using Core.Enumerations;
using Core.Enumerations.Logger;

namespace Core.Web.Enumerations.Logger
{
    public class EWebLogMessage : ECoreLogMessage
    {
        public static readonly string FailedToRead = $"{_Failed} {_to} {_read} \"{{0}}\".";
    }
}