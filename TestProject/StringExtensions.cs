using System.Collections.Generic;
using System.Linq;

namespace TestProject
{
    public static class StringExtensions
    {
        public static List<string> WithDoubleQuotes(this List<string> list)
        {
            return list.Select(s => s.Replace('\'', '\"')).ToList();
        }
    }
}
