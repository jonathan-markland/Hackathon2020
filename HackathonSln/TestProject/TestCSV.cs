using System;
using Xunit;
using MCosmosClassLibrary;

namespace TestProject
{
    public class TestCSV
    {
        [Fact]
        public void SerialiseDiscToCSVPassesFieldsThroughInExpectedOrder()
        {
            var metadata = new DiscMetadata {
                SerialNo = "123",
                OriginFilePath = "C:\\CMMFiles\\123.txt"
            };

            // TODO: copy-pasted from DaleSpreadsheetProvider.cs
            MeasureAndGrade flatGraded(double n) { return new MeasureAndGrade(n, ToleranceMathematics.FlatParaGradeFor(n)); }
            MeasureAndGrade paraGraded(double n) { return new MeasureAndGrade(n, ToleranceMathematics.FlatParaGradeFor(n)); }
            MeasureAndGrade distGraded(double n) { return new MeasureAndGrade(n, ToleranceMathematics.DistanceGradeFor(n)); }

            var flatness = new FlatnessMeasurements
            {
                DatumF = flatGraded(1.0),
                DatumE = flatGraded(2.0),
                DatumD = flatGraded(3.0),
                DatumG = flatGraded(4.0),
            };

            var parallel = new ParallelMeasurements
            {
                DatumELH1 = paraGraded(5.0),
                DatumERH1 = paraGraded(6.0),
                DatumGFR1 = paraGraded(7.0),
                DatumGBK1 = paraGraded(8.0),
            };

            var distances = new DistanceMeasurements
            {
                EtoFLeft1  = distGraded(9.0),
                EtoFRight1 = distGraded(10.0),
                EtoFLeft2  = distGraded(11.0),
                EtoFRight2 = distGraded(12.0),
                GtoDFront1 = distGraded(13.0),
                GtoDBack1  = distGraded(14.0),
                GtoDFront2 = distGraded(15.0),
                GtoDBack2  = distGraded(16.0),
            };

            var discInfo = new DiscInfo(metadata, flatness, parallel, distances);
            var actual = discInfo.CSVLineOnItsOwn();
            var expectation = "\"123\",\"1.00000\",\"2.00000\",\"3.00000\",\"4.00000\",\"5.00000\",\"6.00000\",\"7.00000\",\"8.00000\",\"9.00000\",\"10.00000\",\"11.00000\",\"12.00000\",\"13.00000\",\"14.00000\",\"15.00000\",\"16.00000\"";
            Assert.Equal(expectation, actual);
        }
    }
}
