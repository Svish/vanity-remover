using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GeekyProductions.FolderVanityRemover
{
    /// <summary>
    /// The main and only form of this application.
    /// </summary>
    internal partial class MainForm : Form
    {
        private readonly ICleaner cleaner;

        /// <summary>
        /// Creates a new <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Set icons
            Icon = Resources.GetIcon("GeekyProductions.FolderVanityRemover.Icons.app.ico");
            folderButton.Image = Resources.GetBitmap("GeekyProductions.FolderVanityRemover.Icons.folder.png");
            cleanButton.Image = Resources.GetBitmap("GeekyProductions.FolderVanityRemover.Icons.go.png");

            // Create cleaner
            cleaner = new Cleaner(SynchronizationContext.Current);
            cleaner.CleaningDone += CleaningDone;

        }


        /// <summary>
        /// Show folder browser dialog
        /// </summary>
        private void folderButtonClick(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = folderTextbox.Text;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                folderTextbox.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Start cleaner
        /// </summary>
        private void cleanButtonClick(object sender, EventArgs e)
        {
            // Fix UI
            progressBar.Style = ProgressBarStyle.Marquee;
            folderTextbox.Enabled = false;
            folderButton.Enabled = false;
            cleanButton.Image = Resources.GetBitmap("GeekyProductions.FolderVanityRemover.Icons.stop.png");

            // Start cleaner, or cancel if already running.
            if(!cleaner.Clean(new DirectoryInfo(folderTextbox.Text)))
                cleaner.Cancel();

        }


        /// <summary>
        /// Cleaning done
        /// </summary>
        private void CleaningDone(object sender, CleaningDoneEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            var m = string.Format("{0} folders scanned.{1}{2} folders removed.",
                                  e.Total,
                                  Environment.NewLine,
                                  e.Deleted);

            MessageBox.Show(m, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            folderTextbox.Enabled = true;
            folderButton.Enabled = true;
            cleanButton.Image = Resources.GetBitmap("GeekyProductions.FolderVanityRemover.Icons.go.png");
        }

        /// <summary>
        /// Drag Drop
        /// </summary>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files == null || files.Length < 1)
                    return;

                var filename = files[0];
                if (Directory.Exists(filename))
                    folderTextbox.Text = filename;
            }
        }

        /// <summary>
        /// Drag Enter
        /// </summary>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // Only accept files
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                           ? DragDropEffects.Link
                           : DragDropEffects.None;
        }

        /// <summary>
        /// Folder text changed
        /// </summary>
        private void folderTextbox_TextChanged(object sender, EventArgs e)
        {
            cleanButton.Enabled = Directory.Exists(folderTextbox.Text);
        }
    }
}