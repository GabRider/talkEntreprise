namespace talkEntreprise
{
    partial class FrmConnection
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblmdp = new System.Windows.Forms.Label();
            this.lblPseudo = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbxmdp = new System.Windows.Forms.TextBox();
            this.tbxPseudo = new System.Windows.Forms.TextBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblmdp
            // 
            this.lblmdp.AutoSize = true;
            this.lblmdp.Location = new System.Drawing.Point(60, 61);
            this.lblmdp.Name = "lblmdp";
            this.lblmdp.Size = new System.Drawing.Size(76, 13);
            this.lblmdp.TabIndex = 15;
            this.lblmdp.Text = "mot de passe: ";
            // 
            // lblPseudo
            // 
            this.lblPseudo.AutoSize = true;
            this.lblPseudo.Location = new System.Drawing.Point(57, 15);
            this.lblPseudo.Name = "lblPseudo";
            this.lblPseudo.Size = new System.Drawing.Size(49, 13);
            this.lblPseudo.TabIndex = 14;
            this.lblPseudo.Text = "Pseudo: ";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(69, 101);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connexion";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // tbxmdp
            // 
            this.tbxmdp.Location = new System.Drawing.Point(137, 58);
            this.tbxmdp.Name = "tbxmdp";
            this.tbxmdp.Size = new System.Drawing.Size(100, 20);
            this.tbxmdp.TabIndex = 12;
            this.tbxmdp.Text = "123";
            this.tbxmdp.UseSystemPasswordChar = true;
            // 
            // tbxPseudo
            // 
            this.tbxPseudo.Location = new System.Drawing.Point(137, 12);
            this.tbxPseudo.Name = "tbxPseudo";
            this.tbxPseudo.Size = new System.Drawing.Size(100, 20);
            this.tbxPseudo.TabIndex = 11;
            this.tbxPseudo.Text = "gabriel@aspirateur.com";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(196, 101);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(100, 23);
            this.btnQuit.TabIndex = 16;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            // 
            // FrmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 142);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lblmdp);
            this.Controls.Add(this.lblPseudo);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbxmdp);
            this.Controls.Add(this.tbxPseudo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TalkEntreprise";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblmdp;
        private System.Windows.Forms.Label lblPseudo;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbxmdp;
        private System.Windows.Forms.TextBox tbxPseudo;
        private System.Windows.Forms.Button btnQuit;
    }
}

