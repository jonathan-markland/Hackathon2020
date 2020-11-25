using System;
using System.Linq;
using MCosmosClassLibrary;

namespace DaleHackathon2020
{
    class Program
    {
        static void Main(string[] args)
        {
            // var filePath = @"C:\Users\ukjmak\OneDrive - Waters Corporation\Documents\Hackathon 2020 - Dale Beardsall - Quadrupole discs\TEST2 -- output from MCOSMOS software.txt";
            // var loadedFile = MCosmosClassLibrary.DiscFileLoader.LoadFromFile(filePath);

            //var discList = FolderProcessor.FolderToDiscList("ExampleFiles");
            var discList = ExampleFilesCollection.DalesSpreadsheetProvider.GroundAtStruder();
            var pairings = Mathematics.ListOfMatchedPairs(discList.ToList());

            foreach(Pair p in pairings)
            {
                // TODO: Escape for CSV
                Console.WriteLine($"\"{p.EuclideanDistance}\", \"{p.Disc1.Metadata.SerialNo}\", \"{p.Disc2.Metadata.SerialNo}\"");
            }

            //foreach(var line in csv)
            //{
            //    Console.WriteLine(line);
            //}


            Console.WriteLine("Complete");
        }
    }
}
