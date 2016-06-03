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
    public partial class Form1 : Form
    {
        private Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        public Form1()
        {
            InitializeComponent();
            this.Ctrl = new Controler();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.Ctrl.connection(tbxId.Text, tbxPassword.Text))
            {
                MessageBox.Show("k", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("connection impossible ou connexion incorrecte ", "erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
