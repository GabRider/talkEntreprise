using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_client
{
    public class Client
    {
        private Controler _ctrl;
        private TcpClient _clientSocket;
        private NetworkStream _serverStream;
        private string _readData;
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }


        public TcpClient ClientSocket
        {
            get { return _clientSocket; }
            set { _clientSocket = value; }
        }


        public NetworkStream ServerStream
        {
            get { return _serverStream; }
            set { _serverStream = value; }
        }


        public string ReadData
        {
            get { return _readData; }
            set { _readData = value; }
        }

        //////////////Constructeur////////////

        public Client(Controler c)
        {
            this.Ctrl = c;
            this.ClientSocket = new TcpClient();
            this.ServerStream = default(NetworkStream);
            this.ReadData = null;
           
            this.ClientSocket.Connect("127.0.0.1", 8888);
            this.ServerStream = this.ClientSocket.GetStream();
        }
        //////////////méthodes////////////
        /// <summary>
        /// elle permet de savoir si l'utilisateur se trouve dans la base de données
        /// </summary>
        /// <param name="id">identifiant de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns>vrais ou faux</returns>
        public bool connection(string id, string password)
        {
            string cryptedPassword = this.Ctrl.sha1(password);
            return connectionServer(id, cryptedPassword);
        }
        /// <summary>
        /// elle permet d'envoyer l'identifiant et le mot de passe au serveur et récupérer la réponse.
        /// </summary>
        /// <param name="id">identifiant de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns>vrais ou faux</returns>
        private bool connectionServer(string id, string password)
        {
            byte[] inStream = new byte[10025];
            int buffSize = 0;
            string toSend = "#0001;" + id + ";" + password + ";####";
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(toSend);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();


            //Savoir la taille de mémoire à allouer

            //Assignation de la valeur envoyée par le serveur(sous forme de tableau de bytes)
            this.ServerStream.Read(inStream, 0, inStream.Length);
            bool result =Convert.ToBoolean( Encoding.ASCII.GetString(inStream));
            return result;
        }
        public void ResetConnection()
        {
           
            this.ClientSocket = new TcpClient();
            this.ServerStream = default(NetworkStream);
            this.ReadData = null;

            this.ClientSocket.Connect("127.0.0.1", 8888);
            this.ServerStream = this.ClientSocket.GetStream();
        }

        public void CloseConnection()
        {
            byte[] inStream = new byte[10025];
            int buffSize = 0;
            string toSend = "#0002####";
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(toSend + "$");
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
    }
}
