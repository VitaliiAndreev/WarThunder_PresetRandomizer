using Core.Enumerations.Logger;

namespace Core.Json.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Json"/>" assembly. </summary>
    public class EJsonLogMessage : CoreLogMessage
    {
        #region Extension Methods

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: JSON token. </para>
        /// </summary>
        public static readonly string JsonTokenDoesntContainJasonValue = $"{_JSON} {_token} (\"{{0}}\") {_doesnt} {_contain} {_JSON} {_value}.";
        public static readonly string JsonValueCouldNotBeConverted = $"{_JSON} {_value} (\"{{0}}\") {_couldnt} {_be} {_converted} {_to} \"{{1}}\".";

        #endregion Extension Methods
        #region JSON Helper

        private static readonly string _jsonString = $"{_JSON} {_string}";
        private static readonly string _TryingToDeserializeJsonStringInto = $"{_TryingTo} {_deserialise} {_a} {_jsonString} {_of} {{0}} {_characters} {_into}";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public static readonly string TryingToDeserializeJsonStringIntoObject = $"{_TryingToDeserializeJsonStringInto} {_an} {_instance} {_of} \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public static readonly string TryingToDeserializeJsonStringIntoCollection = $"{_TryingToDeserializeJsonStringInto} {_a} {_collection} {_of} \"{{0}}\" {_instances}.";
        public static readonly string InstanceDeserialized = $"{_Instance} {_deserialised}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: instance count. </para>
        /// </summary>
        public static readonly string InstancesDeserialized = $"{{0}} {_instances} {_deserialised}.";

        public static readonly string JsonStringEmpty = $"{_The} {_jsonString} {_is} {_empty}.";
        public static readonly string ErrorDeserializingJsonText = $"{_Error} {_deserialising} {_JSON} {_text}.";
        public static readonly string MustBeJsonContainerToStandardize = $"{_Must} {_be} {_a} {_JSON} {_container} {_to} {_be} {_standardised}.";

        #endregion JSON Helper
    }
}