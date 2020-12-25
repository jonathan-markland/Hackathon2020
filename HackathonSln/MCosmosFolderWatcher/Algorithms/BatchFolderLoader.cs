﻿using System;
using System.IO;
using System.Collections.Generic;
using MCosmosClassLibrary;
using MCosmosClassLibrary.Models;
using MCosmosClassLibrary.Algorithms;
using MCosmosFolderWatcher.Models;

namespace MCosmosFolderWatcher.Algorithms
{
    public static class BatchFolderLoader
    {
        public static BatchBase LoadDiscsFromFolder(string pathToFolder)
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


        private static BatchBase LoadDiscsFromFolder2(string pathToFolder)
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

            var rodiscs = discs.AsReadOnly();
            var roerrors = errors.AsReadOnly();

            return new Batch { PathToFolder = pathToFolder, Discs = rodiscs, FileProcessingErrors = roerrors };
        }
    }
}
