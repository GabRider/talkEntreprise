using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Threading;
using System.Threading;
using System.Net.Sockets;

namespace talkEntreprise_client
{
    delegate void VisibleChange();
    delegate void CloseConnection();
    delegate string GetEmployees();
    public class Controler
    {
        private Client _client;
        private Connection _connect;
        private TcpClient _tcpCli;
        private NetworkStream _netStream;

        public NetworkStream NetStream
        {
            get { return _netStream; }
            set { _netStream = value; }
        }
        public TcpClient TcpCli
        {
            get { return _tcpCli; }
            set { _tcpCli = value; }
        }
        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }
       
        public Connection Connect
        {
            get { return _connect; }
            set { _connect = value; }
        }

        public Controler( Connection c)
        {
            this.Connect = c;
            this.Client = new Client(this);
            
        }
        //////méthodes Générales///////

        private void setTcp(TcpClient t, NetworkStream s)
        {
            this.TcpCli = t;
            this.NetStream = s;
        }
        /// <summary>
        /// permet de coder le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns></returns>
        public string sha1(string password)
        {
            //créer une instance sha1
            SHA1 sha1 = SHA1.Create();
            //convertit le texte en byte
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(password));
            //créer une instance StringBuilder pour sauver les hashData
            StringBuilder returnValue = new StringBuilder();
            //transform un tableau en string
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
        public bool connection(string id, string password)
        {
            return this.Client.connection(id,password);
        }
        public void CreateProgram()
        {
            Thread frmProg = new Thread(new ThreadStart(ThreadProgram));
            frmProg.SetApartmentState(ApartmentState.STA);
            frmProg.IsBackground = true;
            frmProg.Start();
        }
        public void ThreadProgram()
        {
            FrmProgram prog = new FrmProgram(this);
            prog.Show();
            Dispatcher.Run();
        }
        public void VisibleChange()
        {
            this.Connect.Visible = !this.Connect.Visible;
        }
        public void CloseConnection()
        {
            this.Client.CloseConnection();
        }
        public void ResetConnection()
        {
            this.Client.ResetConnection();
        }
        public string Employees()
        {
            return this.Client.GetEmployees();
        }
    }
}
