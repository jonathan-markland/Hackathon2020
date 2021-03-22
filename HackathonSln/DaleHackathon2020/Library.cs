using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using MCosmosClassLibrary.Models;

namespace DaleHackathon2020
{
    public static class Library
    {
        public static void CheckFolderPathAccess(string folderDescription, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new System.Exception($"Cannot find or access the {folderDescription} folder:  {folderPath}");
            }
        }

        public static void CheckFileCountIsBelowThresholdForFolder(string folderPath, int maxCount)
        {
            var actualCount = Directory.EnumerateFiles(folderPath).Count();
            if (actualCount > maxCount)
            {
                throw new System.Exception($"Folder contains too many files, limit is {maxCount}.  Please see folder: {folderPath}");
            }
        }

        public static string FolderNameForCurrentTime()
        {
            var now = System.DateTime.Now;
            var date = now.ToString("yyyy-MM-dd");
            var time = now.ToString("HH-mm-ss");
            return $"pair-report-{date}-at-{time}";
        }

        public static string OutputPathForCurrentTime(string historyFolderPath)
        {
            var subFolder = FolderNameForCurrentTime();
            return Path.Combine(historyFolderPath, subFolder);
        }
        
        public static void AttemptToCreateDistinctFolder(string folderDescription, string outputPath)
        {
            if (Directory.Exists(outputPath))
            {
                throw new System.Exception($"Error:  Folder '{outputPath}' already exists, so will not do any work so as to protect the contents.");
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(outputPath);
                }
                catch(IOException e)
                {
                    throw new System.Exception($"Error:  Cannot create {folderDescription} at location '{outputPath}' because of error:  {e.Message}");
                }
            }
        }

        public static void AttemptFileCopy(string sourceFilePath, string outputFolderPath, string outputFileName)
        {
            var outputFilePath = Path.Combine(outputFolderPath, outputFileName);

            try
            {
                File.Copy(sourceFilePath, outputFilePath);
            }
            catch(IOException e)
            {
                throw new System.Exception($"Error:  Cannot copy file to '{outputFolderPath}' because of error:  {e.Message}");
            }
        }

        public static void EnsureDiscSerialNumbersAreUnique(ReadOnlyCollection<DiscInfo> discs)
        {
            var nonUniqueSerialNumbers = 
                discs.Select(disc => disc.Metadata.SerialNo)
                    .GroupBy(x => x)
                    .Where(g => g.Count() > 1)
                    .ToDictionary(x => x.Key, y => y.Count())
                    .Select(x => x.Key)
                    .OrderBy(x => x)
                    .ToList();

            if (nonUniqueSerialNumbers.Count() > 0)
            {
                var problemSerialNumbers = string.Join(", ", nonUniqueSerialNumbers);
                throw new System.Exception($"Error:  The file set has duplicate serial numbers.  Please search for files with serial number(s):  {problemSerialNumbers}");
            }
        }
    }
}
