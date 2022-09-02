using Core.Enumerations;
using Core.Enumerations.Logger;

namespace Core.Web.Enumerations.Logger
{
    public class EWebLogCategory : CoreLogCategory
    {
        public static readonly string HtmlParser = $"{Word.Html} {Word.Parser}";
    }
}