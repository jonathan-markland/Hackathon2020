using Xunit;
using MCosmosClassLibrary.Models;
using MCosmosClassLibrary.Services;

namespace TestProject
{
    public class DiscFileLoaderScenarios
	{
		// TODO: Manually examine that the disc grades are correct anyway.
		// TODO: We have no tests on known-bad files.




		private static MeasureAndGrade mg(double value, DiscGrade grade)
        {
			return new MeasureAndGrade(value, grade);
        }



        [Fact]
        public void ExpectationsForSerialNo_2222222222222222File()
        {
            var disc = DiscFileLoader.LoadDiscFromFile(
					"ExampleFiles\\SerialNo_2222222222222222.txt",
					FileHeadings.FileHeadingsForTestFramework);

			Assert.Equal("2222222222222222", disc.Metadata.SerialNo);

			Assert.Equal(mg(0.20532, DiscGrade.GradeC), disc.Flatness.DatumF);
			Assert.Equal(mg(0.23289, DiscGrade.GradeC), disc.Flatness.DatumE);
			Assert.Equal(mg(0.17347, DiscGrade.GradeC), disc.Flatness.DatumD);
			Assert.Equal(mg(0.17389, DiscGrade.GradeC), disc.Flatness.DatumG);

			Assert.Equal(mg(0.02684, DiscGrade.GradeC), disc.Parallel.DatumELH1);
			Assert.Equal(mg(0.15320, DiscGrade.GradeC), disc.Parallel.DatumERH1);
			Assert.Equal(mg(0.01577, DiscGrade.GradeC), disc.Parallel.DatumGFR1);
			Assert.Equal(mg(0.10453, DiscGrade.GradeC), disc.Parallel.DatumGBK1);

			Assert.Equal(mg(28.03189, DiscGrade.GradeC), disc.Distances.EtoFLeft1 );
			Assert.Equal(mg(28.08107, DiscGrade.GradeC), disc.Distances.EtoFRight1);
			Assert.Equal(mg(28.10921, DiscGrade.GradeC), disc.Distances.EtoFLeft2 );
			Assert.Equal(mg(28.12824, DiscGrade.GradeC), disc.Distances.EtoFRight2);
			Assert.Equal(mg(28.07349, DiscGrade.GradeC), disc.Distances.GtoDFront1);
			Assert.Equal(mg(28.02423, DiscGrade.GradeC), disc.Distances.GtoDBack1);
			Assert.Equal(mg(28.08503, DiscGrade.GradeC), disc.Distances.GtoDFront2);
			Assert.Equal(mg(28.08362, DiscGrade.GradeC), disc.Distances.GtoDBack2);
		}

		[Fact]
		public void ExpectationsForSerNo1File()
		{
			var disc = DiscFileLoader.LoadDiscFromFile(
					"ExampleFiles\\Ser No 1    repeat -- 1.txt",
					FileHeadings.FileHeadingsForTestFramework);

			Assert.Equal("1", disc.Metadata.SerialNo);

			Assert.Equal(mg(0.00127, DiscGrade.GradeA), disc.Flatness.DatumF);
			Assert.Equal(mg(0.00145, DiscGrade.GradeA), disc.Flatness.DatumE);
			Assert.Equal(mg(0.00122, DiscGrade.GradeA), disc.Flatness.DatumD);
			Assert.Equal(mg(0.00113, DiscGrade.GradeA), disc.Flatness.DatumG);

			Assert.Equal(mg(0.00180, DiscGrade.GradeA), disc.Parallel.DatumELH1);
			Assert.Equal(mg(0.00165, DiscGrade.GradeA), disc.Parallel.DatumERH1);
			Assert.Equal(mg(0.00161, DiscGrade.GradeA), disc.Parallel.DatumGFR1);
			Assert.Equal(mg(0.00128, DiscGrade.GradeA), disc.Parallel.DatumGBK1);

			Assert.Equal(mg(28.01644, DiscGrade.GradeC), disc.Distances.EtoFLeft1);
			Assert.Equal(mg(28.01626, DiscGrade.GradeC), disc.Distances.EtoFRight1);
			Assert.Equal(mg(28.01824, DiscGrade.GradeB), disc.Distances.EtoFLeft2);
			Assert.Equal(mg(28.01746, DiscGrade.GradeC), disc.Distances.EtoFRight2);
			Assert.Equal(mg(28.01817, DiscGrade.GradeB), disc.Distances.GtoDFront1);
			Assert.Equal(mg(28.01658, DiscGrade.GradeC), disc.Distances.GtoDBack1);
			Assert.Equal(mg(28.01960, DiscGrade.GradeA), disc.Distances.GtoDFront2);
			Assert.Equal(mg(28.01759, DiscGrade.GradeC), disc.Distances.GtoDBack2);
		}

		[Fact]
		public void ExpectationsForSerNo2File()
		{
			var disc = DiscFileLoader.LoadDiscFromFile(
					"ExampleFiles\\Ser No 1    repeat -- 2.txt",
					FileHeadings.FileHeadingsForTestFramework);

			Assert.Equal("2", disc.Metadata.SerialNo);

			Assert.Equal(mg(0.00124, DiscGrade.GradeA), disc.Flatness.DatumF);
			Assert.Equal(mg(0.00145, DiscGrade.GradeA), disc.Flatness.DatumE);
			Assert.Equal(mg(0.00125, DiscGrade.GradeA), disc.Flatness.DatumD);
			Assert.Equal(mg(0.00120, DiscGrade.GradeA), disc.Flatness.DatumG);

			Assert.Equal(mg(0.00184, DiscGrade.GradeA), disc.Parallel.DatumELH1);
			Assert.Equal(mg(0.00160, DiscGrade.GradeA), disc.Parallel.DatumERH1);
			Assert.Equal(mg(0.00159, DiscGrade.GradeA), disc.Parallel.DatumGFR1);
			Assert.Equal(mg(0.00129, DiscGrade.GradeA), disc.Parallel.DatumGBK1);

			Assert.Equal(mg(28.01655, DiscGrade.GradeC), disc.Distances.EtoFLeft1);
			Assert.Equal(mg(28.01621, DiscGrade.GradeC), disc.Distances.EtoFRight1);
			Assert.Equal(mg(28.01835, DiscGrade.GradeB), disc.Distances.EtoFLeft2);
			Assert.Equal(mg(28.01757, DiscGrade.GradeC), disc.Distances.EtoFRight2);
			Assert.Equal(mg(28.01774, DiscGrade.GradeC), disc.Distances.GtoDFront1);
			Assert.Equal(mg(28.01652, DiscGrade.GradeC), disc.Distances.GtoDBack1);
			Assert.Equal(mg(28.01963, DiscGrade.GradeA), disc.Distances.GtoDFront2);
			Assert.Equal(mg(28.01780, DiscGrade.GradeC), disc.Distances.GtoDBack2);
		}
	}
}
