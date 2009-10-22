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
        private static readonly string Base = typeof(Resources).Namespace;

        public static Icon Application
        {
            get { return GetIcon(Base + ".Icons.trash.png"); }
        }
        public static Bitmap Browse
        {
            get { return GetBitmap(Base + ".Icons.browse.png"); }
        }

        public static Bitmap Go
        {
            get { return GetBitmap(Base + ".Icons.go.png"); }
        }

        public static Bitmap Stop
        {
            get { return GetBitmap(Base + ".Icons.stop.png"); }
        }

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