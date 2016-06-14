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
        private Controler _ctrl;
        private User _userConnected;

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
