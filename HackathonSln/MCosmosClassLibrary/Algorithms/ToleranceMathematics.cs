using System.Collections.Generic;
using System.Linq;
using MCosmosClassLibrary.Algorithms;
using MCosmosClassLibrary.Models;

namespace MCosmosClassLibrary.Algorithms
{
    public static class ToleranceMathematics
    {
        public static IEnumerable<DiscInfo> IncludingGradeAandBonly(this IEnumerable<DiscInfo> discList)
        {
            return discList.Where(disc =>
            {
                var grade = disc.OverallGrade;
                return grade == DiscGrade.GradeA || grade == DiscGrade.GradeB;
            });
        }

        /// <summary>
        /// Get the overall grade for the disc from the detail records.
        /// </summary>
        public static DiscGrade OverallGradeFrom(
            FlatnessMeasurements flatness, 
            ParallelMeasurements parallel, 
            DistanceMeasurements distances)
        {
            return flatness.DatumF.Grade
                .Floor(flatness.DatumE.Grade)
                .Floor(flatness.DatumD.Grade)
                .Floor(flatness.DatumG.Grade)
                .Floor(parallel.DatumELH1.Grade)
                .Floor(parallel.DatumERH1.Grade)
                .Floor(parallel.DatumGFR1.Grade)
                .Floor(parallel.DatumGBK1.Grade)
                .Floor(distances.EtoFLeft1.Grade)
                .Floor(distances.EtoFRight1.Grade)
                .Floor(distances.EtoFLeft2.Grade)
                .Floor(distances.EtoFRight2.Grade)
                .Floor(distances.GtoDFront1.Grade)
                .Floor(distances.GtoDBack1.Grade)
                .Floor(distances.GtoDFront2.Grade)
                .Floor(distances.GtoDBack2.Grade);
        }

        public static DiscGrade Floor(this DiscGrade a, DiscGrade b)
        {
            return (a < b) ? b : a;
        }

        /// <summary>
        /// Get parallel and flatness grading for respective reading.
        /// </summary>
        public static DiscGrade FlatParaGradeFor(FlatParaGradeBoundaries bounds, double n)
        {
            if (n <= bounds.BoundaryGradeA)
            {
                return DiscGrade.GradeA;
            }
            else if (n <= bounds.BoundaryGradeB)
            {
                return DiscGrade.GradeB;
            }
            else
            {
                return DiscGrade.GradeC;
            }
        }

        /// <summary>
        /// Get distance grading for respective reading.
        /// </summary>
        public static DiscGrade DistanceGradeFor(DistanceGradeBoundaries bounds, double n)
        {
            var distTarget = bounds.DistTarget;

            bool liesInBand(double n, double target, double tolerance)
            {
                return (n >= target - tolerance) && (n <= target + tolerance);
            }

            if (liesInBand(n, distTarget, bounds.BoundaryEitherSideGradeA))
            {
                return DiscGrade.GradeA;
            }
            else if (liesInBand(n, distTarget, bounds.BoundaryEitherSideGradeB))
            {
                return DiscGrade.GradeB;
            }
            else
            {
                return DiscGrade.GradeC;
            }
        }
    }
}
