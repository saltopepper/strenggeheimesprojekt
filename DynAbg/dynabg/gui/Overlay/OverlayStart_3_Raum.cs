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
    public partial class OverlayStart_3_Raum : UserControl
    {
        public int setupraeume;
        public OverlayStart_3_Raum()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            roomCount.Value++;
        }

        private void subButton_Click(object sender, EventArgs e)
        {
            if (roomCount.Value != 1)
            {
                roomCount.Value--;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            setupraeume = (int) roomCount.Value;
            this.Hide();
        }


    }
}
