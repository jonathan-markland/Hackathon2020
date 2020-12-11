
namespace MCosmosClassLibrary
{
    public static class CsvGenerator
    {
        public static string Headings
        {
            get { return "\"Serial No.\",\"flatDatumF\",\"flatDatumE\",\"flatDatumD\",\"flatDatumG\",\"paraDatumELH1\",\"paraDatumERH1\",\"paraDatumGFR1\",\"paraDatumGBK1\",\"distEtoFLeft1\",\"distEtoFRight1\",\"distEtoFLeft2\",\"distEtoFRight2\",\"distGtoDFront1\",\"distGtoDBack1\",\"distGtoDFront2\",\"distGtoDBack2\""; }
        }

        public static string CSVLine(this DiscInfo discInfo)
        {
            var serialNo       = discInfo.Metadata.SerialNo;  // TODO: Escape for CSV
            var flatDatumF     = discInfo.Flatness.DatumF;
            var flatDatumE     = discInfo.Flatness.DatumE;
            var flatDatumD     = discInfo.Flatness.DatumD;
            var flatDatumG     = discInfo.Flatness.DatumG;
            var paraDatumELH1  = discInfo.Parallel.DatumELH1;
            var paraDatumERH1  = discInfo.Parallel.DatumERH1;
            var paraDatumGFR1  = discInfo.Parallel.DatumGFR1;
            var paraDatumGBK1  = discInfo.Parallel.DatumGBK1;
            var distEtoFLeft1  = discInfo.Distances.EtoFLeft1;
            var distEtoFRight1 = discInfo.Distances.EtoFRight1;
            var distEtoFLeft2  = discInfo.Distances.EtoFLeft2;
            var distEtoFRight2 = discInfo.Distances.EtoFRight2;
            var distGtoDFront1 = discInfo.Distances.GtoDFront1;
            var distGtoDBack1  = discInfo.Distances.GtoDBack1;
            var distGtoDFront2 = discInfo.Distances.GtoDFront2;
            var distGtoDBack2  = discInfo.Distances.GtoDBack2;
            return $"\"{serialNo}\",\"{flatDatumF:0.00000}\",\"{flatDatumE:0.00000}\",\"{flatDatumD:0.00000}\",\"{flatDatumG:0.00000}\",\"{paraDatumELH1:0.00000}\",\"{paraDatumERH1:0.00000}\",\"{paraDatumGFR1:0.00000}\",\"{paraDatumGBK1:0.00000}\",\"{distEtoFLeft1:0.00000}\",\"{distEtoFRight1:0.00000}\",\"{distEtoFLeft2:0.00000}\",\"{distEtoFRight2:0.00000}\",\"{distGtoDFront1:0.00000}\",\"{distGtoDBack1:0.00000}\",\"{distGtoDFront2:0.00000}\",\"{distGtoDBack2:0.00000}\"";
        }

        /// <summary>
        /// Includes serial number, readings and overall grade of disc.
        /// </summary>
        public static string CSVLine(this DiscGradeInfo disc)
        {
            return disc.Disc.CSVLine() + $",\"{disc.OverallGrade}\"";
        }

        public static string CSVLine(this Pair p)
        {
            var gradeOfDisc1 = p.Disc1.OverallGrade;
            var gradeOfDisc2 = p.Disc2.OverallGrade;
            return $"\"{p.EuclideanDistance}\", \"{p.Disc1.Disc.Metadata.SerialNo}\", \"{gradeOfDisc1.ToString()}\", \"{p.Disc2.Disc.Metadata.SerialNo}\", \"{gradeOfDisc2.ToString()}\"";
        }

        public static string CSVLineDetailed(this DiscGradeInfo discInfo)
        {
            string Field(string s)
            {
                return $"\"{s}\",";
            }

            string ToLetter(DiscGrade dg)
            {
                return $"{GradeToLetter(dg)}";
            }

            string FieldAndGrade(MeasureAndGrade mag)
            {
                return Field($"{mag.Value:0.00000}") + Field(ToLetter(mag.Grade));
            }

            var serialNo = discInfo.Disc.Metadata.SerialNo;  // TODO: Escape for CSV

            var flatDatumF = discInfo.Disc.Flatness.DatumF;
            var flatDatumE = discInfo.Disc.Flatness.DatumE;
            var flatDatumD = discInfo.Disc.Flatness.DatumD;
            var flatDatumG = discInfo.Disc.Flatness.DatumG;
            var paraDatumELH1 = discInfo.Disc.Parallel.DatumELH1;
            var paraDatumERH1 = discInfo.Disc.Parallel.DatumERH1;
            var paraDatumGFR1 = discInfo.Disc.Parallel.DatumGFR1;
            var paraDatumGBK1 = discInfo.Disc.Parallel.DatumGBK1;
            var distEtoFLeft1 = discInfo.Disc.Distances.EtoFLeft1;
            var distEtoFRight1 = discInfo.Disc.Distances.EtoFRight1;
            var distEtoFLeft2 = discInfo.Disc.Distances.EtoFLeft2;
            var distEtoFRight2 = discInfo.Disc.Distances.EtoFRight2;
            var distGtoDFront1 = discInfo.Disc.Distances.GtoDFront1;
            var distGtoDBack1 = discInfo.Disc.Distances.GtoDBack1;
            var distGtoDFront2 = discInfo.Disc.Distances.GtoDFront2;
            var distGtoDBack2 = discInfo.Disc.Distances.GtoDBack2;

            var gradeOverall = ToLetter(discInfo.OverallGrade);

            return
                  Field($"{serialNo}")
                + FieldAndGrade(flatDatumF)
                + FieldAndGrade(flatDatumE)
                + FieldAndGrade(flatDatumD)
                + FieldAndGrade(flatDatumG)
                + FieldAndGrade(paraDatumELH1)
                + FieldAndGrade(paraDatumERH1)
                + FieldAndGrade(paraDatumGFR1)
                + FieldAndGrade(paraDatumGBK1)
                + FieldAndGrade(distEtoFLeft1)
                + FieldAndGrade(distEtoFRight1)
                + FieldAndGrade(distEtoFLeft2)
                + FieldAndGrade(distEtoFRight2)
                + FieldAndGrade(distGtoDFront1)
                + FieldAndGrade(distGtoDBack1)
                + FieldAndGrade(distGtoDFront2)
                + FieldAndGrade(distGtoDBack2)
                + Field($"Overall {gradeOverall}");
        }

        public static string GradeToLetter(DiscGrade dg)
        {
            if (dg == DiscGrade.GradeA) return "(A)";
            if (dg == DiscGrade.GradeB) return "(B)";
            if (dg == DiscGrade.GradeC) return "(C)";
            throw new System.Exception("Cannot obtain grade letter for DiscGrade enum value.");
        }

    }
}
