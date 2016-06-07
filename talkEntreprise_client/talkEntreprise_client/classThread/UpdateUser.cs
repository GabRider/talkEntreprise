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

        private Controler _ctrl;
        private FrmProgram _prog;
        private TcpClient _tClient;
        private NetworkStream _stream;


        ///////propriétées/////////

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }

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
            Ctrl = c;
            this.TClient = this.Ctrl.GetTcpClient();
            this.Stream = this.Ctrl.GetNetStream();

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
            string[] userInfo;
            bool first = true;

            while (true)
            {
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
            }

        }
    }
}
