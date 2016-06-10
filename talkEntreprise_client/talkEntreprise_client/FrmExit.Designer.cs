namespace talkEntreprise_client
{
    partial class FrmExit
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.pbxImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(334, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 43);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(26, 211);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(129, 43);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Quitter";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(174, 211);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(127, 43);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "Connexion";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // pbxImg
            // 
            this.pbxImg.ErrorImage = null;
            this.pbxImg.Image = global::talkEntreprise_client.Properties.Resources.Question;
            this.pbxImg.InitialImage = global::talkEntreprise_client.Properties.Resources.Question;
            this.pbxImg.Location = new System.Drawing.Point(141, 12);
            this.pbxImg.Name = "pbxImg";
            this.pbxImg.Size = new System.Drawing.Size(186, 187);
            this.pbxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImg.TabIndex = 7;
            this.pbxImg.TabStop = false;
            // 
            // FrmExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 272);
            this.Controls.Add(this.pbxImg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(486, 310);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(486, 310);
            this.Name = "FrmExit";
            this.Text = "FrmExit";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxImg;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnYes;
    }
}