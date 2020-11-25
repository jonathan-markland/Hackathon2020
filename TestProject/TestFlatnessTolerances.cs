using Xunit;
using MCosmosClassLibrary;


namespace TestProject
{
    public class TestFlatnessTolerances
    {
        [Fact]
        public void InToleranceWhenAllFlatnessesAreZero()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = 0.0,
                DatumE = 0.0,
                DatumF = 0.0,
                DatumG = 0.0
            };

            Assert.True(flatness.AllWithinTolerance, "We expected a flatness record with all zeroes to be within tolerance.");
        }

        [Fact]
        public void OutOfToleranceWhenDatumDisTooLarge()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = FlatnessMeasurements.Tolerance + 0.00001,
                DatumE = 0.0,
                DatumF = 0.0,
                DatumG = 0.0
            };

            Assert.False(flatness.AllWithinTolerance, "We expected flatness record to be out of tolerance.");
        }

        [Fact]
        public void OutOfToleranceWhenDatumEisTooLarge()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = 0.0,
                DatumE = FlatnessMeasurements.Tolerance + 0.00001,
                DatumF = 0.0,
                DatumG = 0.0
            };

            Assert.False(flatness.AllWithinTolerance, "We expected flatness record to be out of tolerance.");
        }

        [Fact]
        public void OutOfToleranceWhenDatumFisTooLarge()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = 0.0,
                DatumE = 0.0,
                DatumF = FlatnessMeasurements.Tolerance + 0.00001,
                DatumG = 0.0
            };

            Assert.False(flatness.AllWithinTolerance, "We expected flatness record to be out of tolerance.");
        }

        [Fact]
        public void OutOfToleranceWhenDatumGisTooLarge()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = 0.0,
                DatumE = 0.0,
                DatumF = 0.0,
                DatumG = FlatnessMeasurements.Tolerance + 0.00001,
            };

            Assert.False(flatness.AllWithinTolerance, "We expected flatness record to be out of tolerance.");
        }

        [Fact]
        public void InToleranceWhenAllAreJustInside()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = FlatnessMeasurements.Tolerance - 0.00001,
                DatumE = FlatnessMeasurements.Tolerance - 0.00001,
                DatumF = FlatnessMeasurements.Tolerance - 0.00001,
                DatumG = FlatnessMeasurements.Tolerance - 0.00001,
            };

            Assert.True(flatness.AllWithinTolerance, "We expected flatness record to be in tolerance.");
        }

        [Fact]
        public void OutOfToleranceWhenAllAreTooLarge()
        {
            var flatness = new FlatnessMeasurements
            {
                DatumD = FlatnessMeasurements.Tolerance + 0.00001,
                DatumE = FlatnessMeasurements.Tolerance + 0.00001,
                DatumF = FlatnessMeasurements.Tolerance + 0.00001,
                DatumG = FlatnessMeasurements.Tolerance + 0.00001,
            };

            Assert.False(flatness.AllWithinTolerance, "We expected flatness record to be out of tolerance.");
        }
    }
}
