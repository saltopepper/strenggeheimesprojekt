namespace DynAbg.gui.control
{
    partial class HeaterPanelSimplified
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
            this.ventilNummer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ventilIDBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hubSollBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.hubIstBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tempBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.modusBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ventilNummer
            // 
            this.ventilNummer.AutoSize = true;
            this.ventilNummer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ventilNummer.Location = new System.Drawing.Point(-5, 26);
            this.ventilNummer.Name = "ventilNummer";
            this.ventilNummer.Size = new System.Drawing.Size(131, 24);
            this.ventilNummer.TabIndex = 0;
            this.ventilNummer.Text = "VentilNummer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ventilIDBox);
            this.groupBox1.Location = new System.Drawing.Point(95, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 63);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ventil ID";
            // 
            // ventilIDBox
            // 
            this.ventilIDBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ventilIDBox.Location = new System.Drawing.Point(7, 20);
            this.ventilIDBox.Name = "ventilIDBox";
            this.ventilIDBox.Size = new System.Drawing.Size(79, 31);
            this.ventilIDBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.hubSollBox);
            this.groupBox2.Location = new System.Drawing.Point(194, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(62, 63);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hub Soll";
            // 
            // hubSollBox
            // 
            this.hubSollBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hubSollBox.Location = new System.Drawing.Point(7, 20);
            this.hubSollBox.Name = "hubSollBox";
            this.hubSollBox.Size = new System.Drawing.Size(49, 31);
            this.hubSollBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.hubIstBox);
            this.groupBox3.Location = new System.Drawing.Point(262, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(62, 63);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hub Ist";
            // 
            // hubIstBox
            // 
            this.hubIstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hubIstBox.Location = new System.Drawing.Point(7, 20);
            this.hubIstBox.Name = "hubIstBox";
            this.hubIstBox.Size = new System.Drawing.Size(49, 31);
            this.hubIstBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tempBox);
            this.groupBox4.Location = new System.Drawing.Point(330, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(62, 63);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Temp";
            // 
            // tempBox
            // 
            this.tempBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tempBox.Location = new System.Drawing.Point(7, 20);
            this.tempBox.Name = "tempBox";
            this.tempBox.Size = new System.Drawing.Size(49, 31);
            this.tempBox.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.modusBox);
            this.groupBox5.Location = new System.Drawing.Point(398, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(62, 63);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Modus";
            // 
            // modusBox
            // 
            this.modusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modusBox.Location = new System.Drawing.Point(7, 20);
            this.modusBox.Name = "modusBox";
            this.modusBox.Size = new System.Drawing.Size(49, 31);
            this.modusBox.TabIndex = 0;
            // 
            // HeaterPanelSimplified
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ventilNummer);
            this.Name = "HeaterPanelSimplified";
            this.Size = new System.Drawing.Size(516, 142);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ventilIDBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox hubSollBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox hubIstBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tempBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox modusBox;
        public System.Windows.Forms.Label ventilNummer;
    }
}
