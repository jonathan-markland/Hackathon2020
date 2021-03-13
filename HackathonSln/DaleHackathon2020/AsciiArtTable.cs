using System;
using System.Collections.Generic;
using System.Linq;

namespace DaleHackathon2020
{
    public static class AsciiArtTable
    {
        /// <summary>
        /// Returns an array of the maximum widths of the columns in the collection.
        /// </summary>
        public static int[] MeasuredColumnWidths(IEnumerable<IEnumerable<string>> collection)
        {
            if (collection.Any())
            {
                var firstLine = collection.First();
                var maxWidthsSoFar = firstLine.Select(text => 0).ToArray();
                var numColumns = maxWidthsSoFar.Length;

                int index = 0;
                foreach (var item in collection)
                {
                    if (item.Count() != numColumns)
                    {
                        throw new Exception("Inconsistent column counts encountered in dataset.");
                    }
                    int i = 0;
                    foreach(var item2 in item)
                    {
                        maxWidthsSoFar[i] = Math.Max(maxWidthsSoFar[i], item2.Length);
                        ++i;
                    }
                    ++index;
                }

                return maxWidthsSoFar;
            }
            else
            {
                return new int[] { };
            }
        }

        /// <summary>
        /// Returns a version of the input with space padding applied to bring the 
        /// columns up to the widths given in the array.
        /// </summary>
        public static IEnumerable<string> Format(IEnumerable<string> cellRow, int[] columnWidths)
        {
            var paddedStrings = cellRow.Select((text, i) => {
                var textLength = text.Length;
                var width = columnWidths[i];
                if (textLength > width)
                {
                    throw new Exception("Column data is longer than the maximum width.");  // Should never happen because of expected measurement.
                }
                var paddingWidth = width - textLength;
                return text.PadRight(width);
            });
            return paddedStrings;
        }

        public static void OutputTable<T>(IEnumerable<string> headings, IEnumerable<T> collection, Func<T, int, IEnumerable<string>> rowOfStringsGetter, Action<string> output)
        {
            var headingRows  = new[] { headings };
            var dataRows     = collection.Select(rowOfStringsGetter);
            var fullTable    = Enumerable.Concat(headingRows, dataRows);
            var columnWidths = MeasuredColumnWidths(fullTable);

            int index = 0;
            foreach (var cellRow in fullTable)
            {
                var rowContent = Format(cellRow, columnWidths);
                output(String.Join(" | ", rowContent));
                ++index;
            }
        }
    }
}
