using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using talkEntreprise_server;
namespace talkEntreprise_server.classThread
{
   public class ClientToServer
    {
        private Server _serv;
        private Thread _userUpdate;
 
       
       ///////////////////////////
       
        public Server Serv
        {
            get { return _serv; }
            set { _serv = value; }
        }
        
        public Thread UserUpdate
        {
            get { return _userUpdate; }
            set { _userUpdate = value; }
        }
        


        //////////////////Constructeur///////////
        public ClientToServer(Server s)
        {
            this.Serv = s;
        }
        //////////////////méthodes///////////
        public Boolean validateConnection(string id, string password)
        {
            return this.Serv.ValidateConnection(id, password);
        }

        public void init()
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
  

            serverSocket.Start();

            while ((true))
            {

                Thread userThread = null;
                Byte[] sendBytedMessage = null;
                byte[] bytesFrom = new byte[10025];

                string dataFromClient = null;
                string id = string.Empty;
                string password = string.Empty;
                bool sendToClient = false;
                clientSocket = serverSocket.AcceptTcpClient();
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                if (dataFromClient.Contains("$"))
                {
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    id = dataFromClient.Split(' ')[0];
                    password = dataFromClient.Split(' ')[1];
                }
              
                if (this.validateConnection(id, password))
                {
                    sendToClient = true;
                 //   this.UserUpdate = new Thread(new UpdateUser(clientSocket, networkStream, sendToClient, this).update);
                    this.Serv.SucessConnectionToServer(id);
                    userThread = new Thread(new UpdateUser(clientSocket, networkStream, this, id).update);
                    
                    userThread.Start();
                    this.Serv.Update(id, clientSocket,userThread);
                    
                }
               
                
                Thread.Sleep(10);
                sendBytedMessage = Encoding.ASCII.GetBytes(sendToClient.ToString());
                networkStream.Write(sendBytedMessage, 0, sendBytedMessage.Length);
                
                
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        
       public void shutdownConnection(string user)
        {
            Thread t = this.Serv.getThreadByName(user);
            this.Serv.ShutdownConnection(user);
            this.Serv.DeconnectionToServer(user);
            t.Abort();
           
        }
    }
}
