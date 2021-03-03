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
        public string FlatHead { get; init; }
        public string FlatLbl1 { get; init; }
        public string FlatLbl2 { get; init; }
        public string FlatLbl3 { get; init; }
        public string FlatLbl4 { get; init; }
        public string ParaHead { get; init; }
        public string ParaLbl1 { get; init; }
        public string ParaLbl2 { get; init; }
        public string ParaLbl3 { get; init; }
        public string ParaLbl4 { get; init; }
        public string DistHead1 { get; init; }
        public string DistLbl1 { get; init; }
        public string DistLbl2 { get; init; }
        public string DistLbl3 { get; init; }
        public string DistLbl4 { get; init; }
        public string DistHead2 { get; init; }
        public string DistLbl5 { get; init; }
        public string DistLbl6 { get; init; }
        public string DistLbl7 { get; init; }
        public string DistLbl8 { get; init; }

        public static FileHeadings FileHeadingsForTestFramework = new FileHeadings
        {
            FlatHead = "Flatness - Datums F, E, D & G (as common zones)",
            FlatLbl1 = "Datum F",
            FlatLbl2 = "Datum E",
            FlatLbl3 = "Datum D",
            FlatLbl4 = "Datum G",
            ParaHead = "Parallelism - 4 opposed positions",
            ParaLbl1 = "Datum E LH 1",
            ParaLbl2 = "Datum E RH 1",
            ParaLbl3 = "Datum G FR 1",
            ParaLbl4 = "Datum G BK 1",
            DistHead1 = "Datum E to Datum F - (diagonals at -1.5 & -10.3)",
            DistLbl1 = "E to F at -1.5 LH",
            DistLbl2 = "E to F at -1.5 RH",
            DistLbl3 = "E to F at -10.3 LH",
            DistLbl4 = "E to F at -10.3 RH",
            DistHead2 = "Datum G to Datum D - (diagonals at -1.5 & -10.3)",
            DistLbl5 = "G to D at -1.5 FR",
            DistLbl6 = "G to D at -1.5 BK",
            DistLbl7 = "G to D at -10.3 FR",
            DistLbl8 = "G to D at -10.3 BK",
        };
    }
}
