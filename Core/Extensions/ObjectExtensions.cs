namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="object"/> class. </summary>
    public static class ObjectExtensions
    {
        /// <summary> Gets the object's type name as a string. If the object is of generic class, type arguments are appended. </summary>
        /// <param name="source"> The source object. </param>
        /// <returns></returns>
        public static string GetTypeString(this object source) =>
            source.GetType().ToStringLikeCode();

        #region Fluency

        #region Type Casting

        /// <summary> Fluently casts the object into a given type. </summary>
        /// <param name="source"> The source object. </param>
        /// <returns></returns>
        public static T CastTo<T>(this object source) =>
            (T)source;

        #endregion Type Casting

        #endregion Fluency
    }
}