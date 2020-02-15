using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    /// <summary> A custom exception to catch when the settings file is regenerated after migrating to a newer release of the WTPR. </summary>
    public class SettingsFileRegeneratedException : Exception
    {
        public SettingsFileRegeneratedException()
        {
        }

        public SettingsFileRegeneratedException(string message)
            : base(message)
        {
        }

        public SettingsFileRegeneratedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
