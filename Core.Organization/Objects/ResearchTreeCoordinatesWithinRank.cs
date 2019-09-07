namespace Core.Organization.Objects
{
    /// <summary> A set of coordinates for a cell within a specific rank of a research tree. </summary>
    public class ResearchTreeCoordinatesWithinRank
    {
        /// <summary> The column number. </summary>
        public int ColumnNumber { get; }
        /// <summary> The row number. </summary>
        public int RowNumber { get; }

        /// <summary> Creates a new set of coordinates for a cell within a specific rank of a research tree. </summary>
        /// <param name="columnNumber"> The column number. </param>
        /// <param name="rowNumber"> The row number. </param>
        public ResearchTreeCoordinatesWithinRank(int columnNumber, int rowNumber)
        {
            ColumnNumber = columnNumber;
            RowNumber = rowNumber;
        }
    }
}