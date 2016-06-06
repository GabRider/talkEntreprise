using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using talkEntreprise_server;

namespace talkEntreprise_server.classThread
{
    public class UpdateUser
    {
        private bool _isConnected;
        private NetworkStream _stream;
        private TcpClient _client;
        private ClientConnectToServ _clientServ;
        private string _idUser;

       
        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; }
        }
        public NetworkStream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }
        

        public TcpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
       

        private ClientConnectToServ ClientServ
        {
            get { return _clientServ; }
            set { _clientServ = value; }
        }
        public string IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }
        ////////////Constructeur//////////////////
      
        public UpdateUser(TcpClient c, NetworkStream s, bool stateConnect,ClientConnectToServ clientToServ, string user)
        {
            this.Client = c;
            this.IsConnected = stateConnect;
            this.Stream = s;
            this.ClientServ = clientToServ;
            this.IdUser = user;
        }
        ////////////méthodes//////////////////
        public void update()
        {
            Byte[] sendBytedMessage = null;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;

           

            while (!this.IsConnected)
            {
                //permet de récupérer les informations envoyé par le client
                this.Stream.Read(bytesFrom, 0, bytesFrom.Length);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                if (dataFromClient =="deconnection")
                {
                    this.ClientServ.CloseConnection(this.IdUser);
                }
               

              
               
               
            }
            this.ClientServ.CloseConnection(this.IdUser);
        }
    }
}
