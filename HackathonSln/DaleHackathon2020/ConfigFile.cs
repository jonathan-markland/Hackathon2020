using System;
using System.Linq;
using System.IO;
using MCosmosClassLibrary.Models;

namespace DaleHackathon2020
{
    // TODO: test framework for config file, positive load case at least.

    public class ConfigFile
    {
        public static ConfigFile Load(string path)
        {
            return new ConfigFile(path);
        }

        private ConfigFile(string path)
        {
            ConfigFilePath = path;

            bool isNotEmptyOrComment(string s)
            {
                return !(String.IsNullOrWhiteSpace(s) || s.StartsWith("#"));
            }

            var lines = File.ReadAllLines(path).Where(isNotEmptyOrComment).ToList();

            string Find(string keyName)
            {
                string found = null;
                foreach (string line in lines)
                {
                    var equalsPos = line.IndexOf('=');
                    if (equalsPos >= 0)
                    {
                        var candidateKey = line.Substring(0, equalsPos).Trim();
                        if (candidateKey == keyName)
                        {
                            if (found != null)
                            {
                                throw new Exception($"The configuration file contains a duplicate key: {keyName}.  Please remove the duplicate.");
                            }
                            found = line.Substring(equalsPos + 1).Trim();
                        }
                    }
                }
                if (found == null)
                {
                    throw new Exception($"Cannot find '{keyName}' key in the configuration file.");
                }
                return found;
            }

            int ParseIntegerInRange(string s, string keyName, int minimum, int maximum)
            {
                int i;
                if (!int.TryParse(s, out i))
                {
                    throw new Exception($"An integer numeric value is expected in the configuration file for the '{keyName}' setting.");
                }
                if (i < minimum || i > maximum)
                {
                    throw new Exception($"The '{keyName}' configuration file setting is out of range, and must be in range {minimum}..{maximum}.");
                }
                return i;
            }

            double ParseDoubleInRange(string s, string keyName, double minimum, double maximum)
            {
                double i;
                if (!double.TryParse(s, out i))
                {
                    throw new Exception($"A numeric value is expected in the configuration file for the '{keyName}' setting.");
                }
                if (i < minimum || i > maximum)
                {
                    throw new Exception($"The '{keyName}' configuration file setting is out of range, and must be in range {minimum}..{maximum}.");
                }
                return i;
            }

            // Obtain all strings:

            var sourceFolder = Find("SourceFolder");    /* C:\Users\Jonathan\Documents\Dale\CMMSourceFiles */
            var historyFolder = Find("HistoryFolder");  /* C:\Users\Jonathan\Documents\Dale\PairingHistory */

            var numberOfPairsString = Find("NumberOfPairs");   /* 15 */
            var euclideanCutoffAboveString = Find("Euclidean cutoff above");   /* 0.001 */

            var flatParaToleranceGradeAString = Find("FlatParaToleranceGradeA");  /* 0.002 */
            var flatParaToleranceGradeBString = Find("FlatParaToleranceGradeB");  /* 0.0025 */

            var distTargetString = Find("DistTarget");            /* 28.020 */
            var distToleranceGradeAString = Find("DistToleranceGradeA");   /*  0.001 */
            var distToleranceGradeBString = Find("DistToleranceGradeB");   /*  0.002 */

            var serialNumberLabel = Find("Serial number label");  /* Serial number label */

            var flatHeadingF = Find("Flatness F heading");         /* Datum F */
            var flatHeadingE = Find("Flatness E heading");         /* Datum E */
            var flatHeadingD = Find("Flatness D heading");         /* Datum D */
            var flatHeadingG = Find("Flatness G heading");         /* Datum G */
            var flatValueLabel = Find("Flatness value label");     /* Flatness */

            var paraHead = Find("Parallelism section heading");    /* Parallelism - 4 opposed positions */
            var paraLbl1 = Find("Parallelism label 1");            /* Datum E LH 1 */
            var paraLbl2 = Find("Parallelism label 2");            /* Datum E RH 1 */
            var paraLbl3 = Find("Parallelism label 3");            /* Datum G FR 1 */
            var paraLbl4 = Find("Parallelism label 4");            /* Datum G BK 1 */

            var distHead1 = Find("Distance heading 1");            /* Datum E to Datum F - (diagonals at -1.5 & -10.3) */
            var distLbl1 = Find("Distance label 1");              /* E to F at -1.5 LH */
            var distLbl2 = Find("Distance label 2");              /* E to F at -1.5 RH */
            var distLbl3 = Find("Distance label 3");              /* E to F at -10.3 LH */
            var distLbl4 = Find("Distance label 4");              /* E to F at -10.3 RH */

            var distHead2 = Find("Distance heading 2");   /* Datum G to Datum D - (diagonals at -1.5 & -10.3) */
            var distLbl5 = Find("Distance label 5");     /* G to D at -1.5 FR */
            var distLbl6 = Find("Distance label 6");     /* G to D at -1.5 BK */
            var distLbl7 = Find("Distance label 7");     /* G to D at -10.3 FR */
            var distLbl8 = Find("Distance label 8");     /* G to D at -10.3 BK */

            // Collect values:

            var numberOfPairs = ParseIntegerInRange(numberOfPairsString, "NumberOfPairs", 1, 1000);
            var euclideanCutoffAbove = ParseDoubleInRange(euclideanCutoffAboveString, "Euclidean cutoff above", 0.0, 1.0);
            var flatParaToleranceGradeA = ParseDoubleInRange(flatParaToleranceGradeAString, "FlatParaToleranceGradeA", 0.0, 1.0);
            var flatParaToleranceGradeB = ParseDoubleInRange(flatParaToleranceGradeBString, "FlatParaToleranceGradeB", 0.0, 1.0);
            var distTarget = ParseDoubleInRange(distTargetString, "DistTarget", 0.0, 1000000.0);
            var distToleranceGradeA = ParseDoubleInRange(distToleranceGradeAString, "DistToleranceGradeA", 0.0, 1.0);
            var distToleranceGradeB = ParseDoubleInRange(distToleranceGradeBString, "DistToleranceGradeB", 0.0, 1.0);

            var flatParaGradeBoundaries = new FlatParaGradeBoundaries
            {
                BoundaryGradeA = flatParaToleranceGradeA,
                BoundaryGradeB = flatParaToleranceGradeB
            };

            var distanceGradeBoundaries = new DistanceGradeBoundaries
            {
                DistTarget = distTarget,
                BoundaryEitherSideGradeA = distToleranceGradeA,
                BoundaryEitherSideGradeB = distToleranceGradeB,
            };

            var fileHeadings = new FileHeadings
            {
                SerialNumberLabel = serialNumberLabel,

                FlatSubHeading1 = flatHeadingF,
                FlatSubHeading2 = flatHeadingE,
                FlatSubHeading3 = flatHeadingD,
                FlatSubHeading4 = flatHeadingG,
                FlatValueLabel = flatValueLabel,

                ParaHeading = paraHead,
                ParaLabel1 = paraLbl1,
                ParaLabel2 = paraLbl2,
                ParaLabel3 = paraLbl3,
                ParaLabel4 = paraLbl4,

                DistHeading1 = distHead1,
                DistLabel1 = distLbl1,
                DistLabel2 = distLbl2,
                DistLabel3 = distLbl3,
                DistLabel4 = distLbl4,
                DistHeading2 = distHead2,
                DistLabel5 = distLbl5,
                DistLabel6 = distLbl6,
                DistLabel7 = distLbl7,
                DistLabel8 = distLbl8,
            };

            // Set properties:

            SourceFolderPath = sourceFolder;
            HistoryFolderPath = historyFolder;

            NumberOfPairs = numberOfPairs;
            EuclideanCutoffAbove = euclideanCutoffAbove;

            DiscConfig = new DiscConfig
            { 
                DistBounds = distanceGradeBoundaries,
                FileHeadings = fileHeadings,
                FlatParaBounds = flatParaGradeBoundaries
            };
        }

        public string ConfigFilePath { get; init; }

        public string SourceFolderPath { get; init; }
        public string HistoryFolderPath { get; init; }

        public int NumberOfPairs { get; init; }

        public double EuclideanCutoffAbove { get; init; }

        public DiscConfig DiscConfig { get; init; }

    }
}
