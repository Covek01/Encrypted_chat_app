namespace CipherNew.Forms
{
    partial class Chat
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
            chkEncrypted = new CheckBox();
            listBoxMessages = new ListBox();
            txtInputMessage = new TextBox();
            btnSend = new Button();
            chkRailFenceCaseSensitive = new RadioButton();
            chkXXTeaWithCBC = new RadioButton();
            openFileDialogSend = new OpenFileDialog();
            btnSendFile = new Button();
            btnReceivedFiles = new Button();
            chkRailFence = new RadioButton();
            SuspendLayout();
            // 
            // chkEncrypted
            // 
            chkEncrypted.AutoSize = true;
            chkEncrypted.Location = new Point(34, 27);
            chkEncrypted.Name = "chkEncrypted";
            chkEncrypted.Size = new Size(97, 24);
            chkEncrypted.TabIndex = 0;
            chkEncrypted.Text = "Encrypted";
            chkEncrypted.UseVisualStyleBackColor = true;
            chkEncrypted.CheckedChanged += chkEncrypted_CheckedChanged;
            // 
            // listBoxMessages
            // 
            listBoxMessages.FormattingEnabled = true;
            listBoxMessages.HorizontalScrollbar = true;
            listBoxMessages.Location = new Point(14, 151);
            listBoxMessages.Name = "listBoxMessages";
            listBoxMessages.Size = new Size(701, 284);
            listBoxMessages.TabIndex = 1;
            // 
            // txtInputMessage
            // 
            txtInputMessage.Location = new Point(14, 451);
            txtInputMessage.Name = "txtInputMessage";
            txtInputMessage.Size = new Size(579, 27);
            txtInputMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(615, 444);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(109, 41);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // chkRailFenceCaseSensitive
            // 
            chkRailFenceCaseSensitive.AutoSize = true;
            chkRailFenceCaseSensitive.Checked = true;
            chkRailFenceCaseSensitive.Location = new Point(198, 27);
            chkRailFenceCaseSensitive.Name = "chkRailFenceCaseSensitive";
            chkRailFenceCaseSensitive.Size = new Size(245, 24);
            chkRailFenceCaseSensitive.TabIndex = 6;
            chkRailFenceCaseSensitive.TabStop = true;
            chkRailFenceCaseSensitive.Text = "RailFence cipher (Case Sensitive)";
            chkRailFenceCaseSensitive.UseVisualStyleBackColor = true;
            chkRailFenceCaseSensitive.CheckedChanged += chkRailFence_CheckedChanged_1;
            // 
            // chkXXTeaWithCBC
            // 
            chkXXTeaWithCBC.AutoSize = true;
            chkXXTeaWithCBC.Location = new Point(198, 87);
            chkXXTeaWithCBC.Name = "chkXXTeaWithCBC";
            chkXXTeaWithCBC.Size = new Size(184, 26);
            chkXXTeaWithCBC.TabIndex = 7;
            chkXXTeaWithCBC.Text = "XXTea with CBC cypher";
            chkXXTeaWithCBC.UseCompatibleTextRendering = true;
            chkXXTeaWithCBC.UseVisualStyleBackColor = true;
            chkXXTeaWithCBC.CheckedChanged += chkXXTeaWithCBC_CheckedChanged_1;
            // 
            // openFileDialogSend
            // 
            openFileDialogSend.FileName = "openFileDialogSend";
            // 
            // btnSendFile
            // 
            btnSendFile.Location = new Point(619, 79);
            btnSendFile.Margin = new Padding(3, 4, 3, 4);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(96, 41);
            btnSendFile.TabIndex = 8;
            btnSendFile.Text = "Send file";
            btnSendFile.UseVisualStyleBackColor = true;
            btnSendFile.Click += btnSendFile_Click;
            // 
            // btnReceivedFiles
            // 
            btnReceivedFiles.Location = new Point(474, 79);
            btnReceivedFiles.Margin = new Padding(3, 4, 3, 4);
            btnReceivedFiles.Name = "btnReceivedFiles";
            btnReceivedFiles.Size = new Size(119, 41);
            btnReceivedFiles.TabIndex = 9;
            btnReceivedFiles.Text = "Received files";
            btnReceivedFiles.UseVisualStyleBackColor = true;
            btnReceivedFiles.Click += btnReceivedFiles_Click;
            // 
            // chkRailFence
            // 
            chkRailFence.AutoSize = true;
            chkRailFence.Location = new Point(198, 57);
            chkRailFence.Name = "chkRailFence";
            chkRailFence.Size = new Size(138, 24);
            chkRailFence.TabIndex = 10;
            chkRailFence.Text = "RailFence cipher";
            chkRailFence.UseVisualStyleBackColor = true;
            chkRailFence.CheckedChanged += chkRailFence_CheckedChanged;
            // 
            // Chat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(754, 539);
            Controls.Add(chkRailFence);
            Controls.Add(btnReceivedFiles);
            Controls.Add(btnSendFile);
            Controls.Add(chkXXTeaWithCBC);
            Controls.Add(chkRailFenceCaseSensitive);
            Controls.Add(btnSend);
            Controls.Add(txtInputMessage);
            Controls.Add(listBoxMessages);
            Controls.Add(chkEncrypted);
            Name = "Chat";
            Text = "Chat";
            FormClosing += Chat_FormClosing;
            Load += Chat_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkEncrypted;
        private ListBox listBoxMessages;
        private TextBox txtInputMessage;
        private Button btnSend;
        private RadioButton chkRailFenceCaseSensitive;
        private RadioButton chkXXTeaWithCBC;
        private OpenFileDialog openFileDialogSend;
        private Button btnSendFile;
        private Button btnReceivedFiles;
        private RadioButton chkRailFence;
    }
}