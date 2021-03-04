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

            var flatHead = Find("Flatness section heading");
            var flatLbl1 = Find("Flatness label 1");
            var flatLbl2 = Find("Flatness label 2");
            var flatLbl3 = Find("Flatness label 3");
            var flatLbl4 = Find("Flatness label 4");
            var paraHead = Find("Parallelism section heading");
            var paraLbl1 = Find("Parallelism label 1");
            var paraLbl2 = Find("Parallelism label 2");
            var paraLbl3 = Find("Parallelism label 3");
            var paraLbl4 = Find("Parallelism label 4");
            var distHead1 = Find("Distance heading 1");
            var distLbl1 = Find("Distance label 1");
            var distLbl2 = Find("Distance label 2");
            var distLbl3 = Find("Distance label 3");
            var distLbl4 = Find("Distance label 4");
            var distHead2 = Find("Distance heading 2");
            var distLbl5 = Find("Distance label 5");
            var distLbl6 = Find("Distance label 6");
            var distLbl7 = Find("Distance label 7");
            var distLbl8 = Find("Distance label 8");

            // Numerics:

            var numberOfPairs = ParseIntegerInRange(numberOfPairsString, "NumberOfPairs", 1, 1000);
            var flatParaToleranceGradeA = ParseDoubleInRange(flatParaToleranceGradeAString, "FlatParaToleranceGradeA", 0.0, 1.0);
            var flatParaToleranceGradeB = ParseDoubleInRange(flatParaToleranceGradeBString, "FlatParaToleranceGradeB", 0.0, 1.0);
            var distTarget = ParseDoubleInRange(distTargetString, "DistTarget", 0.0, 1000000.0);
            var distToleranceGradeA = ParseDoubleInRange(distToleranceGradeAString, "DistToleranceGradeA", 0.0, 1.0);
            var distToleranceGradeB = ParseDoubleInRange(distToleranceGradeBString, "DistToleranceGradeB", 0.0, 1.0);

            // Set properties:

            SourceFolder = sourceFolder;
            HistoryFolder = historyFolder;

            NumberOfPairs = numberOfPairs;

            FlatParaToleranceGradeA = flatParaToleranceGradeA;
            FlatParaToleranceGradeB = flatParaToleranceGradeB;

            DistTarget = distTarget;
            DistToleranceGradeA = distToleranceGradeA;
            DistToleranceGradeB = distToleranceGradeB;

            FileHeadings = new FileHeadings
            {
                FlatHead = flatHead,
                FlatLbl1 = flatLbl1,
                FlatLbl2 = flatLbl2,
                FlatLbl3 = flatLbl3,
                FlatLbl4 = flatLbl4,
                ParaHead = paraHead,
                ParaLbl1 = paraLbl1,
                ParaLbl2 = paraLbl2,
                ParaLbl3 = paraLbl3,
                ParaLbl4 = paraLbl4,
                DistHead1 = distHead1,
                DistLbl1 = distLbl1,
                DistLbl2 = distLbl2,
                DistLbl3 = distLbl3,
                DistLbl4 = distLbl4,
                DistHead2 = distHead2,
                DistLbl5 = distLbl5,
                DistLbl6 = distLbl6,
                DistLbl7 = distLbl7,
                DistLbl8 = distLbl8,
            };
        }

        public string ConfigFilePath { get; init; }

        public string SourceFolder { get; init; }
        public string HistoryFolder { get; init; }

        public int NumberOfPairs { get; init; }

        public double FlatParaToleranceGradeA { get; init; }
        public double FlatParaToleranceGradeB { get; init; }

        public double DistTarget { get; init; }
        public double DistToleranceGradeA { get; init; }
        public double DistToleranceGradeB { get; init; }

        public FileHeadings FileHeadings { get; init; }

    }
}
