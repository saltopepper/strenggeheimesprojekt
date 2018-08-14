namespace DynAbg.gui
{
    partial class RoomPanelSimplified
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
            this.roomNumber = new System.Windows.Forms.GroupBox();
            this.sollSollBox = new System.Windows.Forms.GroupBox();
            this.sollSoll = new System.Windows.Forms.TextBox();
            this.ModuleIDBox = new System.Windows.Forms.GroupBox();
            this.moduleID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sollButton = new System.Windows.Forms.Button();
            this.sollGroup = new System.Windows.Forms.GroupBox();
            this.sollModule = new System.Windows.Forms.TextBox();
            this.istGroup = new System.Windows.Forms.GroupBox();
            this.istModule = new System.Windows.Forms.TextBox();
            this.roomName = new System.Windows.Forms.GroupBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.roomNumber.SuspendLayout();
            this.sollSollBox.SuspendLayout();
            this.ModuleIDBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.sollGroup.SuspendLayout();
            this.istGroup.SuspendLayout();
            this.roomName.SuspendLayout();
            this.SuspendLayout();
            // 
            // roomNumber
            // 
            this.roomNumber.AutoSize = true;
            this.roomNumber.Controls.Add(this.sollSollBox);
            this.roomNumber.Controls.Add(this.ModuleIDBox);
            this.roomNumber.Controls.Add(this.groupBox1);
            this.roomNumber.Controls.Add(this.sollButton);
            this.roomNumber.Controls.Add(this.sollGroup);
            this.roomNumber.Controls.Add(this.istGroup);
            this.roomNumber.Controls.Add(this.roomName);
            this.roomNumber.Location = new System.Drawing.Point(4, 4);
            this.roomNumber.Name = "roomNumber";
            this.roomNumber.Size = new System.Drawing.Size(694, 244);
            this.roomNumber.TabIndex = 0;
            this.roomNumber.TabStop = false;
            this.roomNumber.Text = "Raum: ";
            // 
            // sollSollBox
            // 
            this.sollSollBox.Controls.Add(this.sollSoll);
            this.sollSollBox.Location = new System.Drawing.Point(48, 159);
            this.sollSollBox.Name = "sollSollBox";
            this.sollSollBox.Size = new System.Drawing.Size(85, 64);
            this.sollSollBox.TabIndex = 3;
            this.sollSollBox.TabStop = false;
            this.sollSollBox.Text = "Sollprofil Soll";
            // 
            // sollSoll
            // 
            this.sollSoll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sollSoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sollSoll.Location = new System.Drawing.Point(3, 16);
            this.sollSoll.Name = "sollSoll";
            this.sollSoll.Size = new System.Drawing.Size(79, 31);
            this.sollSoll.TabIndex = 0;
            // 
            // ModuleIDBox
            // 
            this.ModuleIDBox.Controls.Add(this.moduleID);
            this.ModuleIDBox.Location = new System.Drawing.Point(200, 19);
            this.ModuleIDBox.Name = "ModuleIDBox";
            this.ModuleIDBox.Size = new System.Drawing.Size(85, 64);
            this.ModuleIDBox.TabIndex = 2;
            this.ModuleIDBox.TabStop = false;
            this.ModuleIDBox.Text = "Module ID";
            // 
            // moduleID
            // 
            this.moduleID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moduleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moduleID.Location = new System.Drawing.Point(3, 16);
            this.moduleID.Name = "moduleID";
            this.moduleID.Size = new System.Drawing.Size(79, 31);
            this.moduleID.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(315, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 209);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ventile";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 168);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sollButton
            // 
            this.sollButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sollButton.Location = new System.Drawing.Point(38, 89);
            this.sollButton.Name = "sollButton";
            this.sollButton.Size = new System.Drawing.Size(105, 60);
            this.sollButton.TabIndex = 3;
            this.sollButton.Text = "Sollprofil";
            this.sollButton.UseVisualStyleBackColor = true;
            this.sollButton.Click += new System.EventHandler(this.sollButton_Click);
            // 
            // sollGroup
            // 
            this.sollGroup.Controls.Add(this.sollModule);
            this.sollGroup.Location = new System.Drawing.Point(200, 159);
            this.sollGroup.Name = "sollGroup";
            this.sollGroup.Size = new System.Drawing.Size(85, 64);
            this.sollGroup.TabIndex = 2;
            this.sollGroup.TabStop = false;
            this.sollGroup.Text = "Soll-Wert";
            // 
            // sollModule
            // 
            this.sollModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sollModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sollModule.Location = new System.Drawing.Point(3, 16);
            this.sollModule.Name = "sollModule";
            this.sollModule.Size = new System.Drawing.Size(79, 31);
            this.sollModule.TabIndex = 0;
            // 
            // istGroup
            // 
            this.istGroup.Controls.Add(this.istModule);
            this.istGroup.Location = new System.Drawing.Point(200, 89);
            this.istGroup.Name = "istGroup";
            this.istGroup.Size = new System.Drawing.Size(85, 64);
            this.istGroup.TabIndex = 1;
            this.istGroup.TabStop = false;
            this.istGroup.Text = "Ist-Wert";
            // 
            // istModule
            // 
            this.istModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.istModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.istModule.Location = new System.Drawing.Point(3, 16);
            this.istModule.Name = "istModule";
            this.istModule.Size = new System.Drawing.Size(79, 31);
            this.istModule.TabIndex = 0;
            // 
            // roomName
            // 
            this.roomName.Controls.Add(this.nameBox);
            this.roomName.Location = new System.Drawing.Point(32, 19);
            this.roomName.Name = "roomName";
            this.roomName.Size = new System.Drawing.Size(121, 65);
            this.roomName.TabIndex = 0;
            this.roomName.TabStop = false;
            this.roomName.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(6, 19);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(109, 31);
            this.nameBox.TabIndex = 0;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // RoomPanelSimplified
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.roomNumber);
            this.Name = "RoomPanelSimplified";
            this.Size = new System.Drawing.Size(710, 260);
            this.roomNumber.ResumeLayout(false);
            this.roomNumber.PerformLayout();
            this.sollSollBox.ResumeLayout(false);
            this.sollSollBox.PerformLayout();
            this.ModuleIDBox.ResumeLayout(false);
            this.ModuleIDBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.sollGroup.ResumeLayout(false);
            this.sollGroup.PerformLayout();
            this.istGroup.ResumeLayout(false);
            this.istGroup.PerformLayout();
            this.roomName.ResumeLayout(false);
            this.roomName.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox sollSollBox;
        public System.Windows.Forms.TextBox sollSoll;
        public System.Windows.Forms.GroupBox ModuleIDBox;
        public System.Windows.Forms.TextBox moduleID;
        public System.Windows.Forms.GroupBox roomNumber;
        public System.Windows.Forms.GroupBox roomName;
        public System.Windows.Forms.TextBox nameBox;
        public System.Windows.Forms.Button sollButton;
        public System.Windows.Forms.GroupBox sollGroup;
        public System.Windows.Forms.TextBox sollModule;
        public System.Windows.Forms.GroupBox istGroup;
        public System.Windows.Forms.TextBox istModule;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}
