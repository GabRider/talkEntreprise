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
        private List<Message> _lstOldMessages;



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
        public List<Message> LstOldMessages
        {
            get { return _lstOldMessages; }
            set { _lstOldMessages = value; }
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
        public void Init()
        {
            this.LstOldMessages = new List<Message>();
            bool stop = false;
            byte[] inStream = new byte[10025];
            string result = string.Empty;
            List<User> lstUsers = new List<User>();
            List<Message> lstMessages = new List<Message>();
            string[] userInfos;
            bool first = true;


            while (true)
            {

                lstUsers.Clear();


                first = true;



                try
                {
                    //récupération du flux de données envoyé par le serveur --> recodage du message
                    this.Stream.Read(inStream, 0, inStream.Length);
                    result = Encoding.ASCII.GetString(inStream);
                    result = result.Substring(0, result.IndexOf("####"));
                }
                catch (Exception)
                {
                    stop = true;


                }

                if (stop)
                {
                    break;
                }
                switch (result.Split(';')[0])
                {
                    case "#0015":
                        //création des utilisateurs donnés par le serveur
                        foreach (string user in result.Split(';'))
                        {
                            if (!first)
                            {
                                userInfos = user.Split(',');

                                if (userInfos[0] != "DB")
                                {
                                    lstUsers.Add(new User(userInfos[0], userInfos[1], Convert.ToInt32(userInfos[2]), Convert.ToBoolean(userInfos[3]), Convert.ToInt32(userInfos[4]), userInfos[5]));
                                }
                                else
                                {
                                    this.Prog.DatabaseClosed();
                                    stop = true;
                                }
                            }
                            else
                            {
                                first = false;
                            }
                        }
                        this.Prog.SetEmployees(lstUsers);
                        break;
                    ///récupération des messages envoyés par le serveur
                    case "#0004":
                        foreach (string message in result.Split(';'))
                        {
                            if (result.Contains("false"))
                            {
                                if (!first)
                                {
                                    if (message.Split('-')[0] != "DB")
                                    {

                                        lstMessages.Add(new Message(message.Split('-')[0], this.Prog.DecryptMessage(message.Split('-')[1]), message.Split('-')[2]));
                                    }
                                    else
                                    {
                                        this.Prog.DatabaseClosed();
                                        stop = true;
                                    }
                                }
                                else
                                {
                                    first = false;
                                }
                            }
                            else
                            {
                                if (stop)
                                {
                                    break;
                                }
                                if (lstMessages.Count != 0)
                                {
                                    lstMessages = this.LstOldMessages.Concat(lstMessages).ToList<Message>();
                                    this.Prog.ShowMessages(lstMessages, result.Split('-')[1], result.Split('-')[2], Convert.ToBoolean(result.Split('-')[3]));
                                    lstMessages.Clear();
                                    LstOldMessages.Clear();
                                }
                            }
                        }
                        break;

                    case "#0007":
                        foreach (string message in result.Split(';'))
                        {
                            if (result.Contains("false"))
                            {
                                if (!first)
                                {
                                    if (message.Split('-')[0] != "DB")
                                    {
                                        this.LstOldMessages.Add(new Message(message.Split('-')[0], this.Prog.DecryptMessage(message.Split('-')[1]), message.Split('-')[2]));
                                    }
                                    else
                                    {
                                        this.Prog.DatabaseClosed();
                                        stop = true;
                                    }
                                }
                                else
                                {
                                    first = false;
                                }
                            }
                            else
                            {
                                if (stop)
                                {
                                    break;
                                }
                            }
                        }
                        break;

                    case "#0008":
                        this.Prog.PasswordIsChanged(Convert.ToBoolean(result.Split(';')[1]), result.Split(';')[2]);
                        break;
                    default:
                        stop = true;
                        break;
                }
            }
        }
    }
}
