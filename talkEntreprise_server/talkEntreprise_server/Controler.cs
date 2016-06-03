using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
    
     public class Controler
    {
        FrmConnection _frmLogin;
        private Server _serv;
        private RequestSQL _request;
        public FrmConnection FrmLogin
        {
            get { return _frmLogin; }
            set { _frmLogin = value; }
        }
        

        public Server Serv
        {
            get { return _serv; }
            set { _serv = value; }
        }
        

        public RequestSQL Request
        {
            get { return _request; }
            set { _request = value; }
        }

        public Controler(FrmConnection frm)
        {
            this.FrmLogin = frm;
            this.Request = new RequestSQL(this);
            this.Serv = new Server(this);
            

        }
        //////méthodes RequestSQL///
        public Boolean validateConnection(string id, string password)
        {
            return this.Request.validateConnectionUser(id,password);
        }
       
    }
}
