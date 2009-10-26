using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Cleans out empty directories.
    /// </summary>
    public class Cleaner : ICleaner, IContextAware
    {
        private event EventHandler<CleaningDoneEventArgs> CleaningDone = (s, e) => { };

        private readonly object padLock = new object();

        private Thread cleaningThread;
        private SynchronizationContext context;

        private volatile bool isCleaning;
        private volatile bool cancel;

        private volatile uint totalDeleted;
        private volatile uint totalScanned;


        /// <summary>
        /// Creates a new <see cref="Cleaner"/>.
        /// </summary>
        public Cleaner()
        {
            context = new SynchronizationContext();
        }


        #region ICleaner Members

        bool ICleaner.Clean(DirectoryInfo directory)
        {
            lock (padLock)
            {
                if (isCleaning)
                    return false;
                isCleaning = true;
            }

            StartCleaningThread(directory);
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


        #region IContextAware Members

        public SynchronizationContext Context
        {
            get { return context; }
            set { context = value ?? new SynchronizationContext(); }
        }

        #endregion


        private void StartCleaningThread(DirectoryInfo directory)
        {
            cleaningThread = new Thread(DoCleaning)
                                 {
                                     Name = "Cleaning thread",
                                     IsBackground = false,
                                 };
            cleaningThread.Start(directory);
        }


        private void DoCleaning(object directory)
        {
            totalScanned = 0;
            totalDeleted = 0;
            cancel = false;

            DeleteEmptyDirectories(directory as DirectoryInfo);

            DoneCleaning();
        }


        private void DeleteEmptyDirectories(DirectoryInfo directory)
        {
            totalScanned++;

            if (cancel || !directory.Exists)
                return;

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

            if (directory.IsNotEmpty())
                return;

            try
            {
                directory.Delete();
                totalDeleted++;
                Debug.WriteLine("Deleted: " + directory.FullName);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + " (" + directory.FullName + ")");
            }
        }


        private void DoneCleaning()
        {
            InvokeCleaningDone();

            lock (padLock)
                isCleaning = false;
        }

        private void InvokeCleaningDone()
        {
            var eventArgs = new CleaningDoneEventArgs(totalScanned, totalDeleted);
            context.Post(e => CleaningDone(this, e as CleaningDoneEventArgs), eventArgs);
        }
    }
}