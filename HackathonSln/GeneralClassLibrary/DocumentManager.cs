using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneralClassLibrary
{
    public class DocumentManager
    {
        private string[] Lines;
        private bool[] LinesConsumed;

        public DocumentManager(string[] lines)
        {
            Lines = lines;
            LinesConsumed = new bool[lines.Length];
        }

        public string AllTextToTheRightOf(string label)
        {
            var locationsWhereFound = LocationsOf.AllTextToTheRightOf(Lines, label);
            EnsureNotConsumedPreviously(locationsWhereFound, () => $"Error:  More than one search tried to fetch data from the '{label}' slot.");
            return ExtractFrom.AllTextToTheRightOf(label, Lines[0]);
        }

        public double ValueUnderneathHeading(string subHeading, string label, int rowValueIndex, int numberOfValuesOnRow)
        {
            var locationsWhereFound = LocationsOf.ValueUnderneathHeading(Lines, subHeading, label, numberOfValuesOnRow);
            EnsureNotConsumedPreviously(locationsWhereFound, () => $"Error:  More than one search tried to fetch data from the '{label}' slot under heading '{subHeading}'.");
            return ExtractFrom.ValueUnderneathHeading(label, rowValueIndex, numberOfValuesOnRow, Lines[0]);
        }

        private void EnsureNotConsumedPreviously(List<int> locationsWhereFound, Func<string> errorMessageGetter)
        {
            foreach(int i in locationsWhereFound)
            {
                if (LinesConsumed[i])
                {
                    throw new System.Exception(errorMessageGetter());
                }
                LinesConsumed[i] = true;
            }
        }
    }




    internal static class LocationsOf
    {
        public static List<int> AllTextToTheRightOf(string[] Lines, string label)
        {
            bool labelSeen = false;
            string labelLine = "";
            var foundLocations = new List<int>();

            var n = Lines.Length;

            for (int i = 0; i < n; i++)
            {
                var thisLine = Lines[i];

                if (Match.AllTextToTheRightOf(label, thisLine))
                {
                    if (labelSeen && labelLine != thisLine)
                    {
                        throw new Exception($"Error: Label '{label}' exists more than once in this file!");
                    }
                    labelLine = thisLine;
                    labelSeen = true;
                    foundLocations.Add(i);
                }
            }

            if (labelSeen)
            {
                return foundLocations;
            }
            else
            {
                throw new Exception($"Cannot find a label '{label}' in this file.");
            }
        }



        public static List<int> ValueUnderneathHeading(
            string[] Lines, string subHeading, string label, int numberOfValuesOnRow)
        {
            bool subHeadingSeen = false;
            bool labelSeen = false;
            string labelLine = "";
            var foundLocations = new List<int>();

            var n = Lines.Length;

            for (int i = 0; i < n; i++)
            {
                var thisLine = Lines[i];

                if (Match.SubHeadingLine(subHeading, thisLine))
                {
                    if (subHeadingSeen)
                    {
                        throw new Exception($"Error: Heading '{subHeading}' exists more than once in this file!");
                    }
                    subHeadingSeen = true;
                }
                else if (Match.Label(label, numberOfValuesOnRow, thisLine) && subHeadingSeen)
                {
                    if (labelSeen && labelLine != thisLine)
                    {
                        throw new Exception($"Error: Label '{label}' exists more than once in this file!");
                    }
                    labelLine = thisLine;
                    labelSeen = true;
                    foundLocations.Add(i);
                }
            }

            if (subHeadingSeen)
            {
                if (labelSeen)
                {
                    return foundLocations;
                }
                else
                {
                    throw new Exception($"Found heading '{subHeading}', but cannot find a label '{label}' with {numberOfValuesOnRow} values on the row.");
                }
            }
            else
            {
                throw new Exception($"Cannot find heading '{subHeading}' while looking for label '{label}'.");
            }
        }
    }



    internal static class Match
    { 
        public static bool AllTextToTheRightOf(string label, string line)
        {
            return line.Contains(label);
        }

        public static bool SubHeadingLine(string subHeading, string line)
        {
            return line.Trim().Equals(subHeading);
        }

        public static bool Label(string label, int numberOfValuesOnRow, string line)
        {
            // TODO: onyl return true if the double values x numberOfValuesOnRow are seen
            return line.Contains(label);
        }
    }



    internal static class ExtractFrom
    {
        public static string AllTextToTheRightOf(string label, string line)
        {
            var i = line.LastIndexOf(label);
            if (i >= 0)
            {
                return line.Substring(i + label.Length).Trim();
            }
            throw new System.Exception($"Failed to obtain text for label, where label is already found."); // Should never happen.
        }

        internal static double ValueUnderneathHeading(string label, int rowValueIndex, int numberOfValuesOnRow, string line)
        {
            System.Diagnostics.Debug.Assert(rowValueIndex < numberOfValuesOnRow);  // The index requested is out of bounds of the number of items you set.

            var i = line.LastIndexOf(label);
            if (i >= 0)
            {
                var remainder = line.Substring(i + label.Length);

                var splittings = remainder.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (splittings.Length == numberOfValuesOnRow)
                {
                    var s = splittings[rowValueIndex];
                    if (double.TryParse(s, out double value))
                    {
                        return value;
                    }
                    throw new System.Exception($"Cannot interpret '{s}' as a numeric field, for label {label}.");
                }
                throw new System.Exception($"Expected {numberOfValuesOnRow} numbers for label '{label}' but only got {splittings.Length}.");
            }
            throw new System.Exception($"Failed to obtain text for label, where label is already found."); // Should never happen.
        }
    }

}
