using Core.Helpers.Interfaces;

namespace Core
{
    /// <summary> Possible statuses of the settings file attached to an <see cref="ISettingsManager"/>. </summary>
    public enum SettingsFileStatus
    {
        Pending,
        NotFoundAndGenerated,
        FoundAndUpToDate,
        FoundAndAutomaticallyUpdated,
        FoundAndNeedsManualUpdate,
    }
}