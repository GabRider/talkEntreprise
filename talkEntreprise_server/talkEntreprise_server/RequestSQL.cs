using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
    public class RequestSQL
    {

        ///////Champs//////

        private const int STATEDEFAULT = 2;
        private const int STATENOTREAD = 3;
        private const int STATEREAD = 4;
        private const int IDADMINISTRATORS = 3;
        private MySqlConnection _connectionUser;

        /////propriétées///// 

        public MySqlConnection ConnectionUser
        {
            get { return _connectionUser; }
            set { _connectionUser = value; }
        }
        private Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }

        ///////Constructeur//// 

        public RequestSQL(Controler c)
        {
            this.Ctrl = c;
        }

        ///////méhthodes////

        /// <summary>
        /// permet d'initialiser la connexion à la base de données
        /// </summary>
        /// <returns>true false</returns>
        public bool ConnectionDB()
        {
            try
            {

                string connectionString = @"server=127.0.0.1;userid=IT;password=Super;database=db_talkEntreprise";
                this.ConnectionUser = new MySqlConnection(connectionString);
                this.ConnectionUser.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }

        }

        /// <summary>
        /// permet de fermer la connexion à la base de données
        /// </summary>
        public void ShutdownConnectionDB()
        {
            ConnectionUser.Close();
        }
        /// <summary>
        /// Permet de vérifier si les informations entrées par l'utilisateur sont valide ou non
        /// </summary>
        /// <param name="user">Identifiant de l'utilisateur</param>
        /// <param name="password"> mot de passe de l'utilisateur</param>
        /// <returns>retourne "true" si l'utilisateur à les bonnes informations de connection</returns>
        /// 
        public Boolean ValidateConnectionUser(string user, string password)
        {
            bool result = false;
            if (this.ConnectionDB())
            {
                string sql = String.Format("SELECT Count(*) as total FROM t_users where  idUser  = '{0}' AND Password = '{1}'", user, password);
                MySqlCommand cmd = new MySqlCommand(sql, ConnectionUser);
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (Convert.ToInt32(reader.GetString("total")) == 1)
                {

                    result = true;

                }
               
                reader.Close();

                this.ShutdownConnectionDB();

            }
           
            return result;
        }
        /// <summary>
        /// permet de récupérer les informations de l'utilisateur demmandé
        /// </summary>
        /// <param name="user">identifiant de l'utilisateu</param>
        /// <returns>list des informations de l'utilisateur</returns>
        public List<string> GetInformation(string user)
        {
            List<string> lstInfoUser = new List<string>();

            if (this.ConnectionDB())
            {
                string sql = String.Format("SELECT u.idGroup, g.group, password FROM t_users u, t_group g where u.idGroup = g.idGroup AND  idUser  = '{0}'", user);
                MySqlCommand cmd = new MySqlCommand(sql, ConnectionUser);
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                lstInfoUser.Add(reader.GetString("idGroup"));
                lstInfoUser.Add(reader.GetString("group"));
                lstInfoUser.Add(reader.GetString("password"));
            }


            return lstInfoUser;
        }

        /// <summary>
        /// permet de dire que l'utilisateur c'est connecté sur le server --> log de la base de données
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        public void SucessConnectionToServer(string user)
        {

            if (this.ConnectionDB())
            {
                string destination = "Host";
                long lastId = 0;

                string sql = string.Format("INSERT INTO t_log (`Code`,`lenTot`,`CodeSender`,`lenSender`,`valueSender`,`CodeDestination`,`lenDestination`,`valueDestination`,`valueDate`,`CodeEnd`) VALUES( '{0}', '{1}' , '{2}' , '{3}' , '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                    "0001",//0 quel type de message
                    "0000", //1 longueur de tout la chaine (depuis idCode sender à idCodeEnd)
                    "0011", //2 Code Envoye
                    this.Ctrl.NumberToHexadecimal(user.Count()),//3 longueur de l'identifiant
                    user, //4 identifiant de la personne
                    "0012",//5 Code Destinataire
                    this.Ctrl.NumberToHexadecimal(destination.Count()), //6 longueur du destinataire
                    destination, //7 identifiant du destinataire
                    DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd_HH-mm-ss"),//date de la connection (heure universelle)
                    "#####" // Code de fin
                    );

                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                cmd.ExecuteNonQuery();
                lastId = cmd.LastInsertedId;
                cmd.CommandText = String.Format("SELECT * FROM `t_log` where idLog = {0}", lastId);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                sql = String.Format("Update `t_log` set lenTot = '{0}' where idLog = {1}",
                this.Ctrl.NumberToHexadecimal(
                    reader.GetString("CodeSender").Count() + reader.GetString("lenSender").Count() + reader.GetString("valueSender").Count()
                    + reader.GetString("CodeDestination").Count() + reader.GetString("lenDestination").Count() + reader.GetString("valueDestination").Count()
                    + reader.GetString("CodeDate").Count() + reader.GetString("lenDate").Count() + reader.GetString("valueDate").Count()
                    ), lastId);
                reader.Close();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cmd.CommandText = String.Format("Update `t_users` set Connection = 1 where idUser = '{0}'", user);
                cmd.ExecuteNonQuery();
                this.ShutdownConnectionDB();
            }
        }

        /// <summary>
        /// permet de dire que l'utilisateur c'est déconnecté du serveur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        public void DeconnectionToServer(string user)
        {

            if (ConnectionDB())
            {


                string destination = "Host";
                long lastId = 0;

                string sql = string.Format("INSERT INTO t_log (`Code`,`lenTot`,`CodeSender`,`lenSender`,`valueSender`,`CodeDestination`,`lenDestination`,`valueDestination`,`valueDate`,`CodeEnd`) VALUES( '{0}', '{1}' , '{2}' , '{3}' , '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                      "0002",//0 quel type de message
                      "0000", //1 longueur de tout la chaine (depuis idCode sender à idCodeEnd)
                      "0011", //2 Code Envoye
                      this.Ctrl.NumberToHexadecimal(user.Count()),//3 longueur de l'identifiant
                      user, //4 identifiant de la personne
                      "0012",//5 Code Destinataire
                      this.Ctrl.NumberToHexadecimal(destination.Count()), //6 longueur du destinataire
                      destination, //7 identifiant du destinataire
                       DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd_HH-mm-ss"),//date de la connection (heure universelle)
                      "#####" // Code de fin
                      );
                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                cmd.ExecuteNonQuery();
                lastId = cmd.LastInsertedId;
                cmd.CommandText = String.Format("SELECT * FROM `t_log` where idLog = {0}", lastId);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                sql = String.Format("Update `t_log` set lenTot = '{0}' where idLog = {1}",
                this.Ctrl.NumberToHexadecimal(
                    reader.GetString("CodeSender").Count() + reader.GetString("lenSender").Count() + reader.GetString("valueSender").Count()
                    + reader.GetString("CodeDestination").Count() + reader.GetString("lenDestination").Count() + reader.GetString("valueDestination").Count()
                    + reader.GetString("CodeDate").Count() + reader.GetString("lenDate").Count() + reader.GetString("valueDate").Count()
                    ), lastId);
                reader.Close();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cmd.CommandText = String.Format("Update `t_users` set Connection = 0 where idUser = '{0}'", user);
                cmd.ExecuteNonQuery();
                this.ShutdownConnectionDB();
            }
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
            List<User> lsbUsers = new List<User>();
            bool first = true;
            string sql = string.Empty;
            //  bool ConnectedFriend = true;
            //bool NotConnectedFriend = true;
            //  List<string> lsbFriends = new List<string>();
            lsbUsers.Add(new User(nameGroup, "", idGroup, true, 0, nameGroup));
            if (idGroup != IDADMINISTRATORS)
            {
                sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser != \"{1}\" OR idGroup = {2} AND idUser != \"{3}\" ORDER BY `idUser` ASC", IDADMINISTRATORS, user, idGroup, user);
            }
            else
            {
                sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser != \"{1}\" ORDER BY `idUser` ASC", idGroup, user);
            }


            if (this.ConnectionDB())
            {



                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lsbUsers.Add(new User(reader.GetString("idUser"), reader.GetString("password"), Convert.ToInt32(reader.GetString("idGroup")), Convert.ToBoolean(reader.GetString("connection")), 0, nameGroup));

                }
                ShutdownConnectionDB();
            }
            else
            {
                lsbUsers.Add(new User("DB", "", idGroup, false, 0, nameGroup));
            }
            foreach (User friend in lsbUsers)
            {

                if (first)
                {
                    friend.SetMessagesNotRead(GetStatesMessages(user, friend.GetidUser(), true));
                    first = false;
                }
                else
                {
                    friend.SetMessagesNotRead(GetStatesMessages(user, friend.GetidUser(), false));
                }

            }
            return lsbUsers;
        }
        /// <summary>
        /// récupération de la liste des employés
        /// </summary>
        /// <param name="nameGroup">nom du groupe de l'utilisateur</param>
        /// <param name="idGroup">identifiant du groupe de l'utilisateur</param>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <returns>chaine de caractère contenant la liste des employés</returns>
        public string GetUserListInString(string nameGroup, int idGroup, string user)
        {
            string result = "#0015;";
            List<User> lsbUsers = new List<User>();
            bool first = true;
            string sql;

            lsbUsers.Add(new User(nameGroup, "", idGroup, true, 0, nameGroup));
            if (idGroup != IDADMINISTRATORS)
            {
                sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser != \"{1}\" OR idGroup = {2} AND idUser != \"{3}\" ORDER BY `idUser` ASC", IDADMINISTRATORS, user, idGroup, user);
            }
            else
            {
                sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser != \"{1}\" ORDER BY `idUser` ASC", idGroup, user);
            }
            if (this.ConnectionDB())
            {



                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lsbUsers.Add(new User(reader.GetString("idUser"), reader.GetString("password"), Convert.ToInt32(reader.GetString("idGroup")), Convert.ToBoolean(reader.GetString("connection")), 0, nameGroup));

                }
                ShutdownConnectionDB();
            }
            else
            {
                lsbUsers.Add(new User("DB", "", idGroup, false, 0, nameGroup));
            }

            foreach (User userInfo in lsbUsers)
            {

                if (first)
                {
                    userInfo.SetMessagesNotRead(GetStatesMessages(user, userInfo.GetidUser(), true));
                    result += userInfo.GetidUser() + "," + userInfo.GetPassword() + "," + userInfo.GetIdGroup() + "," + userInfo.GetInformationConnection() + "," + userInfo.GetMessagesNotRead() + "," + userInfo.GetMessagesNotRead() + "," + nameGroup;
                    first = false;
                }
                else
                {
                    userInfo.SetMessagesNotRead(GetStatesMessages(user, userInfo.GetidUser(), false));
                    result += ";" + userInfo.GetidUser() + "," + userInfo.GetPassword() + "," + userInfo.GetIdGroup() + "," + userInfo.GetInformationConnection() + "," + userInfo.GetMessagesNotRead() + "," + userInfo.GetMessagesNotRead() + "," + nameGroup;
                }

            }
            return result;
        }
        /// <summary>
        /// permet de récupérer les informations relatifs au messages non lu de l'utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="userDestination">identifiant d'un autre utilisateur</param>
        /// <param name="forGroup">si le message est envoyé au groupe</param>
        /// <returns>nombre de message eb abscence</returns>
        public int GetStatesMessages(string user, string userDestination, bool forGroup)
        {
            string sql = string.Empty;
            int numberMessages = 0;
            if (forGroup)
            {
                sql = string.Format("SELECT Count( Distinct valueMessage,valueDate) as numberMessages FROM t_log"
               + " where  valueDestination=\'{0}\' AND forGroup ={1} AND state={2} OR valueDestination=\'{3}\' AND forGroup ={4} AND state={5}  ", user, forGroup, STATENOTREAD, user, forGroup, STATEDEFAULT);
            }
            else
            {
                sql = String.Format("SELECT COUNT(*) as numberMessages FROM t_log where valueSender = \'{0}\' AND valueDestination = \'{1}\' AND state = {2} AND forGroup={3} OR valueSender = \'{4}\' AND valueDestination = \'{5}\' AND state= {6} AND forGroup={7} ", userDestination, user, STATEDEFAULT, forGroup, userDestination, user, STATENOTREAD, forGroup);
            }

            if (this.ConnectionDB())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                numberMessages = reader.GetInt32("numberMessages");
                reader.Close();
                if (forGroup)
                {
                    cmd.CommandText = String.Format("Update t_log SET state=\'{0}\' where  valueDestination = \'{1}\' AND state = {2} AND forGroup={3} ", STATENOTREAD, user, STATEDEFAULT, forGroup);
                }
                else
                {
                    cmd.CommandText = String.Format("Update t_log SET state=\'{0}\' where valueSender = \'{1}\' AND valueDestination = \'{2}\' AND state = {3} AND forGroup={4} ", STATENOTREAD, userDestination, user, STATEDEFAULT, forGroup);
                }

                cmd.ExecuteNonQuery();
                this.ShutdownConnectionDB();
            }

            return numberMessages;
        }
        /// <summary>
        /// permet  d'enregistrer le message dans la base de données
        /// </summary>
        /// <param name="message">message crypter</param>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire du message</param>
        /// <param name="forGroup">si c'est pour un groupe</param>
        public void SendMessage(string user, string destination, string message, bool forGroup)
        {
            if (this.ConnectionDB())
            {
                long lastInsertId = 0;
                //string date = DateTime.Now.ToUniversalTime().AddDays(-1).ToString("yyyy-MM-dd_HH-mm-ss");
                string date = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd_HH-mm-ss");
                string sql = String.Format("INSERT INTO t_log (`Code`,`lenTot`,`CodeSender`,`lenSender`,`valueSender`,`CodeDestination`,`lenDestination`,`valueDestination`,`CodeMessage`,`lenMessage`,`valueMessage`,`valueDate`,`CodeEnd`,`forGroup`) VALUES( '{0}', '{1}' , '{2}' , '{3}' , '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}','{11}','{12}',{13})",
                    "0003",//0 quel type de message
                    "0000", //1 longueur de tout la chaine (depuis idCode sender à idCodeEnd)
                    "0011", //2 Code Envoye
                    this.Ctrl.NumberToHexadecimal(user.Count()),//3 longueur de l'identifiant
                    user, //4 identifiant de la personne
                    "0012",//5 Code Destinataire
                    this.Ctrl.NumberToHexadecimal(destination.Count()), //6 longueur du destinataire
                    destination, //7 identifiant du destinataire
                    "0020",//8 code pour contenu du message
                    this.Ctrl.NumberToHexadecimal(message.Count()),//9 longueur du message
                    message, //10 Message à envoyer
                    date,//11 date de la connection (heure universelle)
                    "#####", //12 Code de fin
                    forGroup
                    );

                MySqlCommand cmd = new MySqlCommand(sql, ConnectionUser);
                cmd.ExecuteNonQuery();

                lastInsertId = cmd.LastInsertedId;

                cmd.CommandText = String.Format("SELECT * FROM `t_log` where idLog = {0}", lastInsertId);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                sql = String.Format("Update `t_log` set lenTot = '{0}' where idLog = {1}",
                this.Ctrl.NumberToHexadecimal(
                    reader.GetString("CodeSender").Count() + reader.GetString("lenSender").Count() + reader.GetString("valueSender").Count()
                    + reader.GetString("CodeDestination").Count() + reader.GetString("lenDestination").Count() + reader.GetString("valueDestination").Count()
                    + reader.GetString("CodeMessage").Count() + reader.GetString("lenMessage").Count() + reader.GetString("valueMessage").Count()
                    + reader.GetString("CodeDate").Count() + reader.GetString("lenDate").Count() + reader.GetString("valueDate").Count()
                    ), lastInsertId);
                reader.Close();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                this.ShutdownConnectionDB();
            }
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
            List<Message> lsbMessage = new List<Message>();
            string date = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");
            string LastDay = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");
            string sql = string.Empty;
            if (forGroup)
            {
                sql = string.Format("SELECT DISTINCT  valueSender, valueMessage, DATE_FORMAT(valueDate,'%Y-%m-%d_%H-%i-%S') AS valueDate FROM t_log"
               + " where valueSender=\'{0}\' AND valueMessage!=\"\"  AND forGroup ={1} AND valuedate BETWEEN  '{2} 00:00:00' AND '{3} 23:59:59'  OR valueMessage!=\"\"  AND valueDestination=\'{4}\' AND forGroup ={5} AND valuedate BETWEEN  '{6} 00:00:00' AND '{7} 23:59:59'"
           , user, forGroup, LastDay, date, user, forGroup, LastDay, date);
            }
            else
            {
                sql = string.Format("SELECT valueSender, valueMessage, DATE_FORMAT(valueDate,'%Y-%m-%d_%H-%i-%S') AS valueDate FROM t_log"
              + " where valueSender ='{0}'"
              + "AND valueDestination = '{1}'"
              + " AND forGroup={2} "
               + "AND valuedate BETWEEN  '{3} 00:00:00' AND '{4} 23:59:59'"
              + " OR valueSender ='{5}'"
              + "AND valueDestination = '{6}'"

              + "AND valuedate BETWEEN  '{7} 00:00:00' AND '{8} 23:59:59'"
          + "AND forGroup  ={9} ", user, destination, forGroup, LastDay, date, destination, user, LastDay, date, forGroup);
            }

            if (this.ConnectionDB())
            {



                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lsbMessage.Add(new Message(reader.GetString("valueSender"), reader.GetString("valueMessage"), reader.GetString("valueDate")));
                }
                reader.Close();
                this.ShutdownConnectionDB();
            }
            else
            {
                lsbMessage.Add(new Message("DB", "", "2016-06-09_05-49-44"));
            }
            return lsbMessage;
        }
        /// <summary>
        /// permet de mettre à jour l'état des messages
        /// </summary>
        /// <param name="user"></param>
        /// <param name="destination"></param>
        /// <param name="isforGroup"></param>
        public void UpdateStateMessages(string user, string destination, bool isforGroup)
        {

            string sql = String.Format("UPDATE t_log set state={0} where valueSender = \'{1}\' AND valueDestination = \'{2}\' AND state = {3}  AND forGroup={4}  ", STATEREAD, user, destination, STATENOTREAD, isforGroup);



            if (this.ConnectionDB())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                cmd.ExecuteNonQuery();
                this.ShutdownConnectionDB();
            }
        }
        /// <summary>
        /// permet de mettre à zéro tous les utilisateurs
        /// </summary>
        public void SetAllEmployeesDeconnected()
        {
            string sql = "UPDATE t_users SET connection= false ";
            if (this.ConnectionDB())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                cmd.ExecuteNonQuery();
                this.ShutdownConnectionDB();

            }
        }
        /// <summary>
        /// permet de récupérer les messages envoyé par les utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="destination">destinataire du messahe</param>
        /// <param name="forGroup">si c'est pour le groupe</param>
        /// <returns></returns>
        public List<Message> GetOldConversation(string user, string destination, bool forGroup, int nbDays)
        {
            List<Message> lsbMessage = new List<Message>();
            string date = DateTime.Now.ToUniversalTime().AddDays(-1).ToString("yyyy-MM-dd");
            string oldDate = DateTime.Now.ToUniversalTime().AddDays(-nbDays).ToString("yyyy-MM-dd");
            string sql = string.Empty;
            if (forGroup)
            {
                sql = string.Format("SELECT DISTINCT  valueSender, valueMessage, DATE_FORMAT(valueDate,'%Y-%m-%d_%H-%i-%S') AS valueDate FROM t_log"
               + " where valueSender=\'{0}\' AND valueMessage!=\"\" AND forGroup ={1} AND valuedate BETWEEN  '{2} 00:00:00' AND '{3} 23:59:59' OR valueDestination=\'{4}\'  AND valuedate BETWEEN  '{5} 00:00:00' AND '{6} 23:59:59'"
           + "AND forGroup ={7}   ", user, forGroup, oldDate, date, user, oldDate, date, forGroup);
            }
            else
            {
                sql = string.Format("SELECT valueSender, valueMessage, DATE_FORMAT(valueDate,'%Y-%m-%d_%H-%i-%S') AS valueDate FROM t_log"
              + " where valueSender ='{0}'"
              + "AND valueDestination = '{1}'"
              + " AND forGroup={2} "
               + "AND valuedate BETWEEN  '{3} 00:00:00' AND '{4} 23:59:59'"
              + " OR valueSender ='{5}'"
              + "AND valueDestination = '{6}'"

              + "AND valuedate BETWEEN  '{7} 00:00:00' AND '{8} 23:59:59'"
          + "AND forGroup  ={9} ", user, destination, forGroup, oldDate, date, destination, user, oldDate, date, forGroup);
            }

            if (this.ConnectionDB())
            {



                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lsbMessage.Add(new Message(reader.GetString("valueSender"), reader.GetString("valueMessage"), reader.GetString("valueDate")));
                }
                reader.Close();
                this.ShutdownConnectionDB();
            }
            else
            {
                lsbMessage.Add(new Message("DB", "", "2016-06-09_05-49-44"));
            }
            return lsbMessage;
        }
        /// <summary>
        /// permet de changer le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="password">nouveau mot de passe de l'utilisateur</param>
        /// <returns>réussit ou annuler</returns>
        public bool ChangePassword(string user, string password)
        {
            string sql = string.Format("UPDATE t_users SET password= '{0}' where idUser = '{1}' ", password, user);
            if (this.ConnectionDB())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                cmd.ExecuteNonQuery();
                this.ShutdownConnectionDB();
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}

