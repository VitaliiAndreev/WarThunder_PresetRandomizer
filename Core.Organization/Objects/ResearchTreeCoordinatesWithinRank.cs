namespace Core.Organization.Objects
{
    /// <summary> A set of coordinates for a cell within a specific rank of a research tree. </summary>
    public class ResearchTreeCoordinatesWithinRank
    {
        #region Properties

        /// <summary> The column number. </summary>
        public int ColumnNumber { get; }

        /// <summary> The row number. </summary>
        public int RowNumber { get; }

        /// <summary> A 0-based index of a vehicle in its research tree folder. </summary>
        public int? FolderIndex { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new set of coordinates for a cell within a specific rank of a research tree. </summary>
        /// <param name="columnNumber"> The column number. </param>
        /// <param name="rowNumber"> The row number. </param>
        /// <param name="folderIndex"> A 0-based index of a vehicle in its research tree folder. </param>
        public ResearchTreeCoordinatesWithinRank(int columnNumber, int rowNumber, int? folderIndex)
        {
            ColumnNumber = columnNumber;
            RowNumber = rowNumber;
            FolderIndex = folderIndex;
        }

        #endregion Constructors
        #region Methods: Comparison

        /// <summary> Determines whether the specified object is equal to the current object. </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ResearchTreeCoordinatesWithinRank anotherInstance))
                return false;

            return ColumnNumber == anotherInstance.ColumnNumber
                && RowNumber == anotherInstance.RowNumber
                && FolderIndex == anotherInstance.FolderIndex
            ;
        }

        /// <summary> Serves as the default hash function. </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 101;

                hash = hash * 103 + ColumnNumber.GetHashCode();
                hash = hash * 107 + RowNumber.GetHashCode();
                hash = hash * 109 + (FolderIndex.HasValue ? FolderIndex.Value.GetHashCode() : -1.GetHashCode());

                return hash;
            }
        }

        #endregion Methods: Comparison
    }
}