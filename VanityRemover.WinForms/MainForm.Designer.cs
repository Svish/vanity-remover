namespace Geeky.VanityRemover
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.path = new System.Windows.Forms.TextBox();
            this.pathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dropZoneText = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.browse = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.AccessibleDescription = "Type in the path to clean here.";
            this.path.AccessibleName = "Path";
            this.path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.errorProvider.SetIconAlignment(this.path, System.Windows.Forms.ErrorIconAlignment.BottomRight);
            this.path.Location = new System.Drawing.Point(39, 6);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(175, 20);
            this.path.TabIndex = 1;
            this.path.TextChanged += new System.EventHandler(this.PathChanged);
            // 
            // pathDialog
            // 
            this.pathDialog.Description = "Please select the folder to clean:";
            this.pathDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.pathDialog.ShowNewFolderButton = false;
            // 
            // dropZoneText
            // 
            this.dropZoneText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dropZoneText.AutoSize = true;
            this.dropZoneText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropZoneText.ForeColor = System.Drawing.Color.Gray;
            this.dropZoneText.Location = new System.Drawing.Point(22, 54);
            this.dropZoneText.Name = "dropZoneText";
            this.dropZoneText.Size = new System.Drawing.Size(174, 32);
            this.dropZoneText.TabIndex = 3;
            this.dropZoneText.Text = "Give me a folder and\r\nthe vanity within will go away";
            this.dropZoneText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // start
            // 
            this.start.AccessibleDescription = "Starts the cleaning process.";
            this.start.AccessibleName = "Start";
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.start.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.start.Enabled = false;
            this.start.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.start.Image = global::Geeky.VanityRemover.Properties.Resources.go;
            this.start.Location = new System.Drawing.Point(187, 111);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(27, 24);
            this.start.TabIndex = 2;
            this.start.Tag = "";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.StartClicked);
            // 
            // browse
            // 
            this.browse.AccessibleDescription = "Opens up a folder browse dialog for selecting the path to clean.";
            this.browse.AccessibleName = "Browse";
            this.browse.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.browse.Image = global::Geeky.VanityRemover.Properties.Resources.browse;
            this.browse.Location = new System.Drawing.Point(6, 3);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(27, 24);
            this.browse.TabIndex = 0;
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.BrowseClicked);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 113);
            this.progressBar.MarqueeAnimationSpeed = 20;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(142, 20);
            this.progressBar.TabIndex = 8;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // cancel
            // 
            this.cancel.AccessibleDescription = "Cancels the cleaning process.";
            this.cancel.AccessibleName = "Cancel";
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Enabled = false;
            this.cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.cancel.Image = global::Geeky.VanityRemover.Properties.Resources.stop;
            this.cancel.Location = new System.Drawing.Point(154, 111);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(27, 24);
            this.cancel.TabIndex = 3;
            this.cancel.Tag = "";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.CancelClicked);
            // 
            // MainForm
            // 
            this.AcceptButton = this.start;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(219, 141);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.start);
            this.Controls.Add(this.dropZoneText);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.path);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(230, 150);
            this.Name = "MainForm";
            this.Text = "Vanity remover";
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SomethingDropped);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SomethingEntered);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.FolderBrowserDialog pathDialog;
        private System.Windows.Forms.Label dropZoneText;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button cancel;
    }
}