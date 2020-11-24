using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace MCosmosClassLibrary
{
    public class FileReader
    {
        private int LineIndex;
        private FileLine[] FileText;
        public readonly string SourceFilePath;

        public FileReader(string sourceFile)
        {
            SourceFilePath = sourceFile;
            LineIndex = 0;
            var lines = File.ReadAllLines(sourceFile);
            FileText = lines
                .Select((s, i) => new FileLine { String = s.Trim(), LineNumber = i + 1 })
                .Where(x => !String.IsNullOrEmpty(x.String))
                .ToArray();
        }

        public FileReader ExpectLineStartingWith(string leftSideMatchRequired)
        {
            return this; // TODO
        }

        public FileReader ExpectWholeLine(string lineContentRequired)
        {
            return this; // TODO
        }

        public FileReader Parameter<T>(string leftSideMatchRequired, Func<string, T?> parser, out T result) where T:struct
        {
            var parseResult = parser("TODO -- string from file to the right side");
            if (parseResult.HasValue)
            {
                result = parseResult.Value;
                return this;
            }
            else
            {
                throw new Exception($"Failed to read value after '{leftSideMatchRequired}'."); // TODO: message according to parser type
            }
        }
    }

    public struct FileLine
    {
        public string String;
        public int LineNumber;
    }
}
