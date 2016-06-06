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

        private NetworkStream _stream;
        private TcpClient _client;
        private ClientConnectToServ _clientServ;
        private User _userInformations;

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

        public User UserInformations
        {
            get { return _userInformations; }
            set { _userInformations = value; }
        }
  
        ////////////Constructeur//////////////////

        public UpdateUser(TcpClient c, NetworkStream s, bool stateConnect, ClientConnectToServ clientToServ, string user)
        {
          
            List<string> userInformations = this.ClientServ.GetInformation(user);            
            this.Client = c;
            this.UserInformations= new User(user, userInformations[2], Convert.ToInt32(userInformations[0]), stateConnect, 0, userInformations[1]);
            this.Stream = s;
            this.ClientServ = clientToServ;
            

        }


        ////////////méthodes//////////////////
        public void update()
        {
            Byte[] sendBytedMessage = null;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;



            while (this.IsConnected)
            {
                //permet de récupérer les informations envoyé par le client
                this.Stream.Read(bytesFrom, 0, bytesFrom.Length);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                if (dataFromClient.Contains("#0002"))
                {

                    this.IsConnected = false;
                }




            }
            this.ClientServ.CloseConnection(this.IdUser);
        }
    }
}
