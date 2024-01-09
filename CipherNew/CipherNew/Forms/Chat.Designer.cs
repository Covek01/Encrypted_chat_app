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
            chkRailFence = new RadioButton();
            chkXXTeaWithCBC = new RadioButton();
            openFileDialogSend = new OpenFileDialog();
            btnSendFile = new Button();
            btnReceivedFiles = new Button();
            SuspendLayout();
            // 
            // chkEncrypted
            // 
            chkEncrypted.AutoSize = true;
            chkEncrypted.Location = new Point(30, 20);
            chkEncrypted.Margin = new Padding(3, 2, 3, 2);
            chkEncrypted.Name = "chkEncrypted";
            chkEncrypted.Size = new Size(79, 19);
            chkEncrypted.TabIndex = 0;
            chkEncrypted.Text = "Encrypted";
            chkEncrypted.UseVisualStyleBackColor = true;
            chkEncrypted.CheckedChanged += chkEncrypted_CheckedChanged;
            // 
            // listBoxMessages
            // 
            listBoxMessages.FormattingEnabled = true;
            listBoxMessages.ItemHeight = 15;
            listBoxMessages.Location = new Point(12, 110);
            listBoxMessages.Margin = new Padding(3, 2, 3, 2);
            listBoxMessages.Name = "listBoxMessages";
            listBoxMessages.Size = new Size(614, 199);
            listBoxMessages.TabIndex = 1;
            // 
            // txtInputMessage
            // 
            txtInputMessage.Location = new Point(12, 338);
            txtInputMessage.Margin = new Padding(3, 2, 3, 2);
            txtInputMessage.Name = "txtInputMessage";
            txtInputMessage.Size = new Size(507, 23);
            txtInputMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(538, 333);
            btnSend.Margin = new Padding(3, 2, 3, 2);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(95, 31);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // chkRailFence
            // 
            chkRailFence.AutoSize = true;
            chkRailFence.Enabled = false;
            chkRailFence.Location = new Point(173, 20);
            chkRailFence.Margin = new Padding(3, 2, 3, 2);
            chkRailFence.Name = "chkRailFence";
            chkRailFence.Size = new Size(114, 19);
            chkRailFence.TabIndex = 6;
            chkRailFence.TabStop = true;
            chkRailFence.Text = "RailFence cypher";
            chkRailFence.UseVisualStyleBackColor = true;
            chkRailFence.CheckedChanged += chkRailFence_CheckedChanged_1;
            // 
            // chkXXTeaWithCBC
            // 
            chkXXTeaWithCBC.AutoSize = true;
            chkXXTeaWithCBC.Enabled = false;
            chkXXTeaWithCBC.Location = new Point(173, 46);
            chkXXTeaWithCBC.Margin = new Padding(3, 2, 3, 2);
            chkXXTeaWithCBC.Name = "chkXXTeaWithCBC";
            chkXXTeaWithCBC.Size = new Size(149, 22);
            chkXXTeaWithCBC.TabIndex = 7;
            chkXXTeaWithCBC.TabStop = true;
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
            btnSendFile.Location = new Point(542, 59);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(84, 31);
            btnSendFile.TabIndex = 8;
            btnSendFile.Text = "Send file";
            btnSendFile.UseVisualStyleBackColor = true;
            btnSendFile.Click += btnSendFile_Click;
            // 
            // btnReceivedFiles
            // 
            btnReceivedFiles.Location = new Point(415, 59);
            btnReceivedFiles.Name = "btnReceivedFiles";
            btnReceivedFiles.Size = new Size(104, 31);
            btnReceivedFiles.TabIndex = 9;
            btnReceivedFiles.Text = "Received files";
            btnReceivedFiles.UseVisualStyleBackColor = true;
            btnReceivedFiles.Click += btnReceivedFiles_Click;
            // 
            // Chat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(660, 404);
            Controls.Add(btnReceivedFiles);
            Controls.Add(btnSendFile);
            Controls.Add(chkXXTeaWithCBC);
            Controls.Add(chkRailFence);
            Controls.Add(btnSend);
            Controls.Add(txtInputMessage);
            Controls.Add(listBoxMessages);
            Controls.Add(chkEncrypted);
            Margin = new Padding(3, 2, 3, 2);
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
        private RadioButton chkRailFence;
        private RadioButton chkXXTeaWithCBC;
        private OpenFileDialog openFileDialogSend;
        private Button btnSendFile;
        private Button btnReceivedFiles;
    }
}