using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Threading;
using System.Threading;
using System.Net.Sockets;
namespace talkEntreprise_client
{
    delegate void VisibleChange();
    delegate void CloseConnection();
    public class Controler
    {
        ////////////////Champs/////////////
        private Client _client;
        private FrmConnection _connect;
        private TcpClient _tClient;
        private NetworkStream _stream;
        private Thread _frmProg;
        private User _userInformation;
        private ManageMessage _manMessage;

        public ManageMessage ManMessage
        {
            get { return _manMessage; }
            set { _manMessage = value; }
        }
        
        
        //////////////propriétées///////////
        

        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public FrmConnection Connect
        {
            get { return _connect; }
            set { _connect = value; }
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
        public Thread FrmProg
        {
            get { return _frmProg; }
            set { _frmProg = value; }
        } 
        public User UserInformation
        {
            get { return _userInformation; }
            set { _userInformation = value; }
        }
        //////////////Constructeur///////////

        public Controler(FrmConnection c)
        {
            this.Connect = c;
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
        /// <summary>
        /// elle permet de lancer le programme principal 
        /// </summary>
        public void CreateProgram(string user)
        {
            //création d'un nouveau processus
            this.FrmProg = new Thread(new ThreadStart(ThreadProgram));
            this.FrmProg.SetApartmentState(ApartmentState.STA);
            //donne le droit de travailler en arrière plan
            this.FrmProg.IsBackground = true;
            //lancer le processus
            this.FrmProg.Start();
        }
        /// <summary>
        /// permet de créer la fenêre FrmProgram dans un aute processus
        /// </summary>
        public void ThreadProgram()
        {
            FrmProgram prog = new FrmProgram(this);
            prog.Show();
            //permet de garder la fenêtre ouverte
            Dispatcher.Run();
        }
        /// <summary>
        /// permet de sauvegarder la connexion existente au serveur
        /// </summary>
        /// <param name="t">connexion du client</param>
        /// <param name="s">flux d'information entre le client et le serveur</param>
        public void setTcpClientAndNetworkStream(TcpClient t, NetworkStream s)
        {
            this.TClient = t;
            this.Stream = s;
        }
        ///////////////méthodes Client /////////////////7
        /// <summary>
        /// elle permet de savoir si l'utilisateur peut se connecter
        /// </summary>
        /// <param name="user">identifiant de l'utilisateur</param>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns></returns>
        public bool Connection(string user, string password)
        {
            return this.Client.connection(user, password);
        }

        /// <summary>
        /// permet d'avertire le serveur que l'utilisateur se déconnecte
        /// </summary>
        public void CloseConnection()
        {
           
            this.FrmProg.Interrupt();
            Dispatcher.ExitAllFrames();
            this.Client.CloseConnection();
          
        }
        /// <summary>
        /// permet de réinitialiser la connexion avec le server
        /// </summary>
        public void ResetConnection()
        {
            this.Client.ResetConnection();
        }
        /////////////méthodes FrmConnection//////////////
        /// <summary>
        /// permet de modifier la visibilité de la vue
        /// </summary>
        public void VisibleChange()
        {
            this.Connect.Visible = !this.Connect.Visible;
        }
        /////////////méthodes spécifique au controler////
        /// <summary>
        /// permet de donner la connexion du client
        /// </summary>
        /// <returns>connexion du client</returns>
        public TcpClient GetTcpClient()
        {
            return this.TClient;
        }
        /// <summary>
        /// permet de donner le flux d'information du client
        /// </summary>
        /// <returns>flux d'information du client</returns>
        public NetworkStream GetNetStream()
        {
            return this.Stream;
        }

        public void setUserConnected()
        {
            this.UserInformation = this.Client.getInformationUserConnected();
        }
        public User GetUserConnected()
        {
            return this.UserInformation;
        }
    }
}
