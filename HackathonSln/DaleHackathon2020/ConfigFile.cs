using System;
using System.Linq;
using System.IO;
using MCosmosClassLibrary.Models;

namespace DaleHackathon2020
{
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

            var sourceFolder = Find("SourceFolder");
            var historyFolder = Find("HistoryFolder");

            var numberOfPairsString = Find("NumberOfPairs");

            var flatParaToleranceGradeAString = Find("FlatParaToleranceGradeA");
            var flatParaToleranceGradeBString = Find("FlatParaToleranceGradeB");

            var distTargetString = Find("DistTarget");
            var distToleranceGradeAString = Find("DistToleranceGradeA");
            var distToleranceGradeBString = Find("DistToleranceGradeB");

            var programNameLabel   = Find("Program name label");
            var dateTimeLabel      = Find("Date time label");
            var drawingNumberLabel = Find("Drawing number label");
            var serialNumberLabel  = Find("Serial number label");
            var issueNumberLabel   = Find("Issue number label");
            var descriptionLabel   = Find("Description label");

            var flatHead = Find("Flatness section heading");
            var flatLbl1 = Find("Flatness label 1");
            var flatLbl2 = Find("Flatness label 2");
            var flatLbl3 = Find("Flatness label 3");
            var flatLbl4 = Find("Flatness label 4");
            var flatValueLabel = Find("Flatness value label");

            var paraHead = Find("Parallelism section heading");
            var paraLbl1 = Find("Parallelism label 1");
            var paraLbl2 = Find("Parallelism label 2");
            var paraLbl3 = Find("Parallelism label 3");
            var paraLbl4 = Find("Parallelism label 4");

            var distHead1 = Find("Distance heading 1");
            var distLbl1  = Find("Distance label 1");
            var distLbl2  = Find("Distance label 2");
            var distLbl3  = Find("Distance label 3");
            var distLbl4  = Find("Distance label 4");
            var distHead2 = Find("Distance heading 2");
            var distLbl5  = Find("Distance label 5");
            var distLbl6  = Find("Distance label 6");
            var distLbl7  = Find("Distance label 7");
            var distLbl8  = Find("Distance label 8");

            // Numerics:

            var numberOfPairs = ParseIntegerInRange(numberOfPairsString, "NumberOfPairs", 1, 1000);
            var flatParaToleranceGradeA = ParseDoubleInRange(flatParaToleranceGradeAString, "FlatParaToleranceGradeA", 0.0, 1.0);
            var flatParaToleranceGradeB = ParseDoubleInRange(flatParaToleranceGradeBString, "FlatParaToleranceGradeB", 0.0, 1.0);
            var distTarget = ParseDoubleInRange(distTargetString, "DistTarget", 0.0, 1000000.0);
            var distToleranceGradeA = ParseDoubleInRange(distToleranceGradeAString, "DistToleranceGradeA", 0.0, 1.0);
            var distToleranceGradeB = ParseDoubleInRange(distToleranceGradeBString, "DistToleranceGradeB", 0.0, 1.0);

            // Set properties:

            SourceFolderPath = sourceFolder;
            HistoryFolderPath = historyFolder;

            NumberOfPairs = numberOfPairs;

            FlatParaToleranceGradeA = flatParaToleranceGradeA;
            FlatParaToleranceGradeB = flatParaToleranceGradeB;

            DistTarget = distTarget;
            DistToleranceGradeA = distToleranceGradeA;
            DistToleranceGradeB = distToleranceGradeB;

            FileHeadings = new FileHeadings
            {
                ProgramNameLabel = programNameLabel,
                DateTimeLabel = dateTimeLabel,
                DrawingNumberLabel = drawingNumberLabel,
                SerialNumberLabel = serialNumberLabel,
                IssueNumberLabel = issueNumberLabel,
                DescriptionLabel = descriptionLabel,

                FlatHeading = flatHead,
                FlatSubHeading1 = flatLbl1,
                FlatSubHeading2 = flatLbl2,
                FlatSubHeading3 = flatLbl3,
                FlatSubHeading4 = flatLbl4,
                FlatValueLabel  = flatValueLabel,

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
        }

        public string ConfigFilePath { get; init; }

        public string SourceFolderPath { get; init; }
        public string HistoryFolderPath { get; init; }

        public int NumberOfPairs { get; init; }

        public double FlatParaToleranceGradeA { get; init; }
        public double FlatParaToleranceGradeB { get; init; }

        public double DistTarget { get; init; }
        public double DistToleranceGradeA { get; init; }
        public double DistToleranceGradeB { get; init; }

        public FileHeadings FileHeadings { get; init; }

    }
}
