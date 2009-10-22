using System;
using System.Threading;
using System.Windows.Forms;
using Geeky.VanityRemover.Core;
using System.Drawing;

namespace Geeky.VanityRemover
{
    /// <summary>
    /// The main and only form of this application.
    /// </summary>
    internal partial class MainForm : Form
    {
        private readonly ICleaner cleaner;
        private readonly IPathValidator pathValidator;

        /// <summary>
        /// Creates a new <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Icon = Resources.Application;
            browse.Image = Resources.Browse;
            clean.Image = Resources.Go;

            pathValidator = new PathValidator();
            cleaner = new Cleaner
                          {
                              Context = SynchronizationContext.Current,
                          };
            cleaner.CleaningDone += CleaningDone;

        }


        /// <summary>
        /// Show path dialog.
        /// </summary>
        private void browseButtonClick(object sender, EventArgs e)
        {
            pathDialog.SelectedPath = path.Text;

            if (pathDialog.ShowDialog() == DialogResult.OK)
                path.Text = pathDialog.SelectedPath;
        }

        /// <summary>
        /// Start cleaner.
        /// </summary>
        private void cleanClick(object sender, EventArgs e)
        {
            // Fix UI
            progressBar.Style = ProgressBarStyle.Marquee;
            path.Enabled = false;
            browse.Enabled = false;
            clean.Image = Resources.Stop;

            AcceptButton = null;
            CancelButton = clean;

            // Start cleaner, or cancel if already running.
            if (!cleaner.Clean(path.Text))
                cleaner.Cancel();

        }


        /// <summary>
        /// Cleaning done
        /// </summary>
        private void CleaningDone(object sender, CleaningDoneEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            
            MessageBox.Show(e.ToString(), "Cleaning done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            path.Enabled = true;
            browse.Enabled = true;
            clean.Image = Resources.Go;

            AcceptButton = clean;
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
                if (pathValidator.IsValid(filename))
                    path.Text = filename;
            }
        }

        /// <summary>
        /// Drag Enter.
        /// </summary>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // Only accept files
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                           ? DragDropEffects.Link
                           : DragDropEffects.None;
        }

        /// <summary>
        /// Folder text changed.
        /// </summary>
        private void pathTextChanged(object sender, EventArgs e)
        {
            var isValid = pathValidator.IsValid(path.Text);

            clean.Enabled = isValid;
            path.BackColor = isValid ? Color.AliceBlue : Color.LightCoral;
        }

        /// <summary>
        /// Cancel on close.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cleaner.Cancel();
        }
    }
}