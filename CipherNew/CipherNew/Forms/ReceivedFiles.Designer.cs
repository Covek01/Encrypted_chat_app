namespace CipherNew.Forms
{
    partial class ReceivedFiles
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
            chkListBoxFiles = new CheckedListBox();
            btnDownload = new Button();
            label1 = new Label();
            lblFolderName = new Label();
            btnChooseFolder = new Button();
            SuspendLayout();
            // 
            // chkListBoxFiles
            // 
            chkListBoxFiles.FormattingEnabled = true;
            chkListBoxFiles.Location = new Point(14, 16);
            chkListBoxFiles.Margin = new Padding(3, 4, 3, 4);
            chkListBoxFiles.Name = "chkListBoxFiles";
            chkListBoxFiles.Size = new Size(457, 290);
            chkListBoxFiles.TabIndex = 0;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(358, 327);
            btnDownload.Margin = new Padding(3, 4, 3, 4);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(113, 53);
            btnDownload.TabIndex = 1;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 389);
            label1.Name = "label1";
            label1.Size = new Size(129, 20);
            label1.TabIndex = 3;
            label1.Text = "Local folder name";
            // 
            // lblFolderName
            // 
            lblFolderName.AutoSize = true;
            lblFolderName.Location = new Point(169, 389);
            lblFolderName.Name = "lblFolderName";
            lblFolderName.Size = new Size(0, 20);
            lblFolderName.TabIndex = 4;
            // 
            // btnChooseFolder
            // 
            btnChooseFolder.Location = new Point(218, 335);
            btnChooseFolder.Margin = new Padding(3, 4, 3, 4);
            btnChooseFolder.Name = "btnChooseFolder";
            btnChooseFolder.Size = new Size(113, 36);
            btnChooseFolder.TabIndex = 5;
            btnChooseFolder.Text = "Choose folder";
            btnChooseFolder.UseVisualStyleBackColor = true;
            btnChooseFolder.Click += btnChooseFolder_Click;
            // 
            // ReceivedFiles
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(518, 439);
            Controls.Add(btnChooseFolder);
            Controls.Add(lblFolderName);
            Controls.Add(label1);
            Controls.Add(btnDownload);
            Controls.Add(chkListBoxFiles);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ReceivedFiles";
            Text = "ReceivedFiles";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox chkListBoxFiles;
        private Button btnDownload;
        private Label label1;
        private Label lblFolderName;
        private Button btnChooseFolder;
    }
}