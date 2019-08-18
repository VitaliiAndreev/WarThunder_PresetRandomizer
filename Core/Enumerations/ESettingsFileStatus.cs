using Core.Helpers.Interfaces;

namespace Core.Enumerations
{
    /// <summary> Possible statuses of the settings file attached to an <see cref="ISettingsManager"/>. </summary>
    public enum ESettingsFileStatus
    {
        Pending,
        NotFoundAndGenerated,
        FoundAndUpToDate,
        FoundAndAutomaticallyUpdated,
        FoundAndNeedsManualUpdate,
    }
}