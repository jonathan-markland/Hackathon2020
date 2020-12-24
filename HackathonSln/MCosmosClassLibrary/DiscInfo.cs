﻿using System;

namespace MCosmosClassLibrary
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
        /// <summary>
        /// The serial number of the disc.
        /// </summary>
        public string SerialNo;

        /// <summary>
        /// The path to the file from which this data was loaded.
        /// </summary>
        public string OriginFilePath;
    }

    /// <summary>
    /// Flatness - Datums F, E, D & G (as common zones)
    /// Flatness is a matter of "pass or fail".
    /// </summary>
    public struct FlatnessMeasurements
    {
        public FlatnessMeasurements(
            MeasureAndGrade datumF,
            MeasureAndGrade datumE,
            MeasureAndGrade datumD,
            MeasureAndGrade datumG)
        {
            DatumF = datumF;
            DatumE = datumE;
            DatumD = datumD;
            DatumG = datumG;
        }

        public readonly MeasureAndGrade DatumF;
        public readonly MeasureAndGrade DatumE;
        public readonly MeasureAndGrade DatumD;
        public readonly MeasureAndGrade DatumG;
    }

    /// <summary>
    /// Parallelism - 4 opposed positions.
    /// Parallelism is a matter of "pass or fail".
    /// </summary>
    public struct ParallelMeasurements
    {
        public ParallelMeasurements(
            MeasureAndGrade datumELH1,
            MeasureAndGrade datumERH1,
            MeasureAndGrade datumGFR1,
            MeasureAndGrade datumGBK1)
        {
            DatumELH1 = datumELH1;
            DatumERH1 = datumERH1;
            DatumGFR1 = datumGFR1;
            DatumGBK1 = datumGBK1;
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

    public struct DistanceMeasurements
    {
        /// <summary>
        /// E to F at -1.5 LH
        /// </summary>
        public MeasureAndGrade EtoFLeft1;

        /// <summary>
        /// E to F at -1.5 RH
        /// </summary>
        public MeasureAndGrade EtoFRight1;

        /// <summary>
        /// E to F at -10.3 LH
        /// </summary>
        public MeasureAndGrade EtoFLeft2;

        /// <summary>
        /// E to F at -10.3 RH
        /// </summary>
        public MeasureAndGrade EtoFRight2;

        /// <summary>
        /// G to D at -1.5 FR
        /// </summary>
        public MeasureAndGrade GtoDFront1;

        /// <summary>
        /// G to D at -1.5 BK
        /// </summary>
        public MeasureAndGrade GtoDBack1;

        /// <summary>
        /// G to D at -10.3 FR
        /// </summary>
        public MeasureAndGrade GtoDFront2;

        /// <summary>
        /// G to D at -10.3 BK
        /// </summary>
        public MeasureAndGrade GtoDBack2;

    }
}
