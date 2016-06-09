using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
    
     public class Controler
    {
        /////Champs/////
        FrmConnection _frmLogin;
        private Server _serv;
        private RequestSQL _request;
        private Converter _conv;

        /////propriétées/////
        public FrmConnection FrmLogin
                 {
            get { return _frmLogin; }
            set { _frmLogin = value; }
        }
        

        public Server Serv
        {
            get { return _serv; }
            set { _serv = value; }
        }
        

        public RequestSQL Request
        {
            get { return _request; }
            set { _request = value; }
        }
         public Converter Conv
        {
            get { return _conv; }
            set { _conv = value; }
        }
        /////Constructeur/////
        public Controler(FrmConnection frm)
        {
            this.FrmLogin = frm;
            this.Request = new RequestSQL(this);
            this.Serv = new Server(this);
            this.Conv = new Converter(this);

        }
       
        //////méthodes RequestSQL///
        /// <summary>
        /// Permet de vérifier si les informations entrées par l'utilisateur sont valide ou non
        /// </summary>
        /// <param name="user">Identifiant de l'utilisateur</param>
        /// <param name="password"> mot de passe de l'utilisateur</param>
        /// <returns>retourne "true" si l'utilisateur à les bonnes informations de connection</returns>
        /// 
        public Boolean validateConnection(string id, string password)
        {
            return this.Request.ValidateConnectionUser(id,password);
        }
        /// <summary>
        /// permet de convertire un nombre en hexadécimal
        /// </summary>
        /// <param name="number">nombre à convertire</param>
        /// <returns>nombre en hexadécimal</returns>
        public string NumberToHexadecimal(int number) 
        {
            return this.Conv.NumberToHexadecimal(number);
        }
        /// <summary>
        /// permet de dire que l'utilisateur c'est connecté sur le server --> log de la base de données
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        public void SucessConnectionToServer(string user)
        {
            this.Request.SucessConnectionToServer(user);
        }
        /// <summary>
        /// permet de dire que l'utilisateur c'est déconnecté du serveur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        public void DeconnectionToServer(string user)
        {
            this.Request.DeconnectionToServer(user);
        }
        /// <summary>
        /// permet de récupérer les informations de l'utilisateur demmandé
        /// </summary>
        /// <param name="user">identifiant de l'utilisateu</param>
        /// <returns>list des informations de l'utilisateur</returns>
        public List<string> GetInformation(string user)
        {
            return this.Request.GetInformation(user);
        }
        /// <summary>
        /// récupération de la liste des employés
        /// </summary>
        /// <param name="nameGroup">nom du groupe de l'utilisateur</param>
        /// <param name="idGroup">identifiant du groupe de l'utilisateur</param>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns>list des employés</returns>
        public List<User> GetUserList(string nameGroup, int idGroup, string user)
        {
            return this.Request.GetUserList(nameGroup,idGroup,user);
        }
        /// <summary>
        /// permet de récupérer les informations relatifs au messages non lu de l'utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="userDestination">identifiant d'un autre utilisateur</param>
        /// <param name="forGroup">si le message est envoyé au groupe</param>
        /// <returns>nombre de message eb abscence</returns> 
        public string GetUserListInString(string nameGroup, int idGroup, string user)
        {
            return this.Request.GetUserListInString(nameGroup, idGroup, user);
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
            this.Request.sendMessage(user, destinationUsername, message, forGroup);
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
            return this.Request.GetConversation(user,destination,forGroup);
        }
         /// <summary>
        /// permet de mettre à jour l'état des messages
        /// </summary>
        /// <param name="user"></param>
        /// <param name="destination"></param>
        /// <param name="forGroup"></param>
        public void UpdateStateMessages(string user, string destination, bool isforGroup)
        {
            this.Request.UpdateStateMessages(user,destination,isforGroup);
        }
         /// <summary>
        /// permet de mettre à zéro tous les utilisateurs
        /// </summary>
        public void SetAllEmployeesDeconnected()
        {
            this.Request.SetAllEmployeesDeconnected();
        }
         //////méthodes Server///////
          /// <summary>
        /// permet de quitter la connection
        /// </summary>
        public void CloseConnection()
        {
            this.Serv.CloseConnection();
        }
    }
}
