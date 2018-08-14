using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynAbg.gui.Overlay
{
    public partial class OverlayStart_4_Ventile : UserControl
    {
        public int raeume;
        public int[] ventile;
        public OverlayStart_4_Ventile(int anzahlraeume)
        {
            this.raeume = anzahlraeume;
            this.ventile = new int[anzahlraeume];
            
            InitializeComponent();
            roomNumber.Value = 0;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            anzahlVentile.Value++;
        }

        private void subButton_Click(object sender, EventArgs e)
        {
            if (anzahlVentile.Value != 1)
            {
                anzahlVentile.Value--;
            }
        }

        private void amountBox_Click(object sender, EventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            ventile[(int)roomNumber.Value] = (int)anzahlVentile.Value;
            // Wenn noch Anzahl Ventile ausgewaehlt werden muessen
            if ((int)roomNumber.Value != raeume-1)
            {
                roomNumber.Value++;
                anzahlVentile.Value = 1;
            }
            else
            {
                this.Hide();
            }
        }
    }
}
