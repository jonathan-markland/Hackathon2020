using System;
using MCosmosClassLibrary.Models;

namespace MCosmosClassLibrary
{
    public class Parse
    {
        private static double? SoleNumberX(string s) // TODO: rename
        {
            if (double.TryParse(s.Trim(), out double d))
            {
                return d;
            }
            return null;
        }

        public static FlatnessMeasure? FlatnessNumber(string s)
        {
            if (double.TryParse(s.Trim(), out double d))
            {
                return new FlatnessMeasure(d);
            }
            else return null;
        }

        public static ParallelMeasure? ParallelNumber(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 2)
            {
                var value = SoleNumberX(splittings[1]);
                if (value != null)
                {
                    return new ParallelMeasure(value.Value);
                }
            }
            return null;
        }

        public static DistanceMeasure? DistanceNumber(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 4)
            {
                var value = SoleNumberX(splittings[2]);
                if (value != null)
                {
                    return new DistanceMeasure(value.Value);
                }
            }
            return null;
        }

        /// <summary>
        /// Partial application in C#!
        /// </summary>
        public static Func<string,StringBox?> StringFromSecondColumn(string secondColumnLabelText)
        {
            StringBox? parseFunc(string s)
            {
                var i = s.LastIndexOf(secondColumnLabelText);
                if (i >= 0)
                {
                    return new StringBox
                    {
                        Value = s.Substring(i + secondColumnLabelText.Length).Trim()
                    };
                }
                else
                {
                    return null;
                }
            }

            return parseFunc;
        }
    }

    /// <summary>
    /// TODO: Find out what #nullable context is in C# 8.0
    /// </summary>
    public struct StringBox
    {
        public string Value;
    }

}
