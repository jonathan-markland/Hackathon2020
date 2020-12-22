using System;
using System.IO;
using System.Collections.Generic;
using MCosmosClassLibrary;
using MCosmosFolderWatcher.Models;

namespace MCosmosFolderWatcher.Algorithms
{
    public class BatchFolderLoader
    {
        public BatchBase LoadDiscsFromFolder(string pathToFolder)
        {
            try
            {
                return LoadDiscsFromFolder2(pathToFolder);
            }
            catch(Exception ex)
            {
                return new BatchOverallError
                { 
                    PathToFolder = pathToFolder, 
                    OverallErrorMessage = ex.Message 
                };
            }
        }


        private BatchBase LoadDiscsFromFolder2(string pathToFolder)
        {
            // A best-effort returning error detail for individual files.

            var discs = new List<DiscInfo>();
            var errors = new List<FileProcessingError>();

            foreach(var filePath in Directory.GetFiles(pathToFolder))
            {
                try
                {
                    var d = DiscFileLoader.LoadDiscFromFile(filePath);
                    discs.Add(d);
                }
                catch(Exception ex) // TODO: more specific?
                {
                    var e = new FileProcessingError
                    {
                        Error = ex.Message,
                        PathToErrantFile = filePath
                    };
                    errors.Add(e);
                }
            }

            return new Batch { Discs = discs, FileProcessingErrors = errors };
        }
    }
}
