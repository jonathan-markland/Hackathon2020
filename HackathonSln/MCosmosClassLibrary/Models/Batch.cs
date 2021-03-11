using System.Collections.ObjectModel;

namespace MCosmosClassLibrary.Models
{
    /// <summary>
    /// Base class for batch folder processing results.
    /// </summary>
    public class BatchBase
    {
        /// <summary>
        /// The folder on which the load was attempted.
        /// </summary>
        public string PathToFolder { get; init; }
    }

    /// <summary>
    /// Batch failed because of a very fundamental single reason.
    /// </summary>
    public class BatchOverallError: BatchBase
    {
        public string OverallErrorMessage { get; init; }
    }

    /// <summary>
    /// A batch of Alignment Disc files loaded from a folder, and failure information.
    /// </summary>
    public class Batch: BatchBase
    {
        /// <summary>
        /// Files successfully loaded and parsed as discs
        /// </summary>
        public ReadOnlyCollection<DiscInfo> Discs;

        /// <summary>
        /// If non-empty, is the file processing errors of individual files.
        /// </summary>
        public ReadOnlyCollection<FileProcessingError> FileProcessingErrors;
    }

    /// <summary>
    /// If processing of a single file in the batch folder failed, this is created.
    /// </summary>
    public class FileProcessingError
    {
        /// <summary>
        /// The path to the file that had a processing error.
        /// </summary>
        public string PathToErrantFile { get; init; }

        /// <summary>
        /// The error message text.
        /// </summary>
        public string Error { get; init; }
    }
}
