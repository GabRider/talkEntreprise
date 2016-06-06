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
        private Converter _conv;

        
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
            this.Conv = new Converter(this);

        }
        public Converter Conv
        {
            get { return _conv; }
            set { _conv = value; }
        }
        //////méthodes RequestSQL///
        public Boolean validateConnection(string id, string password)
        {
            return this.Request.ValidateConnectionUser(id,password);
        }

        public string NumberToHexadecimal(int number) 
        {
            return this.Conv.NumberToHexadecimal(number);
        }
        public void SucessConnectionToServer(string user)
        {
            this.Request.SucessConnectionToServer(user);
        }
        public void DeconnectionToServer(string user)
        {
            this.Request.DeconnectionToServer(user);
        }
        public List<string> GetInformation(string user)
        {
            return this.Request.GetInformation(user);
        }
    }
}
