using System.Threading;

namespace MCosmosFolderWatcher.Services
{
    public class FolderWatcher
    {
        public FolderWatcher(string folderPath)
        {
            FolderPath = folderPath;
            _threadData = new ThreadData();
            _thread = new Thread(() => { 
                while(!_shutdownRequested)
                {
                    var updatedData = Algorithms.BatchFolderLoader.LoadDiscsFromFolder(folderPath);
                    _threadData.BatchBase = updatedData;
                    Thread.Sleep(5000); // TODO: OS watch folder
                }
            });
        }

        public void RequestShutdown()
        {
            _shutdownRequested = true;
        }

        /// <summary>
        /// Path to the folder being watched.
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// Obtain the most recent view of the folder.  Threadsafe.
        /// </summary>
        public Models.BatchBase BatchBase { get { return _threadData.BatchBase; } }

        private volatile bool _shutdownRequested = false;
        private Thread _thread;
        private ThreadData _threadData;

        private class ThreadData
        {
            public volatile Models.BatchBase BatchBase = null;
        }
    }
}
