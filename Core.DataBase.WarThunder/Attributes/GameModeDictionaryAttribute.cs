using Core.DataBase.WarThunder.Enumerations;
using System;

namespace Core.DataBase.WarThunder.Attributes
{
    /// <summary> Designates a property for aggregation of values tied to <see cref="EGameMode"/> values. </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GameModeDictionaryAttribute : Attribute
    {
        /// <summary> The key by which to aggregate. </summary>
        public string Key { get; }

        /// <summary> Designates a property for aggregation of values tied to <see cref="EGameMode"/> values. </summary>
        /// <param name="key"> The key by which to aggregate. </param>
        public GameModeDictionaryAttribute(string key) => Key = key;
    }
}