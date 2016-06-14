namespace talkEntreprise_server
{
    partial class FrmProgram
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
            this.msProgram = new System.Windows.Forms.MenuStrip();
            this.tsmIOption = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOldMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToday = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOneDayAgo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTwoDaysAgo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOneWeekAgo = new System.Windows.Forms.ToolStripMenuItem();
            this.tssDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbxMessages = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxWriteMessage = new System.Windows.Forms.TextBox();
            this.lsbEmployees = new System.Windows.Forms.ListBox();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.ssProgram = new System.Windows.Forms.StatusStrip();
            this.msProgram.SuspendLayout();
            this.ssProgram.SuspendLayout();
            this.SuspendLayout();
            // 
            // msProgram
            // 
            this.msProgram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmIOption,
            this.tsmHelp});
            this.msProgram.Location = new System.Drawing.Point(0, 0);
            this.msProgram.Name = "msProgram";
            this.msProgram.Size = new System.Drawing.Size(723, 24);
            this.msProgram.TabIndex = 23;
            this.msProgram.Text = "menuStrip1";
            // 
            // tsmIOption
            // 
            this.tsmIOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings,
            this.toolStripSeparator2,
            this.tsmiQuit});
            this.tsmIOption.Name = "tsmIOption";
            this.tsmIOption.Size = new System.Drawing.Size(56, 20);
            this.tsmIOption.Text = "&Option";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.tsmiSettings.Size = new System.Drawing.Size(227, 22);
            this.tsmiSettings.Text = "&Paramètre du compte";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(224, 6);
            // 
            // tsmiQuit
            // 
            this.tsmiQuit.Name = "tsmiQuit";
            this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsmiQuit.Size = new System.Drawing.Size(227, 22);
            this.tsmiQuit.Text = "&Quitter";
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout,
            this.tsmiOldMessages});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(56, 20);
            this.tsmHelp.Text = "&Edition";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(212, 22);
            this.tsmiAbout.Text = "À &propos de talkEntreprise";
            // 
            // tsmiOldMessages
            // 
            this.tsmiOldMessages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToday,
            this.tsmiOneDayAgo,
            this.tsmiTwoDaysAgo,
            this.tsmiOneWeekAgo});
            this.tsmiOldMessages.Name = "tsmiOldMessages";
            this.tsmiOldMessages.Size = new System.Drawing.Size(212, 22);
            this.tsmiOldMessages.Tag = "0";
            this.tsmiOldMessages.Text = "voir anciens messages";
            // 
            // tsmiToday
            // 
            this.tsmiToday.Checked = true;
            this.tsmiToday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiToday.Name = "tsmiToday";
            this.tsmiToday.Size = new System.Drawing.Size(159, 22);
            this.tsmiToday.Text = "Aujourd\'hui";
            // 
            // tsmiOneDayAgo
            // 
            this.tsmiOneDayAgo.Name = "tsmiOneDayAgo";
            this.tsmiOneDayAgo.Size = new System.Drawing.Size(159, 22);
            this.tsmiOneDayAgo.Tag = "1";
            this.tsmiOneDayAgo.Text = "1 jour avant";
            // 
            // tsmiTwoDaysAgo
            // 
            this.tsmiTwoDaysAgo.Name = "tsmiTwoDaysAgo";
            this.tsmiTwoDaysAgo.Size = new System.Drawing.Size(159, 22);
            this.tsmiTwoDaysAgo.Tag = "2";
            this.tsmiTwoDaysAgo.Text = "2 jours avant";
            // 
            // tsmiOneWeekAgo
            // 
            this.tsmiOneWeekAgo.Name = "tsmiOneWeekAgo";
            this.tsmiOneWeekAgo.Size = new System.Drawing.Size(159, 22);
            this.tsmiOneWeekAgo.Tag = "7";
            this.tsmiOneWeekAgo.Text = "1 semaine avant";
            // 
            // tssDate
            // 
            this.tssDate.Name = "tssDate";
            this.tssDate.Size = new System.Drawing.Size(708, 17);
            this.tssDate.Spring = true;
            // 
            // tbxMessages
            // 
            this.tbxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbxMessages.Location = new System.Drawing.Point(139, 26);
            this.tbxMessages.Multiline = true;
            this.tbxMessages.Name = "tbxMessages";
            this.tbxMessages.ReadOnly = true;
            this.tbxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessages.Size = new System.Drawing.Size(580, 322);
            this.tbxMessages.TabIndex = 24;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(647, 346);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(73, 83);
            this.btnSend.TabIndex = 20;
            this.btnSend.Text = "Envoyer";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // tbxWriteMessage
            // 
            this.tbxWriteMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxWriteMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxWriteMessage.Location = new System.Drawing.Point(139, 347);
            this.tbxWriteMessage.MaxLength = 600;
            this.tbxWriteMessage.Multiline = true;
            this.tbxWriteMessage.Name = "tbxWriteMessage";
            this.tbxWriteMessage.Size = new System.Drawing.Size(509, 81);
            this.tbxWriteMessage.TabIndex = 19;
            // 
            // lsbEmployees
            // 
            this.lsbEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbEmployees.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lsbEmployees.FormattingEnabled = true;
            this.lsbEmployees.ItemHeight = 50;
            this.lsbEmployees.Location = new System.Drawing.Point(1, 74);
            this.lsbEmployees.Name = "lsbEmployees";
            this.lsbEmployees.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lsbEmployees.Size = new System.Drawing.Size(139, 354);
            this.lsbEmployees.TabIndex = 18;
            // 
            // tbxUser
            // 
            this.tbxUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxUser.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.tbxUser.Location = new System.Drawing.Point(1, 26);
            this.tbxUser.Multiline = true;
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.ReadOnly = true;
            this.tbxUser.Size = new System.Drawing.Size(139, 50);
            this.tbxUser.TabIndex = 21;
            this.tbxUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ssProgram
            // 
            this.ssProgram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssDate});
            this.ssProgram.Location = new System.Drawing.Point(0, 439);
            this.ssProgram.Name = "ssProgram";
            this.ssProgram.Size = new System.Drawing.Size(723, 22);
            this.ssProgram.SizingGrip = false;
            this.ssProgram.TabIndex = 22;
            // 
            // FrmProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 461);
            this.Controls.Add(this.msProgram);
            this.Controls.Add(this.tbxMessages);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxWriteMessage);
            this.Controls.Add(this.lsbEmployees);
            this.Controls.Add(this.tbxUser);
            this.Controls.Add(this.ssProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(739, 499);
            this.MinimumSize = new System.Drawing.Size(739, 499);
            this.Name = "FrmProgram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmProgram";
            this.msProgram.ResumeLayout(false);
            this.msProgram.PerformLayout();
            this.ssProgram.ResumeLayout(false);
            this.ssProgram.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msProgram;
        private System.Windows.Forms.ToolStripMenuItem tsmIOption;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiOldMessages;
        private System.Windows.Forms.ToolStripMenuItem tsmiToday;
        private System.Windows.Forms.ToolStripMenuItem tsmiOneDayAgo;
        private System.Windows.Forms.ToolStripMenuItem tsmiTwoDaysAgo;
        private System.Windows.Forms.ToolStripMenuItem tsmiOneWeekAgo;
        private System.Windows.Forms.ToolStripStatusLabel tssDate;
        private System.Windows.Forms.TextBox tbxMessages;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxWriteMessage;
        private System.Windows.Forms.ListBox lsbEmployees;
        private System.Windows.Forms.TextBox tbxUser;
        private System.Windows.Forms.StatusStrip ssProgram;
    }
}