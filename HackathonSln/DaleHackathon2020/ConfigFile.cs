using System;
using System.Linq;
using System.IO;

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

            bool isNotEmpty(string s)
            {
                return !String.IsNullOrWhiteSpace(s);
            }

            var lines = File.ReadAllLines(path).Where(isNotEmpty).ToList();

            string Find(string keyName)
            {
                var keyToFind = keyName + "=";
                foreach (string line in lines)
                {
                    if (line.StartsWith(keyToFind))
                    {
                        return line.Substring(keyToFind.Length).Trim();
                    }
                }
                throw new Exception($"Cannot find '{keyName}' key in the configuration file.");
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

            var sourceFolder = Find("SourceFolder");
            var historyFolder = Find("HistoryFolder");
            var numberOfPairsString = Find("NumberOfPairs");
            var flatParaToleranceGradeAString = Find("FlatParaToleranceGradeA");
            var flatParaToleranceGradeBString = Find("FlatParaToleranceGradeB");
            var distTargetString = Find("DistTarget");
            var distToleranceAString = Find("DistToleranceA");
            var distToleranceBString = Find("DistToleranceB");

            var numberOfPairs = ParseIntegerInRange(numberOfPairsString, "NumberOfPairs", 1, 1000);
            var flatParaToleranceGradeA = ParseDoubleInRange(flatParaToleranceGradeAString, "FlatParaToleranceGradeA", 0.0, 1.0);
            var flatParaToleranceGradeB = ParseDoubleInRange(flatParaToleranceGradeBString, "FlatParaToleranceGradeB", 0.0, 1.0);
            var distTarget = ParseDoubleInRange(distTargetString, "DistTarget", 0.0, 1000000.0);
            var distToleranceA = ParseDoubleInRange(distToleranceAString, "DistToleranceA", 0.0, 1.0);
            var distToleranceB = ParseDoubleInRange(distToleranceBString, "DistToleranceB", 0.0, 1.0);

            SourceFolder = sourceFolder;
            HistoryFolder = historyFolder;
            NumberOfPairs = numberOfPairs;
            FlatParaToleranceGradeA = flatParaToleranceGradeA;
            FlatParaToleranceGradeB = flatParaToleranceGradeB;
            DistTarget = distTarget;
            DistToleranceA = distToleranceA;
            DistToleranceB = distToleranceB;
        }

        public string ConfigFilePath { get; init; }

        public string SourceFolder { get; init; }
        public string HistoryFolder { get; init; }
        public int NumberOfPairs { get; init; }
        public double FlatParaToleranceGradeA { get; init; }
        public double FlatParaToleranceGradeB { get; init; }
        public double DistTarget { get; init; }
        public double DistToleranceA { get; init; }
        public double DistToleranceB { get; init; }

    }
}
