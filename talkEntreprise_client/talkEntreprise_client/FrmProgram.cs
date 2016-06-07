using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using talkEntreprise_client.classThread;

namespace talkEntreprise_client
{
    public partial class FrmProgram : Form
    {
        private Controler _ctrl;
        private Thread _updateLstUser;
        private List<User> _lstUser;
        private User _userConnected;



        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
        public Thread UpdateLstUser
        {
            get { return _updateLstUser; }
            set { _updateLstUser = value; }
        }
        public List<User> LstUser
        {
            get { return _lstUser; }
            set { _lstUser = value; }
        }
        public User UserConnected
        {
            get { return _userConnected; }
            set { _userConnected = value; }
        }
        public FrmProgram(Controler c)
        {
            InitializeComponent();
            this.Ctrl = c;

            this.UpdateLstUser = new Thread(new UpdateUser(this, this.Ctrl).init);
            this.UpdateLstUser.Start();
            this.UserConnected = this.Ctrl.GetUserConnected();
            this.tbxUser.Text = Environment.NewLine + this.UserConnected.GetidUser();

           
        }

        private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UpdateLstUser.Abort();
            Invoke(new MethodInvoker(delegate { this.Ctrl.VisibleChange(); }));
            Invoke(new MethodInvoker(delegate { this.Ctrl.CloseConnection(); }));


        }





        public void setEmployees(List<User> listUser)
        {
            this.LstUser = listUser;


            Invoke(new MethodInvoker(delegate
            {
                this.lsbEmployees.DataSource = null;
                this.lsbEmployees.DataSource = listUser;
                List<Message> lstMessage = new List<Message>();
                lstMessage.Add(new Message("gabriel@aspirateur.com", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavvaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaavv", "2016-03-21_10-10-10"));
                lstMessage.Add(new Message("gabriel@aspirateur.com", "a", "2016-03-21_10-10-10"));
                lstMessage.Add(new Message("gabriejl@aspirateur.com", "a", "2016-03-21_10-10-10"));
                lstMessage.Add(new Message("gabrjkliel@aspirateur.com", "a", "2016-03-21_10-10-10"));
                lstMessage.Add(new Message("gabriel@aspirateur.com", "a", "2016-03-21_10-10-10"));
                lsbConversation.DataSource = lstMessage;
            }));


        }

        private void lsbEmployees_DrawItem(object sender, DrawItemEventArgs e)
        {
            //repris d'un exercice fait avec M.Beney
            if (e.Index < 0) return;
            User userDrawing = this.lsbEmployees.Items[e.Index] as User;
            // Si l'état de l'élément est sélectionné alors change la couleur de sélection...
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,   // ^ --> XOR logique
                                          e.ForeColor,
                                          Color.LightBlue);//Couleur de son choix
            e.DrawBackground();

            // Définition du pinceau par défaut en noir...
            Brush myBrush = Brushes.Black;

            Pen myPen = new Pen(Color.Black);
            myPen.Width = 2;

            if (userDrawing.GetInformationConnection())
                myBrush = Brushes.Green;
            else
                myBrush = Brushes.Red;

            // Dessine un Cercle rouge ou vert
            e.Graphics.FillEllipse(myBrush, e.Bounds.Left + 8, e.Bounds.Top + 15, 12, 12);
            e.Graphics.DrawEllipse(myPen, e.Bounds.Left + 8, e.Bounds.Top + 15, 12, 12);
            myBrush = Brushes.Black;
            e.Graphics.DrawString(userDrawing.GetidUser().Split('@')[0], new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 30, e.Bounds.Top + 15, StringFormat.GenericTypographic);
            // Dessine un Rectangle gris autour de chaque éléments
            myPen.Color = Color.LightGray;
            myPen.Width = 1;
            e.Graphics.DrawRectangle(myPen, e.Bounds);
            if (userDrawing.GetMessagesNotRead() != 0)
            {
                myBrush = Brushes.Yellow;
                myPen.Color = Color.Black;
                myPen.Width = 2;
                e.Graphics.FillEllipse(myBrush, e.Bounds.Left + 90, e.Bounds.Top + 15, 20, 20);
                e.Graphics.DrawEllipse(myPen, e.Bounds.Left + 90, e.Bounds.Top + 15, 20, 20);
                myBrush = Brushes.Black;

                if (userDrawing.GetMessagesNotRead() >= 100)
                {
                    e.Graphics.DrawString(userDrawing.GetMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 91, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }
                else if (userDrawing.GetMessagesNotRead() < 10)
                {

                    e.Graphics.DrawString(userDrawing.GetMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 97, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }
                else
                {
                    e.Graphics.DrawString(userDrawing.GetMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 95, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }

            }
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        public void showMessage(int nbNewMessage)
        {
            string messages = string.Empty;
            for (int i = this.LstoldMessages.Count; i < this.LstNewMessages.Count; i++)
            {
                Message msg = LstNewMessages[i] as Message;

                if (this.LastAuthor != msg.Author)
                {
                    messages += Environment.NewLine + String.Format("{0,30}----------------------------------", "") + this.Ctrl.UpFirstLetter(msg.getAuthor().Split('@')[0]) + "----------------------------------";
                }
                messages += Environment.NewLine + this.Ctrl.DecryptMessage(msg.getContent()) + Environment.NewLine + String.Format(" {0,130 }Date: ", string.Empty) + msg.getDate();
                this.LastAuthor = msg.getAuthor();
            }
            this.LstoldMessages = this.LstNewMessages;
            tbxMessage.AppendText(messages);
        }
    }
}
