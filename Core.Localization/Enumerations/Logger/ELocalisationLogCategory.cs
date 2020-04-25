using Core.Enumerations;

namespace Core.Localization.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class ELocalisationLogCategory : EWord
    {
        public static string LocalisationManager = $"{Localisation}{ECharacter.Space}{Manager}";
    }
}