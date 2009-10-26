using System;
using System.Windows.Forms;

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
            var arg = args.Length > 0 
                ? args[0] 
                : "";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(arg));
        }
    }
}