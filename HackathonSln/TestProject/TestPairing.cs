using System.Collections.Generic;
using System.Linq;
using Xunit;
using MCosmosClassLibrary;
using ExampleFilesCollection;

namespace TestProject
{
    public class TestPairing
    {
        [Fact]
        public void TestStruderPairs()
        {
            var primaryList = DalesSpreadsheetProvider.GroundAtStruder();

            var filteredList = primaryList.IncludingGradeAandBonly();

            var pairings = filteredList.AsListOfMatchedPairs();

            var stringList = pairings.Select(pair => pair.CSVLine()).ToList();

            var expected = new List<string>
            {
                "'0.0005713142742828179', 'S076', 'GradeB', 'S116', 'GradeB'",
                "'0.0007547847375234144', 'S043', 'GradeB', 'S063', 'GradeB'",
                "'0.0008676981041822505', 'S067', 'GradeB', 'S119', 'GradeB'",
                "'0.0010464702575802502', '015', 'GradeB', 'S102', 'GradeB'",
                "'0.0011931890043071832', 'S027', 'GradeB', 'S080', 'GradeB'",
                "'0.001736807415924052', 'S079', 'GradeB', 'S082', 'GradeB'",
                "'0.0020293595048673937', 'S021', 'GradeB', 'S024', 'GradeB'",
                "'0.0031510315771177467', '008', 'GradeB', 'S070', 'GradeB'",
                "'0.0038955359066505588', 'S011', 'GradeB', 'S074', 'GradeB'",
            };

            Assert.Equal(expected.WithDoubleQuotes(), stringList);
        }
    }
}
