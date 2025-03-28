﻿using MCosmosClassLibrary.Models;

namespace TestProject
{
    public static class CsvGenerator
    {
        public static string Headings
        {
            get { return "\"Serial No.\",\"flatDatumF\",\"flatDatumE\",\"flatDatumD\",\"flatDatumG\",\"paraDatumELH1\",\"paraDatumERH1\",\"paraDatumGFR1\",\"paraDatumGBK1\",\"distEtoFLeft1\",\"distEtoFRight1\",\"distEtoFLeft2\",\"distEtoFRight2\",\"distGtoDFront1\",\"distGtoDBack1\",\"distGtoDFront2\",\"distGtoDBack2\""; }
        }

        public static string CSVLineOnItsOwn(this DiscInfo discInfo)  // TODO: rename
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
            return $"\"{serialNo}\",\"{flatDatumF.Value:0.00000}\",\"{flatDatumE.Value:0.00000}\",\"{flatDatumD.Value:0.00000}\",\"{flatDatumG.Value:0.00000}\",\"{paraDatumELH1.Value:0.00000}\",\"{paraDatumERH1.Value:0.00000}\",\"{paraDatumGFR1.Value:0.00000}\",\"{paraDatumGBK1.Value:0.00000}\",\"{distEtoFLeft1.Value:0.00000}\",\"{distEtoFRight1.Value:0.00000}\",\"{distEtoFLeft2.Value:0.00000}\",\"{distEtoFRight2.Value:0.00000}\",\"{distGtoDFront1.Value:0.00000}\",\"{distGtoDBack1.Value:0.00000}\",\"{distGtoDFront2.Value:0.00000}\",\"{distGtoDBack2.Value:0.00000}\"";
        }

        /// <summary>
        /// Includes serial number, readings and overall grade of disc.
        /// </summary>
        public static string CSVLineWithOverallGrade(this DiscInfo disc)
        {
            // TODO: There is no companion that provides the headings for this case.

            return disc.CSVLineOnItsOwn() + $",\"{disc.OverallGrade}\"";
        }

        public static string CSVLine(this Pair p)
        {
            var gradeOfDisc1 = p.Disc1.OverallGrade;
            var gradeOfDisc2 = p.Disc2.OverallGrade;
            return $"\"{p.EuclideanDistance}\", \"{p.Disc1.Metadata.SerialNo}\", \"{gradeOfDisc1.ToString()}\", \"{p.Disc2.Metadata.SerialNo}\", \"{gradeOfDisc2.ToString()}\"";
        }

        // TODO: Use this in the test framework.  We should capture all the grades.

        public static string CSVLineDetailed(this DiscInfo discInfo)
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

            var serialNo = discInfo.Metadata.SerialNo;  // TODO: Escape for CSV

            var flatDatumF = discInfo.Flatness.DatumF;
            var flatDatumE = discInfo.Flatness.DatumE;
            var flatDatumD = discInfo.Flatness.DatumD;
            var flatDatumG = discInfo.Flatness.DatumG;
            var paraDatumELH1 = discInfo.Parallel.DatumELH1;
            var paraDatumERH1 = discInfo.Parallel.DatumERH1;
            var paraDatumGFR1 = discInfo.Parallel.DatumGFR1;
            var paraDatumGBK1 = discInfo.Parallel.DatumGBK1;
            var distEtoFLeft1 = discInfo.Distances.EtoFLeft1;
            var distEtoFRight1 = discInfo.Distances.EtoFRight1;
            var distEtoFLeft2 = discInfo.Distances.EtoFLeft2;
            var distEtoFRight2 = discInfo.Distances.EtoFRight2;
            var distGtoDFront1 = discInfo.Distances.GtoDFront1;
            var distGtoDBack1 = discInfo.Distances.GtoDBack1;
            var distGtoDFront2 = discInfo.Distances.GtoDFront2;
            var distGtoDBack2 = discInfo.Distances.GtoDBack2;

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
