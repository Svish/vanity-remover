using System.Drawing;
using System.IO;
using System.Reflection;

namespace GeekyProductions.FolderVanityRemover
{
    /// <summary>
    /// Easy access to embedded resources
    /// </summary>
    internal static class Resources
    {
        public static Bitmap GetBitmap(string resourceName)
        {
            using (var resource = GetResource(resourceName))
                return new Bitmap(resource);
        }

        public static Icon GetIcon(string resourceName)
        {
            var bitmap = GetBitmap(resourceName);
            return Icon.FromHandle(bitmap.GetHicon());
        }


        private static Stream GetResource(string resourceName)
        {
            return Assembly
                .GetCallingAssembly()
                .GetManifestResourceStream(resourceName);
        }
    }
}