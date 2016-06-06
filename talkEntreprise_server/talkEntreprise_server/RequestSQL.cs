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
        const int STATEDEFAULT = 2;
        const int STATENOTREAD = 3;
        const int STATEREAD = 4;


        private MySqlConnection _connectionUser;
        private Controler _ctrl;

        public MySqlConnection ConnectionUser
        {
            get { return _connectionUser; }
            set { _connectionUser = value; }
        }
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }

        public RequestSQL(Controler c)
        {
            this.Ctrl = c;
        }
        public bool connectionDB()
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

        public void shutdownConnectionDB()
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
            bool result = true;
            if (this.connectionDB())
            {
                string sql = String.Format("SELECT Count(*) as total FROM t_users where  idUser  = '{0}' AND Password = '{1}'", user, password);
                MySqlCommand cmd = new MySqlCommand(sql, ConnectionUser);
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (Convert.ToInt32(reader.GetString("total")) == 1)
                {
                    /*  reader.Close();
                      cmd.CommandText = String.Format("SELECT * FROM t_users where  idUser  = '{0}' AND Password = '{1}'", user, password);
                      cmd.ExecuteNonQuery();
                      reader = cmd.ExecuteReader();
                      reader.Read();*/
                    result = true;
                    ///  Ctrl.setUser(reader.GetString("idUser"), reader.GetString("Password"), Convert.ToInt32(reader.GetString("idGroupe")));
                }
                else
                {

                    result = false;
                }
                reader.Close();

                this.shutdownConnectionDB();

            }
            else
            {

                result = false;
            }
            return result;
        }

        public List<string> GetInformation(string user)
        {
            List<string> lstInfoUser = new List<string>();

            if (this.connectionDB())
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

        public void SucessConnectionToServer(string user)
        {

            if (this.connectionDB())
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
                this.shutdownConnectionDB();
            }
        }

        public void DeconnectionToServer(string user)
        {

            if (connectionDB())
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
                this.shutdownConnectionDB();
            }
        }

        public int getStatesMessages(string userconnected, string usernameDestination, bool forGroup)
        {
            string sql = string.Empty;
            int numberMessages = 0;
            if (forGroup)
            {
                sql = string.Format("SELECT Count( Distinct valueMessage,valueDate) as numberMessages FROM t_log"
               + " where  valueDestination=\'{0}\' AND forGroup ={1}   ", userconnected, forGroup);
            }
            else
            {
                sql = String.Format("SELECT COUNT(*) as numberMessages FROM t_log where valueSender = \'{0}\' AND valueDestination = \'{1}\' AND state = {2} AND forGroup={3} OR valueSender = \'{4}\' AND valueDestination = \'{5}\' AND state= {6} AND forGroup={7} ", usernameDestination, userconnected, STATEDEFAULT, forGroup, usernameDestination, userconnected, STATENOTREAD, forGroup);
            }

            if (this.connectionDB())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                numberMessages = reader.GetInt32("numberMessages");
                reader.Close();
                if (forGroup)
                {
                    cmd.CommandText = String.Format("Update t_log SET state=\'{0}\' where  valueDestination = \'{1}\' AND state = {2} AND forGroup={3} ", STATENOTREAD, userconnected, STATEDEFAULT, forGroup);
                }
                else
                {
                    cmd.CommandText = String.Format("Update t_log SET state=\'{0}\' where valueSender = \'{1}\' AND valueDestination = \'{2}\' AND state = {3} AND forGroup={4} ", STATENOTREAD, usernameDestination, userconnected, STATEDEFAULT, forGroup);
                }

                cmd.ExecuteNonQuery();
                this.shutdownConnectionDB();
            }

            return numberMessages;
        }
       
        public string GetEmployee(string nameGroup,int idGroup, string idUser)
        {
            List<User> lsbUsers = new List<User>();
            bool first = true;
            string listEmployee = "#0015;";
            //  bool ConnectedFriend = true;
            //bool NotConnectedFriend = true;
            //  List<string> lsbFriends = new List<string>();
            //  string sql = string.Format("SELECT * From users where idGroup = {0} AND idUser!= \"{1}\" ORDER BY Connection DESC,idUser ASC", idGroup, this.Ctrl.getIdUser());
            string sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser != \"{1}\" ORDER BY `idUser` ASC", idGroup, idUser);

            
            lsbUsers.Add(new User(nameGroup, "", idGroup, true, 0,nameGroup));
            if (this.connectionDB())
            {



                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lsbUsers.Add(new User(reader.GetString("idUser"), reader.GetString("password"), Convert.ToInt32(reader.GetString("idGroup")), Convert.ToBoolean(reader.GetString("connection")), 0,nameGroup));

                }
                shutdownConnectionDB();
            }
            foreach (User employee in lsbUsers)
            {

                if (first)
                {
                    employee.setMessagesNotRead(getStatesMessages(idUser, employee.getidUser(), true));
                    first = false;
                    listEmployee += ""+employee.getidUser()+","+employee.getPassword()+","+employee.getConnection()+","+employee.getConnection();
                }
                else
                {
                    employee.setMessagesNotRead(getStatesMessages(idUser, employee.getidUser(), false));
                    listEmployee += ";" + employee.getidUser() + "," + employee.getPassword() + "," + employee.getConnection() + "," + employee.getConnection();
                }

            }
            return listEmployee;
        }
        public List<User> LstGetEmployee(string nameGroup, int idGroup, string idUser)
        {
            List<User> lsbUsers = new List<User>();
            bool first = true;
            
            //  bool ConnectedFriend = true;
            //bool NotConnectedFriend = true;
            //  List<string> lsbFriends = new List<string>();
            //  string sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser!= \"{1}\" ORDER BY Connection DESC,idUser ASC", idGroup, this.Ctrl.getIdUser());
            string sql = string.Format("SELECT * From t_users where idGroup = {0} AND idUser != \"{1}\" ORDER BY `idUser` ASC", idGroup, idUser);


            lsbUsers.Add(new User(nameGroup, "", idGroup, true, 0, nameGroup));
            if (this.connectionDB())
            {



                MySqlCommand cmd = new MySqlCommand(sql, this.ConnectionUser);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lsbUsers.Add(new User(reader.GetString("idUser"), reader.GetString("password"), Convert.ToInt32(reader.GetString("idGroup")), Convert.ToBoolean(reader.GetString("connection")), 0, nameGroup));

                }
                shutdownConnectionDB();
            }
            foreach (User employee in lsbUsers)
            {

                if (first)
                {
                    employee.setMessagesNotRead(getStatesMessages(idUser, employee.getidUser(), true));
                    first = false;
                   
                }
                else
                {
                    employee.setMessagesNotRead(getStatesMessages(idUser, employee.getidUser(), false));
                    
                }

            }
            return lsbUsers;
        }
    }

}
