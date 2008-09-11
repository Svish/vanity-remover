using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FolderVanityRemover
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = false;

            if (txtFolder.Text != "")
                folderBrowserDialog.SelectedPath = txtFolder.Text;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (txtFolder.Text.Length > 0)
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                backgroundWorker.RunWorkerAsync();
            }
            else
                MessageBox.Show("Choose a folder");
        }
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
                if (Directory.Exists(filename))
                    txtFolder.Text = filename;
            }
        }
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
            DirectoryInfo di = new DirectoryInfo(txtFolder.Text);
            if (di.Exists)
            {
                DeletedFolders = 0;
                DeleteEmpty(di);
                return;
            }
            MessageBox.Show("Folder does not exist");
        }

        private static volatile int DeletedFolders;
        private void DeleteEmpty(DirectoryInfo directory)
        {
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                DeleteEmpty(d);
            }
            if (directory.GetFileSystemInfos().Length == 0)
            {
                try
                {
                    directory.Delete();
                    DeletedFolders++;
                }
                catch (Exception)
                {
                }
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            MessageBox.Show(String.Format("Done. {0} folders were removed.", DeletedFolders));
        }

        private VanitySettings vs;

        private void MainForm_Load(object sender, EventArgs e)
        {
            vs = new VanitySettings();
            txtFolder.DataBindings.Add("Text", vs, "LastFolder");
            //txtFolder.Text = vs.LastFolder;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //vs.LastFolder = txtFolder.Text;
            vs.Save();
        }
    }
}
