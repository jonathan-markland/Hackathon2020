using System;
using System.Collections.Generic;
using System.Text;

namespace MCosmosClassLibrary
{
    /// <summary>
    /// Parses and loads the disc information file.
    /// </summary>
    public class DiscFileLoader
    {
        private FileReader reader;
        private DiscInfo results;

        public static DiscInfo LoadFromFile(string sourceFilePath)
        {
            return new DiscFileLoader(sourceFilePath).results;
        }

        private DiscFileLoader(string sourceFile)
        {
            reader = new FileReader(sourceFile);
            results = Load();
        }

        private DiscInfo Load()
        {
            var metadata = LoadMetadata();
            var flatness = LoadFlatness();
            var parallelism = LoadParallelism();
            var diagonals = LoadDiagonals();
            ReadPostamble();
            
            return new DiscInfo {
                Metadata = metadata,
                Flatness = flatness,
                Parallel = parallelism,
                Distances = diagonals
            };
        }

        /// <summary>
        /// Just a means of being sure the file really is of the kind we're interested in.
        /// </summary>
        private DiscMetadata LoadMetadata()
        {
            reader
                .ExpectLineStartingWith("Program Name   :")
                .ExpectLineStartingWith("Date / Time    :")
                .Parameter("Drawing No     :", Parse.StringFromSecondColumn("Serial No      :"), out StringBox serialNo)
                .ExpectLineStartingWith("Issue No       :")
                .ExpectLineStartingWith("Description    : Alignment Disc Hybrid");   // TODO: Could this title change?

            return new DiscMetadata { 
                SerialNo = serialNo.Value
            };
        }

        private FlatnessMeasurements LoadFlatness()
        {
            reader
                .ExpectWholeLine("Flatness - Datums F, E, D & G (as common zones)")
                .ExpectWholeLine("Datum F").Parameter("Flatness", Parse.SoleNumber, out double flatF)
                .ExpectWholeLine("Datum E").Parameter("Flatness", Parse.SoleNumber, out double flatE)
                .ExpectWholeLine("Datum D").Parameter("Flatness", Parse.SoleNumber, out double flatD)
                .ExpectWholeLine("Datum G").Parameter("Flatness", Parse.SoleNumber, out double flatG);

            return new FlatnessMeasurements
            {
                DatumF = flatF,
                DatumE = flatE,
                DatumD = flatD,
                DatumG = flatG,
            };
        }

        private ParallelMeasurements LoadParallelism()
        {
            reader
                .ExpectWholeLine("Parallelism - 4 opposed positions")
                // TODO: Talk to Dale and Louis about what these leading numbers (199 etc) actually are.
                .Parameter("199 Datum E LH 1", Parse.SecondNumberOfTwo, out double datumELH1)
                .Parameter("200 Datum E RH 1", Parse.SecondNumberOfTwo, out double datumERH1)
                .Parameter("113 Datum G FR 1", Parse.SecondNumberOfTwo, out double datumGFR1)
                .Parameter("202 Datum G BK 1", Parse.SecondNumberOfTwo, out double datumGBK1);

            return new ParallelMeasurements
            {
                DatumELH1 = datumELH1,
                DatumERH1 = datumERH1,
                DatumGFR1 = datumGFR1,
                DatumGBK1 = datumGBK1,
            };
        }

        private DistanceMeasurements LoadDiagonals()
        {
            // TODO: Talk to Dale and Louis about whether these -1.5 and -10.3 numbers could change.
            //       Are they user-definable?  Can they be fixed at certain values, or eliminated, or 
            //       replaced with a fixed, unchanging, text string?
            reader
                .ExpectWholeLine("Datum E to Datum F - (diagonals at -1.5 & -10.3)")

                // TODO: Talk to Dale and Louis about what these leading numbers (109 etc) actually are.
                .Parameter("109 E to F at -1.5 LH",  Parse.ThirdNumberOfFour, out double EtoFLeft1 )  // TODO: 199 Datum E LH 1
                .Parameter("211 E to F at -1.5 RH",  Parse.ThirdNumberOfFour, out double EtoFRight1)  // TODO: 199 Datum E LH 1
                .Parameter("213 E to F at -10.3 LH", Parse.ThirdNumberOfFour, out double EtoFLeft2 )  // TODO: 199 Datum E LH 1
                .Parameter("215 E to F at -10.3 RH", Parse.ThirdNumberOfFour, out double EtoFRight2)  // TODO: 199 Datum E LH 1

                .ExpectWholeLine("Datum G to Datum D - (diagonals at -1.5 & -10.3)")

                .Parameter("518 G to D at -1.5 FR",  Parse.ThirdNumberOfFour, out double GtoDFront1)  // TODO: 199 Datum E LH 1
                .Parameter("220 G to D at -1.5 BK",  Parse.ThirdNumberOfFour, out double GtoDBack1 )  // TODO: 199 Datum E LH 1
                .Parameter("222 G to D at -10.3 FR", Parse.ThirdNumberOfFour, out double GtoDFront2)  // TODO: 199 Datum E LH 1
                .Parameter("224 G to D at -10.3 BK", Parse.ThirdNumberOfFour, out double GtoDBack2 );  // TODO: 199 Datum E LH 1

            return new DistanceMeasurements
            {
                EtoFLeft1  = EtoFLeft1 ,
                EtoFRight1 = EtoFRight1,
                EtoFLeft2  = EtoFLeft2 ,
                EtoFRight2 = EtoFRight2,
                GtoDFront1 = GtoDFront1,
                GtoDBack1  = GtoDBack1 ,
                GtoDFront2 = GtoDFront2,
                GtoDBack2  = GtoDBack2 ,
            };
        }

        /// <summary>
        /// Just a means of being sure the file really is of the kind we're interested in.
        /// </summary>
        private void ReadPostamble()
        {
            reader
                .ExpectWholeLine("Position of Datum E to Datums B,A,D")
                .ExpectWholeLine("Position of Datum F to Datums B,A,D")
                .ExpectWholeLine("Position of Datum D to Datums B,A,C")
                .ExpectWholeLine("Position of Datum D to Datums B,A")
                .ExpectWholeLine("Position of Datum G to Datums B,A,E")
                .ExpectLineStartingWith("Radius of Corners");
        }
    }
}
