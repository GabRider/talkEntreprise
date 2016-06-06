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
        private MySqlConnection _connectionUser;

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

        public RequestSQL(Controler c)
        {
            this.Ctrl = c;
        }
        public bool connectionDB()
        {
            try
            {

                string connectionString = @"server=127.0.0.1;userid=IT;password=Super;database=db_talkEntreprise";
                ConnectionUser = new MySqlConnection(connectionString);
                ConnectionUser.Open();
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
    }
}
