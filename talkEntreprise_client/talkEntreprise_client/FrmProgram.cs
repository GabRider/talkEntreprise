using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace talkEntreprise_client
{
    public partial class FrmProgram : Form
    {
        private Controler _ctrl;

        public Controler Ctrl
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }

        public FrmProgram(Controler c)
        {
            InitializeComponent();
            this.Ctrl = c;
        }

        private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
        {

            Invoke(new MethodInvoker(delegate { this.Ctrl.VisibleChange(); }));
            Invoke(new MethodInvoker(delegate { this.Ctrl.CloseConnection(); }));
            

        }

        private void lsbConversations_DrawItem(object sender, DrawItemEventArgs e)
        {
            User userDrawing = null  ;
            //repris d'un exercice fait avec M.Beney
            if (e.Index < 0) return;
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
            Brush monPinceau = Brushes.Black;

            Pen monCrayon = new Pen(Color.Black);
            monCrayon.Width = 2;

            if (userDrawing.getInformationConnection())
                monPinceau = Brushes.Green;
            else
                monPinceau = Brushes.Red;

            // Dessine un Cercle rouge ou vert
            e.Graphics.FillEllipse(monPinceau, e.Bounds.Left + 8, e.Bounds.Top + 15, 12, 12);
            e.Graphics.DrawEllipse(monCrayon, e.Bounds.Left + 8, e.Bounds.Top + 15, 12, 12);
            monPinceau = Brushes.Black;
            e.Graphics.DrawString(userDrawing.getidUser().Split('@')[0], new Font("Arial", 8, FontStyle.Bold), monPinceau, e.Bounds.Left + 30, e.Bounds.Top + 15, StringFormat.GenericTypographic);
            // Dessine un Rectangle gris autour de chaque éléments
            monCrayon.Color = Color.LightGray;
            monCrayon.Width = 1;
            e.Graphics.DrawRectangle(monCrayon, e.Bounds);
            if (userDrawing.getMessagesNotRead() != 0)
            {
                monPinceau = Brushes.Yellow;
                monCrayon.Color = Color.Black;
                monCrayon.Width = 2;
                e.Graphics.FillEllipse(monPinceau, e.Bounds.Left + 90, e.Bounds.Top + 15, 20, 20);
                e.Graphics.DrawEllipse(monCrayon, e.Bounds.Left + 90, e.Bounds.Top + 15, 20, 20);
                monPinceau = Brushes.Black;

                if (userDrawing.getMessagesNotRead() >= 100)
                {
                    e.Graphics.DrawString(userDrawing.getMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), monPinceau, e.Bounds.Left + 91, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }
                else if (userDrawing.getMessagesNotRead() < 10)
                {

                    e.Graphics.DrawString(userDrawing.getMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), monPinceau, e.Bounds.Left + 97, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }
                else
                {
                    e.Graphics.DrawString(userDrawing.getMessagesNotRead().ToString(), new Font("Arial", 8, FontStyle.Bold), monPinceau, e.Bounds.Left + 95, e.Bounds.Top + 18, StringFormat.GenericTypographic);
                }

            }
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
        
        
    }
}
