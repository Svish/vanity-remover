using System;
using System.Windows.Forms;
using Geeky.VanityRemover.Core;

namespace Geeky.VanityRemover
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Get initial directory
            var initialDirectory = args.Length > 0
                ? args[0]
                : "";

            // Create cleaner
            var cleaner = new Cleaner();
            
            // Start application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(initialDirectory, cleaner));
        }
    }
}