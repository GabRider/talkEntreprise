using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
    class Messages
    {
        List<Message> _lstMessage;

        public List<Message> LstMessage
        {
            get { return _lstMessage; }
            set { _lstMessage = value; }
        }

        public Messages(List<Message> msg)
        {
            this.LstMessage = new List<Message>();
            foreach (var message in msg)
            {
                this.LstMessage.Add(message);
            }
        }
        public List<Message> getMessages()
        {
            return this.LstMessage;
        }
    }
}
