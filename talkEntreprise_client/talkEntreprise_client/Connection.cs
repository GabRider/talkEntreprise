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
    public partial class Connection : Form
    {
        private Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        public Connection()
        {
            InitializeComponent();
            this.Ctrl = new Controler(this);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.Ctrl.connection(tbxId.Text, tbxPassword.Text))
            {
                this.Ctrl.CreateProgram();
            }
            else {
                this.Ctrl.ResetConnection();
                MessageBox.Show("connection impossible ou connexion incorrecte ", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
