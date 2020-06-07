using Core.Enumerations;
using Core.Enumerations.Logger;

namespace Core.Web.Enumerations.Logger
{
    public class EWebLogCategory : ECoreLogCategory
    {
        public static readonly string HtmlParser = $"{EWord.Html} {EWord.Parser}";
    }
}