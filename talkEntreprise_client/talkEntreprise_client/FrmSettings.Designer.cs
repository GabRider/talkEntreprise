namespace talkEntreprise_client
{
    partial class FrmSettings
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
            this.tbxNewPassword = new System.Windows.Forms.TextBox();
            this.tbxOldPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxNewPassword
            // 
            this.tbxNewPassword.Location = new System.Drawing.Point(189, 68);
            this.tbxNewPassword.Name = "tbxNewPassword";
            this.tbxNewPassword.PasswordChar = '*';
            this.tbxNewPassword.Size = new System.Drawing.Size(140, 20);
            this.tbxNewPassword.TabIndex = 0;
            // 
            // tbxOldPassword
            // 
            this.tbxOldPassword.Location = new System.Drawing.Point(189, 103);
            this.tbxOldPassword.Name = "tbxOldPassword";
            this.tbxOldPassword.PasswordChar = '*';
            this.tbxOldPassword.Size = new System.Drawing.Size(140, 20);
            this.tbxOldPassword.TabIndex = 1;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(58, 71);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(123, 13);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "Nouveau mot de passe :";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(69, 110);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(112, 13);
            this.lblOldPassword.TabIndex = 3;
            this.lblOldPassword.Text = "Ancien mot de passe :";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(157, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Modifier";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(254, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Annuler";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblInfo.Location = new System.Drawing.Point(29, 31);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(413, 17);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "le nouveau mot de passe doit être au minimum de 6 charactères";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 191);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.tbxOldPassword);
            this.Controls.Add(this.tbxNewPassword);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 229);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 229);
            this.Name = "FrmSettings";
            this.Text = "Paramètres";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxNewPassword;
        private System.Windows.Forms.TextBox tbxOldPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblInfo;
    }
}