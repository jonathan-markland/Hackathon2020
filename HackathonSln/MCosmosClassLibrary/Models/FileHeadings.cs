using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCosmosClassLibrary.Models
{
    /// <summary>
    /// The file headings are configurable.  These are expected by the file parser.
    /// </summary>
    public class FileHeadings
    {
        public string ProgramNameLabel { get; init; }
        public string DateTimeLabel { get; init; }
        public string DrawingNumberLabel { get; init; }
        public string SerialNumberLabel { get; init; }
        public string IssueNumberLabel { get; init; }
        public string DescriptionLabel { get; init; }

        public string FlatSubHeading1 { get; init; }
        public string FlatSubHeading2 { get; init; }
        public string FlatSubHeading3 { get; init; }
        public string FlatSubHeading4 { get; init; }
        public string FlatValueLabel { get; init; }

        public string ParaHeading { get; init; }
        public string ParaLabel1 { get; init; }
        public string ParaLabel2 { get; init; }
        public string ParaLabel3 { get; init; }
        public string ParaLabel4 { get; init; }

        public string DistHeading1 { get; init; }
        public string DistLabel1 { get; init; }
        public string DistLabel2 { get; init; }
        public string DistLabel3 { get; init; }
        public string DistLabel4 { get; init; }
        public string DistHeading2 { get; init; }
        public string DistLabel5 { get; init; }
        public string DistLabel6 { get; init; }
        public string DistLabel7 { get; init; }
        public string DistLabel8 { get; init; }

        public static FileHeadings FileHeadingsForTestFramework = new FileHeadings
        {
            ProgramNameLabel   = "Program Name   :",
            DateTimeLabel      = "Date / Time    :",
            DrawingNumberLabel = "Drawing No     :",
            SerialNumberLabel  = "Serial No      :",
            IssueNumberLabel   = "Issue No       :",
            DescriptionLabel   = "Description    :",

            FlatSubHeading1 = "Datum F",
            FlatSubHeading2 = "Datum E",
            FlatSubHeading3 = "Datum D",
            FlatSubHeading4 = "Datum G",
            FlatValueLabel  = "Flatness",
            
            ParaHeading = "Parallelism - 4 opposed positions",
            ParaLabel1 = "Datum E LH 1",
            ParaLabel2 = "Datum E RH 1",
            ParaLabel3 = "Datum G FR 1",
            ParaLabel4 = "Datum G BK 1",
            
            DistHeading1 = "Datum E to Datum F - (diagonals at -1.5 & -10.3)",
            DistLabel1 = "E to F at -1.5 LH",
            DistLabel2 = "E to F at -1.5 RH",
            DistLabel3 = "E to F at -10.3 LH",
            DistLabel4 = "E to F at -10.3 RH",

            DistHeading2 = "Datum G to Datum D - (diagonals at -1.5 & -10.3)",
            DistLabel5 = "G to D at -1.5 FR",
            DistLabel6 = "G to D at -1.5 BK",
            DistLabel7 = "G to D at -10.3 FR",
            DistLabel8 = "G to D at -10.3 BK",
        };
    }
}
