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
        private string _oldPassword;
        private string _newPassword;
        private FrmProgram _prog;


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
            if (this.OldPassword == this.Prog.sha1(tbxOldPassword.Text))
            {
                if (tbxNewPassword.Text.Trim().Length>=6)
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
        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
          
           
            
            
        }
        public string GetNewPassword()
        {
            return this.NewPassword;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.PasswordIsOk())
            {
                this.NewPassword = this.Prog.sha1(tbxNewPassword.Text);
            }
           
        }
    }
}
