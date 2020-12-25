using System;
using Xunit;
using MCosmosClassLibrary;
using MCosmosClassLibrary.Models;

namespace TestProject
{
    public class CsvGenerationScenarios
    {
        [Fact]
        public void SerialiseDiscToCSVPassesFieldsThroughInExpectedOrder()
        {
            var metadata = new DiscMetadata("123", "C:\\CMMFiles\\123.txt");

            // TODO: copy-pasted from DaleSpreadsheetProvider.cs
            MeasureAndGrade flatGraded(double n) { return new MeasureAndGrade(n, ToleranceMathematics.FlatParaGradeFor(n)); }
            MeasureAndGrade paraGraded(double n) { return new MeasureAndGrade(n, ToleranceMathematics.FlatParaGradeFor(n)); }
            MeasureAndGrade distGraded(double n) { return new MeasureAndGrade(n, ToleranceMathematics.DistanceGradeFor(n)); }

            var flatness = new FlatnessMeasurements(
                datumF: flatGraded(1.0),
                datumE: flatGraded(2.0),
                datumD: flatGraded(3.0),
                datumG: flatGraded(4.0)
            );

            var parallel = new ParallelMeasurements(
                datumELH1: paraGraded(5.0),
                datumERH1: paraGraded(6.0),
                datumGFR1: paraGraded(7.0),
                datumGBK1: paraGraded(8.0)
            );

            var distances = new DistanceMeasurements(
                etoFLeft1  : distGraded(9.0),
                etoFRight1 : distGraded(10.0),
                etoFLeft2  : distGraded(11.0),
                etoFRight2 : distGraded(12.0),
                gtoDFront1 : distGraded(13.0),
                gtoDBack1  : distGraded(14.0),
                gtoDFront2 : distGraded(15.0),
                gtoDBack2  : distGraded(16.0)
            );

            var discInfo = new DiscInfo(metadata, flatness, parallel, distances);
            var actual = discInfo.CSVLineOnItsOwn();
            var expectation = "\"123\",\"1.00000\",\"2.00000\",\"3.00000\",\"4.00000\",\"5.00000\",\"6.00000\",\"7.00000\",\"8.00000\",\"9.00000\",\"10.00000\",\"11.00000\",\"12.00000\",\"13.00000\",\"14.00000\",\"15.00000\",\"16.00000\"";
            Assert.Equal(expectation, actual);
        }
    }
}
