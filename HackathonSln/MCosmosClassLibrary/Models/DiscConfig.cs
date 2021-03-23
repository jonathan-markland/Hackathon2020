namespace MCosmosClassLibrary.Models
{
    public class DiscConfig
    {
        public FileHeadings FileHeadings { get; init; }
        public FlatParaGradeBoundaries FlatParaBounds { get; init; }
        public DistanceGradeBoundaries DistBounds { get; init; }

        public static readonly DiscConfig DiscConfigForTestFramework = new DiscConfig
        {
            FileHeadings = FileHeadings.FileHeadingsForTestFramework,

            FlatParaBounds = new FlatParaGradeBoundaries
            {
                BoundaryGradeA = 0.002,
                BoundaryGradeB = 0.0025
            },

            DistBounds = new DistanceGradeBoundaries
            {
                BoundaryEitherSideGradeA = 0.001,
                BoundaryEitherSideGradeB = 0.002,
                DistTarget = 28.020
            }
        };
    }
}
