using System;
using System.Linq;
using MCosmosClassLibrary;

namespace DaleHackathon2020
{
    class Program
    {
        // TODO: Explain why a disc is the grade it is.
        // 

        static void Main(string[] args)
        {
            var primaryList = ExampleFilesCollection.DalesSpreadsheetProvider.GroundAtStruder();

            foreach (DiscInfo disc in primaryList)
            {
                Console.WriteLine(disc.CSVLineWithGrade());
            }

            var filteredList = primaryList.IncludingGradeAandBonly();
            var pairings = filteredList.AsListOfMatchedPairs();

            foreach(Pair p in pairings)
            {
                Console.WriteLine(p.CSVLine());
            }

            Console.WriteLine("Complete");
        }
    }
}
