using System;
using System.IO;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// A directory cleaner.
    /// </summary>
    public interface ICleaner
    {
        /// <summary>
        /// Starts the cleaning of the selected path.
        /// </summary>
        /// <returns>False if already busy; otherwise true.</returns>
        bool Clean(DirectoryInfo directory);

        /// <summary>
        /// Tells the cleaning thread to stop.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Raised when cleaning is done.
        /// </summary>
        event EventHandler<CleaningDoneEventArgs> CleaningDone;
    }
}