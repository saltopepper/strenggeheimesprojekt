namespace DynAbg.gui
{
    partial class FormValveInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBoxValue = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxTemp = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBoxSignal = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.checkBoxBattOK = new System.Windows.Forms.CheckBox();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkBoxCommunicationOK = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.buttonSetHub = new System.Windows.Forms.Button();
			this.numericUpDownHub = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHub)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textBoxID);
			this.groupBox1.Location = new System.Drawing.Point(12, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(259, 47);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Valve ID";
			// 
			// textBoxID
			// 
			this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxID.Location = new System.Drawing.Point(6, 19);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.ReadOnly = true;
			this.textBoxID.Size = new System.Drawing.Size(247, 20);
			this.textBoxID.TabIndex = 0;
			this.textBoxID.Text = "NO INFORMATION";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.textBoxValue);
			this.groupBox2.Location = new System.Drawing.Point(12, 64);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(259, 47);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Value";
			// 
			// textBoxValue
			// 
			this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxValue.Location = new System.Drawing.Point(6, 19);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.ReadOnly = true;
			this.textBoxValue.Size = new System.Drawing.Size(247, 20);
			this.textBoxValue.TabIndex = 0;
			this.textBoxValue.Text = "NO INFORMATION";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.textBoxTemp);
			this.groupBox3.Location = new System.Drawing.Point(12, 117);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(259, 47);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Temperature";
			// 
			// textBoxTemp
			// 
			this.textBoxTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTemp.Location = new System.Drawing.Point(6, 19);
			this.textBoxTemp.Name = "textBoxTemp";
			this.textBoxTemp.ReadOnly = true;
			this.textBoxTemp.Size = new System.Drawing.Size(247, 20);
			this.textBoxTemp.TabIndex = 0;
			this.textBoxTemp.Text = "NO INFORMATION";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.textBoxSignal);
			this.groupBox4.Location = new System.Drawing.Point(12, 170);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(259, 47);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Signal";
			// 
			// textBoxSignal
			// 
			this.textBoxSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSignal.Location = new System.Drawing.Point(6, 19);
			this.textBoxSignal.Name = "textBoxSignal";
			this.textBoxSignal.ReadOnly = true;
			this.textBoxSignal.Size = new System.Drawing.Size(247, 20);
			this.textBoxSignal.TabIndex = 0;
			this.textBoxSignal.Text = "NO INFORMATION";
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox5.Controls.Add(this.checkBoxBattOK);
			this.groupBox5.Location = new System.Drawing.Point(12, 223);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(127, 47);
			this.groupBox5.TabIndex = 4;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Battery";
			// 
			// checkBoxBattOK
			// 
			this.checkBoxBattOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxBattOK.AutoCheck = false;
			this.checkBoxBattOK.AutoSize = true;
			this.checkBoxBattOK.Location = new System.Drawing.Point(6, 19);
			this.checkBoxBattOK.Name = "checkBoxBattOK";
			this.checkBoxBattOK.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBoxBattOK.Size = new System.Drawing.Size(43, 17);
			this.checkBoxBattOK.TabIndex = 0;
			this.checkBoxBattOK.Text = "Ok:";
			this.checkBoxBattOK.UseVisualStyleBackColor = true;
			// 
			// buttonOpen
			// 
			this.buttonOpen.Location = new System.Drawing.Point(6, 16);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(60, 23);
			this.buttonOpen.TabIndex = 5;
			this.buttonOpen.Text = "Open";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(72, 16);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(60, 23);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox6.Controls.Add(this.checkBoxCommunicationOK);
			this.groupBox6.Location = new System.Drawing.Point(145, 223);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(126, 47);
			this.groupBox6.TabIndex = 7;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Communication";
			// 
			// checkBoxCommunicationOK
			// 
			this.checkBoxCommunicationOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxCommunicationOK.AutoCheck = false;
			this.checkBoxCommunicationOK.AutoSize = true;
			this.checkBoxCommunicationOK.Location = new System.Drawing.Point(6, 19);
			this.checkBoxCommunicationOK.Name = "checkBoxCommunicationOK";
			this.checkBoxCommunicationOK.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBoxCommunicationOK.Size = new System.Drawing.Size(43, 17);
			this.checkBoxCommunicationOK.TabIndex = 0;
			this.checkBoxCommunicationOK.Text = "Ok:";
			this.checkBoxCommunicationOK.UseVisualStyleBackColor = true;
			// 
			// groupBox7
			// 
			this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox7.Controls.Add(this.buttonOpen);
			this.groupBox7.Controls.Add(this.buttonClose);
			this.groupBox7.Location = new System.Drawing.Point(12, 276);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(139, 47);
			this.groupBox7.TabIndex = 8;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Commands";
			// 
			// groupBox8
			// 
			this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox8.Controls.Add(this.buttonSetHub);
			this.groupBox8.Controls.Add(this.numericUpDownHub);
			this.groupBox8.Location = new System.Drawing.Point(157, 276);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(114, 47);
			this.groupBox8.TabIndex = 9;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Valve travel in mm";
			// 
			// buttonSetHub
			// 
			this.buttonSetHub.Location = new System.Drawing.Point(72, 18);
			this.buttonSetHub.Name = "buttonSetHub";
			this.buttonSetHub.Size = new System.Drawing.Size(36, 23);
			this.buttonSetHub.TabIndex = 7;
			this.buttonSetHub.Text = "Set";
			this.buttonSetHub.UseVisualStyleBackColor = true;
			this.buttonSetHub.Click += new System.EventHandler(this.buttonSetHub_Click);
			// 
			// numericUpDownHub
			// 
			this.numericUpDownHub.DecimalPlaces = 1;
			this.numericUpDownHub.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.numericUpDownHub.Location = new System.Drawing.Point(6, 19);
			this.numericUpDownHub.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.numericUpDownHub.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownHub.Name = "numericUpDownHub";
			this.numericUpDownHub.Size = new System.Drawing.Size(57, 20);
			this.numericUpDownHub.TabIndex = 1;
			this.numericUpDownHub.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// FormValveInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 335);
			this.Controls.Add(this.groupBox8);
			this.Controls.Add(this.groupBox7);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormValveInfo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Valve informations";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormValveInfo_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHub)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxTemp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxSignal;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxBattOK;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox checkBoxCommunicationOK;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Button buttonSetHub;
		private System.Windows.Forms.NumericUpDown numericUpDownHub;
    }
}