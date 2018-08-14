using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DynAbg.gui.control;
using System.Windows.Forms;

namespace DynAbg.gui.Overlay
{
    public partial class OverlayForm : Form
    {

        public bool completed = false;
        public int setupraeume;
        public int[] setupventile;
        public OverlayForm()
        {
            InitializeComponent();
            
        }

        /*
            overlayStart1.Left = 0;
            overlayStart1.Top = 69;
            overlayStart1.Height = ClientRectangle.Height;
            overlayStart1.Width = ClientRectangle.Width;
             */

        // Overlays
        OverlayStart os = new OverlayStart();
        OverlayStart_2 os2 = new OverlayStart_2();
        OverlayStart_3_Raum os3 = new OverlayStart_3_Raum();
        OverlayStart_4_Ventile os4;

        private void os1changed(object sender, EventArgs e)
        {
            // Im Falle dass OS1 geschlossen wird
            if (!os.Visible)
            {
                os3.Show();
            }
        }

        //erstmal ausgelassen
        private void os2changed(object sender, EventArgs e)
        {
            // Im Falle dass OS2 geschlossen wird
            if (!os2.Visible)
            {
                os3.Show();
            }
        }

        private void os3changed(object sender, EventArgs e)
        {
            // Im Falle dass OS3 geschlossen wird
            if (!os3.Visible)
            {
                setupraeume = os3.setupraeume;
                setupventile = new int[setupraeume];
                os4 = new OverlayStart_4_Ventile(setupraeume);
                flowLayoutPanel1.Controls.Add(os4);
                os4.VisibleChanged += new EventHandler(os4changed);
                os4.Show();
            }
        }

        private void os4changed(object sender, EventArgs e)
        {
            // Im Falle dass OS4 geschlossen wird
            if (!os4.Visible)
            {
                for (int i = 0; i < setupraeume; i++)
                {
                    setupventile[i] = os4.ventile[i];
                }
                completed = true;
                Form tmp = this.FindForm();
                tmp.Close();
                tmp.Dispose();
            }
        }


        private void OverlayForm_Load(object sender, EventArgs e)
        {
            

            os.Show();
            os2.Hide();
            os3.Hide();
            
          

            os.VisibleChanged += new EventHandler(os1changed);
            os2.VisibleChanged += new EventHandler(os2changed);
            os3.VisibleChanged += new EventHandler(os3changed);
            
            flowLayoutPanel1.Controls.Add(os);
            flowLayoutPanel1.Controls.Add(os2);
            flowLayoutPanel1.Controls.Add(os3);
            


        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
