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
