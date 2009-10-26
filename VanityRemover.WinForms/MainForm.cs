using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Geeky.VanityRemover.Core;

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

            pathValidator = new PathValidator();

            cleaner = new Cleaner
                          {
                              Context = SynchronizationContext.Current,
                          };
            cleaner.CleaningDone += CleaningDone;

            ActiveControl = path;

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

                start.Enabled = !value && pathValidator.IsValid(path.Text);
                cancel.Enabled = value;
            }
        }


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
            var directory = new DirectoryInfo(path.Text);

            var caption = "Please confirm";
            var message = "All empty folders will be deleted from" + Environment.NewLine + Environment.NewLine + directory.FullName;

            var result = MessageBox.Show(this, message, caption,
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                Running = true;
                cleaner.Clean(directory);
            }
        }


        private void CleaningDone(object sender, CleaningDoneEventArgs e)
        {
            Running = false;

            var caption = "Done c'',)";
            var message = string.Format(CultureInfo.InvariantCulture,
                "{0} folders scanned.{1}{2} folders removed.",
                e.Total,
                Environment.NewLine,
                e.Deleted);

            MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void SomethingDropped(object sender, DragEventArgs e)
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


        private void SomethingEntered(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                           ? DragDropEffects.Link
                           : DragDropEffects.None;
        }


        private void PathChanged(object sender, EventArgs e)
        {
            var isValid = pathValidator.IsValid(path.Text);

            start.Enabled = isValid;
            path.BackColor = isValid ? Color.AliceBlue : Color.LightCoral;
        }


        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            cleaner.Cancel();
        }
    }
}