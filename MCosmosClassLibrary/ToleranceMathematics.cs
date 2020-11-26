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
            var flatness = FlatnessGrades(discInfo.Flatness);
            var parallel = ParallelGrades(discInfo.Parallel);
            var distances = DistanceGrades(discInfo.Distances);

            return new DiscGradeInfo 
            {
                Disc = discInfo,
                FlatnessGrades = flatness,
                ParallelGrades = parallel,
                DistanceGrades = distances,
                OverallGrade   = OverallGradeFrom(flatness, parallel, distances)
            };
        }

        /// <summary>
        /// Get the overall grade for the disc from the detail records.
        /// </summary>
        public static DiscGrade OverallGradeFrom(FlatnessGrades flatness, ParallelGrades parallel, DistanceGrades distances)
        {
            return flatness.DatumF
                .Floor(flatness.DatumE)
                .Floor(flatness.DatumD)
                .Floor(flatness.DatumG)
                .Floor(parallel.DatumELH1)
                .Floor(parallel.DatumERH1)
                .Floor(parallel.DatumGFR1)
                .Floor(parallel.DatumGBK1)
                .Floor(distances.EtoFLeft1)
                .Floor(distances.EtoFRight1)
                .Floor(distances.EtoFLeft2)
                .Floor(distances.EtoFRight2)
                .Floor(distances.GtoDFront1)
                .Floor(distances.GtoDBack1)
                .Floor(distances.GtoDFront2)
                .Floor(distances.GtoDBack2);
        }

        public static DiscGrade Floor(this DiscGrade a, DiscGrade b)
        {
            return (a < b) ? b : a;
        }



        /// <summary>
        /// Get all flatness grades for fields of given record.
        /// </summary>
        public static FlatnessGrades FlatnessGrades(this FlatnessMeasurements measurements)
        {
            return new FlatnessGrades 
            {
                DatumF = FlatParaGradeFor(measurements.DatumF),
                DatumE = FlatParaGradeFor(measurements.DatumE),
                DatumD = FlatParaGradeFor(measurements.DatumD),
                DatumG = FlatParaGradeFor(measurements.DatumG),
            };
        }

        /// <summary>
        /// Get all parallel grades for fields of given record.
        /// </summary>
        public static ParallelGrades ParallelGrades(this ParallelMeasurements measurements)
        {
            return new ParallelGrades
            {
                DatumELH1 = FlatParaGradeFor(measurements.DatumELH1),
                DatumERH1 = FlatParaGradeFor(measurements.DatumERH1),
                DatumGFR1 = FlatParaGradeFor(measurements.DatumGFR1),
                DatumGBK1 = FlatParaGradeFor(measurements.DatumGBK1),
            };
        }

        /// <summary>
        /// Get all distance grades for fields of given record.
        /// </summary>
        public static DistanceGrades DistanceGrades(this DistanceMeasurements measurements)
        {
            return new DistanceGrades
            {
                EtoFLeft1  = DistanceGradeFor(measurements.EtoFLeft1),
                EtoFRight1 = DistanceGradeFor(measurements.EtoFRight1),
                EtoFLeft2  = DistanceGradeFor(measurements.EtoFLeft2),
                EtoFRight2 = DistanceGradeFor(measurements.EtoFRight2),
                GtoDFront1 = DistanceGradeFor(measurements.GtoDFront1),
                GtoDBack1  = DistanceGradeFor(measurements.GtoDBack1),
                GtoDFront2 = DistanceGradeFor(measurements.GtoDFront2),
                GtoDBack2  = DistanceGradeFor(measurements.GtoDBack2),
            };
        }



        /// <summary>
        /// Get parallel and flatness grading for respective reading.
        /// </summary>
        private static DiscGrade FlatParaGradeFor(double n)
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
        private static DiscGrade DistanceGradeFor(double n)
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
