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

            var gradedPrimaryList = primaryList.Select(disc => disc.DiscGradeInfo());

            // foreach (DiscGradeInfo disc in gradedPrimaryList)
            // {
            //     Console.WriteLine(disc.CSVLine());
            // }

            var filteredList = gradedPrimaryList.IncludingGradeAandBonly();

            foreach (DiscGradeInfo disc in filteredList)
            {
                Console.WriteLine(disc.CSVLine());
            }


            var pairings = filteredList.AsListOfMatchedPairs();

            foreach(Pair p in pairings)
            {
                Console.WriteLine(p.CSVLine());
            }

            Console.WriteLine("Complete");
        }
    }
}
