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
            txtInputFolder = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // chkListBoxFiles
            // 
            chkListBoxFiles.FormattingEnabled = true;
            chkListBoxFiles.Location = new Point(12, 12);
            chkListBoxFiles.Name = "chkListBoxFiles";
            chkListBoxFiles.Size = new Size(400, 220);
            chkListBoxFiles.TabIndex = 0;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(313, 245);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(99, 40);
            btnDownload.TabIndex = 1;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // txtInputFolder
            // 
            txtInputFolder.Location = new Point(120, 255);
            txtInputFolder.Name = "txtInputFolder";
            txtInputFolder.Size = new Size(159, 23);
            txtInputFolder.TabIndex = 2;
            txtInputFolder.TextChanged += txtInputFolder_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 258);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 3;
            label1.Text = "Local folder name";
            // 
            // ReceivedFiles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 329);
            Controls.Add(label1);
            Controls.Add(txtInputFolder);
            Controls.Add(btnDownload);
            Controls.Add(chkListBoxFiles);
            Name = "ReceivedFiles";
            Text = "ReceivedFiles";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox chkListBoxFiles;
        private Button btnDownload;
        private TextBox txtInputFolder;
        private Label label1;
    }
}