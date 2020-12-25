using System;
using System.Collections.Generic;
using System.Text;
using MCosmosClassLibrary.Models;

namespace MCosmosClassLibrary
{
    public class Parse
    {
        public static MeasureAndGrade? FlatnessNumber(string s)
        {
            if (double.TryParse(s.Trim(), out double d))
            {
                var grade = ToleranceMathematics.FlatParaGradeFor(d);
                return new MeasureAndGrade(d, grade);
            }
            else return null;
        }

        private static double? SoleNumberX(string s) // TODO: rename
        {
            if (double.TryParse(s.Trim(), out double d))
            {
                return d;
            }
            return null;
        }

        public static MeasureAndGrade? ParallelNumber(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 2)
            {
                var value = SoleNumberX(splittings[1]);
                if (value != null)
                {
                    var grade = ToleranceMathematics.FlatParaGradeFor(value.Value);
                    return new MeasureAndGrade(value.Value, grade);
                }
            }
            return null;
        }

        public static MeasureAndGrade? DistanceNumber(string s)
        {
            var splittings = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splittings.Length == 4)
            {
                var value = SoleNumberX(splittings[2]);
                if (value != null)
                {
                    var grade = ToleranceMathematics.DistanceGradeFor(value.Value);
                    return new MeasureAndGrade(value.Value, grade);
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
