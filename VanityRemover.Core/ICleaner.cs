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
        /// Starts the cleaning of a path.
        /// </summary>
        /// <param name="path">Path to clean</param>
        /// <returns>False if already busy; otherwise true.</returns>
        bool Clean(string path);

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