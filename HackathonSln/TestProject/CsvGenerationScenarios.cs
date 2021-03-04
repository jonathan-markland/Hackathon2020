using Xunit;
using MCosmosClassLibrary.Models;

namespace TestProject
{
    public class CsvGenerationScenarios
    {
        [Fact]
        public void SerialiseDiscToCSVPassesFieldsThroughInExpectedOrder()
        {
            var metadata = new DiscMetadata("123", "C:\\CMMFiles\\123.txt");

            var flatness = new FlatnessMeasurements(
                datumF: new FlatnessMeasure(1.0),
                datumE: new FlatnessMeasure(2.0),
                datumD: new FlatnessMeasure(3.0),
                datumG: new FlatnessMeasure(4.0)
            );

            var parallel = new ParallelMeasurements(
                datumELH1: new ParallelMeasure(5.0),
                datumERH1: new ParallelMeasure(6.0),
                datumGFR1: new ParallelMeasure(7.0),
                datumGBK1: new ParallelMeasure(8.0)
            );

            var distances = new DistanceMeasurements(
                etoFLeft1  : new DistanceMeasure(9.0),
                etoFRight1 : new DistanceMeasure(10.0),
                etoFLeft2  : new DistanceMeasure(11.0),
                etoFRight2 : new DistanceMeasure(12.0),
                gtoDFront1 : new DistanceMeasure(13.0),
                gtoDBack1  : new DistanceMeasure(14.0),
                gtoDFront2 : new DistanceMeasure(15.0),
                gtoDBack2  : new DistanceMeasure(16.0)
            );

            var discInfo = new DiscInfo(metadata, flatness, parallel, distances);
            var actual = discInfo.CSVLineOnItsOwn();
            var expectation = "\"123\",\"1.00000\",\"2.00000\",\"3.00000\",\"4.00000\",\"5.00000\",\"6.00000\",\"7.00000\",\"8.00000\",\"9.00000\",\"10.00000\",\"11.00000\",\"12.00000\",\"13.00000\",\"14.00000\",\"15.00000\",\"16.00000\"";
            Assert.Equal(expectation, actual);
        }
    }
}
