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
        //////////Champs//////////
        private NetworkStream _stream;
        private TcpClient _client;
        private ClientConnectToServ _clientServ;
        private User _userInformations;
        ////////////propriétées///////////
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


            this.Client = c;

            this.Stream = s;
            this.ClientServ = clientToServ;
            List<string> userInformations = this.ClientServ.GetInformation(user);
            this.UserInformations = new User(user, userInformations[2], Convert.ToInt32(userInformations[0]), stateConnect, 0, userInformations[1]);
            this.ClientServ.updateAllClient(this.UserInformations.GetNameGroup(), this.UserInformations.GetIdGroup(), this.UserInformations.GetidUser());
        }


        ////////////méthodes//////////////////
        /// <summary>
        /// permet de récupérer les informations du client
        /// </summary>
        public void Update()
        {
            List<string> destinationMessag = new List<string>();
            Byte[] sendBytedMessage = null;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            string sendClient = null;

            ////tant que l'utilisateur est connecté
            while (this.UserInformations.GetInformationConnection())
            {
                destinationMessag.Clear();
                //permet de récupérer les informations envoyé par le client
                this.Stream.Read(bytesFrom, 0, bytesFrom.Length);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                if (dataFromClient.Contains("####"))
                {
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("####"));
                }

                //permet de déconnecter la personne
                if (dataFromClient.Split(';')[0] == "#0002")
                {

                    this.UserInformations.SetConnection(false);
                    
                }


                if (dataFromClient.Split(';')[0] == "#0003" && !dataFromClient.Contains("!"))
                {                  
                    foreach (string messageInformation in dataFromClient.Split(';'))
                    {
                        if (!messageInformation.Contains("#0003"))
                        {
                            this.ClientServ.sendMessage(messageInformation.Split('-')[0], messageInformation.Split('-')[1], messageInformation.Split('-')[2], Convert.ToBoolean(messageInformation.Split('-')[3]));
                            Thread.Sleep(1);
                            this.ClientServ.UpdateAllClientMessages(messageInformation.Split('-')[0], messageInformation.Split('-')[1], Convert.ToBoolean(messageInformation.Split('-')[3]));
                        
                        }
                    }
                }
                    //envoi des messages dans le groupe
                else if (dataFromClient.Split(';')[0] =="#0003" && dataFromClient.Contains("!"))
                {
                    
                       
                            foreach (string info in (dataFromClient.Split(';')[1]).Split('-')[1].Split('!'))
                            {
                                if (info !="")
                                {
                                    this.ClientServ.sendMessage(dataFromClient.Split(';')[1].Split('-')[0], info, dataFromClient.Split('-')[2], Convert.ToBoolean(dataFromClient.Split('-')[3]));
                                }
                                
                            }

                            foreach (string info in (dataFromClient.Split(';')[1]).Split('-')[1].Split('!'))
                            {
                                Thread.Sleep(1);
                                if (info !="")
                                {
                                    this.ClientServ.UpdateAllClientMessages(dataFromClient.Split(';')[1].Split('-')[0], info, Convert.ToBoolean(dataFromClient.Split('-')[3]));
                                
                                }
                                
                            }
                        
                    
                }


                if (dataFromClient.Split(';')[0] =="#0004")
                {
                    foreach (string info in dataFromClient.Split(';'))
                    {
                        if (!info.Contains("#0004"))
                        {
                           
                            this.ClientServ.UpdateAllClientMessages(info.Split('-')[0], info.Split('-')[1], Convert.ToBoolean(info.Split('-')[2]));
                        }
                    }
                }
                if (dataFromClient.Split(';')[0] =="#0005")
                {
                    this.ClientServ.updateAllClient(dataFromClient.Split(';')[1],Convert.ToInt32(dataFromClient.Split(';')[3]),dataFromClient.Split(';')[2]);
                }
              
                //permet de mettre à jour les  états des messages
                if (dataFromClient.Split(';')[0] =="#0006")
                {
                    this.ClientServ.UpdateStateMessages(dataFromClient.Split(';')[1],dataFromClient.Split(';')[2],Convert.ToBoolean(dataFromClient.Split(';')[3]));
                    
                    this.ClientServ.updateAllClient(dataFromClient.Split(';')[4], Convert.ToInt32(dataFromClient.Split(';')[5]), dataFromClient.Split(';')[6]);
                }
                //récupération ancien messages
                if (dataFromClient.Split(';')[0] == "#0007")
                {
                    this.ClientServ.UpdateStateMessages(dataFromClient.Split(';')[1], dataFromClient.Split(';')[2], Convert.ToBoolean(dataFromClient.Split(';')[3]));
                    this.ClientServ.GetOldMessages(dataFromClient.Split(';')[1], dataFromClient.Split(';')[2], Convert.ToBoolean(dataFromClient.Split(';')[3]), Convert.ToInt32(dataFromClient.Split(';')[4]));
                    
                }

            }
            this.ClientServ.CloseConnection(this.UserInformations.GetidUser(), this.UserInformations.GetNameGroup(), this.UserInformations.GetIdGroup());
        }
    }
}
