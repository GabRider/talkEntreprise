using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace talkEntreprise_client
{
    public partial class FrmConnection : Form
    {

        private Controler _ctrl;
        private bool _reConnection;


        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        public bool ReConnection
        {
            get { return _reConnection; }
            set { _reConnection = value; }
        }
        public FrmConnection()
        {
            InitializeComponent();
            this.Ctrl = new Controler(this);
            this.ReConnection = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.ReConnection)
            {
                this.Ctrl.ResetConnection();
            }
            else
            {
                this.ReConnection = true;
            }
            if (this.Ctrl.Connection(tbxId.Text, tbxPassword.Text))
            {

                this.Visible = !this.Visible;
                this.Ctrl.setUserConnected();
                Thread.Sleep(40);
                this.Ctrl.CreateProgram(tbxId.Text);
            }
            else
            {
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
