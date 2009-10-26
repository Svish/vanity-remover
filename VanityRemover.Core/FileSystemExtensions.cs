using System.IO;
using System.Linq;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// File system related extension methods.
    /// </summary>
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Returns true if the <paramref name="directory"/> has any contents; otherwise false.
        /// </summary>
        public static bool IsNotEmpty(this DirectoryInfo directory)
        {
            return directory.GetFileSystemInfos().Any();
        }
    }
}