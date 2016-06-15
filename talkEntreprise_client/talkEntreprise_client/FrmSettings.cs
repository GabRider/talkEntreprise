/******************************************
* Projet : TalkEntreprise_client
* Description : création d'une messagerie instantanée
* Date : juin 2016
* Version : 1.0
* Auteur :Gabriel Strano
*
******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace talkEntreprise_client
{
    public partial class FrmSettings : Form
    {
        ////Champs//////
        private string _oldPassword;
        private string _newPassword;
        private FrmProgram _prog;
        ////propriétées//////
        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value; }
        }
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; }
        }
        public FrmProgram Prog
        {
            get { return _prog; }
            set { _prog = value; }
        }
        public FrmSettings(FrmProgram p, string pwd)
        {
            InitializeComponent();
            this.Prog = p;
            this.OldPassword = pwd;
            this.NewPassword = string.Empty;
        }
        private bool PasswordIsOk()
        {
            if (this.OldPassword == this.Prog.Sha1(tbxOldPassword.Text))
            {
                if (tbxNewPassword.Text.Trim().Length >= 6)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Votre nouveau mot de passe est trop court", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }
            else
            {
                MessageBox.Show("Vous avez tapé le mauvais mot de passe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        public string GetNewPassword()
        {
            return this.NewPassword;
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (this.PasswordIsOk())
            {
                this.NewPassword = this.Prog.Sha1(tbxNewPassword.Text);
            }
        }
    }
}
