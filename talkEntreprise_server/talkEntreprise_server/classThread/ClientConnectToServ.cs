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
    class ClientConnectToServ
    {
        private Server _serv;

        public Server Serv
        {
            get { return _serv; }
            set { _serv = value; }
        }

        public ClientConnectToServ(Server s)
        {
            this.Serv = s;
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
                Byte[] sendBytedMessage = null;
                byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;
                string id = string.Empty;
                string password = string.Empty;
                bool sendToClient = false;
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                //encode le tableau de bytes
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //récupère la valeure envoyée
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                id = dataFromClient.Split(' ')[0];
                password = dataFromClient.Split(' ')[1];
                if (this.Serv.validateConnection(id, password))
                {
                    sendToClient = true;
                   this.Serv.update(id, clientSocket);
                }
                else
                {
                    sendToClient = false;
                }
                Thread.Sleep(10);
                sendBytedMessage = Encoding.ASCII.GetBytes(sendToClient.ToString());
                networkStream.Write(sendBytedMessage, 0, sendBytedMessage.Length);

                /* broadcast(dataFromClient + " Joined ", dataFromClient, false);

                 Console.WriteLine(dataFromClient + " Joined chat room ");
                 handleClinet client = new handleClinet();
                 client.startClient(clientSocket, dataFromClient, clientsList);*/
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }
    }
}
