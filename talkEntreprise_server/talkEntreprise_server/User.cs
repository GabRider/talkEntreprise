using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
    public class User
    {

    
        private string _idUser;
        private string _password;
        private int _idGroup;
        private bool _connection;
        private string _groupeName;

      

        public bool Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public string IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public int IdGroup
        {
            get { return _idGroup; }
            set { _idGroup = value; }
        }
        private int _messageNotRead;

        public int MessageNotRead
        {
            get { return _messageNotRead; }
            set { _messageNotRead = value; }
        }
        public string GroupeName
        {
            get { return _groupeName; }
            set { _groupeName = value; }
        }
        public User( )
        {
         
        }
        public User(string id, string pwd, int group, bool connect, int nbMessagesNotRead, string nameGroup)
        {
            setUser(id,pwd,group,connect,nbMessagesNotRead, nameGroup);
        }
        public void setUser(string id, string pwd, int group, bool connect, int nbMessagesNotRead, string nameGroup)
        {
            this.IdUser = id;
            this.Password = pwd;
            this.IdGroup = group;
            this.Connection = connect;
            this.MessageNotRead = nbMessagesNotRead;

        }

        public string getidUser()
        {
            return this.IdUser;
        }
        public bool getInformationConnection()
        {
            return this.Connection;
        }
        public int getGroupUser()
        {
            return this.IdGroup;
        }
        public int getMessagesNotRead()
        {
            return this.MessageNotRead;
        }
        public void setMessagesNotRead( int nbmessagesNotRead)
        {
            this.MessageNotRead = nbmessagesNotRead;
        }
    }
}
