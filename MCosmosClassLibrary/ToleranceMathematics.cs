using System.Collections.Generic;
using System.Linq;

namespace MCosmosClassLibrary
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



        public static IEnumerable<DiscGradeInfo> IncludingGradeAandBonly(this IEnumerable<DiscGradeInfo> discList)
        {
            return discList.Where(disc =>
            {
                var grade = disc.OverallGrade;
                return grade == DiscGrade.GradeA || grade == DiscGrade.GradeB;
            });
        }

        /// <summary>
        /// Calculate and return all grading information for the given disc.
        /// This includes a complete breakdown of what grade is achieved by each field.
        /// </summary>
        public static DiscGradeInfo DiscGradeInfo(this DiscInfo discInfo)
        {
            return new DiscGradeInfo 
            {
                Disc = discInfo,
                OverallGrade = OverallGradeFrom(discInfo)
            };
        }

        /// <summary>
        /// Get the overall grade for the disc from the detail records.
        /// </summary>
        public static DiscGrade OverallGradeFrom(DiscInfo discInfo)
        {
            return discInfo.Flatness.DatumF.Grade
                .Floor(discInfo.Flatness.DatumE.Grade)
                .Floor(discInfo.Flatness.DatumD.Grade)
                .Floor(discInfo.Flatness.DatumG.Grade)
                .Floor(discInfo.Parallel.DatumELH1.Grade)
                .Floor(discInfo.Parallel.DatumERH1.Grade)
                .Floor(discInfo.Parallel.DatumGFR1.Grade)
                .Floor(discInfo.Parallel.DatumGBK1.Grade)
                .Floor(discInfo.Distances.EtoFLeft1.Grade)
                .Floor(discInfo.Distances.EtoFRight1.Grade)
                .Floor(discInfo.Distances.EtoFLeft2.Grade)
                .Floor(discInfo.Distances.EtoFRight2.Grade)
                .Floor(discInfo.Distances.GtoDFront1.Grade)
                .Floor(discInfo.Distances.GtoDBack1.Grade)
                .Floor(discInfo.Distances.GtoDFront2.Grade)
                .Floor(discInfo.Distances.GtoDBack2.Grade);
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
