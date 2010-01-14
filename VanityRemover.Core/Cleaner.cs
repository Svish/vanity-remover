using System;
using System.IO;
using System.Threading;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Cleans out empty directories.
    /// </summary>
    public class Cleaner
    {
        private SynchronizationContext context = new SynchronizationContext();
        private volatile bool cancel;


        /// <summary>
        /// Raised when the async cleaning is done.
        /// </summary>
        public event EventHandler<EventArgs> CleaningDone = (s, e) => { };


        /// <summary>
        /// Raised when a directory has been scanned and acted upon.
        /// </summary>
        public event EventHandler<DirectoryScannedEventArgs> DirectoryScanned = (s, e) => { };


        /// <summary>
        /// The <see cref="SynchronizationContext"/> to use
        /// when raising events.
        /// </summary>
        public SynchronizationContext Context
        {
            get { return context; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                context = value;
            }
        }


        /// <summary>
        /// Cancel all cleaning jobs.
        /// </summary>
        public void Cancel()
        {
            cancel = true;
        }


        /// <summary>
        /// Starts to clean the given directory. Returns immidiately.
        /// </summary>
        public void StartCleaning(FreshDirectory directory)
        {
            var thread = new Thread(Clean)
            {
                Name = "Cleaning: " + directory.FullName,
                IsBackground = false,
            };

            thread.Start(directory);
        }
        
        private void Clean(object directory)
        {
            Clean(directory as FreshDirectory);
        }


        /// <summary>
        /// Cleans the given directory.
        /// </summary>
        public void Clean(FreshDirectory directory)
        {
            DoClean(directory);
            InvokeCleaningDone();
            cancel = false;
        }


        private void DoClean(FreshDirectory directory)
        {
            // Check for null
            if (directory == null)
                throw new ArgumentNullException("directory");

            // Check for cancel
            if (cancel)
            {
                InvokeDirectoryScanned(new DirectorySkippedEventArgs(directory,
                    new UserCancelledException()));
                return;
            }

            // Check that directory exists
            if (!directory.Exists)
            {
                InvokeDirectoryScanned(new DirectorySkippedEventArgs(directory,
                    new DirectoryNotFoundException("Directory not found.")));
                return;
            }

            // Try to delete all sub directories
            try
            {
                foreach (var subDirectory in directory.GetSubDirectories())
                    DoClean(subDirectory);
            }
            catch (Exception e)
            {
                InvokeDirectoryScanned(new DirectorySkippedEventArgs(directory, e));
                return;
            }

            // Check for cancel again
            if (cancel)
            {
                InvokeDirectoryScanned(new DirectorySkippedEventArgs(directory,
                    new UserCancelledException()));
                return;
            }

            // Try to delete the directory
            try
            {
                directory.Delete();
                InvokeDirectoryScanned(new DirectoryDeletedEventArgs(directory));
            }
            catch (Exception e)
            {
                InvokeDirectoryScanned(new DirectorySkippedEventArgs(directory, e));
                return;
            }
        }


        private void InvokeDirectoryScanned(DirectoryScannedEventArgs result)
        {
            context.Send(e => DirectoryScanned(this, (DirectoryScannedEventArgs)e), result);
        }


        private void InvokeCleaningDone()
        {
            context.Send(e => CleaningDone(this, (EventArgs)e), EventArgs.Empty);
        }
    }
}