namespace Core.Enumerations
{
    /// <summary>
    /// Constants storing usable English words.
    /// <para> Suffix "_L" means "lower case". </para>
    /// </summary>
    public class EWord : EVocabulary
    {
        #region A

        public static string About => _About;
        public static string All => _All;
        public static string Application => _Application;

        #endregion A
        #region B

        public static string Battle => _Battle;
        public static string BattleRating => $"{_Battle}{ECharacter.Space}{_Rating}";
        public static string Britain => _Britain;

        #endregion B
        #region C

        public static string Client => _Client;
        public static string Converter => _Converter;
        public static string CSV => _CSV;

        #endregion C
        #region D

        public static string Data => _Data;
        public static string Deserializer => _Deserialiser;
        public static string Dont => _Dont;

        #endregion D
        #region E

        public static string Error => _Error;

        #endregion E
        #region F

        public static string Factory => _Factory;
        public static string File => _File;

        #endregion F
        #region G

        public const string Gui = _GUI;

        #endregion G
        #region H

        public static string Helper => _Helper;
        public static string Helpers => _Helpers;
        public static string Html => _HTML;

        #endregion H
        #region I

        public static string Integration => _Integration;

        #endregion I
        #region J

        #endregion J
        #region K

        public static string Key => _Key;

        #endregion K
        #region L

        public static string Line => _Line;
        public static string Loading => _Loading;
        public static string Localisation => _Localisation;
        public static string Logger => _Logger;
        public static string Loggers => _Loggers;

        #endregion L
        #region M

        public static string Main => _Main;
        public static string Manager => _Manager;

        #endregion M
        #region N

        public static string None => _None;
        public static string NULL => _NULL;

        #endregion N
        #region O

        #endregion O
        #region P

        public static string Pack => _Pack;
        public static string Parser => _Parser;
        public static string PresetGenerator => _Preset + ESeparator.Space + _Generator;
        public static string Proxy => _Proxy;

        #endregion P
        #region Q

        #endregion Q
        #region R

        public static string Randomiser => _Randomiser;
        public static string Rank => _Rank;
        public static string Rating => _Rating;
        public static string Reader => _Reader;
        public static string Repository => _Repository;
        public static string Reserve => _Reserve;

        #endregion R
        #region S

        public static string Schema => _Schema;
        public static string Session => _Session;
        public static string Settings => _Settings;
        public static string Starter => _Starter;
        public static string Style => _Style;

        #endregion S
        #region T

        public static string Tests => _Tests;

        #endregion T
        #region U

        public static string Unit => _Unit;
        public static string Unpacker => _Unpacker;
        public static string Untagged => _Untagged;

        #endregion U
        #region V

        public static string Value => _Value;
        public static string VehicleSelector => _Vehicle + ESeparator.Space + _Selector;

        #endregion V
        #region W

        public static string Window => _Window;
        public static string WPF => _WPF;

        #endregion W
        #region X

        #endregion X
        #region Y

        #endregion Y
        #region Z

        #endregion Z
    }
}