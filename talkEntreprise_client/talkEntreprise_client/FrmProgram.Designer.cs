namespace talkEntreprise_client
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
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxWriteMessage = new System.Windows.Forms.TextBox();
            this.lsbConversations = new System.Windows.Forms.ListBox();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.tsmIOption = new System.Windows.Forms.ToolStripMenuItem();
            this.enregistrersousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msProgram = new System.Windows.Forms.MenuStrip();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tssDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssProgram = new System.Windows.Forms.StatusStrip();
            this.msProgram.SuspendLayout();
            this.ssProgram.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(647, 348);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(73, 80);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Envoyer";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // tbxWriteMessage
            // 
            this.tbxWriteMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxWriteMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxWriteMessage.Location = new System.Drawing.Point(139, 348);
            this.tbxWriteMessage.MaxLength = 600;
            this.tbxWriteMessage.Multiline = true;
            this.tbxWriteMessage.Name = "tbxWriteMessage";
            this.tbxWriteMessage.Size = new System.Drawing.Size(509, 80);
            this.tbxWriteMessage.TabIndex = 12;
            // 
            // lsbConversations
            // 
            this.lsbConversations.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lsbConversations.FormattingEnabled = true;
            this.lsbConversations.ItemHeight = 50;
            this.lsbConversations.Location = new System.Drawing.Point(1, 74);
            this.lsbConversations.Name = "lsbConversations";
            this.lsbConversations.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lsbConversations.Size = new System.Drawing.Size(139, 354);
            this.lsbConversations.TabIndex = 11;
            this.lsbConversations.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lsbConversations_DrawItem);
            // 
            // tbxUser
            // 
            this.tbxUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxUser.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.tbxUser.Location = new System.Drawing.Point(1, 26);
            this.tbxUser.Multiline = true;
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.ReadOnly = true;
            this.tbxUser.Size = new System.Drawing.Size(139, 50);
            this.tbxUser.TabIndex = 14;
            this.tbxUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tsmIOption
            // 
            this.tsmIOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enregistrersousToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitterToolStripMenuItem});
            this.tsmIOption.Name = "tsmIOption";
            this.tsmIOption.Size = new System.Drawing.Size(56, 20);
            this.tsmIOption.Text = "&Option";
            // 
            // enregistrersousToolStripMenuItem
            // 
            this.enregistrersousToolStripMenuItem.Name = "enregistrersousToolStripMenuItem";
            this.enregistrersousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.enregistrersousToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.enregistrersousToolStripMenuItem.Text = "&Paramètre du compte";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(224, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.quitterToolStripMenuItem.Text = "&Quitter";
            // 
            // msProgram
            // 
            this.msProgram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmIOption,
            this.tsmHelp});
            this.msProgram.Location = new System.Drawing.Point(0, 0);
            this.msProgram.Name = "msProgram";
            this.msProgram.Size = new System.Drawing.Size(723, 24);
            this.msProgram.TabIndex = 16;
            this.msProgram.Text = "menuStrip1";
            // 
            // tsmHelp
            // 
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(24, 20);
            this.tsmHelp.Text = "?";
            // 
            // tssDate
            // 
            this.tssDate.Name = "tssDate";
            this.tssDate.Size = new System.Drawing.Size(708, 17);
            this.tssDate.Spring = true;
            // 
            // ssProgram
            // 
            this.ssProgram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssDate});
            this.ssProgram.Location = new System.Drawing.Point(0, 438);
            this.ssProgram.Name = "ssProgram";
            this.ssProgram.Size = new System.Drawing.Size(723, 22);
            this.ssProgram.SizingGrip = false;
            this.ssProgram.TabIndex = 15;
            // 
            // FrmProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 460);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxWriteMessage);
            this.Controls.Add(this.lsbConversations);
            this.Controls.Add(this.tbxUser);
            this.Controls.Add(this.msProgram);
            this.Controls.Add(this.ssProgram);
            this.Name = "FrmProgram";
            this.Text = "FrmProgram";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgram_FormClosing);
            this.msProgram.ResumeLayout(false);
            this.msProgram.PerformLayout();
            this.ssProgram.ResumeLayout(false);
            this.ssProgram.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxWriteMessage;
        private System.Windows.Forms.ListBox lsbConversations;
        private System.Windows.Forms.TextBox tbxUser;
        private System.Windows.Forms.ToolStripMenuItem tsmIOption;
        private System.Windows.Forms.ToolStripMenuItem enregistrersousToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.MenuStrip msProgram;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripStatusLabel tssDate;
        private System.Windows.Forms.StatusStrip ssProgram;
    }
}