using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_server
{
    public class Message
    {
        private string _author;

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        private string _date;

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        

        public Message(string user, string valueMessage, string valueDate)
        {
            this.Author = user;
            this.Content = valueMessage;
            this.Date = this.GetFormatedDate(valueDate);
            
        }
        
        public string GetFormatedDate( string oldDate)
        {
            bool first=true;
            string[] InglobalDateOrHour;
            string res = string.Empty;
            foreach (string globalDateOrHour in oldDate.Split('_'))
            {
                 InglobalDateOrHour = globalDateOrHour.Split('-');
                    if (first)
                    {
                        res += InglobalDateOrHour[2] + "." + InglobalDateOrHour[1] + "." + InglobalDateOrHour[0]+" ";
                        first = false;
                    }
                    else
                    {
                        res += InglobalDateOrHour[0] + "." + InglobalDateOrHour[1] + "." + InglobalDateOrHour[2];
                    }
                    
                
            }
            return res;
        }
        public string getDate()
        {
            return this.Date;
        }
        public string getAuthor()
        {
            return this.Author;
        }
        public string getContent()
        {
            return this.Content;
        }
      
    }
}
