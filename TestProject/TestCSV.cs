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
                SerialNo = "123"
            };

            var flatness = new FlatnessMeasurements
            {
                DatumF = 1.0,
                DatumE = 2.0,
                DatumD = 3.0,
                DatumG = 4.0,
            };

            var parallel = new ParallelMeasurements
            {
                DatumELH1 = 5.0,
                DatumERH1 = 6.0,
                DatumGFR1 = 7.0,
                DatumGBK1 = 8.0,
            };

            var distances = new DistanceMeasurements
            {
                EtoFLeft1  = 9.0,
                EtoFRight1 = 10.0,
                EtoFLeft2  = 11.0,
                EtoFRight2 = 12.0,
                GtoDFront1 = 13.0,
                GtoDBack1  = 14.0,
                GtoDFront2 = 15.0,
                GtoDBack2  = 16.0,
            };

            var discInfo = new DiscInfo
            {
                Metadata  = metadata,
                Flatness  = flatness,
                Parallel  = parallel,
                Distances = distances
            };

            var actual = CsvGenerator.GetLine(discInfo);
            var expectation = "\"123\",\"1.00000\",\"2.00000\",\"3.00000\",\"4.00000\",\"5.00000\",\"6.00000\",\"7.00000\",\"8.00000\",\"9.00000\",\"10.00000\",\"11.00000\",\"12.00000\",\"13.00000\",\"14.00000\",\"15.00000\",\"16.00000\"";
            Assert.Equal(expectation, actual);
        }
    }
}
