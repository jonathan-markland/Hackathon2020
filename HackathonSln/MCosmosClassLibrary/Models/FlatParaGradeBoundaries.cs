namespace MCosmosClassLibrary.Models
{
    /// <summary>
    /// The boundaries used for selecting between the grades for Flatness and Parallelism.
    /// </summary>
    public class FlatParaGradeBoundaries
    {
        /// <summary>
        /// This value or below is a grade A, else it's B or C.
        /// </summary>
        public double BoundaryGradeA { get; init; }

        /// <summary>
        /// This value or below is a grade B, else it's a grade C.
        /// </summary>
        public double BoundaryGradeB { get; init; }
    }
}
