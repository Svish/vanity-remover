using System;
using System.IO;

namespace GeekyProductions.FolderVanityRemover
{
    /// <summary>
    /// A directory cleaner.
    /// </summary>
    internal interface ICleaner
    {
        /// <summary>
        /// Starts the cleaning of a directory.
        /// </summary>
        /// <param name="directory">Directory to clean</param>
        /// <returns>False if already busy; otherwise true.</returns>
        bool Clean(DirectoryInfo directory);

        /// <summary>
        /// Asks the cleaning process to cancel.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Raised when cleaning is done.
        /// </summary>
        // No ReSharper, it is invoked...
        event EventHandler<CleaningDoneEventArgs> CleaningDone;
    }
}