namespace Core.Json.Enumerations.Logger
{
    public class EJsonLogMessage
    {
        #region Extension Methods

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: JSON token. </para>
        /// </summary>
        public static readonly string JsonTokenDoesntContainJasonValue = $"JSON token (\"{{0}}\") doesn't contain JSON value.";
        public static readonly string JsonValueCouldNotBeConverted = $"JSON value (\"{{0}}\") can't be converted to \"{{1}}\".";

        #endregion Extension Methods
        #region JSON Helper
        
        private static readonly string _TryingToDeserializeJsonStringInto = $"";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public static readonly string TryingToDeserializeJsonStringIntoObject = $"Trying to deserialise a JSON string of {{0}} characters into an instance of \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public static readonly string TryingToDeserializeJsonStringIntoCollection = $"Trying to deserialise a JSON string of {{0}} characters into a collection of \"{{0}}\" instances.";
        public static readonly string InstanceDeserialized = $"Instance deserialised.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: instance count. </para>
        /// </summary>
        public static readonly string InstancesDeserialized = $"{{0}} instances deserialised.";

        public static readonly string JsonStringEmpty = $"The JSON string is empty.";
        public static readonly string ErrorDeserializingJsonText = $"Error deserialising JSON text.";
        public static readonly string MustBeJsonContainerToStandardize = $"Must be a JSON container to be standardised.";

        #endregion JSON Helper
    }
}