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
   public class ClientConnectToServ
    {
        private Server _serv;

        public Server Serv
        {
            get { return _serv; }
            set { _serv = value; }
        }
        private Thread _userUpdate;

        public Thread UserUpdate
        {
            get { return _userUpdate; }
            set { _userUpdate = value; }
        }

        //////////////////Constructeur///////////
        public ClientConnectToServ(Server s)
        {
            this.Serv = s;
        }
        //////////////////méthodes///////////
        public Boolean validateConnection(string id, string password)
        {
            return this.Serv.validateConnection(id, password);
        }

        public void init()
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();

            counter = 0;
            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                byte[] sendBytedMessage = null;
                byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;
                string user = string.Empty;
                string password = string.Empty;
                bool sendToClient = false;
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                if (dataFromClient.Contains("#0001"))
                {
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("####"));
                    user = dataFromClient.Split(';')[1];
                    password = dataFromClient.Split(';')[2]; 
                }
               
                if (this.validateConnection(user, password))
                {
                    this.Serv.SucessConnectionToServer(user);
                    sendToClient = true;
                    this.Serv.update(user, clientSocket);
                 //   this.UserUpdate = new Thread(new UpdateUser(clientSocket, networkStream, sendToClient, this).update);
                    this.UserUpdate = new Thread(new UpdateUser(clientSocket, networkStream, sendToClient, this,user).update);
                    this.Serv.AddThreadList(user, UserUpdate);
                    this.UserUpdate.Start();
                    
                   
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
        public void CloseConnection(string user)
        {
            this.Serv.DeconnectionToServer(user);
            Thread t = this.Serv.getThreadlist(user);
            this.Serv.DelInList(user);
            t.Abort();
        }
        public List<string> GetInformation(string user)
        {
            return this.Serv.GetInformation(user);
        }
        public string GetEmployee(string nameGroup, int idGroup, string user)
        {
            return this.Serv.GetEmployee(nameGroup, idGroup, user);
        }
        public void sendLstEmployeeUpdate( string nameGroup, int idGroup, string user)
        {
            byte[] sendBytedMessage = null;
            byte[] bytesFrom = new byte[10025];
            NetworkStream networkStream ;
            networkStream = this.Serv.GetTcpClientLst(user).GetStream();
            sendBytedMessage = Encoding.ASCII.GetBytes(this.Serv.GetEmployee(nameGroup, idGroup, user));
            networkStream.Write(sendBytedMessage, 0, sendBytedMessage.Length);
            foreach (User userInfo in this.Serv.GetEmployeeLst(nameGroup,idGroup,user))
            {
                
                if (this.Serv.ContainInClientList(userInfo.getidUser()))
	{
                    networkStream = this.Serv.GetTcpClientLst(userInfo.getidUser()).GetStream();
		 sendBytedMessage = Encoding.ASCII.GetBytes(this.Serv.GetEmployee(nameGroup,idGroup,user));
            networkStream.Write(sendBytedMessage, 0, sendBytedMessage.Length);
	}
            }
           
           
            
        }
    }
}
