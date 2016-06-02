using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise
{
   public class ManageMessage
    {
       private Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        public ManageMessage(Controler c)
        {
            this.Ctrl = c;
        }
    }
}
