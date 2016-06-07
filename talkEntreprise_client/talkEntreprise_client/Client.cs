﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace talkEntreprise_client
{
    public class Client
    {

        //////////Champs//////////

        private Controler _ctrl;
        private TcpClient _clientSocket;
        private NetworkStream _serverStream;


        ////////////propriétées///////////

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



        //////////////Constructeur////////////

        public Client(Controler c)
        {
            this.Ctrl = c;
            this.ClientSocket = new TcpClient();
            this.ServerStream = default(NetworkStream);


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
            bool result = Convert.ToBoolean(Encoding.ASCII.GetString(inStream));

            if (result)
            {
                this.Ctrl.setTcpClientAndNetworkStream(this.ClientSocket, this.ServerStream);
            }
            return result;
        }
        /// <summary>
        /// permet de recréer une connection avec le serveur
        /// </summary>
        public void ResetConnection()
        {

            this.ClientSocket = new TcpClient();
            this.ServerStream = default(NetworkStream);
           

            this.ClientSocket.Connect("127.0.0.1", 8888);
            this.ServerStream = this.ClientSocket.GetStream();
        }
        /// <summary>
        /// permet d'envoyer un message au serveur pour lui dire de se connecter
        /// </summary>
        public void CloseConnection()
        {
            byte[] inStream = new byte[10025];
            int buffSize = 0;
            string toSend = "#0002####";
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(toSend + "####");
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
        /// <summary>
        /// permet de récupérer les informations de l'utilisateur
        /// </summary>
        /// <param name="user"> identifiant user</param>
        /// <returns>utilisateur</returns>
        public User getInformationUserConnected()
        {
            bool first = true;
            byte[] inStream = new byte[10025];
            List<string> lstInfo = new List<string>();
           

            this.ServerStream.Read(inStream, 0, inStream.Length);

            string result = Encoding.ASCII.GetString(inStream);
            result = result.Substring(0, result.IndexOf("####"));
            result = result.Split(';')[1];
            foreach (string info in result.Split(','))
            {
             
                    lstInfo.Add(info);

            }
            return new User(lstInfo[0], lstInfo[3], Convert.ToInt32(lstInfo[1]), true, 0, lstInfo[2]);
         }
    }
}
