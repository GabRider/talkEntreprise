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
            if (this.Ctrl.ResetConnection())
            {


                if (this.Ctrl.Connection(tbxId.Text, tbxPassword.Text))
                {
                    tbxPassword.Clear();
                    tbxPassword.Focus();
                    this.Visible = !this.Visible;
                    this.Ctrl.SetUserConnected();
                    Thread.Sleep(40);
                    this.Ctrl.CreateProgram(tbxId.Text);
                }
                else
                {  
                    MessageBox.Show("Identifiant,mot de passe incorrecte ou utilisateur déjà connecté. ", "Connexion non valide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Le serveur est inaccessible pour le moment réessayé ultérieurement", "Serveur injoignable", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// permet de modifier la visibilité de la vue
        /// </summary>
        public void VisibleChange()
        {
            Invoke(new MethodInvoker(delegate
            {
                 this.Visible = !this.Visible;
            }));
           
        }
    }
}
