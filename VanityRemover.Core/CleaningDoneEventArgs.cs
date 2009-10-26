using System;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Provides data for the <see cref="ICleaner.CleaningDone"/> event.
    /// </summary>
    public class CleaningDoneEventArgs : EventArgs
    {
        private readonly uint deleted;
        private readonly uint total;

        /// <summary>
        /// Creates a new <see cref="CleaningDoneEventArgs"/>.
        /// </summary>
        /// <param name="total">Number of directories that were scanned in total.</param>
        /// <param name="deleted">Number of directories that were deleted.</param>
        public CleaningDoneEventArgs(uint total, uint deleted)
        {
            this.deleted = deleted;
            this.total = total;
        }

        /// <summary>
        /// Total number of scanned directories.
        /// </summary>
        public uint Total
        {
            get { return total; }
        }

        /// <summary>
        /// Total number of deleted directories.
        /// </summary>
        public uint Deleted
        {
            get { return deleted; }
        }
    }
}