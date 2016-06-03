using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_client
{
    public class Client
    {
        private Controler _ctrl;
        private TcpClient _clientSocket;
        private NetworkStream _serverStream;
        private string _readData;
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }


        public TcpClient ClientSocket
        {
            get { return _clientSocket; }
            set { _clientSocket = value; }
        }


        public NetworkStream ServerStream
        {
            get { return _serverStream; }
            set { _serverStream = value; }
        }


        public string ReadData
        {
            get { return _readData; }
            set { _readData = value; }
        }

        //////////////Constructeur////////////

        public Client(Controler c)
        {
            this.Ctrl = c;
            this.ClientSocket = new TcpClient();
            this.ServerStream = default(NetworkStream);
            this.ReadData = null;
           
            this.ClientSocket.Connect("127.0.0.1", 8888);
            this.ServerStream = this.ClientSocket.GetStream();
        }
        //////////////méthode////////////
        public bool connection(string id, string password)
        {
            string cryptedPassword = this.Ctrl.sha1(password);
            return connectionServer(id, cryptedPassword);
        }

        private bool connectionServer(string id, string password)
        {
            byte[] inStream = new byte[10025];
            int buffSize = 0;
            string toSend = id + " " + password;

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(toSend + "$");
            this.ServerStream.Write(outStream, 0, outStream.Length);
            this.ServerStream.Flush();

           
           
            buffSize = this.ClientSocket.ReceiveBufferSize;
            this.ServerStream.Read(inStream, 0, buffSize);
            bool result =Convert.ToBoolean( System.Text.Encoding.ASCII.GetString(inStream));
            return result;
        }
    }
}
