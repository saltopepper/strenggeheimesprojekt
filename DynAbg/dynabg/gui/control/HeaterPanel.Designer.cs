﻿namespace DynAbg.gui.control
{
    partial class HeaterPanel
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isValue = new System.Windows.Forms.TextBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numericSoll = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Mode = new System.Windows.Forms.NumericUpDown();
            this.isTemp = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSoll)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mode)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.isValue);
            this.groupBox1.Controls.Add(this.buttonCopy);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // isValue
            // 
            this.isValue.Location = new System.Drawing.Point(112, 26);
            this.isValue.Name = "isValue";
            this.isValue.Size = new System.Drawing.Size(31, 20);
            this.isValue.TabIndex = 15;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.BackgroundImage = global::DynAbg.Properties.Resources.copy;
            this.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCopy.Location = new System.Drawing.Point(589, 27);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(18, 18);
            this.buttonCopy.TabIndex = 12;
            this.buttonCopy.UseVisualStyleBackColor = false;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackgroundImage = global::DynAbg.Properties.Resources.delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.Location = new System.Drawing.Point(589, 9);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(18, 18);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(35, 47);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nr.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonInfo);
            this.groupBox3.Controls.Add(this.buttonLearn);
            this.groupBox3.Controls.Add(this.textBoxID);
            this.groupBox3.Location = new System.Drawing.Point(271, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 47);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ventil ID";
            // 
            // buttonInfo
            // 
            this.buttonInfo.Enabled = false;
            this.buttonInfo.Location = new System.Drawing.Point(144, 17);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(45, 23);
            this.buttonInfo.TabIndex = 2;
            this.buttonInfo.Text = "Info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(82, 17);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(57, 23);
            this.buttonLearn.TabIndex = 1;
            this.buttonLearn.Text = "Anlernen";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // textBoxID
            // 
            this.textBoxID.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxID.Location = new System.Drawing.Point(7, 19);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(69, 20);
            this.textBoxID.TabIndex = 0;
            this.textBoxID.TextChanged += new System.EventHandler(this.textBoxID_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numericSoll);
            this.groupBox6.Location = new System.Drawing.Point(48, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(58, 47);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "HubSoll";
            this.groupBox6.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // numericSoll
            // 
            this.numericSoll.Location = new System.Drawing.Point(6, 19);
            this.numericSoll.Name = "numericSoll";
            this.numericSoll.Size = new System.Drawing.Size(35, 20);
            this.numericSoll.TabIndex = 2;
            this.numericSoll.ValueChanged += new System.EventHandler(this.numericSoll_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Mode);
            this.groupBox5.Controls.Add(this.isTemp);
            this.groupBox5.Location = new System.Drawing.Point(106, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(156, 46);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "HubIst    Temp       Modus";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // Mode
            // 
            this.Mode.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Mode.Location = new System.Drawing.Point(97, 18);
            this.Mode.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Mode.Name = "Mode";
            this.Mode.ReadOnly = true;
            this.Mode.Size = new System.Drawing.Size(38, 20);
            this.Mode.TabIndex = 15;
            this.Mode.ValueChanged += new System.EventHandler(this.Mode_ValueChanged);
            // 
            // isTemp
            // 
            this.isTemp.Location = new System.Drawing.Point(50, 18);
            this.isTemp.Name = "isTemp";
            this.isTemp.Size = new System.Drawing.Size(32, 20);
            this.isTemp.TabIndex = 14;
            // 
            // HeaterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(610, 62);
            this.Name = "HeaterPanel";
            this.Size = new System.Drawing.Size(610, 62);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericSoll)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.TextBox textBoxID;
        public System.Windows.Forms.NumericUpDown numericSoll;
        public System.Windows.Forms.TextBox isTemp;
        public System.Windows.Forms.TextBox isValue;
        public System.Windows.Forms.NumericUpDown Mode;
    }
}