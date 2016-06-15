/******************************************
* Projet : TalkEntreprise_client
* Description : création d'une messagerie instantanée
* Date : juin 2016
* Version : 1.0
* Auteur :Gabriel Strano
*
******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talkEntreprise_client
{
    public class Message
    {
        //////Champs//////////////
        private string _author;
        private string _content;
        private string _date;
        //////Propriétées//////////////
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        //////Constructeur//////////////
        public Message(string user, string valueMessage, string valueDate)
        {
            this.Author = user;
            this.Content = valueMessage;
            this.Date = valueDate;
        }
        public string GetDate()
        {
            return this.Date;
        }
        public string GetAuthor()
        {
            return this.Author;
        }
        public string GetContent()
        {
            return this.Content;
        }
    }
}
