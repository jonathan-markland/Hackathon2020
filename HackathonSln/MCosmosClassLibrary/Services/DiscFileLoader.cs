using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralClassLibrary;
using MCosmosClassLibrary.Models;

namespace MCosmosClassLibrary.Services
{
    public class DiscFileLoader
    {
        private string SourceFilePath;
        private DiscInfo results;
        private DocumentManager Document;
        private FileHeadings fileHeadings;

        public static DiscInfo LoadDiscFromFile(string sourceFilePath, FileHeadings fileHeadings)
        {
            return new DiscFileLoader(sourceFilePath, fileHeadings).results;
        }

        private DiscFileLoader(string sourceFilePath, FileHeadings fileHeadings)
        {
            SourceFilePath = sourceFilePath;
            Document = new DocumentManager(System.IO.File.ReadAllLines(sourceFilePath));
            this.fileHeadings = fileHeadings;
            results = Load();
        }

        private DiscInfo Load()
        {
            var metadata = LoadMetadata();
            var flatness = LoadFlatness();
            var parallelism = LoadParallelism();
            var distances = LoadDistances();
            return new DiscInfo(metadata, flatness, parallelism, distances);
        }

        private DiscMetadata LoadMetadata()
        {
            var serialNo = AllTextToTheRightOf(fileHeadings.SerialNumberLabel /* Serial No      : */);
            return new DiscMetadata(serialNo, SourceFilePath);
        }

        private FlatnessMeasurements LoadFlatness()
        {
            // TODO: no longer read:  fileHeadings.FlatHeading     // "Flatness - Datums F, E, D & G (as common zones)"

            var flatF = ValueUnderneathHeading(fileHeadings.FlatSubHeading1 /* Datum F */, fileHeadings.FlatValueLabel /* Flatness */, 0, 1);
            var flatE = ValueUnderneathHeading(fileHeadings.FlatSubHeading2 /* Datum E */, fileHeadings.FlatValueLabel /* Flatness */, 0, 1);
            var flatD = ValueUnderneathHeading(fileHeadings.FlatSubHeading3 /* Datum D */, fileHeadings.FlatValueLabel /* Flatness */, 0, 1);
            var flatG = ValueUnderneathHeading(fileHeadings.FlatSubHeading4 /* Datum G */, fileHeadings.FlatValueLabel /* Flatness */, 0, 1);

            // eg:
            //      Datum F
            // ... lines ...
            //                                                                        Flatness       0.00127

            return new FlatnessMeasurements(
                datumF: new FlatnessMeasure(flatF),
                datumE: new FlatnessMeasure(flatE),
                datumD: new FlatnessMeasure(flatD),
                datumG: new FlatnessMeasure(flatG)
            );
        }

        private ParallelMeasurements LoadParallelism()
        {
            // TODO: Heading need to be configurable:

            // Parallelism - 4 opposed positions
            // eg:
            //                   202 Datum E LH 1                          0.00200           0.00180

            var datumELH1 = ValueUnderneathHeading("Parallelism - 4 opposed positions", fileHeadings.ParaLabel1 /* Datum E LH 1 */, 1, 2);
            var datumERH1 = ValueUnderneathHeading("Parallelism - 4 opposed positions", fileHeadings.ParaLabel2 /* Datum E RH 1 */, 1, 2);
            var datumGFR1 = ValueUnderneathHeading("Parallelism - 4 opposed positions", fileHeadings.ParaLabel3 /* Datum G FR 1 */, 1, 2);
            var datumGBK1 = ValueUnderneathHeading("Parallelism - 4 opposed positions", fileHeadings.ParaLabel4 /* Datum G BK 1 */, 1, 2);

            return new ParallelMeasurements(
                datumELH1: new ParallelMeasure(datumELH1),
                datumERH1: new ParallelMeasure(datumERH1),
                datumGFR1: new ParallelMeasure(datumGFR1),
                datumGBK1: new ParallelMeasure(datumGBK1)
            );
        }

        private DistanceMeasurements LoadDistances()
        {
            // Eg:
            //      Datum E to Datum F - (diagonals at -1.5 & -10.3)
            // ... lines ...
            //                   112 E to F at -1.5 LH            28.02000 -0.00500 28.01644 -0.00356

            var EtoFLeft1  = ValueUnderneathHeading(fileHeadings.DistHeading1, fileHeadings.DistLabel1 /* E to F at -1.5 LH  */, 2, 4);
            var EtoFRight1 = ValueUnderneathHeading(fileHeadings.DistHeading1, fileHeadings.DistLabel2 /* E to F at -1.5 RH  */, 2, 4);
            var EtoFLeft2  = ValueUnderneathHeading(fileHeadings.DistHeading1, fileHeadings.DistLabel3 /* E to F at -10.3 LH */, 2, 4);
            var EtoFRight2 = ValueUnderneathHeading(fileHeadings.DistHeading1, fileHeadings.DistLabel4 /* E to F at -10.3 RH */, 2, 4);

            var GtoDFront1 = ValueUnderneathHeading(fileHeadings.DistHeading2, fileHeadings.DistLabel5 /* G to D at -1.5 FR  */, 2, 4);
            var GtoDBack1  = ValueUnderneathHeading(fileHeadings.DistHeading2, fileHeadings.DistLabel6 /* G to D at -1.5 BK  */, 2, 4);
            var GtoDFront2 = ValueUnderneathHeading(fileHeadings.DistHeading2, fileHeadings.DistLabel7 /* G to D at -10.3 FR */, 2, 4);
            var GtoDBack2  = ValueUnderneathHeading(fileHeadings.DistHeading2, fileHeadings.DistLabel8 /* G to D at -10.3 BK */, 2, 4);

            return new DistanceMeasurements(
                etoFLeft1:  new DistanceMeasure(EtoFLeft1),
                etoFRight1: new DistanceMeasure(EtoFRight1),
                etoFLeft2:  new DistanceMeasure(EtoFLeft2),
                etoFRight2: new DistanceMeasure(EtoFRight2),
                gtoDFront1: new DistanceMeasure(GtoDFront1),
                gtoDBack1:  new DistanceMeasure(GtoDBack1),
                gtoDFront2: new DistanceMeasure(GtoDFront2),
                gtoDBack2:  new DistanceMeasure(GtoDBack2)
            );
        }


        #region Delegations

        private string AllTextToTheRightOf(string label)
        {
            return Document.AllTextToTheRightOf(label);
        }

        private double ValueUnderneathHeading(string subHeading, string label, int rowValueIndex, int numberOfValuesOnRow)
        {
            return Document.ValueUnderneathHeading(subHeading, label, rowValueIndex, numberOfValuesOnRow);
        }

        #endregion

    }
}
