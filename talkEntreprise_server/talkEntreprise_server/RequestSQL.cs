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
        public Boolean validateConnectionUser(string user, string password)
        {
            bool result = true;
            if (connectionDB())
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
                    reader.Close();
                    result = false;
                }


                shutdownConnectionDB();

            }
            else
            {

                result = false;
            }
            return result;
        }
    }
}
