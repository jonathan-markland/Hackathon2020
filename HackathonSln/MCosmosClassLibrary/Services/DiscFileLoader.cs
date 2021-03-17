using MCosmosClassLibrary.Models;
using GeneralClassLibrary;

namespace MCosmosClassLibrary.Services
{
    /// <summary>
    /// Parses and loads the disc information file.
    /// </summary>
    public class DiscFileLoader
    {
        private FileReader reader;
        private DiscInfo results;
        private string SourceFilePath;
        private FileHeadings fileHeadings;

        public static DiscInfo LoadDiscFromFile(string sourceFilePath, FileHeadings fileHeadings)
        {
            return new DiscFileLoader(sourceFilePath, fileHeadings).results;
        }

        private DiscFileLoader(string sourceFilePath, FileHeadings fileHeadings)
        {
            SourceFilePath = sourceFilePath;
            this.fileHeadings = fileHeadings;
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
                .ExpectLineStartingWith(fileHeadings.ProgramNameLabel /* Program Name   : */ )
                .ExpectLineStartingWith(fileHeadings.DateTimeLabel /* Date / Time    : */)
                .Parameter(fileHeadings.DrawingNumberLabel /* Drawing No     : */, Parse.StringFromSecondColumn(fileHeadings.SerialNumberLabel /* Serial No      : */), out StringBox serialNo)
                .ExpectLineStartingWith(fileHeadings.IssueNumberLabel /* Issue No       : */)
                .ExpectLineStartingWith(fileHeadings.DescriptionLabel /* Description    : */);

            return new DiscMetadata(serialNo.Value, SourceFilePath);
        }

        private FlatnessMeasurements LoadFlatness()
        {
            reader
                .ExpectWholeLine(fileHeadings.FlatHeading) // was: "Flatness - Datums F, E, D & G (as common zones)"
                .ExpectWholeLine(fileHeadings.FlatSubHeading1 /* Datum F */).Parameter(fileHeadings.FlatValueLabel /* Flatness */, Parse.FlatnessNumber, out FlatnessMeasure flatF)
                .ExpectWholeLine(fileHeadings.FlatSubHeading2 /* Datum E */).Parameter(fileHeadings.FlatValueLabel /* Flatness */, Parse.FlatnessNumber, out FlatnessMeasure flatE)
                .ExpectWholeLine(fileHeadings.FlatSubHeading3 /* Datum D */).Parameter(fileHeadings.FlatValueLabel /* Flatness */, Parse.FlatnessNumber, out FlatnessMeasure flatD)
                .ExpectWholeLine(fileHeadings.FlatSubHeading4 /* Datum G */).Parameter(fileHeadings.FlatValueLabel /* Flatness */, Parse.FlatnessNumber, out FlatnessMeasure flatG);

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
                .ExpectWholeLine(fileHeadings.ParaHeading) // was: "Parallelism - 4 opposed positions")
                .Parameter(fileHeadings.ParaLabel1 /* Datum E LH 1 */, Parse.ParallelNumber, out ParallelMeasure datumELH1)
                .Parameter(fileHeadings.ParaLabel2 /* Datum E RH 1 */, Parse.ParallelNumber, out ParallelMeasure datumERH1)
                .Parameter(fileHeadings.ParaLabel3 /* Datum G FR 1 */, Parse.ParallelNumber, out ParallelMeasure datumGFR1)
                .Parameter(fileHeadings.ParaLabel4 /* Datum G BK 1 */, Parse.ParallelNumber, out ParallelMeasure datumGBK1);

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
                .ExpectWholeLine(fileHeadings.DistHeading1) // was: "Datum E to Datum F - (diagonals at -1.5 & -10.3)"
                .Parameter(fileHeadings.DistLabel1 /* E to F at -1.5 LH  */, Parse.DistanceNumber, out DistanceMeasure EtoFLeft1 )
                .Parameter(fileHeadings.DistLabel2 /* E to F at -1.5 RH  */, Parse.DistanceNumber, out DistanceMeasure EtoFRight1)
                .Parameter(fileHeadings.DistLabel3 /* E to F at -10.3 LH */, Parse.DistanceNumber, out DistanceMeasure EtoFLeft2 )
                .Parameter(fileHeadings.DistLabel4 /* E to F at -10.3 RH */, Parse.DistanceNumber, out DistanceMeasure EtoFRight2)

                .ExpectWholeLine(fileHeadings.DistHeading2) // was: "Datum G to Datum D - (diagonals at -1.5 & -10.3)"
                .Parameter(fileHeadings.DistLabel5 /* G to D at -1.5 FR  */, Parse.DistanceNumber, out DistanceMeasure GtoDFront1) 
                .Parameter(fileHeadings.DistLabel6 /* G to D at -1.5 BK  */, Parse.DistanceNumber, out DistanceMeasure GtoDBack1 ) 
                .Parameter(fileHeadings.DistLabel7 /* G to D at -10.3 FR */, Parse.DistanceNumber, out DistanceMeasure GtoDFront2) 
                .Parameter(fileHeadings.DistLabel8 /* G to D at -10.3 BK */, Parse.DistanceNumber, out DistanceMeasure GtoDBack2 );

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
            /* 03/03/2021 - I am choosing not to do this now, since configurability went in.
             *              Really it's a pain that the CMM file is a 'report' rather than a fixed export.
             
            reader
                .ExpectWholeLine("Position of Datum E to Datums B,A,D")
                .ExpectWholeLine("Position of Datum F to Datums B,A,D")
                .ExpectWholeLine("Position of Datum D to Datums B,A,C")
                .ExpectWholeLine("Position of Datum D to Datums B,A")
                .ExpectWholeLine("Position of Datum G to Datums B,A,E")
                .ExpectLineStartingWith("Radius of Corners");
            */
        }
    }
}
