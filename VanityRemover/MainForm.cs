using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security;
using System.Windows.Forms;

namespace FolderVanityRemover
{
    partial class MainForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Settings
        /// </summary>
        private VanitySettings settings;

        /// <summary>
        /// Restore last folder on load
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load images
            Assembly a = Assembly.GetExecutingAssembly();
            folderButton.Image = new Bitmap(a.GetManifestResourceStream("FolderVanityRemover.Icons.folder.png"));
            goButton.Image = new Bitmap(a.GetManifestResourceStream("FolderVanityRemover.Icons.go.png"));
            
            // Load settings
            settings = new VanitySettings();
            folderTextbox.DataBindings.Add("Text", settings, "LastFolder");
        }

        /// <summary>
        /// Save last folder on closing
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.Save();
        }

        /// <summary>
        /// Show folder browser dialog
        /// </summary>
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = false;

            if (!String.IsNullOrEmpty(folderTextbox.Text))
                folderBrowserDialog.SelectedPath = folderTextbox.Text;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderTextbox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Start the background worker
        /// </summary>
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (folderTextbox.Text.Length > 0)
            {
                progressBar.Style = ProgressBarStyle.Marquee;
                backgroundWorker.RunWorkerAsync();
            }
            else
                MessageBox.Show("Choose a folder");
        }

        /// <summary>
        /// Drag Drop
        /// </summary>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
                if (Directory.Exists(filename))
                    folderTextbox.Text = filename;
            }
        }

        /// <summary>
        /// Drag Enter
        /// </summary>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(folderTextbox.Text);
            if (di.Exists)
            {
                DeletedFolders = 0;
                DeleteEmpty(di);
                return;
            }
            MessageBox.Show("Folder does not exist", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Deleted folders counter.
        /// </summary>
        private static volatile int DeletedFolders;

        /// <summary>
        /// Deletes a directory if it is empty.
        /// </summary>
        /// <param name="directory">Directory to delete if empty.</param>
        private void DeleteEmpty(DirectoryInfo directory)
        {
            // Run this same method on all directories in this directory
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                DeleteEmpty(d);
            }
            // If directory has no files or folders in it
            if (directory.GetFileSystemInfos().Length == 0)
            {
                try
                {
                    // Try to delete
                    directory.Delete();

                    // Increase counter
                    DeletedFolders++;
                }
                catch (DirectoryNotFoundException)
                {
                    // Already gone for some reason...
                }
                catch (IOException)
                {
                    // Not empty...
                }
                catch (SecurityException)
                {
                    // Not permission to delete...
                }
            }
        }

        /// <summary>
        /// Message and progressbar back to normal when completed
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            MessageBox.Show(DeletedFolders + " folders were removed.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
