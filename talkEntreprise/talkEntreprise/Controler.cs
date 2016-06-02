using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise
{
    public class Controler
    {
        private FrmConnection _frmConnect;
        private User _user;
        private Connection _connect;
        private Converter _conversion;
        private FrmProgram _frmMain;
        private RequestSQL _request;
        private ManageMessage _manageFrienMessage;

        public ManageMessage ManageFrienMessage
        {
            get { return _manageFrienMessage; }
            set { _manageFrienMessage = value; }
        }
        public RequestSQL Request
        {
            get { return _request; }
            set { _request = value; }
        }

        public FrmProgram FrmMain
        {
            get { return _frmMain; }
            set { _frmMain = value; }
        }
        public FrmConnection FrmConnect
        {
            get { return _frmConnect; }
            set { _frmConnect = value; }
        }
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public Connection Connect
        {
            get { return _connect; }
            set { _connect = value; }
        }
        public Converter Conversion
        {
            get { return _conversion; }
            set { _conversion = value; }
        }
        ///////////////////////////////////////////////Initialization Controler////////////////////////////////////////////////////

        public Controler(FrmConnection frm)
        {
            this.FrmConnect = frm;
            this.Connect = new Connection(this);
            this.User = new User(this);
            this.Conversion = new Converter(this);
            this.FrmMain = new FrmProgram(this);
            this.Request = new RequestSQL(this);
            this.ManageFrienMessage = new ManageMessage(this);

        }
    }
}
