using System;
using System.Linq;
using MCosmosClassLibrary;

namespace DaleHackathon2020
{
    class Program
    {
        // TODO: Uniqueness validation on the SerialNos.

        // TODO: Process output:  Explain why a disc is the grade it is. (HTML colours on pre-filtered data).
        // TODO: Process output:  Summary list of those DiscGradeInfos that were paired off this time.
        // TODO: Process output:  Summary list of those DiscGradeInfos that were left over.

        static void Main(string[] args)
        {
            var primaryList = ExampleFilesCollection.DalesSpreadsheetProvider.GroundAtStruder();

            var gradedPrimaryList = primaryList.Select(disc => disc.DiscGradeInfo());

            foreach (DiscGradeInfo disc in gradedPrimaryList)
            {
                Console.WriteLine(disc.CSVLineDetailed());
            }



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
