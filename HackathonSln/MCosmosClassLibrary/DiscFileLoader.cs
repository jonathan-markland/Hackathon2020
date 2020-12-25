using System;
using System.Collections.Generic;
using System.Text;
using MCosmosClassLibrary.Models;
using GeneralClassLibrary;

namespace MCosmosClassLibrary
{
    /// <summary>
    /// Parses and loads the disc information file.
    /// </summary>
    public class DiscFileLoader
    {
        private FileReader reader;
        private DiscInfo results;
        private string SourceFilePath;

        public static DiscInfo LoadDiscFromFile(string sourceFilePath)
        {
            return new DiscFileLoader(sourceFilePath).results;
        }

        private DiscFileLoader(string sourceFilePath)
        {
            SourceFilePath = sourceFilePath;
            reader = new FileReader(sourceFilePath);
            results = Load();
        }

        private DiscInfo Load()
        {
            var metadata = LoadMetadata();
            var flatness = LoadFlatness();
            var parallelism = LoadParallelism();
            var distances = LoadDistances();
            ReadPostamble();
            return new DiscInfo(metadata, flatness, parallelism, distances);
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

            return new DiscMetadata(serialNo.Value, SourceFilePath);
        }

        private FlatnessMeasurements LoadFlatness()
        {
            reader
                .ExpectWholeLine("Flatness - Datums F, E, D & G (as common zones)")
                .ExpectWholeLine("Datum F").Parameter("Flatness", Parse.FlatnessNumber, out MeasureAndGrade flatF)
                .ExpectWholeLine("Datum E").Parameter("Flatness", Parse.FlatnessNumber, out MeasureAndGrade flatE)
                .ExpectWholeLine("Datum D").Parameter("Flatness", Parse.FlatnessNumber, out MeasureAndGrade flatD)
                .ExpectWholeLine("Datum G").Parameter("Flatness", Parse.FlatnessNumber, out MeasureAndGrade flatG);

            return new FlatnessMeasurements(
                datumF: flatF,
                datumE: flatE,
                datumD: flatD,
                datumG: flatG
            );
        }

        private ParallelMeasurements LoadParallelism()
        {
            reader
                .ExpectWholeLine("Parallelism - 4 opposed positions")
                .Parameter("Datum E LH 1", Parse.ParallelNumber, out MeasureAndGrade datumELH1)
                .Parameter("Datum E RH 1", Parse.ParallelNumber, out MeasureAndGrade datumERH1)
                .Parameter("Datum G FR 1", Parse.ParallelNumber, out MeasureAndGrade datumGFR1)
                .Parameter("Datum G BK 1", Parse.ParallelNumber, out MeasureAndGrade datumGBK1);

            return new ParallelMeasurements(
                datumELH1: datumELH1,
                datumERH1: datumERH1,
                datumGFR1: datumGFR1,
                datumGBK1: datumGBK1
            );
        }

        private DistanceMeasurements LoadDistances()
        {
            reader
                .ExpectWholeLine("Datum E to Datum F - (diagonals at -1.5 & -10.3)")
                .Parameter("E to F at -1.5 LH",  Parse.DistanceNumber, out MeasureAndGrade EtoFLeft1 )  // TODO: 199 Datum E LH 1
                .Parameter("E to F at -1.5 RH",  Parse.DistanceNumber, out MeasureAndGrade EtoFRight1)  // TODO: 199 Datum E LH 1
                .Parameter("E to F at -10.3 LH", Parse.DistanceNumber, out MeasureAndGrade EtoFLeft2 )  // TODO: 199 Datum E LH 1
                .Parameter("E to F at -10.3 RH", Parse.DistanceNumber, out MeasureAndGrade EtoFRight2)  // TODO: 199 Datum E LH 1

                .ExpectWholeLine("Datum G to Datum D - (diagonals at -1.5 & -10.3)")
                .Parameter("G to D at -1.5 FR",  Parse.DistanceNumber, out MeasureAndGrade GtoDFront1)  // TODO: 199 Datum E LH 1
                .Parameter("G to D at -1.5 BK",  Parse.DistanceNumber, out MeasureAndGrade GtoDBack1 )  // TODO: 199 Datum E LH 1
                .Parameter("G to D at -10.3 FR", Parse.DistanceNumber, out MeasureAndGrade GtoDFront2)  // TODO: 199 Datum E LH 1
                .Parameter("G to D at -10.3 BK", Parse.DistanceNumber, out MeasureAndGrade GtoDBack2 );  // TODO: 199 Datum E LH 1

            return new DistanceMeasurements(
                etoFLeft1  : EtoFLeft1 ,
                etoFRight1 : EtoFRight1,
                etoFLeft2  : EtoFLeft2 ,
                etoFRight2 : EtoFRight2,
                gtoDFront1 : GtoDFront1,
                gtoDBack1  : GtoDBack1 ,
                gtoDFront2 : GtoDFront2,
                gtoDBack2  : GtoDBack2 
            );
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
