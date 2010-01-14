using System.IO;

namespace Geeky.VanityRemover.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsCleanablePath(this string path)
        {
            return Directory.Exists(path);
        }
    }
}