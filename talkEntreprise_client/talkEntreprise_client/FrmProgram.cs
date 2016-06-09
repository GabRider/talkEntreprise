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
using System.Windows.Threading;
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
        private bool _serverError;
        private const int IDADMINISTRATOR=3;







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
        public bool ServerError
        {
            get { return _serverError; }
            set { _serverError = value; }
        }
        public FrmProgram(Controler c)
        {
            InitializeComponent();
            this.Ctrl = c;
            this.ServerError = false;
            this.UpdateLstUser = new Thread(new UpdateUser(this, this.Ctrl).init);
            this.UpdateLstUser.IsBackground = true;
            this.UpdateLstUser.Start();
            this.UserConnected = this.Ctrl.GetUserConnected();
            this.tbxUser.Text = Environment.NewLine + this.UserConnected.GetIdUser();
            this.NbMessages = 0;


        }

        private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (!this.ServerError)
            {
                this.Ctrl.CloseConnection();
               
            }

            this.Ctrl.VisibleChange(true);
            



        }





        public void setEmployees(List<User> listUser)
        {
            this.LstUser = listUser;


            Invoke(new MethodInvoker(delegate
            {

                try
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
                }
                catch (Exception)
                {

                    this.Close();
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
            e.Graphics.DrawString(userDrawing.GetIdUser().Split('@')[0], new Font("Arial", 10, FontStyle.Bold), myBrush, e.Bounds.Left + 30, e.Bounds.Top + 15, StringFormat.GenericTypographic);
            // Dessine un Rectangle gris autour de chaque éléments
           
                myBrush = Brushes.Red;
                e.Graphics.DrawString(userDrawing.GetAdmin(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 30, e.Bounds.Top + 32, StringFormat.GenericTypographic); 
           

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
        /// <summary>
        /// permet d'afficher les messages
        /// </summary>
        /// <param name="lstNewMessages">liste des messages</param>
        /// <param name="destination">destinataire</param>
        /// <param name="iduser">envoyeur</param>
        /// <param name="isforGroup">si c'est envoyé au groupe</param>
        public void showMessage(List<Message> lstNewMessages, string destination, string iduser, bool isforGroup)
        {

            Invoke(new MethodInvoker(delegate
            {
                User user = this.lsbEmployees.SelectedItem as User;

                if (user != null)
                {


                 //   if ((user.GetidUser() == destination || user.GetidUser() == iduser) && user.GetidUser().Contains("@") && !isforGroup || (isforGroup && lsbEmployees.SelectedIndex == 0))
                       if (this.LastSelectedUser.GetIdUser() == user.GetIdUser())
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

                }
                else
                {
                    this.ServerClosed();
                }

            }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string allDestinations = string.Empty;
            bool first = true;
            User destination = lsbEmployees.SelectedItem as User;
            if (tbxWriteMessage.Text.Trim() != "" && destination != null)
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
                                allDestinations += user.GetIdUser() + "!";
                            }

                        }
                    }
                    if (this.Ctrl.sendMessageGroup(this.UserConnected.GetIdUser(), allDestinations, this.tbxWriteMessage.Text, true))
                    {
                        Thread.Sleep(10);
                        this.UpdateStateMessagesGroup();
                        Thread.Sleep(10);
                        this.Ctrl.UpdateUsers(this.UserConnected.GetNameGroup(), this.UserConnected.GetIdUser(), this.UserConnected.GetIdGroup());
                        this.tbxWriteMessage.Clear();
                    }
                    else
                    {
                        this.ServerClosed();
                    }

                }
                else
                {
                    if (this.Ctrl.sendMessage(this.UserConnected.GetIdUser(), destination.GetIdUser(), this.tbxWriteMessage.Text, false))
                    {
                        Thread.Sleep(10);
                        this.UpdateStateMessagesOneUser();
                        Thread.Sleep(10);
                        this.Ctrl.UpdateUsers(this.UserConnected.GetNameGroup(), this.UserConnected.GetIdUser(), this.UserConnected.GetIdGroup());
                        this.tbxWriteMessage.Clear();
                    }
                    else
                    {
                        this.ServerClosed();
                    }


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
        /// <summary>
        /// fait quitter le programme à l'utilisateur
        /// </summary>
        public void ServerClosed()
        {
            this.ServerError = true;
            MessageBox.Show("Le Serveur a été étint. vous allez être automatiquement déconnecté", "Serveur Inaccessible", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();

        }
        /// <summary>
        /// fait quitter le programme à l'utilisateur
        /// </summary>
        public void DatabaseClosed()
        {
            this.ServerError = true;
            MessageBox.Show("La base de donnée a été étinte. vous allez être automatiquement déconnecté", "Base de données Inaccessible", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close() ;
        }
        private void lsbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

            User user = this.lsbEmployees.SelectedItem as User;
            if (user != null)
            {

                if (this.LastSelectedUser != null)
                {
                    if (this.LastSelectedUser.GetIdUser() != user.GetIdUser())
                    {
                        this.NbMessages = 0;
                        this.tbxMessage.Clear();
                        this.LastAuthor = string.Empty;
                        if (lsbEmployees.SelectedIndex != 0)
                        {

                            if (this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), false))
                            {
                                Thread.Sleep(3);
                                this.UpdateStateMessagesOneUser();
                            }
                            else
                            {
                                this.ServerClosed();
                            }
                        }
                        else
                        {
                            if (this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), true))
                            {
                                Thread.Sleep(3);
                                this.UpdateStateMessagesGroup();
                            }
                            else
                            {
                                this.ServerClosed();
                            }

                        }

                        this.LastSelectedUser = user;
                    }


                }
                else
                {
                    this.LastSelectedUser = user;
                    foreach (User userInfo in this.lsbEmployees.Items)
                    {
                        if (this.Ctrl.UpdateStateMessages(userInfo.GetIdUser(), this.UserConnected.GetIdUser(), true, this.UserConnected.GetNameGroup(), this.UserConnected.GetIdGroup(), this.UserConnected.GetIdUser()))
                        {
                            Thread.Sleep(3);
                            this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), true);
                        }


                    }
                }







            }



        }
        private void UpdateStateMessagesGroup()
        {

            foreach (User userInfo in this.lsbEmployees.Items)
            {
                if (userInfo.GetIdUser().Contains("@"))
                {
                    this.Ctrl.UpdateStateMessages(userInfo.GetIdUser(), this.UserConnected.GetIdUser(), true, this.UserConnected.GetNameGroup(), this.UserConnected.GetIdGroup(), this.UserConnected.GetIdUser());
                    Thread.Sleep(4);
                }

            }
        }
        private void UpdateStateMessagesOneUser()
        {
            User user = this.lsbEmployees.SelectedItem as User;
            this.Ctrl.UpdateStateMessages(user.GetIdUser(), this.UserConnected.GetIdUser(), false, this.UserConnected.GetNameGroup(), this.UserConnected.GetIdGroup(), this.UserConnected.GetIdUser());
        }




    }
}
