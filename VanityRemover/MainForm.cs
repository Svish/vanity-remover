using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Geeky.VanityRemover
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
            Icon = Resources.Application;
            browseButton.Image = Resources.Browse;
            cleanButton.Image = Resources.Go;

            // Create cleaner
            cleaner = new Cleaner(SynchronizationContext.Current);
            cleaner.CleaningDone += CleaningDone;

        }


        /// <summary>
        /// Show folder browser dialog
        /// </summary>
        private void browseButtonClick(object sender, EventArgs e)
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
            browseButton.Enabled = false;
            cleanButton.Image = Resources.Stop;

            AcceptButton = null;
            CancelButton = cleanButton;

            // Start cleaner, or cancel if already running.
            if (!cleaner.Clean(new DirectoryInfo(folderTextbox.Text)))
                cleaner.Cancel();

        }


        /// <summary>
        /// Cleaning done
        /// </summary>
        private void CleaningDone(object sender, CleaningDoneEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            
            MessageBox.Show(e.ToString(), "Cleaning done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            folderTextbox.Enabled = true;
            browseButton.Enabled = true;
            cleanButton.Image = Resources.Go;

            AcceptButton = cleanButton;
            CancelButton = null;
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