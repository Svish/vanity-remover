using System;
using System.Globalization;

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
        /// <param name="deleted">Number of directories that were deleted.</param>
        /// <param name="total">Number of directories that were scanned in total.</param>
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

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, 
                                 "{0} folders scanned.{1}{2} folders removed.", 
                                 Total, 
                                 Environment.NewLine, 
                                 Deleted);
        }
    }
}