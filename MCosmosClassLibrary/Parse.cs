using System;
using System.Collections.Generic;
using System.Text;

namespace MCosmosClassLibrary
{
    public class Parse
    {
        public static double? SoleNumber(string s)
        {
            if (double.TryParse(s.Trim(), out double d))
            {
                return d;
            }
            else return null;
        }

        public static double? FirstNumberOfTwo(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 2)
            {
                return SoleNumber(splittings[0]);
            }
            else return null;
        }

        public static double? SecondNumberOfTwo(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 2)
            {
                return SoleNumber(splittings[1]);
            }
            else return null;
        }

        public static double? ThirdNumberOfFour(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 4)
            {
                return SoleNumber(splittings[2]);
            }
            else return null;
        }
    }
}
