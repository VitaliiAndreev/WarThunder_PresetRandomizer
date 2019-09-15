using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Localization.Interfaces
{
    /// <summary> A localization set. </summary>
    public interface ILocalization : IPersistentObjectWithIdAndGaijinId
    {
        public string English { get; }
        public string French { get; }
        public string Italian { get; }
        public string German { get; }
        public string Spanish { get; }
        public string Russian { get; }
        public string Polish { get; }
        public string Czech { get; }
        public string Turkish { get; }
        public string Chinese { get; }
        public string Japanese { get; }
        public string Portuguese { get; }
        public string Vietnamese { get; }
        public string Ukrainian { get; }
        public string Serbian { get; }
        public string Hungarian { get; }
        public string Korean { get; }
        public string Belarusian { get; }
        public string Romanian { get; }
        public string TChinese { get; }
        public string HChinese { get; }
    }
}
