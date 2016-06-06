using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using talkEntreprise_server.classThread;

namespace talkEntreprise_server
{
    public class Server
    {
        //tableau contenant les différentes connexions (nom utilisateur, connexion)
        public static Hashtable clientsList = new Hashtable();
        public static Hashtable UserThreadsList = new Hashtable();

        private Controler _ctrl;
        private Thread _firstConnection;
        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }


        public Thread FirstConnection
        {
            get { return _firstConnection; }
            set { _firstConnection = value; }
        }



        public Server(Controler c)
        {
            this.Ctrl = c;

            this.FirstConnection = new Thread(new ClientConnectToServ(this).init);
            this.FirstConnection.Start();
            
        }
        public void update(string id, TcpClient tcp)
        {
            if (!clientsList.ContainsKey(id))
            {
                clientsList.Add(id, tcp);
            }
        }
           public void AddThreadList(string user,Thread t)
        {
           UserThreadsList.Add(user,t);
        }
           public Thread getThreadlist(string user)
           {
               return UserThreadsList[user] as Thread;
           }
           public void DelInList(string user)
           {
               UserThreadsList.Remove(UserThreadsList[user]);
               clientsList.Remove(clientsList[user]);
           }
        public Boolean validateConnection(string id, string password)
        {
            return this.Ctrl.validateConnection(id, password);
        }
        public void SucessConnectionToServer(string user)
        {
            this.Ctrl.SucessConnectionToServer(user);
        }
        public void DeconnectionToServer(string user)
        {
            this.Ctrl.DeconnectionToServer(user);
        }
    }//end Main class
    //permet de lancer en boucle la méthode init sur un autre processus


    //end class handleClinet
}
