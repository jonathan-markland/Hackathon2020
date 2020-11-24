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
            var flatF = new Number();
            var flatE = new Number();
            var flatD = new Number();
            var flatG = new Number();

            reader
                .ExpectWholeLine("Flatness - Datums F, E, D & G (as common zones)")
                .ExpectWholeLine("Datum F").Parameter("Flatness", Parse.SoleNumber, flatF)
                .ExpectWholeLine("Datum E").Parameter("Flatness", Parse.SoleNumber, flatE)
                .ExpectWholeLine("Datum D").Parameter("Flatness", Parse.SoleNumber, flatD)
                .ExpectWholeLine("Datum G").Parameter("Flatness", Parse.SoleNumber, flatG);

            return new FlatnessMeasurements
            {
                DatumF = flatF.Value,
                DatumE = flatE.Value,
                DatumD = flatD.Value,
                DatumG = flatG.Value,
            };
        }

        private ParallelMeasurements LoadParallelism()
        {
            var datumELH1 = new Number();
            var datumERH1 = new Number();
            var datumGFR1 = new Number();
            var datumGBK1 = new Number();

            reader
                .ExpectWholeLine("Parallelism - 4 opposed positions")
                // TODO: Talk to Dale and Louis about what these leading numbers (199 etc) actually are.
                .Parameter("199 Datum E LH 1", Parse.FirstNumberOfTwo, datumELH1)
                .Parameter("200 Datum E RH 1", Parse.FirstNumberOfTwo, datumERH1)
                .Parameter("113 Datum G FR 1", Parse.FirstNumberOfTwo, datumGFR1)
                .Parameter("202 Datum G BK 1", Parse.FirstNumberOfTwo, datumGBK1);

            return new ParallelMeasurements
            {
                DatumELH1 = datumELH1.Value,
                DatumERH1 = datumERH1.Value,
                DatumGFR1 = datumGFR1.Value,
                DatumGBK1 = datumGBK1.Value,
            };
        }

        private DiagonalMeasurements LoadDiagonals()
        {
            var EtoFLeft1  = new Number();
            var EtoFRight1 = new Number();
            var EtoFLeft2  = new Number();
            var EtoFRight2 = new Number();

            var GtoDFront1 = new Number();
            var GtoDBack1  = new Number();
            var GtoDFront2 = new Number();
            var GtoDBack2  = new Number();

            // TODO: Talk to Dale and Louis about whether these -1.5 and -10.3 numbers could change.
            //       Are they user-definable?  Can they be fixed at certain values, or eliminated, or 
            //       replaced with a fixed, unchanging, text string?
            reader
                .ExpectWholeLine("Datum E to Datum F - (diagonals at -1.5 & -10.3)")

                // TODO: Talk to Dale and Louis about what these leading numbers (109 etc) actually are.
                .Parameter("109 E to F at -1.5 LH",  Parse.ThirdNumberOfFour, EtoFLeft1 )  // TODO: 199 Datum E LH 1
                .Parameter("211 E to F at -1.5 RH",  Parse.ThirdNumberOfFour, EtoFRight1)  // TODO: 199 Datum E LH 1
                .Parameter("213 E to F at -10.3 LH", Parse.ThirdNumberOfFour, EtoFLeft2 )  // TODO: 199 Datum E LH 1
                .Parameter("215 E to F at -10.3 RH", Parse.ThirdNumberOfFour, EtoFRight2)  // TODO: 199 Datum E LH 1

                .ExpectWholeLine("Datum G to Datum D - (diagonals at -1.5 & -10.3)")

                .Parameter("518 G to D at -1.5 FR",  Parse.ThirdNumberOfFour, GtoDFront1)  // TODO: 199 Datum E LH 1
                .Parameter("220 G to D at -1.5 BK",  Parse.ThirdNumberOfFour, GtoDBack1 )  // TODO: 199 Datum E LH 1
                .Parameter("222 G to D at -10.3 FR", Parse.ThirdNumberOfFour, GtoDFront2)  // TODO: 199 Datum E LH 1
                .Parameter("224 G to D at -10.3 BK", Parse.ThirdNumberOfFour, GtoDBack2 );  // TODO: 199 Datum E LH 1

            return new DiagonalMeasurements
            {
                EtoFLeft1  = EtoFLeft1 .Value,
                EtoFRight1 = EtoFRight1.Value,
                EtoFLeft2  = EtoFLeft2 .Value,
                EtoFRight2 = EtoFRight2.Value,
                GtoDFront1 = GtoDFront1.Value,
                GtoDBack1  = GtoDBack1 .Value,
                GtoDFront2 = GtoDFront2.Value,
                GtoDBack2  = GtoDBack2 .Value,
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
