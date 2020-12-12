using Xunit;

namespace TestProject
{
    public class TestFileLoader
    {
        [Fact]
        public void LoadingTheTEST2FileLoadsExpectedResults()
        {
            var loadedFile = MCosmosClassLibrary.DiscFileLoader.LoadFromFile("SerialNo_2222222222222222.txt");

			Assert.Equal("2222222222222222", loadedFile.Metadata.SerialNo);

			Assert.Equal(0.20532, loadedFile.Flatness.DatumF.Value);
			Assert.Equal(0.23289, loadedFile.Flatness.DatumE.Value);
			Assert.Equal(0.17347, loadedFile.Flatness.DatumD.Value);
			Assert.Equal(0.17389, loadedFile.Flatness.DatumG.Value);

			Assert.Equal(0.02684, loadedFile.Parallel.DatumELH1.Value);
			Assert.Equal(0.15320, loadedFile.Parallel.DatumERH1.Value);
			Assert.Equal(0.01577, loadedFile.Parallel.DatumGFR1.Value);
			Assert.Equal(0.10453, loadedFile.Parallel.DatumGBK1.Value);

			Assert.Equal(28.03189, loadedFile.Distances.EtoFLeft1.Value);
			Assert.Equal(28.08107, loadedFile.Distances.EtoFRight1.Value);
			Assert.Equal(28.10921, loadedFile.Distances.EtoFLeft2.Value);
			Assert.Equal(28.12824, loadedFile.Distances.EtoFRight2.Value);
			Assert.Equal(28.07349, loadedFile.Distances.GtoDFront1.Value);
			Assert.Equal(28.02423, loadedFile.Distances.GtoDBack1.Value);
			Assert.Equal(28.08503, loadedFile.Distances.GtoDFront2.Value);
			Assert.Equal(28.08362, loadedFile.Distances.GtoDBack2.Value);
		}

		[Fact]
		public void LoadingTheSerNo1FileLoadsExpectedResults()
		{
			var loadedFile = MCosmosClassLibrary.DiscFileLoader.LoadFromFile("Ser No 1    repeat -- 1.txt");

			Assert.Equal("1", loadedFile.Metadata.SerialNo);

			Assert.Equal(0.00127, loadedFile.Flatness.DatumF.Value);
			Assert.Equal(0.00145, loadedFile.Flatness.DatumE.Value);
			Assert.Equal(0.00122, loadedFile.Flatness.DatumD.Value);
			Assert.Equal(0.00113, loadedFile.Flatness.DatumG.Value);

			Assert.Equal(0.00180, loadedFile.Parallel.DatumELH1.Value);
			Assert.Equal(0.00165, loadedFile.Parallel.DatumERH1.Value);
			Assert.Equal(0.00161, loadedFile.Parallel.DatumGFR1.Value);
			Assert.Equal(0.00128, loadedFile.Parallel.DatumGBK1.Value);

			Assert.Equal(28.01644, loadedFile.Distances.EtoFLeft1.Value);
			Assert.Equal(28.01626, loadedFile.Distances.EtoFRight1.Value);
			Assert.Equal(28.01824, loadedFile.Distances.EtoFLeft2.Value);
			Assert.Equal(28.01746, loadedFile.Distances.EtoFRight2.Value);
			Assert.Equal(28.01817, loadedFile.Distances.GtoDFront1.Value);
			Assert.Equal(28.01658, loadedFile.Distances.GtoDBack1.Value);
			Assert.Equal(28.01960, loadedFile.Distances.GtoDFront2.Value);
			Assert.Equal(28.01759, loadedFile.Distances.GtoDBack2.Value);
		}

		[Fact]
		public void LoadingTheSerNo2FileLoadsExpectedResults()
		{
			var loadedFile = MCosmosClassLibrary.DiscFileLoader.LoadFromFile("Ser No 1    repeat -- 2.txt");

			Assert.Equal("2", loadedFile.Metadata.SerialNo);

			Assert.Equal(0.00124, loadedFile.Flatness.DatumF.Value);
			Assert.Equal(0.00145, loadedFile.Flatness.DatumE.Value);
			Assert.Equal(0.00125, loadedFile.Flatness.DatumD.Value);
			Assert.Equal(0.00120, loadedFile.Flatness.DatumG.Value);

			Assert.Equal(0.00184, loadedFile.Parallel.DatumELH1.Value);
			Assert.Equal(0.00160, loadedFile.Parallel.DatumERH1.Value);
			Assert.Equal(0.00159, loadedFile.Parallel.DatumGFR1.Value);
			Assert.Equal(0.00129, loadedFile.Parallel.DatumGBK1.Value);

			Assert.Equal(28.01655, loadedFile.Distances.EtoFLeft1.Value);
			Assert.Equal(28.01621, loadedFile.Distances.EtoFRight1.Value);
			Assert.Equal(28.01835, loadedFile.Distances.EtoFLeft2.Value);
			Assert.Equal(28.01757, loadedFile.Distances.EtoFRight2.Value);
			Assert.Equal(28.01774, loadedFile.Distances.GtoDFront1.Value);
			Assert.Equal(28.01652, loadedFile.Distances.GtoDBack1.Value);
			Assert.Equal(28.01963, loadedFile.Distances.GtoDFront2.Value);
			Assert.Equal(28.01780, loadedFile.Distances.GtoDBack2.Value);
		}
	}
}
