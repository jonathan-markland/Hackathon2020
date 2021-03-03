using System.Threading;

namespace MCosmosFolderWatcher.Services
{
    public class FolderWatcher // TODO: erase this file for MVP.
    {
        public FolderWatcher(string folderPath)
        {
            FolderPath = folderPath;
            _threadData = new ThreadData();
            _thread = new Thread(() => { 
                while(!_shutdownRequested)
                {
                    var updatedFolderData = Algorithms.BatchFolderLoader.LoadDiscsFromFolder(folderPath, null /*TODO*/);
                    _threadData.FolderData = updatedFolderData;
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
        public Models.BatchBase FolderData { get { return _threadData.FolderData; } }

        private volatile bool _shutdownRequested = false;
        private Thread _thread;
        private ThreadData _threadData;

        private class ThreadData
        {
            public volatile Models.BatchBase FolderData = null;
        }
    }
}
