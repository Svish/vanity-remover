using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Cleans out empty directories.
    /// </summary>
    public class Cleaner : ICleaner, ISynchronizationAware
    {
        // Events
        private event EventHandler<CleaningDoneEventArgs> CleaningDone = (s, e) => { };

        // Threading
        private readonly object padLock = new object();
        private SynchronizationContext context;
        private Thread cleaningThread;

        // Control
        private volatile bool cancel;
        private volatile bool isCleaning;

        // Counters
        private volatile uint totalCount;
        private volatile uint deletedCount;



        /// <summary>
        /// Creates a new <see cref="Cleaner"/>.
        /// </summary>
        public Cleaner()
        {
            context = new SynchronizationContext();
            isCleaning = false;
            cancel = false;
        }


        #region ISynchronizationAware Members

        public SynchronizationContext Context
        {
            get { return context; }
            set { context = value ?? new SynchronizationContext(); }
        }

        #endregion



        #region ICleaner Members

        bool ICleaner.Clean(DirectoryInfo directory)
        {
            // Check if cleaning already
            lock (padLock)
            {
                if (isCleaning)
                    return false;
                isCleaning = true;
            }

            // Start the cleaning thread
            cleaningThread = new Thread(DoCleaning)
                                 {
                                     Name = "Cleaning thread",
                                     IsBackground = false,
                                 };
            cleaningThread.Start(directory);

            return true;
        }


        void ICleaner.Cancel()
        {
            lock (padLock)
                cancel = true;
        }


        event EventHandler<CleaningDoneEventArgs> ICleaner.CleaningDone
        {
            add { lock (padLock) CleaningDone += value; }
            remove { lock (padLock) CleaningDone -= value; }
        }

        #endregion


        private void DoCleaning(object directory)
        {
            totalCount = 0;
            deletedCount = 0;
            cancel = false;

            DeleteEmptyDirectories(directory as DirectoryInfo);

            context.Post(InvokeCleaningDone, new CleaningDoneEventArgs(deletedCount, totalCount));

            lock (padLock)
                isCleaning = false;
        }


        private void DeleteEmptyDirectories(DirectoryInfo directory)
        {
            if (cancel || !directory.Exists)
                return;

            totalCount++;

            // Go recursive on all sub directories
            try
            {
                foreach (var subDirectory in directory.GetDirectories())
                    DeleteEmptyDirectories(subDirectory);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + " (" + directory.FullName + ")");
                return;
            }

            // Do nothing if directory is not empty
            if (directory.GetFileSystemInfos().Any())
                return;

            // Otherwise
            try
            {
                // Try to delete
                directory.Delete();
                deletedCount++;
                Debug.WriteLine("Deleted: " + directory.FullName);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + " (" + directory.FullName + ")");
            }
        }
        

        private void InvokeCleaningDone(object e)
        {
            CleaningDone(this, e as CleaningDoneEventArgs);
        }
    }
}