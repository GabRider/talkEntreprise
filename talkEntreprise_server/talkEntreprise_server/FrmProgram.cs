/******************************************
* Projet : TalkEntreprise_server
* Description : création d'une messagerie instantanée
* Date : 15.06.2016
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

namespace talkEntreprise_server
{
    public partial class FrmProgram : Form
    {
        //////Champs////
        private Controler _ctrl;
        private User _userConnected;
        //////Propriétées////////
        public User UserConnected
        {
            get { return _userConnected; }
            set { _userConnected = value; }
        }
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        ////////Constructeur////////
        public FrmProgram(Controler c, User u)
        {
            InitializeComponent();
            this.Ctrl = c;
            this.UserConnected = u;
        }

        private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Ctrl.isVisible();
            this.Ctrl.DeconnectionToServer(this.UserConnected.GetIdUser());
        }
    }
}
