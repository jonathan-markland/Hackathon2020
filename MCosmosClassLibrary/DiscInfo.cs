using System;

namespace MCosmosClassLibrary
{
    /// <summary>
    /// All information pertaining to a Quadrupole disc, as
    /// obtained from an MCOSMOS text file.
    /// </summary>
    public class DiscInfo
    {
        public DiscMetadata Metadata;
        public FlatnessMeasurements Flatness;
        public ParallelMeasurements Parallel; 
        public DistanceMeasurements Distances;
    }

    public struct DiscMetadata
    {
        /// <summary>
        /// The serial number of the disc.
        /// </summary>
        public string SerialNo;
    }

    /// <summary>
    /// Flatness - Datums F, E, D & G (as common zones)
    /// Flatness is a matter of "pass or fail".
    /// </summary>
    public struct FlatnessMeasurements
    {
        public static double Tolerance = 0.002; // TODO: obsolete criteria - GradeA and GradeB instead

        public double DatumF;
        public double DatumE;
        public double DatumD;
        public double DatumG;

        /// <summary>
        /// Returns true/false according to whether all of the values are within the tolerance allowed.
        /// </summary>
        public bool AllWithinTolerance
        {
            get
            {
                return
                    this.DatumF < Tolerance &&
                    this.DatumE < Tolerance &&
                    this.DatumD < Tolerance &&
                    this.DatumG < Tolerance;
            }
        }
    }

    /// <summary>
    /// Parallelism - 4 opposed positions.
    /// Parallelism is a matter of "pass or fail".
    /// </summary>
    public struct ParallelMeasurements
    {
        public static double Tolerance = 0.002;// TODO: obsolete criteria - GradeA and GradeB instead

        /// <summary>
        /// Datum E LH 1
        /// </summary>
        public double DatumELH1;

        /// <summary>
        /// Datum E RH 1
        /// </summary>
        public double DatumERH1;

        /// <summary>
        /// Datum G FR 1
        /// </summary>
        public double DatumGFR1;

        /// <summary>
        /// Datum G BK 1
        /// </summary>
        public double DatumGBK1;

        /// <summary>
        /// Returns true/false according to whether all of the values are within the tolerance allowed.
        /// </summary>
        public bool AllWithinTolerance
        {
            get
            {
                return
                    this.DatumELH1 < Tolerance &&
                    this.DatumERH1 < Tolerance &&
                    this.DatumGFR1 < Tolerance &&
                    this.DatumGBK1 < Tolerance;
            }
        }
    }

    public struct DistanceMeasurements
    {
        /// <summary>
        /// E to F at -1.5 LH
        /// </summary>
        public double EtoFLeft1;

        /// <summary>
        /// E to F at -1.5 RH
        /// </summary>
        public double EtoFRight1;

        /// <summary>
        /// E to F at -10.3 LH
        /// </summary>
        public double EtoFLeft2;

        /// <summary>
        /// E to F at -10.3 RH
        /// </summary>
        public double EtoFRight2;

        /// <summary>
        /// G to D at -1.5 FR
        /// </summary>
        public double GtoDFront1;

        /// <summary>
        /// G to D at -1.5 BK
        /// </summary>
        public double GtoDBack1;

        /// <summary>
        /// G to D at -10.3 FR
        /// </summary>
        public double GtoDFront2;

        /// <summary>
        /// G to D at -10.3 BK
        /// </summary>
        public double GtoDBack2;

    }
}
