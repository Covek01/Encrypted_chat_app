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
            listBoxMessages.Location = new Point(14, 147);
            listBoxMessages.Name = "listBoxMessages";
            listBoxMessages.Size = new Size(701, 264);
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
            // chkRailFence
            // 
            chkRailFence.AutoSize = true;
            chkRailFence.Enabled = false;
            chkRailFence.Location = new Point(198, 26);
            chkRailFence.Name = "chkRailFence";
            chkRailFence.Size = new Size(141, 24);
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
            chkXXTeaWithCBC.Location = new Point(198, 61);
            chkXXTeaWithCBC.Name = "chkXXTeaWithCBC";
            chkXXTeaWithCBC.Size = new Size(184, 26);
            chkXXTeaWithCBC.TabIndex = 7;
            chkXXTeaWithCBC.TabStop = true;
            chkXXTeaWithCBC.Text = "XXTea with CBC cypher";
            chkXXTeaWithCBC.UseCompatibleTextRendering = true;
            chkXXTeaWithCBC.UseVisualStyleBackColor = true;
            chkXXTeaWithCBC.CheckedChanged += chkXXTeaWithCBC_CheckedChanged_1;
            // 
            // Chat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(754, 539);
            Controls.Add(chkXXTeaWithCBC);
            Controls.Add(chkRailFence);
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
        private RadioButton chkRailFence;
        private RadioButton chkXXTeaWithCBC;
    }
}