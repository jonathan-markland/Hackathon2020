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
            ReadPreamble();
            var flatness = LoadFlatness();
            var parallelism = LoadParallelism();
            var diagonals = LoadDiagonals();
            ReadPostamble();
            return new DiscInfo {
                Flatness = flatness,
                Parallel = parallelism,
                Diagonal = diagonals
            };
        }

        /// <summary>
        /// Just a means of being sure the file really is of the kind we're interested in.
        /// </summary>
        private void ReadPreamble()
        {
            reader
                .ExpectLineStartingWith("Program Name   :")
                .ExpectLineStartingWith("Date / Time    :")
                .ExpectLineStartingWith("Drawing No     :")
                .ExpectLineStartingWith("Issue No       :")
                .ExpectLineStartingWith("Description    : Alignment Disc Hybrid");   // TODO: Could this title change?
        }

        private FlatnessMeasurements LoadFlatness()
        {
            reader.ExpectWholeLine("Flatness - Datums F, E, D & G (as common zones)");
            
            reader.ExpectWholeLine("Datum F"); var flatF = reader.Parameter("Flatness", Parse.SoleNumber);
            reader.ExpectWholeLine("Datum E"); var flatE = reader.Parameter("Flatness", Parse.SoleNumber);
            reader.ExpectWholeLine("Datum D"); var flatD = reader.Parameter("Flatness", Parse.SoleNumber);
            reader.ExpectWholeLine("Datum G"); var flatG = reader.Parameter("Flatness", Parse.SoleNumber);

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
            reader.ExpectWholeLine("Parallelism - 4 opposed positions");

            // TODO: Talk to Dale and Louis about what these leading numbers (199 etc) actually are.
            var datumELH1 = reader.Parameter("199 Datum E LH 1", Parse.FirstNumberOfTwo);
            var datumERH1 = reader.Parameter("200 Datum E RH 1", Parse.FirstNumberOfTwo);
            var datumGFR1 = reader.Parameter("113 Datum G FR 1", Parse.FirstNumberOfTwo);
            var datumGBK1 = reader.Parameter("202 Datum G BK 1", Parse.FirstNumberOfTwo);

            return new ParallelMeasurements
            {
                DatumELH1 = datumELH1,
                DatumERH1 = datumERH1,
                DatumGFR1 = datumGFR1,
                DatumGBK1 = datumGBK1,
            };
        }

        private DiagonalMeasurements LoadDiagonals()
        {
            // TODO: Talk to Dale and Louis about whether these -1.5 and -10.3 numbers could change.
            //       Are they user-definable?  Can they be fixed at certain values, or eliminated, or 
            //       replaced with a fixed, unchanging, text string?
            reader.ExpectWholeLine("Datum E to Datum F - (diagonals at -1.5 & -10.3)");

            // TODO: Talk to Dale and Louis about what these leading numbers (109 etc) actually are.
            var EtoFLeft1  = reader.Parameter("109 E to F at -1.5 LH", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1
            var EtoFRight1 = reader.Parameter("211 E to F at -1.5 RH", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1
            var EtoFLeft2  = reader.Parameter("213 E to F at -10.3 LH", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1
            var EtoFRight2 = reader.Parameter("215 E to F at -10.3 RH", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1

            reader.ExpectWholeLine("Datum G to Datum D - (diagonals at -1.5 & -10.3)");

            var GtoDFront1 = reader.Parameter("518 G to D at -1.5 FR", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1
            var GtoDBack1  = reader.Parameter("220 G to D at -1.5 BK", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1
            var GtoDFront2 = reader.Parameter("222 G to D at -10.3 FR", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1
            var GtoDBack2  = reader.Parameter("224 G to D at -10.3 BK", Parse.ThirdNumberOfFour);  // TODO: 199 Datum E LH 1

            return new DiagonalMeasurements
            {
                EtoFLeft1  = EtoFLeft1,
                EtoFRight1 = EtoFRight1,
                EtoFLeft2  = EtoFLeft2,
                EtoFRight2 = EtoFRight2,
                GtoDFront1 = GtoDFront1,
                GtoDBack1  = GtoDBack1,
                GtoDFront2 = GtoDFront2,
                GtoDBack2  = GtoDBack2,
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
