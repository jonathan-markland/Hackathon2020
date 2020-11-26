using System;
using System.Collections.Generic;
using System.Linq;

namespace MCosmosClassLibrary
{
    public static class PairingMathematics
    {
        public static double EuclideanDistanceBetween(
            DistanceMeasurements disc1, 
            DistanceMeasurements disc2)
        {
            double squared(double v) { return v * v; }

            var d1 = squared(disc1.EtoFLeft1  - disc2.EtoFLeft1);
            var d2 = squared(disc1.EtoFRight1 - disc2.EtoFRight1);
            var d3 = squared(disc1.EtoFLeft2  - disc2.EtoFLeft2);
            var d4 = squared(disc1.EtoFRight2 - disc2.EtoFRight2);
            var d5 = squared(disc1.GtoDFront1 - disc2.GtoDFront1);
            var d6 = squared(disc1.GtoDBack1  - disc2.GtoDBack1);
            var d7 = squared(disc1.GtoDFront2 - disc2.GtoDFront2);
            var d8 = squared(disc1.GtoDBack2  - disc2.GtoDBack2);

            var sumDiffs = d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8;

            return Math.Sqrt(sumDiffs);  // TODO: Don't SQRT, just sort on the SQUARES.  Sqrt only on the pairs that make it to the final report.
        }

        public static List<Pair> AsListOfMatchedPairs(this IEnumerable<DiscGradeInfo> discs)
        {
            var discList = discs.ToList();
            var n = discList.Count;
            var resultCount = ((n * n) - n) / 2;
            var pairs = new List<Pair>(resultCount);

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    pairs.Add(new Pair(discList[i], discList[j]));
                }
            }

            return 
                pairs
                    .OrderBy(pair => pair.EuclideanDistance)
                    .WithoutRepeatUsages()
                    .ToList();
        }

        public static IEnumerable<Pair> WithoutRepeatUsages(this IOrderedEnumerable<Pair> pairs)
        {
            var alreadySeen = new List<DiscGradeInfo>();  // TODO: Possibly use associative container, suspect limited benefit at this time.

            foreach(Pair p in pairs)
            {
                if (!(alreadySeen.Contains(p.Disc1) || alreadySeen.Contains(p.Disc2)))  // NB: Is OK to do an address comparison.
                {
                    yield return p;
                    alreadySeen.Add(p.Disc1);
                    alreadySeen.Add(p.Disc2);
                }
            }
        }
    }

    /// <summary>
    /// A matched pair of discs, including all related information.
    /// </summary>
    public class Pair
    {
        public Pair(DiscGradeInfo disc1, DiscGradeInfo disc2)
        {
            Disc1 = disc1;
            Disc2 = disc2;
            EuclideanDistance =
                PairingMathematics.EuclideanDistanceBetween(
                    disc1.Disc.Distances, disc2.Disc.Distances);
        }

        public readonly DiscGradeInfo Disc1;
        public readonly DiscGradeInfo Disc2;
        public readonly double EuclideanDistance;
    }
}
