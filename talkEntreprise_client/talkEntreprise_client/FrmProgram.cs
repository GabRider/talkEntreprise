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
    public partial class FrmProgram : Form
    {
        private Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }

        public FrmProgram(Controler c)
        {
            InitializeComponent();
            this.Ctrl = c;
        }

        private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
        {

            Invoke(new MethodInvoker(delegate { this.Ctrl.VisibleChange(); }));
            Invoke(new MethodInvoker(delegate { this.Ctrl.CloseConnection(); }));
            Application.Exit();

        }
        
        
    }
}
