using DynAbg.Properties;
namespace DynAbg
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.aufheizNumeric = new System.Windows.Forms.NumericUpDown();
            this.updateProfilButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.intervallStart = new System.Windows.Forms.NumericUpDown();
            this.saveValveData = new System.Windows.Forms.Button();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxID = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonInitTCM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.TypeSendReceive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnOceanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.simplifyCheck = new System.Windows.Forms.CheckBox();
            this.manualSoll = new System.Windows.Forms.CheckBox();
            this.loadOption = new System.Windows.Forms.Button();
            this.saveOption = new System.Windows.Forms.Button();
            this.midnightRecalibration = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aufheizNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervallStart)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 67);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(993, 743);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.aufheizNumeric);
            this.tabPage1.Controls.Add(this.updateProfilButton);
            this.tabPage1.Controls.Add(this.restoreButton);
            this.tabPage1.Controls.Add(this.startButton);
            this.tabPage1.Controls.Add(this.intervallStart);
            this.tabPage1.Controls.Add(this.saveValveData);
            this.tabPage1.Controls.Add(this.toolStrip);
            this.tabPage1.Controls.Add(this.statusStrip);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(985, 717);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main (Advanced)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(683, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Aufheiz";
            // 
            // aufheizNumeric
            // 
            this.aufheizNumeric.Location = new System.Drawing.Point(728, 6);
            this.aufheizNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.aufheizNumeric.Name = "aufheizNumeric";
            this.aufheizNumeric.Size = new System.Drawing.Size(46, 20);
            this.aufheizNumeric.TabIndex = 22;
            // 
            // updateProfilButton
            // 
            this.updateProfilButton.Enabled = false;
            this.updateProfilButton.Location = new System.Drawing.Point(781, 6);
            this.updateProfilButton.Name = "updateProfilButton";
            this.updateProfilButton.Size = new System.Drawing.Size(81, 20);
            this.updateProfilButton.TabIndex = 21;
            this.updateProfilButton.Text = "Update Profil";
            this.updateProfilButton.UseVisualStyleBackColor = true;
            this.updateProfilButton.Click += new System.EventHandler(this.updateProfilButton_Click);
            // 
            // restoreButton
            // 
            this.restoreButton.Enabled = false;
            this.restoreButton.Location = new System.Drawing.Point(893, 6);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(86, 20);
            this.restoreButton.TabIndex = 20;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(569, 6);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(70, 20);
            this.startButton.TabIndex = 19;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // intervallStart
            // 
            this.intervallStart.Location = new System.Drawing.Point(521, 6);
            this.intervallStart.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.intervallStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.intervallStart.Name = "intervallStart";
            this.intervallStart.Size = new System.Drawing.Size(41, 20);
            this.intervallStart.TabIndex = 18;
            this.intervallStart.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // saveValveData
            // 
            this.saveValveData.Enabled = false;
            this.saveValveData.Location = new System.Drawing.Point(343, 5);
            this.saveValveData.Name = "saveValveData";
            this.saveValveData.Size = new System.Drawing.Size(145, 22);
            this.saveValveData.TabIndex = 17;
            this.saveValveData.Text = "Ordnerwahl fuer Ergebnisse";
            this.saveValveData.UseVisualStyleBackColor = true;
            this.saveValveData.Click += new System.EventHandler(this.saveValveData_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripSeparator1,
            this.toolStripButtonRoom,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStripTextBoxID,
            this.toolStripButtonInitTCM,
            this.toolStripSeparator5,
            this.toolStripButtonInfo,
            this.toolStripSeparator2});
            this.toolStrip.Location = new System.Drawing.Point(3, 3);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(979, 25);
            this.toolStrip.TabIndex = 16;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::DynAbg.Properties.Resources.NewDocument;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.ToolTipText = "Neues Projekt";
            this.toolStripButtonNew.Click += new System.EventHandler(this.new_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::DynAbg.Properties.Resources.openfolder_24;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "toolSripButton1";
            this.toolStripButtonOpen.ToolTipText = "Projekt öffnen";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.open_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::DynAbg.Properties.Resources.Save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.ToolTipText = "Projekt speichern";
            this.toolStripButtonSave.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRoom
            // 
            this.toolStripButtonRoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRoom.Image = global::DynAbg.Properties.Resources.room;
            this.toolStripButtonRoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRoom.Name = "toolStripButtonRoom";
            this.toolStripButtonRoom.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRoom.ToolTipText = "Neuen Raum hinzufügen";
            this.toolStripButtonRoom.Click += new System.EventHandler(this.toolStripButtonRoom_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "EnOcean ID:";
            // 
            // toolStripTextBoxID
            // 
            this.toolStripTextBoxID.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripTextBoxID.Name = "toolStripTextBoxID";
            this.toolStripTextBoxID.ReadOnly = true;
            this.toolStripTextBoxID.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxID.Text = "Nich initialisiert -->";
            // 
            // toolStripButtonInitTCM
            // 
            this.toolStripButtonInitTCM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInitTCM.Image = global::DynAbg.Properties.Resources.ic;
            this.toolStripButtonInitTCM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInitTCM.Name = "toolStripButtonInitTCM";
            this.toolStripButtonInitTCM.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInitTCM.Text = "toolStripButtonInitTCM";
            this.toolStripButtonInitTCM.ToolTipText = "TCM Modul initialisieren";
            this.toolStripButtonInitTCM.Click += new System.EventHandler(this.initTCM_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonInfo
            // 
            this.toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInfo.Image = global::DynAbg.Properties.Resources.info;
            this.toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInfo.Name = "toolStripButtonInfo";
            this.toolStripButtonInfo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInfo.ToolTipText = "Über...";
            this.toolStripButtonInfo.Click += new System.EventHandler(this.info_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(3, 692);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(979, 22);
            this.statusStrip.TabIndex = 15;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(964, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(985, 656);
            this.flowLayoutPanel1.TabIndex = 14;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel3);
            this.tabPage2.Controls.Add(this.flowLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(985, 717);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Main (Simplified)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(985, 717);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(985, 714);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(985, 717);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Datenlogger";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeSendReceive,
            this.DataGrid,
            this.EnOceanID,
            this.Time});
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(985, 717);
            this.dataGridView2.TabIndex = 0;
            // 
            // TypeSendReceive
            // 
            this.TypeSendReceive.HeaderText = "Type";
            this.TypeSendReceive.Name = "TypeSendReceive";
            this.TypeSendReceive.ReadOnly = true;
            this.TypeSendReceive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TypeSendReceive.Width = 50;
            // 
            // DataGrid
            // 
            this.DataGrid.HeaderText = "Data";
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.ReadOnly = true;
            this.DataGrid.Width = 500;
            // 
            // EnOceanID
            // 
            this.EnOceanID.HeaderText = "EnOceanID";
            this.EnOceanID.Name = "EnOceanID";
            this.EnOceanID.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 300;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.simplifyCheck);
            this.tabPage4.Controls.Add(this.manualSoll);
            this.tabPage4.Controls.Add(this.loadOption);
            this.tabPage4.Controls.Add(this.saveOption);
            this.tabPage4.Controls.Add(this.midnightRecalibration);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(985, 717);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Optionen";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // simplifyCheck
            // 
            this.simplifyCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simplifyCheck.Location = new System.Drawing.Point(129, 173);
            this.simplifyCheck.Name = "simplifyCheck";
            this.simplifyCheck.Size = new System.Drawing.Size(248, 50);
            this.simplifyCheck.TabIndex = 4;
            this.simplifyCheck.Text = "Simplified GUI";
            this.simplifyCheck.UseVisualStyleBackColor = true;
            // 
            // manualSoll
            // 
            this.manualSoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualSoll.Location = new System.Drawing.Point(129, 95);
            this.manualSoll.Name = "manualSoll";
            this.manualSoll.Size = new System.Drawing.Size(248, 50);
            this.manualSoll.TabIndex = 3;
            this.manualSoll.Text = "Manueller Sollwert";
            this.manualSoll.UseVisualStyleBackColor = true;
            // 
            // loadOption
            // 
            this.loadOption.Location = new System.Drawing.Point(547, 98);
            this.loadOption.Name = "loadOption";
            this.loadOption.Size = new System.Drawing.Size(133, 48);
            this.loadOption.TabIndex = 2;
            this.loadOption.Text = "Load";
            this.loadOption.UseVisualStyleBackColor = true;
            this.loadOption.Click += new System.EventHandler(this.loadOption_Click);
            // 
            // saveOption
            // 
            this.saveOption.Location = new System.Drawing.Point(547, 19);
            this.saveOption.Name = "saveOption";
            this.saveOption.Size = new System.Drawing.Size(133, 47);
            this.saveOption.TabIndex = 1;
            this.saveOption.Text = "Save";
            this.saveOption.UseVisualStyleBackColor = true;
            this.saveOption.Click += new System.EventHandler(this.saveOption_Click);
            // 
            // midnightRecalibration
            // 
            this.midnightRecalibration.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.midnightRecalibration.Location = new System.Drawing.Point(129, 25);
            this.midnightRecalibration.Name = "midnightRecalibration";
            this.midnightRecalibration.Size = new System.Drawing.Size(281, 50);
            this.midnightRecalibration.TabIndex = 0;
            this.midnightRecalibration.Text = "Midnight Recalibration";
            this.midnightRecalibration.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.FilterIndex = 0;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "Projektdateien (*.xml)|*.xml";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "Projektdateien (*.xml)|*.xml";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(784, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(208, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(189, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Programm";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dynamischer Abgleich";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 69);
            this.panel1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 812);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1010, 850);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dynamischer Abgleich - Neues Projekt";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aufheizNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervallStart)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown aufheizNumeric;
        private System.Windows.Forms.Button updateProfilButton;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.NumericUpDown intervallStart;
        private System.Windows.Forms.Button saveValveData;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxID;
        private System.Windows.Forms.ToolStripButton toolStripButtonInitTCM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeSendReceive;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnOceanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox manualSoll;
        private System.Windows.Forms.Button loadOption;
        private System.Windows.Forms.Button saveOption;
        private System.Windows.Forms.CheckBox midnightRecalibration;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox simplifyCheck;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    }
}

