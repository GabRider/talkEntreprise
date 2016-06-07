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
    public class ClientConnectToServ
    {

        ///////champs///////

        private Server _serv;
        private Thread _userUpdate;

        ////propriétées/////

        public Server Serv
        {
            get { return _serv; }
            set { _serv = value; }
        }


        public Thread UserUpdate
        {
            get { return _userUpdate; }
            set { _userUpdate = value; }
        }

        //////////////////Constructeur///////////
        public ClientConnectToServ(Server s)
        {
            this.Serv = s;
        }
        //////////////////méthodes///////////
        /// <summary>
        /// permet de savoir si l'utilisateur existe dans la base de données
        /// </summary>
        /// <param name="id">identifiant de connexion de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns>true ou false</returns>
        public Boolean validateConnection(string id, string password)
        {
            return this.Serv.validateConnection(id, password);
        }
        /// <summary>
        /// permet de vérifier si un nouveau client essye de communiquer
        /// </summary>
        public void init()
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();

            counter = 0;
            while ((true))
            {
                counter += 1;
                //si un client veut se connecter on accepte
                clientSocket = serverSocket.AcceptTcpClient();
                byte[] sendBytedMessage = null;
                byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;
                string user = string.Empty;
                string password = string.Empty;
                bool sendToClient = false;
                string sendInfo = string.Empty;
                List<string> UserInfo = new List<string>();
                ///initialisation du flux et récupération des informations envoyé par le client
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                if (dataFromClient.Contains("#0001"))
                {
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("####"));
                    user = dataFromClient.Split(';')[1];
                    password = dataFromClient.Split(';')[2];
                }
                //si la base de données connait l'utilisateur
                if (this.validateConnection(user, password))
                {
                    this.Serv.SucessConnectionToServer(user);
                    sendToClient = true;

                    this.Serv.addClientList(user, clientSocket);
                    //   this.UserUpdate = new Thread(new UpdateUser(clientSocket, networkStream, sendToClient, this).update);




                }
                //envoi true ou false au client 
                Thread.Sleep(10);
                sendBytedMessage = Encoding.ASCII.GetBytes(sendToClient.ToString());
                networkStream.Write(sendBytedMessage, 0, sendBytedMessage.Length);

                
                if (sendToClient)
                {
                    Thread.Sleep(10);
                UserInfo= this.GetInformation(user);
                sendInfo = "#0004;"+user+",";
                foreach (var info in UserInfo)
                {
                    sendInfo += info + ",";
                }
                sendInfo += "####";
                sendBytedMessage = Encoding.ASCII.GetBytes(sendInfo);
                networkStream.Write(sendBytedMessage, 0, sendBytedMessage.Length);
                    //permet de mettre à jour la liste des threads
                    this.UserUpdate = new Thread(new UpdateUser(clientSocket, networkStream, sendToClient, this, user).update);
                    this.Serv.AddThreadList(user, UserUpdate);
                    this.UserUpdate.Start();
                }

            }

            clientSocket.Close();
            serverSocket.Stop();

        }
        /// <summary>
        /// permet de mettre à jour l'état d'un utilisateur lors de sa déconnection
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        public void CloseConnection(string user, string nameGroup, int idGroup)
        {
            Thread t = this.Serv.getThreadlist(user);
            this.Serv.DeconnectionToServer(user);
            this.updateAllClient(nameGroup, idGroup, user);
            this.Serv.DelInList(user);
            t.Abort();
        }
        /// <summary>
        /// permet de prendre les informations d'un utilisateur depuis la base de données
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns>list des information de l'utilisateur</returns>
        public List<string> GetInformation(string user)
        {
            return this.Serv.GetInformation(user);
        }
        /// <summary>
        /// permet de mettre à jour toute les personnes connectés
        /// </summary>
        /// <param name="nameGroup">nom du groupe</param>
        /// <param name="idGroup">identifiant du groupe</param>
        /// <param name="user">identifiant de l'utilisateur</param>
        public void updateAllClient(string nameGroup, int idGroup, string user)
        {
            //envoiela liste des employee à l'utilisateur qui vient de se connecter
            byte[] sendBytedMessage = null;
            TcpClient client = this.Serv.GetTcpClientInClientList(user);
            NetworkStream stream = client.GetStream();
            Thread.Sleep(10);
            if (this.Serv.IsInClientList(user))
            {
                sendBytedMessage = Encoding.ASCII.GetBytes(this.Serv.GetUserListInString(nameGroup, idGroup, user) + "####");
                stream.Write(sendBytedMessage, 0, sendBytedMessage.Length);
            }

            //permet d'envoyer la mise à jour des employés seulement aux employés du mêmes groupe et qui sont connecté.
            foreach (User Employees in this.Serv.GetUserList(nameGroup, idGroup, user))
            {
                if (this.Serv.IsInClientList(Employees.GetidUser()))
                {
                    client = this.Serv.GetTcpClientInClientList(Employees.GetidUser());
                    stream = client.GetStream();
                    Thread.Sleep(10);
                    sendBytedMessage = Encoding.ASCII.GetBytes(this.Serv.GetUserListInString(nameGroup, idGroup, Employees.GetidUser()) + "####");
                    stream.Write(sendBytedMessage, 0, sendBytedMessage.Length);
                }

            }
        }
    }

}
