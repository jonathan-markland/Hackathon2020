using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace MCosmosClassLibrary
{
    public class FileReader
    {
        private int PositionIndex;
        private FileLine[] FileText;
        public readonly string SourceFilePath;

        public FileReader(string sourceFile)
        {
            SourceFilePath = sourceFile;
            PositionIndex = 0;
            var lines = File.ReadAllLines(sourceFile);
            FileText = lines
                .Select((s, i) => new FileLine { String = s.Trim(), LineNumber = i + 1 })
                .Where(x => !String.IsNullOrEmpty(x.String))
                .ToArray();
        }

        private FileLine Next()
        {
            if (PositionIndex < FileText.Length)
            {
                return FileText[PositionIndex++];
            }
            else
            {
                return null;
            }
        }

        private FileReader NextLineWhere(
            Func<string, bool> condition, 
            Func<string> errorMessageGetter, 
            Action<string> applyActionForRowFound)
        {
            var errorIndex = PositionIndex;

            for(; ;)
            {
                var fileLine = Next();

                if (fileLine != null)
                {
                    if (condition(fileLine.String))
                    {
                        // TODO: hack:
                        var traceMsg = $"Scanning from line {FileText[errorIndex].LineNumber}, line {fileLine.LineNumber} ('{fileLine.String}') satisfied the condition.";
                        System.Diagnostics.Debug.WriteLine(traceMsg);

                        applyActionForRowFound(fileLine.String);
                        return this;
                    }
                }
                else
                {
                    var file = SourceFilePath;
                    var line = FileText[errorIndex].LineNumber;
                    var message = errorMessageGetter();
                    throw new Exception($"{file}({line}): {message}");
                }
            }
        }

        public FileReader ExpectLineStartingWith(string leftSideMatchRequired)
        {
            return NextLineWhere(
                s => s.StartsWith(leftSideMatchRequired), 
                () => $"Could not find a line beginning with '{leftSideMatchRequired}' from this point forwards.",
                _ => { });
        }

        public FileReader ExpectWholeLine(string lineContentRequired)
        {
            return NextLineWhere(
                s => String.Compare(s, lineContentRequired) == 0,
                () => $"Could not find a line containing '{lineContentRequired}' from this point forwards.",
                _ => { });
        }

        public FileReader Parameter(string leftSideMatchRequired, Func<string, double?> parser, out double result)
        {
            var tempSlot = new Box<double>();  // because we can't lambda-bind an 'out' parameter.   TODO: Is there a more C#-native solution?

            void TryParseNumber(string documentRow)
            {
                var rightHandSide = documentRow.Substring(leftSideMatchRequired.Length);
                var parseResult = parser(rightHandSide);
                if (parseResult.HasValue)
                {
                    tempSlot.BoxContent = parseResult.Value;
                }
                else
                {
                    throw new Exception($"Failed to read value after '{leftSideMatchRequired}'."); // TODO: message according to parser type
                }
            }

            var retVal = NextLineWhere(
                s => s.StartsWith(leftSideMatchRequired),
                () => $"Could not find a line beginning with '{leftSideMatchRequired}' from this point forwards.",
                TryParseNumber);

            result = tempSlot.BoxContent;
            return retVal;
        }
    }

    public class Box<T>
    {
        public T BoxContent;
    }


    internal class FileLine
    {
        public string String;
        public int LineNumber;
    }
}
