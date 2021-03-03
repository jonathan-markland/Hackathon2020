using System;
using System.Linq;
using MCosmosClassLibrary;
using MCosmosClassLibrary.Algorithms;

namespace DaleHackathon2020
{
    class Program
    {
        // TODO: Uniqueness validation on the SerialNos.

        // TODO: Obtain configured source folder path
        // TODO: Obtain configured output folder path
        // TODO: Test access right to output folder by creating date/timestamped output folder (without allowing overwriting)
        // TODO: Parse and load the input folder
        // TODO: Do pairing
        // TODO: Emit report summary file to output folder.
        // TODO: Move the successfully paired files to the output folder.


        // TODO: Process output:  Explain why a disc is the grade it is. (HTML colours on pre-filtered data).
        // TODO: Process output:  Summary list of those DiscGradeInfos that were paired off this time.
        // TODO: Process output:  Summary list of those DiscGradeInfos that were left over.

        private static string ConfigFileName = "Config.txt";

        static void Main(string[] args)
        {
            try
            {
                MainProcess();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void MainProcess()
        {
            var configFile = ConfigFile.Load(ConfigFileName);
            
            Library.CheckFolderPathAccess("Source", configFile.SourceFolder);
            Library.CheckFolderPathAccess("History", configFile.HistoryFolder);


        }

    }
}
