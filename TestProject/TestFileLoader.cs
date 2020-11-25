using System;
using Xunit;

namespace TestProject
{
    public class TestFileLoader
    {
        [Fact]
        public void LoadingTheTEST2FileLoadsExpectedResults()
        {
            var loadedFile = MCosmosClassLibrary.DiscFileLoader.LoadFromFile("TEST2.txt");
			
			Assert.Equal(28.03189, loadedFile.Diagonal.EtoFLeft1  );
			Assert.Equal(28.10921 , loadedFile.Diagonal.EtoFLeft2 );
			Assert.Equal(28.08107 , loadedFile.Diagonal.EtoFRight1 );
			Assert.Equal(28.12824 , loadedFile.Diagonal.EtoFRight2 );
			Assert.Equal(28.02423 , loadedFile.Diagonal.GtoDBack1 );
			Assert.Equal(28.08362 , loadedFile.Diagonal.GtoDBack2 );
			Assert.Equal(28.07349 , loadedFile.Diagonal.GtoDFront1 );
			Assert.Equal(28.08503 , loadedFile.Diagonal.GtoDFront2 );

			Assert.Equal(0.17347, loadedFile.Flatness.DatumD);
			Assert.Equal(0.23289, loadedFile.Flatness.DatumE);
			Assert.Equal(0.20532, loadedFile.Flatness.DatumF);
			Assert.Equal(0.17389, loadedFile.Flatness.DatumG);

			Assert.Equal(0.02684, loadedFile.Parallel.DatumELH1);
			Assert.Equal(0.15320, loadedFile.Parallel.DatumERH1);
			Assert.Equal(0.10453, loadedFile.Parallel.DatumGBK1);
			Assert.Equal(0.01577, loadedFile.Parallel.DatumGFR1);
		}
	}
}
