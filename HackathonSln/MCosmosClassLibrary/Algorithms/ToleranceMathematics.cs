using System.Collections.Generic;
using System.Linq;
using MCosmosClassLibrary.Algorithms;
using MCosmosClassLibrary.Models;

namespace MCosmosClassLibrary.Algorithms
{
    public static class ToleranceMathematics
    {
        /// <summary>
        /// The maximum value permitted for flatness and parallelism for a GradeA disc.
        /// </summary>
        public static double FlatParaToleranceA = 0.002;

        /// <summary>
        /// The maximum value permitted for flatness and parallelism for a GradeB disc.
        /// </summary>
        public static double FlatParaToleranceB = 0.0025;

        /// <summary>
        /// For the distance measurements, the tolerance band is defined by
        /// an amount either side of this.
        /// </summary>
        public static double DistTarget = 28.020;
        public static double DistToleranceA = 0.001;
        public static double DistToleranceB = 0.002;



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
        public static DiscGrade FlatParaGradeFor(double n)
        {
            if (n <= FlatParaToleranceA)
            {
                return DiscGrade.GradeA;
            }
            else if (n <= FlatParaToleranceB)
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
        public static DiscGrade DistanceGradeFor(double n)
        {
            bool liesInBand(double n, double target, double tolerance)
            {
                return (n >= target - tolerance) && (n <= target + tolerance);
            }

            if (liesInBand(n, DistTarget, DistToleranceA))
            {
                return DiscGrade.GradeA;
            }
            else if (liesInBand(n, DistTarget, DistToleranceB))
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
