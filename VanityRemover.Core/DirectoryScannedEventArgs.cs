using System;

namespace Geeky.VanityRemover.Core
{
    public abstract class DirectoryScannedEventArgs : EventArgs
    {
        public FreshDirectory Directory { get; private set; }

        protected DirectoryScannedEventArgs(FreshDirectory result)
        {
            Directory = result;
        }

        public override string ToString()
        {
            return "Scanned.";
        }
    }
}