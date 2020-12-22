using System;
using Xunit;
using MCosmosFolderWatcher.Algorithms;
using MCosmosFolderWatcher.Models;

namespace MCosmosFolderWatcherTests
{
    public class Tests
    {
        private string InCurrentDirectory(string path)
        {
            return System.IO.Path.Combine(
                System.IO.Directory.GetCurrentDirectory(), path);
        }

        private string PathToExampleFilesCollectionFolder =>
            InCurrentDirectory(@"ExampleFiles");
        
        private string PathThatDoesntExist =>
            InCurrentDirectory(@"PathThatDoesntExist");



        [Fact]
        public void LoadingTheExampleFilesCollectionAsBatchLoads27DiscsWith0Errors()
        {
            var batchLoadResult =
                BatchFolderLoader.LoadDiscsFromFolder(
                    PathToExampleFilesCollectionFolder);

            Assert.Equal(PathToExampleFilesCollectionFolder, batchLoadResult.PathToFolder);

            var batch = batchLoadResult as Batch;
            Assert.NotNull(batch);

            Assert.Empty(batch.FileProcessingErrors);
            Assert.Equal(27, batch.Discs.Count);
        }



        [Fact]
        public void LoadingNonExistentFolder()
        {
            var batchLoadResult =
                BatchFolderLoader.LoadDiscsFromFolder(
                    PathThatDoesntExist);

            Assert.Equal(PathThatDoesntExist, batchLoadResult.PathToFolder);

            var overallError = batchLoadResult as BatchOverallError;
            Assert.NotNull(overallError);

            Assert.StartsWith(
                "Could not find a part of the path ",   // TODO: Admittedly a bit of a risk comparing against a OS localized message string. 
                overallError.OverallErrorMessage);
        }
    }
}
