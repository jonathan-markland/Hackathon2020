using System.IO;
using Xunit;
using MCosmosClassLibrary.Models;
using MCosmosClassLibrary.Services;

namespace TestProject
{
    public class LoadDiscsFromFolderScenarios
    {
        private string InCurrentDirectory(string path)
        {
            return Path.Combine(
                Directory.GetCurrentDirectory(), path);
        }

        private string PathToExampleFilesCollectionFolder =>
            InCurrentDirectory(@"ExampleFiles");  // TODO: There are races within the test framework since two groups of tests compete for this folder (file lock acquisition issue).
        
        private string PathThatDoesntExist =>
            InCurrentDirectory(@"PathThatDoesntExist");

        private string PathToGoodAndBadFilesCollectionFolder =>
            InCurrentDirectory(@"GoodAndBadFiles");



        [Fact]
        public void FolderHasAllGoodFiles()
        {
            var batchLoadResult =
                BatchFolderLoader.LoadDiscsFromFolder(
                    PathToExampleFilesCollectionFolder,
                    DiscConfig.DiscConfigForTestFramework);

            Assert.Equal(PathToExampleFilesCollectionFolder, batchLoadResult.PathToFolder);

            var batch = batchLoadResult as Batch;
            Assert.NotNull(batch);

            Assert.Empty(batch.FileProcessingErrors);
            Assert.Equal(27, batch.Discs.Count);
        }



        [Fact]
        public void FolderDoesntExist()
        {
            var batchLoadResult =
                BatchFolderLoader.LoadDiscsFromFolder(
                    PathThatDoesntExist,
                    DiscConfig.DiscConfigForTestFramework);

            Assert.Equal(PathThatDoesntExist, batchLoadResult.PathToFolder);

            var overallError = batchLoadResult as BatchOverallError;
            Assert.NotNull(overallError);

            // TODO: Admittedly a bit of a risk comparing against a OS localized message string:
            Assert.StartsWith(
                "Could not find a part of the path ",
                overallError.OverallErrorMessage);
        }



        [Fact]
        public void AnotherProcessHasLockedFile()
        {
            var fileToOpenExclusively =
                Path.Combine(
                    PathToExampleFilesCollectionFolder, "Ser No 1    repeat -- 1.txt");

            using (var file = File.Open(fileToOpenExclusively, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var batchLoadResult =
                    BatchFolderLoader.LoadDiscsFromFolder(
                        PathToExampleFilesCollectionFolder,
                        DiscConfig.DiscConfigForTestFramework);

                Assert.Equal(PathToExampleFilesCollectionFolder, batchLoadResult.PathToFolder);

                var batch = batchLoadResult as Batch;
                Assert.NotNull(batch);

                Assert.Single(batch.FileProcessingErrors);
                Assert.Equal(26, batch.Discs.Count);

                // TODO: Admittedly a bit of a risk comparing against a OS localized message string:
                Assert.StartsWith(
                    "The process cannot access the file ", 
                    batch.FileProcessingErrors[0].Error);
                
                Assert.Equal(fileToOpenExclusively, batch.FileProcessingErrors[0].PathToErrantFile);
            }
        }



        [Fact]
        public void FolderHasGoodAndBadFiles()
        {
            var batchLoadResult =
                BatchFolderLoader.LoadDiscsFromFolder(
                    PathToGoodAndBadFilesCollectionFolder,
                    DiscConfig.DiscConfigForTestFramework);

            Assert.Equal(PathToGoodAndBadFilesCollectionFolder, batchLoadResult.PathToFolder);

            var batch = batchLoadResult as Batch;
            Assert.NotNull(batch);

            Assert.Equal(2, batch.FileProcessingErrors.Count);
            Assert.Equal("More than one label 'Serial No      :' was found in this file, resulting in contradicting information: 5, 1", batch.FileProcessingErrors[0].Error);
            Assert.Equal("Cannot find a label 'Serial No      :' in this file.", batch.FileProcessingErrors[1].Error);

            Assert.Equal(3, batch.Discs.Count);
            Assert.Equal("1", batch.Discs[0].Metadata.SerialNo);
            Assert.Equal("2", batch.Discs[1].Metadata.SerialNo);
            Assert.Equal("3", batch.Discs[2].Metadata.SerialNo);
        }



    }
}
