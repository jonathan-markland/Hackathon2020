using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCosmosClassLibrary.Models
{
    /// <summary>
    /// A matched pair of discs, including all related information.
    /// </summary>
    public class Pair
    {
        public Pair(DiscInfo disc1, DiscInfo disc2)
        {
            Disc1 = disc1;
            Disc2 = disc2;
            EuclideanDistance =
                PairingMathematics.EuclideanDistanceBetween(
                    disc1.Distances, disc2.Distances);
        }

        public readonly DiscInfo Disc1;
        public readonly DiscInfo Disc2;
        public readonly double EuclideanDistance;
    }
}
