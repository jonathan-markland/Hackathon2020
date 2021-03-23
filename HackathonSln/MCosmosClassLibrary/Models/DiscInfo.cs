using System;
using MCosmosClassLibrary.Algorithms;

namespace MCosmosClassLibrary.Models
{
    public enum DiscGrade
    {
        GradeA, GradeB, GradeC
    }

    public struct MeasureAndGrade
    {
        public MeasureAndGrade(double value, DiscGrade grade) { this.Value = value; this.Grade = grade; }
        public readonly double Value;
        public readonly DiscGrade Grade;
        public override string ToString()
        {
            return $"({Value},{Grade})";
        }
    }

    /// <summary>
    /// All information pertaining to a Quadrupole disc, as
    /// obtained from an MCOSMOS text file.
    /// </summary>
    public class DiscInfo
    {
        public DiscInfo(
            DiscMetadata metadata,
            FlatnessMeasurements flatness,
            ParallelMeasurements parallel,
            DistanceMeasurements distances)
        {
            Metadata = metadata;
            Flatness = flatness;
            Parallel = parallel;
            Distances = distances;
            OverallGrade = ToleranceMathematics.OverallGradeFrom(flatness, parallel, distances);
        }

        public readonly DiscMetadata Metadata;
        public readonly FlatnessMeasurements Flatness;
        public readonly ParallelMeasurements Parallel;
        public readonly DistanceMeasurements Distances;
        public readonly DiscGrade OverallGrade;
    }

    public struct DiscMetadata
    {
        public DiscMetadata(string serialNo, string originFilePath) { SerialNo = serialNo; OriginFilePath = originFilePath; }

        /// <summary>
        /// The serial number of the disc.
        /// </summary>
        public readonly string SerialNo;

        /// <summary>
        /// The path to the file from which this data was loaded.
        /// </summary>
        public readonly string OriginFilePath;
    }

    /// <summary>
    /// Strong type wrapper for a flatness measurement.
    /// </summary>
    public struct FlatnessMeasure
    {
        public FlatnessMeasure(double flatness) { Flatness = flatness; }
        public double Flatness { get; init; }
    }

    /// <summary>
    /// Flatness - Datums F, E, D & G (as common zones)
    /// Flatness is a matter of "pass or fail".
    /// </summary>
    public struct FlatnessMeasurements
    {
        public FlatnessMeasurements(
            FlatnessMeasure datumF,
            FlatnessMeasure datumE,
            FlatnessMeasure datumD,
            FlatnessMeasure datumG,
            FlatParaGradeBoundaries bounds)
        {
            MeasureAndGrade flatGraded(double n) 
            { 
                return new MeasureAndGrade(n, ToleranceMathematics.FlatParaGradeFor(bounds, n)); 
            }
            
            DatumF = flatGraded(datumF.Flatness);
            DatumE = flatGraded(datumE.Flatness);
            DatumD = flatGraded(datumD.Flatness);
            DatumG = flatGraded(datumG.Flatness);
        }

        public readonly MeasureAndGrade DatumF;
        public readonly MeasureAndGrade DatumE;
        public readonly MeasureAndGrade DatumD;
        public readonly MeasureAndGrade DatumG;
    }

    /// <summary>
    /// Strong type wrapper for a parallel measurement.
    /// </summary>
    public struct ParallelMeasure
    {
        public ParallelMeasure(double parallel) { Parallel = parallel; }
        public double Parallel { get; init; }
    }


    /// <summary>
    /// Parallelism - 4 opposed positions.
    /// Parallelism is a matter of "pass or fail".
    /// </summary>
    public struct ParallelMeasurements
    {
        public ParallelMeasurements(
            ParallelMeasure datumELH1,
            ParallelMeasure datumERH1,
            ParallelMeasure datumGFR1,
            ParallelMeasure datumGBK1,
            FlatParaGradeBoundaries bounds)
        {
            MeasureAndGrade paraGraded(double n) 
            { 
                return new MeasureAndGrade(n, ToleranceMathematics.FlatParaGradeFor(bounds, n)); 
            }

            DatumELH1 = paraGraded(datumELH1.Parallel);
            DatumERH1 = paraGraded(datumERH1.Parallel);
            DatumGFR1 = paraGraded(datumGFR1.Parallel);
            DatumGBK1 = paraGraded(datumGBK1.Parallel);
        }

        /// <summary>
        /// Datum E LH 1
        /// </summary>
        public readonly MeasureAndGrade DatumELH1;

        /// <summary>
        /// Datum E RH 1
        /// </summary>
        public readonly MeasureAndGrade DatumERH1;

        /// <summary>
        /// Datum G FR 1
        /// </summary>
        public readonly MeasureAndGrade DatumGFR1;

        /// <summary>
        /// Datum G BK 1
        /// </summary>
        public readonly MeasureAndGrade DatumGBK1;
    }

    /// <summary>
    /// Strong type wrapper for a distance measurement.
    /// </summary>
    public struct DistanceMeasure
    {
        public DistanceMeasure(double distance) { Distance = distance; }
        public double Distance  { get; init; }
    }

    public struct DistanceMeasurements
    {
        public DistanceMeasurements(
            DistanceMeasure etoFLeft1,
            DistanceMeasure etoFRight1,
            DistanceMeasure etoFLeft2,
            DistanceMeasure etoFRight2,
            DistanceMeasure gtoDFront1,
            DistanceMeasure gtoDBack1,
            DistanceMeasure gtoDFront2,
            DistanceMeasure gtoDBack2,
            DistanceGradeBoundaries bounds)
        {
            MeasureAndGrade distGraded(double n) 
            { 
                return new MeasureAndGrade(n, ToleranceMathematics.DistanceGradeFor(bounds, n)); 
            }

            EtoFLeft1  = distGraded(etoFLeft1 .Distance);
            EtoFRight1 = distGraded(etoFRight1.Distance);
            EtoFLeft2  = distGraded(etoFLeft2 .Distance);
            EtoFRight2 = distGraded(etoFRight2.Distance);
            GtoDFront1 = distGraded(gtoDFront1.Distance);
            GtoDBack1  = distGraded(gtoDBack1 .Distance);
            GtoDFront2 = distGraded(gtoDFront2.Distance);
            GtoDBack2  = distGraded(gtoDBack2 .Distance);
        }

        /// <summary>
        /// E to F at -1.5 LH
        /// </summary>
        public readonly MeasureAndGrade EtoFLeft1;

        /// <summary>
        /// E to F at -1.5 RH
        /// </summary>
        public readonly MeasureAndGrade EtoFRight1;

        /// <summary>
        /// E to F at -10.3 LH
        /// </summary>
        public readonly MeasureAndGrade EtoFLeft2;

        /// <summary>
        /// E to F at -10.3 RH
        /// </summary>
        public readonly MeasureAndGrade EtoFRight2;

        /// <summary>
        /// G to D at -1.5 FR
        /// </summary>
        public readonly MeasureAndGrade GtoDFront1;

        /// <summary>
        /// G to D at -1.5 BK
        /// </summary>
        public readonly MeasureAndGrade GtoDBack1;

        /// <summary>
        /// G to D at -10.3 FR
        /// </summary>
        public readonly MeasureAndGrade GtoDFront2;

        /// <summary>
        /// G to D at -10.3 BK
        /// </summary>
        public readonly MeasureAndGrade GtoDBack2;

    }
}
