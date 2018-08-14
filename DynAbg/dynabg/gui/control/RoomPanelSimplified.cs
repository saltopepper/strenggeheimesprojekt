using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DynAbg.gui.control;

namespace DynAbg.gui
{
    public partial class RoomPanelSimplified : UserControl
    {
        public int ID;
        public RoomPanelSimplified()
        {
            InitializeComponent();
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {

        }

        public void nameBoxChanging(object sender, EventArgs e)
        {
            this.nameBox.Text = ((TextBox)sender).Text;
        }

        public void istModuleChanging(object sender, EventArgs e)
        {
            this.istModule.Text = ((TextBox)sender).Text;
        }

        public void sollModuleChanging(object sender, EventArgs e)
        {
            this.sollModule.Text = ((TextBox)sender).Text;
        }

        public void moduleIDChanging(object sender, EventArgs e)
        {
            this.moduleID.Text = ((TextBox)sender).Text;
        }

        public void sollSollChanging(object sender, EventArgs e)
        {
            this.sollSoll.Text = ((TextBox)sender).Text;
        }

        private void sollButton_Click(object sender, EventArgs e)
        {

        }

        public void addHeater(int a)
        {
            //adds heater
            
            HeaterPanelSimplified heaterPanel = new HeaterPanelSimplified(a);
            heaterPanel.ventilNummer.Text = "Ventil #" + a ;
            tableLayoutPanel1.Controls.Add(heaterPanel);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
