using System;
using System.Linq;
using System.Collections.Generic;
using MCosmosClassLibrary;
using MCosmosClassLibrary.Algorithms;
using MCosmosClassLibrary.Services;
using MCosmosClassLibrary.Models;
using System.Collections.ObjectModel;

namespace DaleHackathon2020
{
    class Program
    {
        // TODO: Process output:  Explain why a disc is the grade it is. (HTML colours on pre-filtered data).

        private static string ConfigFileName = "Config.txt";

        static int Main(string[] args)
        {
            try
            {
                MainProcess();
                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        static void MainProcess()
        {
            var configFile = ConfigFile.Load(ConfigFileName);
            
            Library.CheckFolderPathAccess("Source", configFile.SourceFolderPath);
            Library.CheckFolderPathAccess("History", configFile.HistoryFolderPath);
            
            Library.CheckFileCountIsBelowThresholdForFolder(configFile.SourceFolderPath, 1000);

            var content = BatchFolderLoader.LoadDiscsFromFolder(
                configFile.SourceFolderPath,
                configFile.FileHeadings);

            var error = content as BatchOverallError;
            if (error != null)
            {
                // This means there is no specific file in error.  Something general happened.
                throw new Exception(error.OverallErrorMessage);
            }

            var batch = content as Batch;
            if (batch == null)
            {
                throw new Exception("Failed to obtain the batch information.");  // Should never happen because it's either this class or the above!
            }

            var outputPath = Library.OutputPathForCurrentTime(configFile.HistoryFolderPath);
            Library.AttemptToCreateDistinctFolder("the pairings output folder", outputPath);
            Library.AttemptFileCopy(configFile.ConfigFilePath, outputPath, "Copy of Config file that was used on this date.txt");
            Library.EnsureDiscSerialNumbersAreUnique(batch.Discs);

            var pairings = batch.Discs.AsListOfMatchedPairs().Take(configFile.NumberOfPairs);

            void discLoadingReport(System.IO.StreamWriter streamWriter)
            {
                DiscLoadingReport(configFile.SourceFolderPath, outputPath, batch, streamWriter);
            }

            void discPairingReport(System.IO.StreamWriter streamWriter)
            {
                DiscPairingReport(pairings, streamWriter);
            }

            GenerateAndSaveReportFile(discLoadingReport, outputPath, "disc-loading-report.txt");
            GenerateAndSaveReportFile(discPairingReport, outputPath, "disc-pairing-report.txt");

            MovePairedToOutputFolder(pairings, outputPath);

            Console.WriteLine($"Success!  Please see file set at:  {outputPath}");
        }

        private static void GenerateAndSaveReportFile(
            Action<System.IO.StreamWriter> reportGeneratorForBatch, 
            string outputPath,
            string fileName)
        {
            var discLoadingReportFilePath = System.IO.Path.Combine(outputPath, fileName);
            using (var outputStream = new System.IO.StreamWriter(discLoadingReportFilePath))
            {
                reportGeneratorForBatch(outputStream);
            }
        }



        private static void DiscLoadingReport(string sourceFolderPath, string outputFolderPath, Batch batch, System.IO.StreamWriter outputStream)
        {
            void output(string s)
            {
                outputStream.WriteLine(s);
            }

            void blank()
            {
                output("");
            }

            void heading(string s, char underscore)
            {
                blank();
                blank();
                output(s);
                output(new string(underscore, s.Length));
                blank();
            }

            void explanationForSuccessfulLoadSection()
            {
                output("This section contains serial numbers and information about");
                output("the CMM disc files that were in the source folder");
                output("that are understood by this software.");
                blank();
                output("Only these files were considered for pairing.");
                output("There may be more files listed than were needed in this round.");
                blank();
            }

            void explanationForErrorSection()
            {
                output("This section contains lists of the files that have not");
                output("been loaded successfully, and why.  The number in brackets");
                output("is the line number within the file of the error.");
                blank();
                output("NOTE: - These files will NOT be considered for pairing!");
                output("      - Please consider removing these files from the source folder.");
                blank();
            }

            void collection<T>(string title, Action explanationEmitter, IEnumerable<string> headings, IEnumerable<T> collection, Func<T, int, List<string>> rowOfStringsGetter)
            {
                if (collection.Any())
                {
                    heading(title, '-');
                    explanationEmitter();
                    AsciiArtTable.OutputTable(headings, collection, rowOfStringsGetter, output);
                }
            }

            void ifEmpty<T>(IEnumerable<T> collection, Action messageOutputter)
            {
                if (!collection.Any())
                {
                    messageOutputter();
                }
            }

            List<string> loadingReportHeadings = new List<string>
            {
                "", 
                "Serial No.", 
                "Overall Disc Grade",

                "Flat F",
                "Flat E",
                "Flat D",
                "Flat G",

                "Parallel ELH1",
                "Parallel ERH1",
                "Parallel GFR1",
                "Parallel GBK1",

                "E to F Left 1",
                "E to F Right 1",
                "E to F Left 2",
                "E to F Right 2",
                "G to D Front 1",
                "G to D Back 1",
                "G to D Front 2",
                "G to D Back 2",
            };

            List<string> loadingReportLine(DiscInfo disc, int index)
            {
                return new List<string>
                {
                    $"{index+1}",
                    
                    $"{disc.Metadata.SerialNo}",
                    
                    $"{disc.OverallGrade.ToGradeLetter()}",

                    $"{disc.Flatness.DatumF.ToReport()}",
                    $"{disc.Flatness.DatumE.ToReport()}",
                    $"{disc.Flatness.DatumD.ToReport()}",
                    $"{disc.Flatness.DatumG.ToReport()}",

                    $"{disc.Parallel.DatumELH1.ToReport()}", 
                    $"{disc.Parallel.DatumERH1.ToReport()}", 
                    $"{disc.Parallel.DatumGFR1.ToReport()}", 
                    $"{disc.Parallel.DatumGBK1.ToReport()}", 

                    $"{disc.Distances.EtoFLeft1.ToReport()}",
                    $"{disc.Distances.EtoFRight1.ToReport()}",
                    $"{disc.Distances.EtoFLeft2.ToReport()}",
                    $"{disc.Distances.EtoFRight2.ToReport()}",
                    $"{disc.Distances.GtoDFront1.ToReport()}",
                    $"{disc.Distances.GtoDBack1.ToReport()}",
                    $"{disc.Distances.GtoDFront2.ToReport()}",
                    $"{disc.Distances.GtoDBack2.ToReport()}",
                };
            }

            List<string> errorHeadings = new List<string>
            {
                "Error Information",
            };

            List<string> fileProcessingErrorLine(FileProcessingError error, int index)   // This fits with the table outputting, but is probably not massively worthwhile.
            {
                return new List<string>
                {
                    $"{error.PathToErrantFile}: {error.Error}",
                };
            }



            heading("FILE LOADING REPORT", '=');

            output($"Source folder was:  {sourceFolderPath}");
            output($"Output folder was:  {outputFolderPath}");

            collection(
                $"{batch.Discs.Count()} x CMM disc files successfully loaded", 
                explanationForSuccessfulLoadSection,
                loadingReportHeadings,
                batch.Discs,
                loadingReportLine);

            ifEmpty(
                batch.Discs,
                () => output("No CMM files were loaded this time."));

            collection(
                "Files in error",
                explanationForErrorSection,
                errorHeadings,
                batch.FileProcessingErrors,
                fileProcessingErrorLine);

            ifEmpty(
                batch.FileProcessingErrors,
                () => output("No files have errors."));
        }



        private static void DiscPairingReport(IEnumerable<Pair> pairings, System.IO.StreamWriter outputStream)
        {
            void output(string s)
            {
                outputStream.WriteLine(s);
            }

            void blank()
            {
                output("");
            }

            void heading(string headingText, char underlineChar)
            {
                blank();
                blank();
                output(headingText);
                output(new string(underlineChar, headingText.Length));
                blank();
            }

            void explanationForPairedSection()
            {
                output("This section contains a list of all successful pairings.");
                blank();
                output("The serial numbers of each pair are indicated along with");
                output("the disc overall grades.  The closeness of the match is");
                output("indicated by the Euclidean Distance value, with the best");
                output("match shown first.");
                blank();
            }

            void collection<T>(string title, Action explanationEmitter, IEnumerable<string> headings, IEnumerable<T> collection, Func<T, int, List<string>> rowOfStringsGetter)
            {
                if (collection.Any())
                {
                    heading(title, '-');
                    explanationEmitter();
                    AsciiArtTable.OutputTable(headings, collection, rowOfStringsGetter, output);
                }
            }

            void ifEmpty<T>(IEnumerable<T> collection, Action messageOutputter)
            {
                if (! collection.Any())
                {
                    messageOutputter();
                }
            }

            List<string> pairingHeadings = new List<string>
            {
                "", "Euclidean Distance", "Disc Grade", "Disc Serial No.", "Partner Serial No.", "Partner Grade"
            };

            List<string> pairingLine(Pair pair, int index)
            {
                return new List<string>
                {
                    $"{index+1}",
                    $"{pair.EuclideanDistance}",
                    $"{pair.Disc1.OverallGrade.ToGradeLetter()}",
                    $"{pair.Disc1.Metadata.SerialNo}",
                    $"{pair.Disc2.Metadata.SerialNo}",
                    $"{pair.Disc2.OverallGrade.ToGradeLetter()}",
                };
            }



            heading("DISC PAIRING REPORT", '=');

            collection(
                $"{pairings.Count()} x pairings successfully established",
                explanationForPairedSection,
                pairingHeadings,
                pairings,
                pairingLine);

            ifEmpty(
                pairings,
                () => output("No pairings could be established."));
        }



        private static void MovePairedToOutputFolder(
            IEnumerable<Pair> pairings, 
            string outputPath)
        {
            void move(DiscInfo disc)
            {
                var sourcePath = disc.Metadata.OriginFilePath;
                var fileName = System.IO.Path.GetFileName(sourcePath);
                var destPath = System.IO.Path.Combine(outputPath, fileName);
                System.IO.File.Move(sourcePath, destPath);
            }

            foreach(var pair in pairings)
            {
                move(pair.Disc1);
                move(pair.Disc2);
            }
        }

    }



    public static class DiscGradeExtensionMethods
    {
        public static string ToGradeLetter(this DiscGrade discGrade)
        {
            switch (discGrade)
            {
                case DiscGrade.GradeA: return "(A)";
                case DiscGrade.GradeB: return "(B)";
                case DiscGrade.GradeC: return "(C)";
                default: throw new System.Exception("Disc grade is unknown to the system.");  // Will never happen unless enum extended.
            }
        }
    }



    public static class MeasureAndGradeExtensionMethods
    {
        public static string ToReport(this MeasureAndGrade measureAndGrade)
        {
            return $"{measureAndGrade.Value} {measureAndGrade.Grade.ToGradeLetter()}";
        }
    }
}
