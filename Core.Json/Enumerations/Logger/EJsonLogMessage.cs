using Core.Enumerations.Logger;

namespace Core.Json.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Json"/>" assembly. </summary>
    public class EJsonLogMessage : ECoreLogMessage
    {
        #region JSON Helper

        private static readonly string _jsonString = $"{_JSON} {_string}";
        private static readonly string _TryingToDeserializeJsonStringInto = $"{_TryingTo} {_deserialize} {_a} {_jsonString} {_of} {{0}} {_characters} {_into}";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public static string TryingToDeserializeJsonStringIntoObject = $"{_TryingToDeserializeJsonStringInto} {_an} {_instance} {_of} \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public static string TryingToDeserializeJsonStringIntoCollection = $"{_TryingToDeserializeJsonStringInto} {_a} {_collection} {_of} \"{{0}}\" {_instances}.";
        public static string InstanceDeserialized = $"{_Instance} {_deserialized}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: instance count. </para>
        /// </summary>
        public static string InstancesDeserialized = $"{{0}} {_instances} {_deserialized}.";

        public static string JsonStringEmpty = $"{_The} {_jsonString} {_is} {_empty}.";
        public static string ErrorDeserializingJsonText = $"{_Error} {_deserializing} {_JSON} {_text}.";
        public static string MustBeJsonContainerToStandardize = $"{_Must} {_be} {_a} {_JSON} {_container} {_to} {_be} {_standardized}.";

        #endregion JSON Helper
    }
}