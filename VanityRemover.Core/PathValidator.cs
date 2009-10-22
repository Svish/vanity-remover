using System.IO;

namespace Geeky.VanityRemover.Core
{
    public class PathValidator : IPathValidator
    {
        public bool IsValid(string path)
        {
            return Directory.Exists(path);
        }
    }
}