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
            this.path = new System.Windows.Forms.TextBox();
            this.pathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dropZoneText = new System.Windows.Forms.Label();
            this.clean = new System.Windows.Forms.Button();
            this.browse = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.errorProvider.SetIconAlignment(this.path, System.Windows.Forms.ErrorIconAlignment.BottomRight);
            this.path.Location = new System.Drawing.Point(39, 6);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(175, 20);
            this.path.TabIndex = 0;
            this.path.TextChanged += new System.EventHandler(this.pathTextChanged);
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
            // clean
            // 
            this.clean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clean.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.clean.Enabled = false;
            this.clean.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.clean.Location = new System.Drawing.Point(6, 111);
            this.clean.Name = "clean";
            this.clean.Size = new System.Drawing.Size(27, 24);
            this.clean.TabIndex = 2;
            this.clean.Tag = "";
            this.clean.UseVisualStyleBackColor = true;
            this.clean.Click += new System.EventHandler(this.cleanClick);
            // 
            // browse
            // 
            this.browse.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.browse.Location = new System.Drawing.Point(6, 3);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(27, 24);
            this.browse.TabIndex = 1;
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browseButtonClick);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(39, 113);
            this.progressBar.MarqueeAnimationSpeed = 20;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(175, 20);
            this.progressBar.TabIndex = 8;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AcceptButton = this.clean;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 141);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.clean);
            this.Controls.Add(this.dropZoneText);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.path);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(230, 150);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vanity remover";
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.FolderBrowserDialog pathDialog;
        private System.Windows.Forms.Label dropZoneText;
        private System.Windows.Forms.Button clean;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}