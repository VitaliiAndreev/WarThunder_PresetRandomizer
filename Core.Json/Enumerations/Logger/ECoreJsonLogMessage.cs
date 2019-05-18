using Core.Helpers.Logger.Enumerations;

namespace Core.Json.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Json"/>" assembly. </summary>
    public class ECoreJsonLogMessage : ECoreLogMessage
    {
        #region JSON Helper

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public const string TryingToDeserializeJsonStringIntoObject = _TryingTo + _deserialize + _a + _JSON + _string + _of + _SPC_FMT + _characters + _into + _an + _instance + _of + _SPC_FMT_Q + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// <para> 2: object type. </para>
        /// </summary>
        public const string TryingToDeserializeJsonStringIntoCollection = _TryingTo + _deserialize + _a + _JSON + _string + _of + _SPC_FMT + _characters + _into + _a + _collection + _of + _SPC_FMT_Q + _instances + _FS;
        public const string DeserializedInstance = _An + _instance + _has_been + _successfully + _deserialized + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: instance count. </para>
        /// </summary>
        public const string DeserializedInstances = _FMT + _instances + _have + _been + _deserialized + _FS;

        public const string ErrorJsonStringEmpty = _The + _JSON + _string + _is + _empty + _FS;
        public const string ErrorDeserializingJsonText = _Error + _deserializing + _JSON + _text + _FS;
        public const string ErrorMustBeJsonContainerToStandardize = _Must + _be + _a + _JSON + _container + _to + _be + _standardized + _FS;

        #endregion JSON Helper
    }
}