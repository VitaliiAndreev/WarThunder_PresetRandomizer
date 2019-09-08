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
        public int? FolderIndex { get; internal set; }

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
    }
}