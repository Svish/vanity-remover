using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Geeky.VanityRemover.Core;
using Geeky.VanityRemover.Core.Extensions;

namespace Geeky.VanityRemover
{
    /// <summary>
    /// The main and only form of this application.
    /// </summary>
    internal partial class Main : Form
    {
        private readonly Cleaner cleaner;


        /// <summary>
        /// Creates a new <see cref="Main"/>.
        /// </summary>
        public Main(string initialDirectory, Cleaner cleaner)
        {
            InitializeComponent();

            this.cleaner = cleaner;
            this.cleaner.Context = SynchronizationContext.Current;
            this.cleaner.CleaningDone += CleaningDone;
            this.cleaner.DirectoryScanned += DirectoryScanned;

            path.Text = initialDirectory ?? "";
            path.SelectionStart = 0;

            ActiveControl = path.Text == "" ? (Control)path : start;
            Running = false;
        }


        /// <summary>
        /// Updates UI accordingly.
        /// </summary>
        private bool Running
        {
            set
            {
                progressBar.Style = value ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;

                path.Enabled = !value;
                browse.Enabled = !value;

                start.Enabled = !value && path.Text.IsCleanablePath();
                cancel.Enabled = value;
            }
        }


        private void PathChanged(object sender, EventArgs e)
        {
            var isValid = path.Text.IsCleanablePath();

            start.Enabled = isValid;
            path.BackColor = isValid ? Color.AliceBlue : Color.LightCoral;
        }

        #region EventHandler: Buttons

        private void BrowseClicked(object sender, EventArgs e)
        {
            pathDialog.SelectedPath = path.Text;

            if (pathDialog.ShowDialog() == DialogResult.OK)
                path.Text = pathDialog.SelectedPath;
        }


        private void CancelClicked(object sender, EventArgs e)
        {
            cancel.Enabled = false;
            cleaner.Cancel();
        }


        private void StartClicked(object sender, EventArgs e)
        {
            var directory = new FreshDirectory(path.Text);

            if (Dialogs.ConfirmClean(this, directory) == DialogResult.OK)
            {
                scannedDirectories = 0;
                deletedDirectories = 0;

                Running = true;
                cleaner.StartCleaning(directory);
            }
        }

        #endregion


        private int scannedDirectories;
        private int deletedDirectories;


        #region EventHandler: Cleaning Done + Directory Scanned


        private void DirectoryScanned(object sender, DirectoryScannedEventArgs e)
        {
            scannedDirectories += 1;
            if (e is DirectoryDeletedEventArgs)
                deletedDirectories += 1;
        }


        private void CleaningDone(object sender, EventArgs e)
        {
            Console.WriteLine("Done. {0} folders scanned. {1} were removed.",
                scannedDirectories,
                deletedDirectories);

            Running = false;

            var caption = "Done c'',)";
            var message = string.Format(CultureInfo.InvariantCulture,
                                        "{0} folders scanned.{1}{2} folders removed.",
                                        scannedDirectories,
                                        Environment.NewLine,
                                        deletedDirectories);

            MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion


        #region EventHandler: Drag + Drop

        private void SomethingDropped(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files == null || files.Length < 1)
                    return;

                var filename = files[0];
                if (filename.IsCleanablePath())
                    path.Text = filename;
            }
        }


        private void SomethingEntered(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                           ? DragDropEffects.Link
                           : DragDropEffects.None;
        }

        #endregion


        #region EventHandler: FormClosing

        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            cleaner.Cancel();
        }

        #endregion

    }
}