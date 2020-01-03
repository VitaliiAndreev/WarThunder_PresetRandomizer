namespace Core.Organization.Enumerations
{
    /// <summary> Methods of randomisation. </summary>
    public enum ERandomisation
    {
        /// <summary> Random selection is done in every category before selecting any vehicles that adhere to the criteria. </summary>
        CategoryBased,
        /// <summary> Filter criteria don't determine selecting but only what vehicles are available. Randomisation is done only over the set of available vehicles. </summary>
        VehicleBased,
    }
}