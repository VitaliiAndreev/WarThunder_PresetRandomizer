using Core.DataBase.WarThunder.Enumerations;
using System;

namespace Core.DataBase.WarThunder.Attributes
{
    /// <summary> Designates a property for aggregation into a dictionary of values tied to <see cref="EGameMode"/> values. </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PersistAsDictionaryItemAttribute : Attribute
    {
        /// <summary> The key by which to aggregate. </summary>
        public string Key { get; }

        /// <summary> The game mode to assign the value to. </summary>
        public EGameMode GameMode { get; }

        /// <summary> Designates a property for aggregation into a dictionary of values tied to <see cref="EGameMode"/> values. </summary>
        /// <param name="key"> The key by which to aggregate. </param>
        /// <param name="gameMode"> The game mode to assign the value to. </param>
        public PersistAsDictionaryItemAttribute(string key, EGameMode gameMode)
        {
            Key = key;
            GameMode = gameMode;
        }
    }
}