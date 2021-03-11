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


        // DONE: Obtain configured source folder path
        // DONE: Obtain configured output folder path
        // DONE: Test access right to output folder by creating date/timestamped output folder (without allowing overwriting)
        // TODO: Parse and load the input folder
        // TODO: Uniqueness validation on the SerialNos.
        // TODO: Do pairing
        // TODO: Emit report summary file to output folder.
        // TODO: Move the successfully paired files to the output folder.


        // TODO: Process output:  Explain why a disc is the grade it is. (HTML colours on pre-filtered data).
        // TODO: Process output:  Summary list of those DiscGradeInfos that were paired off this time.
        // TODO: Process output:  Summary list of those DiscGradeInfos that were left over.

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

            void discLoadingReport(System.IO.StreamWriter streamWriter)
            {
                DiscLoadingReport(batch, streamWriter);
            }

            void discPairingReport(System.IO.StreamWriter streamWriter)
            {
                var pairings = batch.Discs.AsListOfMatchedPairs().Take(configFile.NumberOfPairs);
                DiscPairingReport(pairings, streamWriter);
            }

            GenerateAndSaveReportFile(discLoadingReport, outputPath, "disc-loading-report.txt");
            GenerateAndSaveReportFile(discPairingReport, outputPath, "disc-pairing-report.txt");

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



        private static void DiscLoadingReport(Batch batch, System.IO.StreamWriter outputStream)
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
                output("This section contains serial numbers and information");
                output("of all CMM disc files currently in the source folder");
                output("that can be successfully understood by this software.");
                blank();
                output("Only these files will be considered for pairing.");
                output("There may be more files listed than needed in this round.");
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

            void collection<T>(string title, Action explanationEmitter, IEnumerable<T> collection, Action<T> outputter)
            {
                if (collection.Any())
                {
                    heading(title, '-');

                    explanationEmitter();

                    foreach (T item in collection)
                    {
                        outputter(item);
                    }
                }
            }

            void ifEmpty<T>(IEnumerable<T> collection, Action messageOutputter)
            {
                if (!collection.Any())
                {
                    messageOutputter();
                }
            }


            heading("FILE LOADING REPORT", '=');

            collection(
                $"{batch.Discs.Count()} x CMM disc files successfully loaded", 
                explanationForSuccessfulLoadSection,
                batch.Discs, 
                disc => output($"{disc.Metadata.SerialNo}    {disc.OverallGrade.ToGradeLetter()}"));

            ifEmpty(
                batch.Discs,
                () => output("No CMM files were loaded this time."));

            collection(
                "Files in error",
                explanationForErrorSection,
                batch.FileProcessingErrors,
                error => output(error.Error));
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

            void heading(string s, char underscore)
            {
                blank();
                blank();
                output(s);
                output(new string(underscore, s.Length));
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

            void collection<T>(string title, Action explanationEmitter, IEnumerable<T> collection, Action<T, int> outputter)
            {
                if (collection.Any())
                {
                    heading(title, '-');

                    explanationEmitter();

                    int index = 0;
                    foreach (T item in collection)
                    {
                        outputter(item, index);
                        ++index;
                    }
                }
            }

            void ifEmpty<T>(IEnumerable<T> collection, Action messageOutputter)
            {
                if (! collection.Any())
                {
                    messageOutputter();
                }
            }

            void pairingLine(Pair pair, int index)
            {
                var euclid = pair.EuclideanDistance;

                var serial1 = pair.Disc1.Metadata.SerialNo;
                var serial2 = pair.Disc2.Metadata.SerialNo;

                var grade1 = pair.Disc1.OverallGrade.ToGradeLetter();
                var grade2 = pair.Disc2.OverallGrade.ToGradeLetter();

                output($"{index+1}.  {euclid}   {grade1} {serial1} <=> {serial2} {grade2}");
            }



            heading("DISC PAIRING REPORT", '=');

            collection(
                $"{pairings.Count()} x pairings successfully established",
                explanationForPairedSection,
                pairings,
                pairingLine);

            ifEmpty(
                pairings,
                () => output("No pairings could be established."));
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

}
