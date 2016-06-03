using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace talkEntreprise_client
{
    public class Controler
    {
        private Client _client;

        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public Controler()
        {
            this.Client = new Client(this);
        }
        //////méthodes Générales///////

        /// <summary>
        /// permet de coder le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns></returns>
        public string sha1(string password)
        {
            //créer une instance sha1
            SHA1 sha1 = SHA1.Create();
            //convertit le texte en byte
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(password));
            //créer une instance StringBuilder pour sauver les hashData
            StringBuilder returnValue = new StringBuilder();
            //transform un tableau en string
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
        public bool connection(string id, string password)
        {
            return this.Client.connection(id,password);
        }
    }
}
