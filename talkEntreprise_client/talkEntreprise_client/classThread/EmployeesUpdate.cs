using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace talkEntreprise_client.classThread
{
    public class EmployeesUpdate 
    {
         private List<User> _lstUser;
        private FrmProgram _program;
        
        public List<User> LstUser
        {
            get { return _lstUser; }
            set { _lstUser = value; }
        }


        public FrmProgram Program
        {
            get { return _program; }
            set { _program = value; }
        }
        public EmployeesUpdate(FrmProgram p)
        {
            this.Program = p;
            
        }

        public void init()
        {
            
      
            string listeUser = string.Empty;
            string[] informations;
            while (true)
            {
                listeUser = ;
                if (!listeUser.Contains("false")&&listeUser.Contains("#0015"))
                {
                    foreach (string userInfo in listeUser.Split(';'))
                    {
                        if (!userInfo.Contains("#0015"))
                        {
                            informations = userInfo.Split(',');
                            LstUser.Add(new User(informations[0],informations[1],Convert.ToInt32(informations[2]) ,Convert.ToBoolean(informations[3]),Convert.ToInt32(informations[4])));
                        }
                        this.Program.updateLstEmployee(this.LstUser);
                    }
                  
                }
            }
        }
       
    }
}
