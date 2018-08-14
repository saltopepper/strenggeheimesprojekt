namespace DynAbg.gui.control
{
	partial class RoomPanel
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
            this.groupBoxTitle = new System.Windows.Forms.GroupBox();
            this.Sollwert = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.roomDataLoad = new System.Windows.Forms.Button();
            this.roomDataSave = new System.Windows.Forms.Button();
            this.raumModule = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.moduleSoll = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.moduleID = new System.Windows.Forms.TextBox();
            this.moduleAnlernen = new System.Windows.Forms.Button();
            this.moduleIst = new System.Windows.Forms.TextBox();
            this.showGraph = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddHeater = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxName = new System.Windows.Forms.GroupBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.pictureBoxResize = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxTitle.SuspendLayout();
            this.Sollwert.SuspendLayout();
            this.raumModule.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTitle
            // 
            this.groupBoxTitle.Controls.Add(this.Sollwert);
            this.groupBoxTitle.Controls.Add(this.roomDataLoad);
            this.groupBoxTitle.Controls.Add(this.roomDataSave);
            this.groupBoxTitle.Controls.Add(this.raumModule);
            this.groupBoxTitle.Controls.Add(this.showGraph);
            this.groupBoxTitle.Controls.Add(this.buttonDelete);
            this.groupBoxTitle.Controls.Add(this.buttonAddHeater);
            this.groupBoxTitle.Controls.Add(this.buttonCopy);
            this.groupBoxTitle.Controls.Add(this.groupBox1);
            this.groupBoxTitle.Controls.Add(this.groupBoxName);
            this.groupBoxTitle.Controls.Add(this.pictureBoxResize);
            this.groupBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTitle.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTitle.Name = "groupBoxTitle";
            this.groupBoxTitle.Size = new System.Drawing.Size(973, 181);
            this.groupBoxTitle.TabIndex = 0;
            this.groupBoxTitle.TabStop = false;
            this.groupBoxTitle.Text = "Raum:";
            // 
            // Sollwert
            // 
            this.Sollwert.Controls.Add(this.textBox1);
            this.Sollwert.Location = new System.Drawing.Point(119, 72);
            this.Sollwert.Name = "Sollwert";
            this.Sollwert.Size = new System.Drawing.Size(75, 55);
            this.Sollwert.TabIndex = 19;
            this.Sollwert.TabStop = false;
            this.Sollwert.Text = "Sollwert";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(62, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // roomDataLoad
            // 
            this.roomDataLoad.Location = new System.Drawing.Point(18, 102);
            this.roomDataLoad.Name = "roomDataLoad";
            this.roomDataLoad.Size = new System.Drawing.Size(85, 25);
            this.roomDataLoad.TabIndex = 18;
            this.roomDataLoad.Text = "Data Load";
            this.roomDataLoad.UseVisualStyleBackColor = true;
            this.roomDataLoad.Click += new System.EventHandler(this.roomDataLoad_Click);
            // 
            // roomDataSave
            // 
            this.roomDataSave.Location = new System.Drawing.Point(18, 72);
            this.roomDataSave.Name = "roomDataSave";
            this.roomDataSave.Size = new System.Drawing.Size(85, 24);
            this.roomDataSave.TabIndex = 17;
            this.roomDataSave.Text = "Data Save";
            this.roomDataSave.UseVisualStyleBackColor = true;
            this.roomDataSave.Click += new System.EventHandler(this.roomDataSave_Click);
            // 
            // raumModule
            // 
            this.raumModule.Controls.Add(this.label3);
            this.raumModule.Controls.Add(this.moduleSoll);
            this.raumModule.Controls.Add(this.label2);
            this.raumModule.Controls.Add(this.label1);
            this.raumModule.Controls.Add(this.moduleID);
            this.raumModule.Controls.Add(this.moduleAnlernen);
            this.raumModule.Controls.Add(this.moduleIst);
            this.raumModule.Location = new System.Drawing.Point(219, 19);
            this.raumModule.Name = "raumModule";
            this.raumModule.Size = new System.Drawing.Size(95, 156);
            this.raumModule.TabIndex = 16;
            this.raumModule.TabStop = false;
            this.raumModule.Text = "Modul";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Soll-Wert";
            // 
            // moduleSoll
            // 
            this.moduleSoll.Location = new System.Drawing.Point(10, 70);
            this.moduleSoll.Name = "moduleSoll";
            this.moduleSoll.ReadOnly = true;
            this.moduleSoll.Size = new System.Drawing.Size(78, 20);
            this.moduleSoll.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Modul ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ist-Wert";
            // 
            // moduleID
            // 
            this.moduleID.Location = new System.Drawing.Point(9, 105);
            this.moduleID.Name = "moduleID";
            this.moduleID.ReadOnly = true;
            this.moduleID.Size = new System.Drawing.Size(79, 20);
            this.moduleID.TabIndex = 2;
            // 
            // moduleAnlernen
            // 
            this.moduleAnlernen.Location = new System.Drawing.Point(14, 128);
            this.moduleAnlernen.Name = "moduleAnlernen";
            this.moduleAnlernen.Size = new System.Drawing.Size(64, 20);
            this.moduleAnlernen.TabIndex = 1;
            this.moduleAnlernen.Text = "Anlernen";
            this.moduleAnlernen.UseVisualStyleBackColor = true;
            this.moduleAnlernen.Click += new System.EventHandler(this.moduleAnlernen_Click);
            // 
            // moduleIst
            // 
            this.moduleIst.Location = new System.Drawing.Point(9, 35);
            this.moduleIst.Name = "moduleIst";
            this.moduleIst.ReadOnly = true;
            this.moduleIst.Size = new System.Drawing.Size(79, 20);
            this.moduleIst.TabIndex = 0;
            this.moduleIst.TextChanged += new System.EventHandler(this.moduleIst_TextChanged);
            // 
            // showGraph
            // 
            this.showGraph.Location = new System.Drawing.Point(119, 36);
            this.showGraph.Name = "showGraph";
            this.showGraph.Size = new System.Drawing.Size(75, 23);
            this.showGraph.TabIndex = 15;
            this.showGraph.Text = "Sollprofil";
            this.showGraph.UseVisualStyleBackColor = true;
            this.showGraph.Click += new System.EventHandler(this.showGraph_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackgroundImage = global::DynAbg.Properties.Resources.delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.Location = new System.Drawing.Point(953, 9);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(18, 18);
            this.buttonDelete.TabIndex = 13;
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddHeater
            // 
            this.buttonAddHeater.Location = new System.Drawing.Point(18, 136);
            this.buttonAddHeater.Name = "buttonAddHeater";
            this.buttonAddHeater.Size = new System.Drawing.Size(176, 23);
            this.buttonAddHeater.TabIndex = 11;
            this.buttonAddHeater.Text = "Ventil hinzufügen";
            this.buttonAddHeater.UseVisualStyleBackColor = true;
            this.buttonAddHeater.Click += new System.EventHandler(this.buttonAddHeater_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.BackgroundImage = global::DynAbg.Properties.Resources.copy;
            this.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCopy.Location = new System.Drawing.Point(953, 27);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(18, 18);
            this.buttonCopy.TabIndex = 14;
            this.buttonCopy.UseVisualStyleBackColor = false;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(320, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 159);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ventile";
            // 
            // groupBoxName
            // 
            this.groupBoxName.Controls.Add(this.textBoxName);
            this.groupBoxName.Location = new System.Drawing.Point(12, 19);
            this.groupBoxName.Name = "groupBoxName";
            this.groupBoxName.Size = new System.Drawing.Size(97, 45);
            this.groupBoxName.TabIndex = 0;
            this.groupBoxName.TabStop = false;
            this.groupBoxName.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxName.Location = new System.Drawing.Point(6, 19);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(85, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // pictureBoxResize
            // 
            this.pictureBoxResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxResize.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.pictureBoxResize.Image = global::DynAbg.Properties.Resources.SizeGrip;
            this.pictureBoxResize.Location = new System.Drawing.Point(960, 168);
            this.pictureBoxResize.Name = "pictureBoxResize";
            this.pictureBoxResize.Size = new System.Drawing.Size(12, 12);
            this.pictureBoxResize.TabIndex = 10;
            this.pictureBoxResize.TabStop = false;
            this.pictureBoxResize.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxResize_MouseDoubleClick);
            this.pictureBoxResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxResize_MouseDown);
            this.pictureBoxResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxResize_MouseMove);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FilterIndex = 0;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.FilterIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(621, 140);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Layout += new System.Windows.Forms.LayoutEventHandler(this.flowLayoutPanel1_Layout);
            // 
            // RoomPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxTitle);
            this.MinimumSize = new System.Drawing.Size(951, 113);
            this.Name = "RoomPanel";
            this.Size = new System.Drawing.Size(973, 181);
            this.SizeChanged += new System.EventHandler(this.RoomPanel_SizeChanged);
            this.groupBoxTitle.ResumeLayout(false);
            this.Sollwert.ResumeLayout(false);
            this.Sollwert.PerformLayout();
            this.raumModule.ResumeLayout(false);
            this.raumModule.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBoxName.ResumeLayout(false);
            this.groupBoxName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResize)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        public System.Windows.Forms.TextBox textBoxName;
        public System.Windows.Forms.GroupBox groupBoxTitle;
        public System.Windows.Forms.GroupBox groupBoxName;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.PictureBox pictureBoxResize;
        public System.Windows.Forms.Button buttonAddHeater;
        public System.Windows.Forms.Button buttonCopy;
        public System.Windows.Forms.Button buttonDelete;
        public System.Windows.Forms.Button showGraph;
        public System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.SaveFileDialog saveFileDialog;
        public System.Windows.Forms.GroupBox raumModule;
        public System.Windows.Forms.Button moduleAnlernen;
        public System.Windows.Forms.TextBox moduleIst;
        public System.Windows.Forms.TextBox moduleID;
        public System.Windows.Forms.Button roomDataLoad;
        public System.Windows.Forms.Button roomDataSave;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox moduleSoll;
        public System.Windows.Forms.GroupBox Sollwert;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
