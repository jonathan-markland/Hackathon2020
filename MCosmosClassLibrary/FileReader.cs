using System;
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
            return (PositionIndex < FileText.Length) ? FileText[PositionIndex++] : null;
        }

        private FileReader NextLineWhere(
            Func<string, bool> condition, 
            Func<string> errorMessageGetter, 
            Action<string> applyActionForRowFound)
        {
            WithFileAndLineNumberExceptionReporting(
                () => 
                {
                    var traceIndex = PositionIndex; // TODO: remove

                    for (; ; )
                    {
                        var fileLine = Next();

                        if (fileLine != null)
                        {
                            if (condition(fileLine.String))
                            {
                                // TODO: hack:
                                var traceMsg = $"Scanning from line {FileText[traceIndex].LineNumber}, line {fileLine.LineNumber} ('{fileLine.String}') satisfied the condition.";
                                System.Diagnostics.Debug.WriteLine(traceMsg);

                                applyActionForRowFound(fileLine.String);
                                break;
                            }
                        }
                        else
                        {
                            throw new Exception(errorMessageGetter());
                        }
                    }
                });

            return this;
        }

        private void WithFileAndLineNumberExceptionReporting(Action a)
        {
            var bailoutErrorIndex = PositionIndex;
            try
            {
                a();
            }
            catch (Exception e)  // TODO: specialise the type
            {
                var file = SourceFilePath;
                var index = PositionIndex < FileText.Length ? PositionIndex : bailoutErrorIndex;
                if (index < FileText.Length)
                {
                    var line = FileText[index].LineNumber;
                    throw new Exception($"{file}({line}): {e.Message}");
                }
                else
                {
                    throw new Exception($"{file}: {e.Message}");
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

        public FileReader Parameter<T>(string labelRequired, Func<string, T?> parser, out T result) where T: struct
        {
            var box = new Box<T>();  // because we can't lambda-bind an 'out' parameter.   TODO: Is there a more C#-native solution?

            void TryExtractDataFromLineFound(string documentRow)
            {
                var i = documentRow.LastIndexOf(labelRequired);
                if (i >= 0)
                {
                    var rightHandSide = documentRow.Substring(i + labelRequired.Length).Trim();
                    var parseResult = parser(rightHandSide);
                    if (parseResult.HasValue)
                    {
                        box.Content = parseResult.Value;
                    }

                    return;
                }

                throw new Exception($"Failed to read value after '{labelRequired}'."); // TODO: message according to parser type
            }

            var retVal = NextLineWhere(
                s => s.Contains(labelRequired),
                () => $"Could not find a line beginning with '{labelRequired}' from this point forwards.",
                TryExtractDataFromLineFound);

            result = box.Content;
            return retVal;
        }
    }

    internal class Box<T>
    {
        public T Content;
    }


    internal class FileLine
    {
        public string String;
        public int LineNumber;
    }
}
