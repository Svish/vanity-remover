using System;
using System.IO;
using System.Security;
using System.Threading;
using System.Collections.Generic;

namespace GeekyProductions.FolderVanityRemover
{
    /// <summary>
    /// Cleans out empty directories.
    /// </summary>
    internal class Cleaner : ICleaner
    {
        // Events
        private event EventHandler<CleaningDoneEventArgs> CleaningDone = (s, e) => { };
        
        // Threading
        private readonly object padLock = new object();
        private readonly SynchronizationContext context;

        private Thread cleaningThread;
        private volatile bool isCleaning;
        private volatile bool cancel;

        // Counters
        private volatile uint totalCount;
        private volatile uint deletedCount;


        /// <summary>
        /// Creates a new <see cref="Cleaner"/>.
        /// </summary>
        /// <param name="context"><see cref="SynchronizationContext"/> to use when
        /// raising events. If <c>null</c> no synchronization will be used.</param>
        public Cleaner(SynchronizationContext context)
        {
            this.context = context ?? new SynchronizationContext();
            isCleaning = false;
            cancel = false;
        }

        #region ICleaner Members

        bool ICleaner.Clean(DirectoryInfo directory)
        {
            // Check if cleaning already
            lock(padLock)
            {
                if(isCleaning)
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

            DeleteEmpty(directory as DirectoryInfo);

            context.Post(InvokeCleaningDone, new CleaningDoneEventArgs(deletedCount, totalCount));

            isCleaning = false;
        }

        private void InvokeCleaningDone(object e)
        {
            CleaningDone(this, e as CleaningDoneEventArgs);
        }


        private void DeleteEmpty(DirectoryInfo directory)
        {
            // Run this same method on all directories in this directory
            foreach (var d in directory.GetDirectories())
            {
                DeleteEmpty(d);
            }

            // If directory has no files or folders in it
            if (directory.GetFileSystemInfos().Length == 0)
            {
                try
                {
                    // Try to delete
                    directory.Delete();

                    // Increase counter
                    deletedCount++;
                }
                catch (DirectoryNotFoundException)
                {
                    // Already gone for some reason...
                }
                catch (IOException)
                {
                    // Not empty...
                }
                catch (SecurityException)
                {
                    // Not permission to delete...
                }
            }

            totalCount++;
        }
    }
}