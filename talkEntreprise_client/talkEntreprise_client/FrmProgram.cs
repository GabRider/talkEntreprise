using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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
        private string _lastAuthor;
        private int _nbMessages;
        private User _lastSelectedUser;







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
        public int NbMessages
        {
            get { return _nbMessages; }
            set { _nbMessages = value; }
        }
        public string LastAuthor
        {
            get { return _lastAuthor; }
            set { _lastAuthor = value; }
        }
        public User LastSelectedUser
        {
            get { return _lastSelectedUser; }
            set { _lastSelectedUser = value; }
        }

        public FrmProgram(Controler c)
        {
            InitializeComponent();
            this.Ctrl = c;

            this.UpdateLstUser = new Thread(new UpdateUser(this, this.Ctrl).init);
            this.UpdateLstUser.Start();
            this.UserConnected = this.Ctrl.GetUserConnected();
            this.tbxUser.Text = Environment.NewLine + this.UserConnected.GetidUser();
            this.NbMessages = 0;


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
                int getLastSelected = lsbEmployees.SelectedIndex;
                this.LastSelectedUser = lsbEmployees.SelectedItem as User;
                this.lsbEmployees.DataSource = null;
                this.lsbEmployees.DataSource = listUser;

                if (getLastSelected < 0)
                {
                    this.lsbEmployees.SelectedIndex = 0;
                }
                else
                {
                    this.lsbEmployees.SelectedIndex = getLastSelected;
                }



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

        public void showMessage(List<Message> lstNewMessages, string destination, string iduser)
        {

            Invoke(new MethodInvoker(delegate
            {
                User user = this.lsbEmployees.SelectedItem as User;

                if (user.GetidUser() == destination || user.GetidUser() == iduser || this.lsbEmployees.SelectedIndex == 0)
                {

                    string messages = string.Empty;
                    for (int i = this.NbMessages; i < lstNewMessages.Count; i++)
                    {
                        Message msg = lstNewMessages[i] as Message;


                        if (this.LastAuthor != msg.Author)
                        {

                            messages += Environment.NewLine + String.Format("{0,30}----------------------------------", "") + msg.GetAuthor().Split('@')[0] + "----------------------------------";
                        }
                        messages += Environment.NewLine + msg.GetContent() + Environment.NewLine + String.Format(" {0,130 }Date: ", string.Empty) + msg.GetDate();
                        this.LastAuthor = msg.GetAuthor();
                    }

                    this.NbMessages = lstNewMessages.Count;
                    tbxMessage.AppendText(messages);
                }


            }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string allDestinations = string.Empty;
            bool first = true;
            User destination = lsbEmployees.SelectedItem as User;
            if (tbxWriteMessage.Text.Trim() != "")
            {
                if (lsbEmployees.SelectedIndex == 0)
                {

                    foreach (User user in lsbEmployees.Items)
                    {
                        if (user.GetIdGroup() == this.UserConnected.GetIdGroup())
                        {
                            if (first)
                            {
                                first = false;
                            }
                            else
                            {
                                allDestinations += user.GetidUser() + "!";


                            }

                        }
                    }
                    this.Ctrl.sendMessageGroup(this.UserConnected.GetidUser(),allDestinations, this.tbxWriteMessage.Text, true);
                    this.tbxWriteMessage.Clear();
                }
                else
                {
                    this.Ctrl.sendMessage(this.UserConnected.GetidUser(), destination.GetidUser(), this.tbxWriteMessage.Text, false);
                    this.tbxWriteMessage.Clear();
                }

            }

        }
        /// <summary>
        /// permet de donner la connexion du client
        /// </summary>
        /// <returns>connexion du client</returns>
        public TcpClient GetTcpClient()
        {
            return this.Ctrl.TClient;
        }
        /// <summary>
        /// permet de donner le flux d'information du client
        /// </summary>
        /// <returns>flux d'information du client</returns>
        public NetworkStream GetNetStream()
        {
            return this.Ctrl.Stream;
        }

        /// <summary>
        /// permet de décoder le message
        /// </summary>
        /// <param name="message">message codé</param>
        /// <returns>message original</returns>
        public string DecryptMessage(string message)
        {
            return this.Ctrl.DecryptMessage(message);
        }

        private void lsbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

            User user = this.lsbEmployees.SelectedItem as User;
            if (user != null)
            {

                if (this.LastSelectedUser != null)
                {
                    if (this.LastSelectedUser.GetidUser() != user.GetidUser())
                    {
                        this.NbMessages = 0;
                        this.tbxMessage.Clear();
                        if (lsbEmployees.SelectedIndex != 0)
                        {
                            this.Ctrl.GetConversation(this.UserConnected.GetidUser(), user.GetidUser(), false);
                        }
                        else
                        {
                            this.Ctrl.GetConversation(this.UserConnected.GetidUser(), user.GetidUser(), true);
                        }

                        this.LastSelectedUser = user;
                    }
                }
                else
                {
                    this.LastSelectedUser = user;
                    this.Ctrl.GetConversation(this.UserConnected.GetidUser(), user.GetidUser(), true);
                }







            }



        }

        private void lsbEmployees_DataSourceChanged(object sender, EventArgs e)
        {

        }

    }
}
