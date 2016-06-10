﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using talkEntreprise_server.classThread;

namespace talkEntreprise_server
{
    public class Server
    {
        /////Champs//////

        //tableau contenant les différentes connexions (nom utilisateur, connexion)
        public static Hashtable clientsList = new Hashtable();
        public static Hashtable UserThreadsList = new Hashtable();
        private Controler _ctrl;
        private Thread _firstConnection;

        ///////propriété/////////

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }


        public Thread FirstConnection
        {
            get { return _firstConnection; }
            set { _firstConnection = value; }
        }


        
        /////Constructeur////
       
        public Server(Controler c)
        {
            this.Ctrl = c;

            this.FirstConnection = new Thread(new ClientConnectToServ(this).init);
            this.FirstConnection.IsBackground = true;
            this.FirstConnection.Start();
            
        }

        /////////méthodes////////
     
        /// <summary>
        /// permet d'ajouter le nouveau client à la liste de client
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="tcp">connection au client</param>
        public void addClientList(string user, TcpClient tcp)
        {
            if (!clientsList.ContainsKey(user))
            {
                clientsList.Add(user, tcp);
            }
        }
        /// <summary>
        /// permet d'ajouter le nouveau processus à la liste de processus
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="t">processus de l'utilisateur</param>
           public void AddThreadList(string user,Thread t)
        {
            if (!UserThreadsList.ContainsKey(user))
            {
                UserThreadsList.Add(user, t);
            }
          
        }
        /// <summary>
        /// permet de récupérer le processus d'un utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns>processus de l'utilisateur concerné</returns>
           public Thread getThreadlist(string user)
           {
               return UserThreadsList[user] as Thread;
           }
        /// <summary>
        /// permet la suppresion de la connection d'un utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
           public void DelInList(string user)
           {
               UserThreadsList.Remove(user);
               clientsList.Remove(user);
           }
        /// <summary>
        /// permet de savoir su l'utilisateur se trouve dans la base de données
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns></returns>
        public Boolean validateConnection(string user, string password)
        {
            return this.Ctrl.validateConnection(user, password);
        }
        /// <summary>
        /// permet d'envoyer un message à la base de données pour dir que l'utilisateur c'est connectée
        /// </summary>
        /// <param name="user">identifiant de l'utiliseur</param>
        public void SucessConnectionToServer(string user)
        {
            this.Ctrl.SucessConnectionToServer(user);
        }
        /// <summary>
        /// permet d'envoyer l'information à la base de données que l'utilisateur c'est déconnecté
        /// </summary>
        /// <param name="user"></param>
        public void DeconnectionToServer(string user)
        {
            this.Ctrl.DeconnectionToServer(user);
        }
        /// <summary>
        /// permet de récupérer les informations de l'utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns></returns>
        public List<string> GetInformation(string user)
        {
            return this.Ctrl.GetInformation(user);
        }
        /// <summary>
        /// récupération de la liste des employés par rapport à l'utilisateur envoyé
        /// </summary>
        /// <param name="nameGroup">nom du groupe des utilisateurs</param>
        /// <param name="idGroup">identifiant du groupe des utilisateurs</param>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns></returns>
        public List<User> GetUserList(string nameGroup, int idGroup, string user)
        {
            return this.Ctrl.GetUserList(nameGroup, idGroup, user);
        }
        /// <summary>
        /// permert de récupérer la liste des employés pour l'envoyer au client
        /// </summary>
        /// <param name="nameGroup"> nom du groupe des utilisateur</param>
        /// <param name="idGroup">identifiant du groupe des utilisateur</param>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns></returns>
        public string GetUserListInString(string nameGroup, int idGroup, string user)
        {
            return this.Ctrl.GetUserListInString(nameGroup, idGroup, user);
        }
        /// <summary>
        /// récupératuion de la connexion au client par rapport à son identifant de connexion
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns>connnexion du client</returns>
        public TcpClient GetTcpClientInClientList(string user)
        {
            return clientsList[user] as TcpClient;
        }
        /// <summary>
        /// vérifie si l'utilisateur est connecté sur le serveur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns>connnexion du client</returns>
        public bool IsInClientList(string user)
        {
            return clientsList.ContainsKey(user);
        }
           
         /// <summary>
         /// permet  d'enregistrer le message dans la base de données
         /// </summary>
         /// <param name="message">message crypter</param>
         /// <param name="user">nom de l'utilisateur</param>
         /// <param name="destinationUsername">destinataire du message</param>
         /// <param name="forGroup">si c'est pour un groupe</param>
        public void sendMessage(string user, string destinationUsername, string message, bool forGroup)
        {
            this.Ctrl.sendMessage(user, destinationUsername, message, forGroup);
        }
        /// <summary>
        /// permet de récupérer les messages envoyé par les utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire du messahe</param>
        /// <param name="forGroup">si c'est pour le groupe</param>
        /// <returns></returns>
        public List<Message> GetConversation(string user, string destination, bool forGroup)
        {
            return this.Ctrl.GetConversation(user, destination, forGroup);
        }
         /// permet de récupérer les anciens messages envoyé par les utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire du messahe</param>
        /// <param name="forGroup">si c'est pour le groupe</param>
        /// <returns></returns>
        public List<Message> GetOldConversation(string user, string destination, bool forGroup, int nbDays)
        {
            return this.Ctrl.GetOldConversation(user,destination,forGroup,nbDays);
        }
        /// <summary>
        /// permet de mettre à jour l'état des messages
        /// </summary>
        /// <param name="user"></param>
        /// <param name="destination"></param>
        /// <param name="forGroup"></param>
        public void UpdateStateMessages(string user, string destination, bool isforGroup)
        {
            this.Ctrl.UpdateStateMessages(user, destination, isforGroup);
        }
        /// <summary>
        /// permet de changer le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="password">nouveau mot de passe de l'utilisateur</param>
        /// <returns>réussit ou annuler</returns>
        public bool ChangePassword(string user, string password)
        {
            return this.Ctrl.ChangePassword(user, password);
        }
    }


   
}
