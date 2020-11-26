using System;
using System.Collections.Generic;
using System.Text;

namespace MCosmosClassLibrary
{
    public enum DiscGrade
    {
        GradeA, GradeB, GradeC
    }

    public class DiscGradeInfo
    {
        public DiscInfo Disc;
        public FlatnessGrades FlatnessGrades;
        public ParallelGrades ParallelGrades;
        public DistanceGrades DistanceGrades;
        public DiscGrade OverallGrade;
    }

    public struct FlatnessGrades
    {
        public DiscGrade DatumF;
        public DiscGrade DatumE;
        public DiscGrade DatumD;
        public DiscGrade DatumG;
    }

    public struct ParallelGrades
    {
        public DiscGrade DatumELH1;
        public DiscGrade DatumERH1;
        public DiscGrade DatumGFR1;
        public DiscGrade DatumGBK1;
    }

    public struct DistanceGrades
    {
        public DiscGrade EtoFLeft1;
        public DiscGrade EtoFRight1;
        public DiscGrade EtoFLeft2;
        public DiscGrade EtoFRight2;
        public DiscGrade GtoDFront1;
        public DiscGrade GtoDBack1;
        public DiscGrade GtoDFront2;
        public DiscGrade GtoDBack2;
    }
}
