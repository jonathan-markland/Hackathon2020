using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MCosmosClassLibrary
{
    public enum DiscGrade
    {
        GradeA, GradeB, GradeC
    }

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

        public static DiscGrade Grade(this DiscInfo disc)
        {
            return DiscGradeFor(disc.Flatness, disc.Parallel, disc.Distances);
        }

        public static DiscGrade DiscGradeFor(
            FlatnessMeasurements flatness,
            ParallelMeasurements parallel,
            DistanceMeasurements distances)
        {
            if (flatness.Below(FlatParaToleranceA)
                && parallel.Below(FlatParaToleranceA)
                && distances.WithinBand(DistTarget, DistToleranceA))
            {
                return DiscGrade.GradeA;
            }
            else if (flatness.Below(FlatParaToleranceB)
                && parallel.Below(FlatParaToleranceB)
                && distances.WithinBand(DistTarget, DistToleranceB))
            {
                return DiscGrade.GradeB;
            }
            else
            {
                return DiscGrade.GradeC;
            }
        }

        public static IEnumerable<DiscInfo> IncludingGradeAandBonly(this IEnumerable<DiscInfo> discList)
        {
            return discList.Where(disc =>
            {
                var grade = disc.Grade();
                return grade == DiscGrade.GradeA || grade == DiscGrade.GradeB;
            });
        }

        public static bool Below(this FlatnessMeasurements flatness, double tolerance)
        {
            return
                flatness.DatumF <= tolerance &&
                flatness.DatumE <= tolerance &&
                flatness.DatumD <= tolerance &&
                flatness.DatumG <= tolerance;
        }

        public static bool Below(this ParallelMeasurements parallel, double tolerance)
        {
            return
                parallel.DatumELH1 <= tolerance &&
                parallel.DatumERH1 <= tolerance &&
                parallel.DatumGFR1 <= tolerance &&
                parallel.DatumGBK1 <= tolerance;
        }

        public static bool WithinBand(this DistanceMeasurements distance, double target, double tolerance)
        {
            bool liesInBand(double n) { return (n >= target - tolerance) && (n <= target + tolerance); }

            return
                liesInBand(distance.EtoFLeft1 ) &&
                liesInBand(distance.EtoFRight1) &&
                liesInBand(distance.EtoFLeft2 ) &&
                liesInBand(distance.EtoFRight2) &&
                liesInBand(distance.GtoDFront1) &&
                liesInBand(distance.GtoDBack1 ) &&
                liesInBand(distance.GtoDFront2) &&
                liesInBand(distance.GtoDBack2 );
        }
    }
}
