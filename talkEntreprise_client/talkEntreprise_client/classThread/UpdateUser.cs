using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace talkEntreprise_client.classThread
{
    class UpdateUser
    {

        //////Champs////////

        private FrmProgram _prog;
        private TcpClient _tClient;
        private NetworkStream _stream;


        ///////propriétées/////////


        public FrmProgram Prog
        {
            get { return _prog; }
            set { _prog = value; }
        }
        public TcpClient TClient
        {
            get { return _tClient; }
            set { _tClient = value; }
        }
        public NetworkStream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }
        ///////Constructeur////////
        public UpdateUser(FrmProgram p, Controler c)
        {
            this.Prog = p;

            this.TClient = this.Prog.GetTcpClient();
            this.Stream = this.Prog.GetNetStream();

        }
        /////////méthodes///////////

        /// <summary>
        /// permet de vérifier si l'utilisateur reçoit un message du serveur
        /// </summary>
        public void init()
        {
            byte[] inStream = new byte[10025];
            string result = string.Empty;
            List<User> lstUser = new List<User>();
            List<Message> lstMessages = new List<Message>();
            string[] userInfo;
            bool first = true;
            bool updateMessage = false;
            while (true)
            {
                updateMessage = false;
                lstUser.Clear();
               
                first = true;
                //récupération du flux de données envoyé par le serveur --> recodage du message
                this.Stream.Read(inStream, 0, inStream.Length);
                result = Encoding.ASCII.GetString(inStream);
                result = result.Substring(0, result.IndexOf("####"));
                if (result.Contains("#0015"))
                {

                    //création des utilisateurs donnés par le serveur
                    foreach (string user in result.Split(';'))
                    {
                        if (!first)
                        {
                            userInfo = user.Split(',');
                            
                           
                                lstUser.Add(new User(userInfo[0], userInfo[1], Convert.ToInt32(userInfo[2]), Convert.ToBoolean(userInfo[3]), Convert.ToInt32(userInfo[4]), userInfo[5]));
                            
                        }
                        else
                        {
                            first = false;
                        }

                    }
                    Thread.Sleep(10);
                    this.Prog.setEmployees(lstUser);
                }
                if (result.Contains("#0004"))
                {
                    foreach (string message in result.Split(';'))
                    {
                        if (result.Contains("false"))
                        {


                            if (!first)
                            {
                                lstMessages.Add(new Message(message.Split('-')[0], this.Prog.DecryptMessage(message.Split('-')[1]), message.Split('-')[2]));
                            }
                            else
                            {
                                first = false;
                            }
                        }
                        else
                        {

                            this.Prog.showMessage(lstMessages, result.Split('-')[1],  result.Split('-')[2]);
                                  
                           
                            lstMessages.Clear(); 
                            break;
                        }
                    }

                }
            }

        }
    }
}
