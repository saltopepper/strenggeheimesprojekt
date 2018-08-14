using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DynAbg.gui.control
{
    public partial class HeaterPanelSimplified : UserControl
    {
        public int VentilID;
        public HeaterPanelSimplified(int a)
        {
            VentilID = a;
            InitializeComponent();
        }

        public void textChanged(object sender, EventArgs e)
        {
            this.ventilIDBox.Text = ((TextBox)sender).Text;
        }

        public void numericSollChanged(object sender, EventArgs e)
        {
           this.hubSollBox.Text = ((TextBox)sender).Text;
        }

        public void isValueChanged(object sender, EventArgs e)
        {
            this.hubIstBox.Text = ((TextBox)sender).Text;
        }

        public void isTempChanged(object sender, EventArgs e)
        {
            this.tempBox.Text = ((TextBox)sender).Text;
        }

        public void modusChanged(object sender, EventArgs e)
        {
            this.modusBox.Text = ((TextBox)sender).Text;
        }
    }
}
