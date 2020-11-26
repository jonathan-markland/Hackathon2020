using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCosmosClassLibrary
{
    public class FolderProcessor
    {
        public static IEnumerable<DiscInfo> FolderToDiscList(string folderPath)
        {
            foreach (string filePath in Directory.EnumerateFiles(folderPath))
            {
                yield return DiscFileLoader.LoadFromFile(filePath);
            }
        }

        public static IEnumerable<string> FolderToCSV(string folderPath)
        {
            yield return CsvGenerator.Headings;

            foreach(DiscInfo discInfo in FolderToDiscList(folderPath))
            {
                yield return discInfo.CSVLine();
            }
        }
    }
}
