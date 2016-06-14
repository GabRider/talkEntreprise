using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using talkEntreprise_server.classThread;

namespace talkEntreprise_server
{
    public partial class FrmConnection : Form
    {
        private Controler _ctrl;

        internal Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        public FrmConnection()
        {
            InitializeComponent();
            this.Ctrl = new Controler(this);
            this.Ctrl.SetAllEmployeesDeconnected();
        }

        private void FrmConnection_FormClosing(object sender, FormClosingEventArgs e)
        {


            Application.Exit();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.Ctrl.ValidateConnectionAdmin(tbxId.Text, this.Ctrl.Sha1(tbxPassword.Text)))
            {
                this.IsVisible();
                List<string> lstInfo = this.Ctrl.GetInformation(tbxId.Text);
                User user = new User(tbxId.Text, lstInfo[2], Convert.ToInt32(lstInfo[0]), true, 0, lstInfo[1]);

                this.Ctrl.SucessConnectionToServer(tbxId.Text);

                this.Ctrl.CreateProgram(tbxId.Text, user);
                

            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrecte. ", "Connexion non valide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// changer l'état visuel de la forme de connexion
        /// </summary>
        public void IsVisible()
        {
            Invoke(new MethodInvoker(delegate
            {
                this.Visible = !this.Visible;
            }));

        }
    }
}
