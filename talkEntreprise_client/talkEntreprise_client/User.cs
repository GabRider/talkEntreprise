/******************************************
* Projet : TalkEntreprise_client
* Description : création d'une messagerie instantanée
* Date : juin 2016
* Version : 1.0
* Auteur :Gabriel Strano
*
******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_client
{
    public class User
    {
        ///////Champs///////////
        private string _idUser;
        private string _password;
        private int _idGroup;
        private bool _connection;
        private string _groupeName;
        private bool _forGroup;
        private string _admin;

        ///////propriétées///////

        public bool Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public string IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public int IdGroup
        {
            get { return _idGroup; }
            set { _idGroup = value; }
        }
        private int _messageNotRead;

        public int MessageNotRead
        {
            get { return _messageNotRead; }
            set { _messageNotRead = value; }
        }
        public string GroupeName
        {
            get { return _groupeName; }
            set { _groupeName = value; }
        }
        public string Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
        public bool ForGroup
        {
            get { return _forGroup; }
            set { _forGroup = value; }
        }
        ////////Constructeur/////////

        public User(string id, string pwd, int group, bool connect, int nbMessagesNotRead, string nameGroup)
        {
            SetUser(id, pwd, group, connect, nbMessagesNotRead, nameGroup);
        }

        /////////méthodes/////////
        /// <summary>
        /// permet d'initialiser les informations de l'utilisateur
        /// </summary>
        /// <param name="id">identifiant de connexion</param>
        /// <param name="pwd">mot de passe de l'utilisateur</param>
        /// <param name="group">numéro du groupe de l'utilisateur</param>
        /// <param name="connect">état de connection de l'utilisateur</param>
        /// <param name="nbMessagesNotRead">nombre de message en attente</param>
        /// <param name="nameGroup">nom du groupe de l'utilisateur</param>
        public void SetUser(string id, string pwd, int group, bool connect, int nbMessagesNotRead, string nameGroup)
        {
            this.IdUser = id;
            this.Password = pwd;
            this.IdGroup = group;
            this.Connection = connect;
            this.MessageNotRead = nbMessagesNotRead;
            this.GroupeName = nameGroup;

            if (this.IdGroup == 3)
            {
                this.Admin = "Administrateur";
            }
            else
            {
                this.Admin = "";
            }
        }
        ///////////méthodes//////////
        /// <summary>
        /// donne le nom du groupe de l'utiliateur
        /// </summary>
        /// <returns>nom du groupe de l'utilisateur</returns>
        public string GetNameGroup()
        {
            return this.GroupeName;
        }
        /// <summary>
        /// donne l'identifiant de connexion de l'utilisateur
        /// </summary>
        /// <returns></returns>
        public string GetIdUser()
        {
            return this.IdUser;
        }
        /// <summary>
        /// donne l'information si l'utilisateur est connecté ou pas
        /// </summary>
        /// <returns>true ou false</returns>
        public bool GetInformationConnection()
        {
            return this.Connection;
        }
        /// <summary>
        /// donne l'identifiant du groupe de l'utilisateur
        /// </summary>
        /// <returns></returns>
        public int GetIdGroup()
        {
            return this.IdGroup;
        }
        /// <summary>
        /// retourne le nombre de message non lu de l'utilisateur
        /// </summary>
        /// <returns>nombre de message non lu</returns>
        public int GetMessagesNotRead()
        {
            return this.MessageNotRead;
        }
        /// <summary>
        /// permet de mettre à jour le nombre de message non lu de l'utilisateur
        /// </summary>
        /// <param name="nbmessagesNotRead">nombre de messages non lu</param>
        public void SetMessagesNotRead(int nbmessagesNotRead)
        {
            this.MessageNotRead = nbmessagesNotRead;
        }
        /// <summary>
        /// met à jour la connexion de l'utilisateur
        /// </summary>
        /// <param name="b"></param>
        public void SetConnection(bool b)
        {
            this.Connection = b;
        }
        /// <summary>
        /// retourne le mot de passe de l'utilisateur
        /// </summary>
        /// <returns>mot de passe de l'utilisateur</returns>
        public string GetPassword()
        {
            return this.Password;
        }
        /// <summary>
        /// si la personne est admin
        /// </summary>
        /// <returns>si admin</returns>
        public string GetAdmin()
        {
            return this.Admin;
        }
        public void SetPassword(string password)
        {
            this.Password = password;
        }
    }
}
