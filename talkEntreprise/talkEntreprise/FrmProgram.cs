using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace talkEntreprise
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
            this.Ctrl = c;
            InitializeComponent();
        }
    }
}
