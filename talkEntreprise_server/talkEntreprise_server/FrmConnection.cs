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
        }
    }
}
