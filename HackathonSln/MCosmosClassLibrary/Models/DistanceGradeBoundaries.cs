namespace MCosmosClassLibrary.Models
{
    /// <summary>
    /// The target (preferred) distance, and the permitted deviances allowed for each grade (A or B or C).
    /// </summary>
    public class DistanceGradeBoundaries
    {
        /// <summary>
        /// The preferred diagonal distance.
        /// </summary>
        public double DistTarget { get; init; }

        /// <summary>
        /// Permitted deviance either side of the preferred diagonal distance, for a grade A.
        /// </summary>
        public double BoundaryEitherSideGradeA { get; init; }

        /// <summary>
        /// Permitted deviance either side of the preferred diagonal distance, for a grade B.  Anything worse than this is grade C.
        /// </summary>
        public double BoundaryEitherSideGradeB { get; init; }
    }
}
