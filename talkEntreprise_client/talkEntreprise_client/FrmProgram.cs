using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private const int IDADMINISTRATOR = 3;
        private int _dayOldMessages;








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
        public int DayOldMessages
        {
            get { return _dayOldMessages; }
            set { _dayOldMessages = value; }
        }

        /////////Constructeur////////

        public FrmProgram(Controler c)
        {
            InitializeComponent();
            this.Ctrl = c;
            this.ServerError = false;
            this.UpdateLstUser = new Thread(new UpdateUser(this, this.Ctrl).Init);
            this.UpdateLstUser.IsBackground = true;
            this.UpdateLstUser.Start();
            this.UserConnected = this.Ctrl.GetUserConnected();
            this.tbxUser.Text = Environment.NewLine + this.UserConnected.GetIdUser().Split('@')[0];
            this.NbMessages = 0;
            this.DayOldMessages = 0;

            // this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), this.UserConnected.GetIdUser(), true);

        }

        ////méthodes de la fenêtre//////////
        private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.ServerError)
            {
                DialogResult answer;
                FrmExit exit = new FrmExit();
                answer = exit.ShowDialog();
                if (answer == DialogResult.Cancel)
                {
                    // empêche la fermeture de la fenêtre
                    e.Cancel = true;
                }
                else
                {
                    if (answer == DialogResult.OK)
                    {
                        try
                        {
                            this.Ctrl.CloseConnection();
                            Process.GetCurrentProcess().Kill();
                        }
                        catch (Exception)
                        {
                            Process.GetCurrentProcess().Kill();
                        }
                    }

                    else
                    {
                        try
                        {
                            this.Ctrl.CloseConnection();
                            this.Ctrl.VisibleChange(true);
                        }
                        catch (Exception)
                        {
                            this.Ctrl.VisibleChange(true);
                        }


                    }
                }
            }
            else
            {
                this.Ctrl.VisibleChange(true);
            }
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
                e.Graphics.FillEllipse(myBrush, e.Bounds.Left + 105, e.Bounds.Top + 15, 20, 20);
                e.Graphics.DrawEllipse(myPen, e.Bounds.Left + 105, e.Bounds.Top + 15, 20, 20);
                myBrush = Brushes.Black;

                if (userDrawing.GetMessagesNotRead() >= 100)
                {
                    e.Graphics.DrawString(userDrawing.GetMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 106, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }
                else if (userDrawing.GetMessagesNotRead() < 10)
                {

                    e.Graphics.DrawString(userDrawing.GetMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 112, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }
                else
                {
                    e.Graphics.DrawString(userDrawing.GetMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), myBrush, e.Bounds.Left + 110, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }

            }
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
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

                        if (this.DayOldMessages != 0)
                        {
                            this.GetOldConversation();
                            Thread.Sleep(200);
                        }
                        this.Ctrl.SendMessageGroup(this.UserConnected.GetIdUser(), allDestinations, this.tbxWriteMessage.Text, true);

                        Thread.Sleep(10);
                        this.UpdateStateMessagesGroup();
                        Thread.Sleep(10);
                        this.Ctrl.UpdateUsers(this.UserConnected.GetNameGroup(), this.UserConnected.GetIdUser(), this.UserConnected.GetIdGroup());
                        this.tbxWriteMessage.Clear();
                    }
                    else
                    {
                        if (this.DayOldMessages != 0)
                        {
                            this.GetOldConversation();
                            Thread.Sleep(200);
                        }
                        this.Ctrl.SendMessage(this.UserConnected.GetIdUser(), destination.GetIdUser(), this.tbxWriteMessage.Text, false);

                        Thread.Sleep(10);
                        this.UpdateStateMessagesOneUser();
                        Thread.Sleep(10);
                        this.Ctrl.UpdateUsers(this.UserConnected.GetNameGroup(), this.UserConnected.GetIdUser(), this.UserConnected.GetIdGroup());
                        this.tbxWriteMessage.Clear();
                    }
                }

            }
            catch (Exception)
            {

                this.ServerClosed();
            }

        }
        private void lsbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                User user = this.lsbEmployees.SelectedItem as User;
                if (user != null)
                {

                    if (this.LastSelectedUser != null)
                    {
                        if (this.LastSelectedUser.GetIdUser() != user.GetIdUser())
                        {
                            this.NbMessages = 0;
                            this.tbxMessages.Clear();
                            this.LastAuthor = string.Empty;
                            if (lsbEmployees.SelectedIndex != 0)
                            {
                                if (this.DayOldMessages != 0)
                                {
                                    this.GetOldConversation();
                                    Thread.Sleep(3);
                                }

                                this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), false);
                                Thread.Sleep(200);
                                this.UpdateStateMessagesOneUser();



                            }
                            else
                            {
                                if (this.DayOldMessages != 0)
                                {
                                    this.GetOldConversation();
                                    Thread.Sleep(200);
                                }
                                this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), true);
                                Thread.Sleep(20);

                                Thread.Sleep(20);
                                this.UpdateStateMessagesGroup();


                            }

                            this.LastSelectedUser = user;
                        }


                    }
                    else
                    {
                        this.LastSelectedUser = user;
                        foreach (User userInfo in this.lsbEmployees.Items)
                        {

                            Thread.Sleep(3);
                            this.Ctrl.UpdateStateMessages(userInfo.GetIdUser(), this.UserConnected.GetIdUser(), true, this.UserConnected.GetNameGroup(), this.UserConnected.GetIdGroup(), this.UserConnected.GetIdUser());
                            Thread.Sleep(3);
                            if (this.DayOldMessages != 0)
                            {
                                this.GetOldConversation();
                                Thread.Sleep(200);
                            }

                            this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), true);



                        }
                    }

                }

            }
            catch (Exception)
            {
                this.ServerClosed();
            }

        }
        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsmiOldMesssage_Click(object sender, EventArgs e)
        {
            try
            {


                this.LastAuthor = "";
                tbxMessages.Clear();
                User user = lsbEmployees.SelectedItem as User;
                ToolStripMenuItem tsmiFocus = sender as ToolStripMenuItem;
                foreach (ToolStripMenuItem tsmi in this.tsmiOldMessages.DropDownItems)
                {

                    tsmi.Checked = false;
                }
                tsmiFocus.Checked = true;
                this.DayOldMessages = Convert.ToInt32(tsmiFocus.Tag);
                Thread.Sleep(10);
                if (lsbEmployees.SelectedIndex != 0)
                {
                    if (this.DayOldMessages != 0)
                    {
                        this.Ctrl.GetOldMessages(this.UserConnected.GetIdUser(), user.GetIdUser(), false, this.DayOldMessages);
                        Thread.Sleep(10);
                        this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), false);
                    }
                    else
                    {
                        this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), false);
                    }

                }
                else
                {
                    if (this.DayOldMessages != 0)
                    {
                        this.Ctrl.GetOldMessages(this.UserConnected.GetIdUser(), user.GetIdUser(), true, this.DayOldMessages);
                        Thread.Sleep(10);
                        this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), true);
                    }
                    else
                    {
                        this.Ctrl.GetConversation(this.UserConnected.GetIdUser(), user.GetIdUser(), true);
                    }
                }


                this.NbMessages = 0;
            }
            catch (Exception)
            {

                this.ServerClosed();
            }

        }
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }
        private void tsmiOldMessages_Click(object sender, EventArgs e)
        {

        }
        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            DialogResult res = new DialogResult();
            FrmSettings settings = new FrmSettings(this, this.UserConnected.GetPassword());
            res = settings.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    if (settings.GetNewPassword() != string.Empty)
                    {
                        this.Ctrl.ChangePassword(this.UserConnected.GetIdUser(), settings.GetNewPassword());
                    }
                    else
                    {
                        tsmiSettings_Click(sender, e);
                    }



                }
                catch (Exception)
                {

                    this.ServerClosed();
                }
            }
        }
        private void tsmDateTime_Tick(object sender, EventArgs e)
        {
            tssDate.Text = DateTime.Now.ToLocalTime().ToString() + " (Heure UTC)";
        }
        ////////méthodes///////
        /// <summary>
        /// permet de mettre à jour la liste des employés
        /// </summary>
        /// <param name="listUsers">liste d'employés</param>
        public void SetEmployees(List<User> listUsers)
        {
            this.LstUser = listUsers;
            Invoke(new MethodInvoker(delegate
            {

                try
                {
                    int getLastSelected = lsbEmployees.SelectedIndex;
                    this.LastSelectedUser = lsbEmployees.SelectedItem as User;
                    this.lsbEmployees.DataSource = null;
                    this.lsbEmployees.DataSource = listUsers;

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
        /// <summary>
        /// permet d'afficher les messages
        /// </summary>
        /// <param name="lstNewMessages">liste des messages</param>
        /// <param name="destination">destinataire</param>
        /// <param name="iduser">envoyeur</param>
        /// <param name="isforGroup">si c'est envoyé au groupe</param>
        public void ShowMessages(List<Message> lstNewMessages, string destination, string iduser, bool isforGroup)
        {
            //sécursation pour pouvoir exécuter les prochaines instructions.
            Invoke(new MethodInvoker(delegate
            {
                //récupération de l'utilisateur sélectionné
                User user = this.lsbEmployees.SelectedItem as User;

                if (user != null)
                {

                    // si les messages reçus sont pour la conversation actuellement visible par l'utilisateur 
                    if ((user.GetIdUser() == destination || user.GetIdUser() == iduser && user.GetIdUser().Contains("@")) || (isforGroup && lsbEmployees.SelectedIndex == 0))
                    {
                        //variable permettant de stocker les nouveau messages à afficher
                        string messages = string.Empty;
                        //pour chaque nouveau message
                        for (int i = this.NbMessages; i < lstNewMessages.Count; i++)
                        {
                            //récupération des informations relatif au message
                            Message msg = lstNewMessages[i] as Message;

                            //Si le message n'est pas envoyé par le même utilisateur alors afficher l'autheur du messae
                            if (this.LastAuthor != msg.Author)
                            {

                                messages += Environment.NewLine + String.Format("{0,30}----------------------------------", "") + msg.GetAuthor().Split('@')[0] + "----------------------------------";
                            }
                            messages += Environment.NewLine + msg.GetContent() + Environment.NewLine + String.Format(" {0,130 }Date: ", string.Empty) + msg.GetDate();
                            this.LastAuthor = msg.GetAuthor();
                        }
                        //permet de sauvegarder le nombre de message afficher sur l'écran
                        this.NbMessages = lstNewMessages.Count;
                        // Ajout des nouveaux messages visuellement (l'instruction "Invoke" permet de modifier les composants du programme)
                        tbxMessages.AppendText(messages);
                    }

                }
                else
                {
                    //lancer la méthode qui permet
                    this.ServerClosed();
                }

            }));
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
            this.Close();
        }
        /// <summary>
        /// permet de ^mettre à jour les états des messages de son groupe
        /// </summary>
        private void UpdateStateMessagesGroup()
        {

            foreach (User userInfo in this.lsbEmployees.Items)
            {
                if (userInfo.GetIdUser().Contains("@") && userInfo.GetIdGroup() == this.UserConnected.GetIdGroup())
                {
                    this.Ctrl.UpdateStateMessages(userInfo.GetIdUser(), this.UserConnected.GetIdUser(), true, this.UserConnected.GetNameGroup(), this.UserConnected.GetIdGroup(), this.UserConnected.GetIdUser());
                    Thread.Sleep(4);
                }

            }
        }
        /// <summary>
        /// permete de mettre à jour les états des messages entre deux utilisateurs
        /// </summary>
        private void UpdateStateMessagesOneUser()
        {
            User user = this.lsbEmployees.SelectedItem as User;
            this.Ctrl.UpdateStateMessages(user.GetIdUser(), this.UserConnected.GetIdUser(), false, this.UserConnected.GetNameGroup(), this.UserConnected.GetIdGroup(), this.UserConnected.GetIdUser());
        }
        /// <summary>
        /// permet de récupérer les anciennes converstaions présent dans la base de données
        /// </summary>
        private void GetOldConversation()
        {
            foreach (ToolStripMenuItem tsmi in this.tsmiOldMessages.DropDownItems)
            {
                User user = lsbEmployees.SelectedItem as User;

                if (tsmi.Checked)
                {
                    if (lsbEmployees.SelectedIndex != 0)
                    {
                        this.Ctrl.GetOldMessages(this.UserConnected.GetIdUser(), user.GetIdUser(), false, this.DayOldMessages);
                    }
                    else
                    {
                        this.Ctrl.GetOldMessages(this.UserConnected.GetIdUser(), user.GetIdUser(), true, this.DayOldMessages);
                    }

                    if (Convert.ToInt32(tsmi.Tag) != this.DayOldMessages)
                    {
                        tbxMessages.Clear();
                        this.NbMessages = 0;
                    }

                    break;
                }
            }
        }
        // <summary>
        /// permet de coder le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="password">mot de passe de l'utilisateur</param>
        /// <returns></returns>
        public string Sha1(string password)
        {
            return this.Ctrl.Sha1(password);
        }
        /// <summary>
        /// permet de savoir si le mot de passe a bel et bien été enregistré dans la base de données
        /// </summary>
        /// <param name="isChanged">si le changement c'est effectué</param>
        /// <param name="password"> le nouveau mot de passe de l'utilisateur </param>
        public void PasswordIsChanged(bool isChanged, string password)
        {
            if (isChanged)
            {
                MessageBox.Show("Votre nouveau mot de passe a été enregistré", "Le mot de passe a été modifié", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.UserConnected.SetPassword(password);
            }
            else
            {
                MessageBox.Show("Votre nouveau mot de passe a été enregistré", "Le mot de passe n'a pas pu être modifié", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DatabaseClosed();
            }
        }




    }
}
