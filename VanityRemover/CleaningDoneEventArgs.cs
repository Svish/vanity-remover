using System;

namespace GeekyProductions.FolderVanityRemover
{
    /// <summary>
    /// Provides data for the <see cref="ICleaner.CleaningDone"/> event.
    /// </summary>
    internal class CleaningDoneEventArgs : EventArgs
    {
        private readonly uint deleted;
        private readonly uint total;

        public CleaningDoneEventArgs(uint deleted, uint total)
        {
            this.deleted = deleted;
            this.total = total;
        }

        /// <summary>
        /// Total number of directories scanned.
        /// </summary>
        public uint Total
        {
            get { return total; }
        }

        /// <summary>
        /// Number of directories that were deleted.
        /// </summary>
        public uint Deleted
        {
            get { return deleted; }
        }
    }
}