namespace Geeky.VanityRemover.Core
{
    public class DirectoryDeletedEventArgs : DirectoryScannedEventArgs
    {
        public DirectoryDeletedEventArgs(FreshDirectory result) 
            : base(result)
        {
        }

        public override string ToString()
        {
            return "Deleted.";
        }
    }
}