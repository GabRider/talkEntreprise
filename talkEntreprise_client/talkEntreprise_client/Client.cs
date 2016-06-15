/******************************************
* Projet : TalkEntreprise_client
* Description : création d'une messgaerie instantanée
* Date : juin 2016
* Version : 1.0
* Auteur :Gabriel Strano
*
******************************************/
using System;
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
        }
        //////////////méthodes////////////
        /// <summary>
        /// elle permet de savoir si l'utilisateur se trouve dans la base de données
        /// </summary>
        /// <param name="id">identifiant de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns>vrais ou faux</returns>
        public bool Connection(string id, string password)
        {
            string cryptedPassword = this.Ctrl.Sha1(password);
            return ConnectionServer(id, cryptedPassword);
        }
        /// <summary>
        /// elle permet d'envoyer l'identifiant et le mot de passe au serveur et récupérer la réponse.
        /// </summary>
        /// <param name="id">identifiant de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns>vrais ou faux</returns>
        private bool ConnectionServer(string id, string password)
        {
            Thread.Sleep(10);
            byte[] inStream = new byte[10025];
            string toSend = "#0001;" + id + ";" + password + ";####";
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(toSend);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
            //Assignation de la valeur envoyée par le serveur(sous forme de tableau de bytes)
            this.ServerStream.Read(inStream, 0, inStream.Length);
            bool result = Convert.ToBoolean(Encoding.ASCII.GetString(inStream));
            if (result)
            {
                this.Ctrl.SetTcpClientAndNetworkStream(this.ClientSocket, this.ServerStream);
            }
            return result;
        }
        /// <summary>
        /// permet de recréer une connection avec le serveur
        /// </summary>
        public bool ResetConnection()
        {
            try
            {
                this.ClientSocket = new TcpClient();
                this.ServerStream = default(NetworkStream);
                this.ClientSocket.Connect("127.0.0.1", 8888);
                this.ServerStream = this.ClientSocket.GetStream();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// permet d'envoyer un message au serveur pour lui dire de se déconnecter
        /// </summary>
        public void CloseConnection()
        {
            byte[] inStream = new byte[10025];
            ;
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
        public User GetInformationUserConnected()
        {
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
        /// <summary>
        /// permet d'envoyer le message ua serveur
        /// </summary>
        /// <param name="message">message</param>
        public bool SendMessage(string user, string destination, string message, bool forGroup)
        {
            string sendMessage = "#0003;" + user + "-" + destination + "-" + this.Ctrl.EncryptMessage(message) + "-" + forGroup + "#####";
            byte[] inStream = new byte[10025];
            try
            {
                //Encode le texte en tableau de byte
                byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
                //Envoie au serveur les données
                this.ServerStream.Write(outStream, 0, outStream.Length);
                //Efface l'historique
                this.ServerStream.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// permet d'envoyer le message au serveur
        /// </summary>
        /// <param name="message">message</param>
        public void SendMessageGroup(string user, string Alldestination, string message, bool forGroup)
        {
            string sendMessage = "#0003;" + user + "-" + Alldestination + "-" + this.Ctrl.EncryptMessage(message) + "-" + forGroup + "#####";
            byte[] inStream = new byte[10025];
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
        /// <summary>
        /// permet d'afficher la conversation de l'utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire du message</param>
        /// <param name="forGroup">si c'est pour le groupe</param>
        public void GetConversation(string user, string destination, bool forGroup)
        {
            string sendMessage = "#0004;" + user + "-" + destination + "-" + forGroup + "#####";
            byte[] inStream = new byte[10025];

            try
            {
                //Encode le texte en tableau de byte
                byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
                //Envoie au serveur les données
                this.ServerStream.Write(outStream, 0, outStream.Length);
                //Efface l'historique
                this.ServerStream.Flush();

            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// met à jour la liste des employés
        /// </summary>
        /// <param name="nameGroupe">nom du groupe de l'utilisateur</param>
        /// <param name="user"> identifiant de l'utilisateur</param>
        /// <param name="idGroup">id du groupe de l'utilisateur</param>
        public void UpdateUsers(string nameGroupe, string user, int idGroup)
        {
            string sendMessage = "#0005;" + nameGroupe + ";" + user + ";" + idGroup + "#####";
            byte[] inStream = new byte[10025];


            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
        /// <summary>
        /// permet de mettre à jour l'état des messages
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire</param>
        /// <param name="isForGroup">pour un groupe</param>
        public void UpdateStateMessages(string user, string destination, bool isForGroup, string nameGroup, int idGroup, string userSecure)
        {
            string sendMessage = "#0006;" + user + ";" + destination + ";" + isForGroup + ";" + nameGroup + ";" + idGroup + ";" + userSecure + "#####";
            byte[] inStream = new byte[10025];
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
        /// <summary>
        /// permet de récupérer les anciens messages
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire</param>
        /// <param name="forGroup">pour le groupe</param>
        /// <param name="nbDays">jour avant aujourd'huit</param>
        public void GetOldMessages(string user, string destination, bool forGroup, int nbDays)
        {
            string sendMessage = "#0007;" + user + ";" + destination + ";" + forGroup + ";" + nbDays + "#####";
            byte[] inStream = new byte[10025];
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
        /// <summary>
        /// permet de modifier le mot de passe actuelle
        /// </summary>
        /// <param name="password"></param>
        public void ChangePassword(string user, string password)
        {
            string sendMessage = "#0008;" + user + ";" + password + "#####";
            byte[] inStream = new byte[10025];
            //Encode le texte en tableau de byte
            byte[] outStream = Encoding.ASCII.GetBytes(sendMessage);
            //Envoie au serveur les données
            this.ServerStream.Write(outStream, 0, outStream.Length);
            //Efface l'historique
            this.ServerStream.Flush();
        }
    }
}
