namespace DynAbg.gui.Overlay
{
    partial class OverlayStart_4_Ventile
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
            this.nextButton = new System.Windows.Forms.Button();
            this.subButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.amountBox = new System.Windows.Forms.Label();
            this.roomBox = new System.Windows.Forms.Label();
            this.roomNumber = new System.Windows.Forms.NumericUpDown();
            this.anzahlVentile = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.roomNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anzahlVentile)).BeginInit();
            this.SuspendLayout();
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(315, 172);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(147, 78);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // subButton
            // 
            this.subButton.Location = new System.Drawing.Point(206, 213);
            this.subButton.Name = "subButton";
            this.subButton.Size = new System.Drawing.Size(101, 78);
            this.subButton.TabIndex = 6;
            this.subButton.Text = "-";
            this.subButton.UseVisualStyleBackColor = true;
            this.subButton.Click += new System.EventHandler(this.subButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(205, 129);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(103, 78);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // amountBox
            // 
            this.amountBox.Location = new System.Drawing.Point(64, 153);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(101, 80);
            this.amountBox.TabIndex = 4;
            this.amountBox.Text = "#Ventile";
            this.amountBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.amountBox.Click += new System.EventHandler(this.amountBox_Click);
            // 
            // roomBox
            // 
            this.roomBox.Location = new System.Drawing.Point(28, 28);
            this.roomBox.Name = "roomBox";
            this.roomBox.Size = new System.Drawing.Size(100, 23);
            this.roomBox.TabIndex = 8;
            this.roomBox.Text = "Room Name";
            // 
            // roomNumber
            // 
            this.roomNumber.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.roomNumber.Location = new System.Drawing.Point(31, 44);
            this.roomNumber.Name = "roomNumber";
            this.roomNumber.Size = new System.Drawing.Size(120, 20);
            this.roomNumber.TabIndex = 9;
            // 
            // anzahlVentile
            // 
            this.anzahlVentile.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.anzahlVentile.Location = new System.Drawing.Point(67, 213);
            this.anzahlVentile.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.anzahlVentile.Name = "anzahlVentile";
            this.anzahlVentile.Size = new System.Drawing.Size(120, 20);
            this.anzahlVentile.TabIndex = 10;
            this.anzahlVentile.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // OverlayStart_4_Ventile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.anzahlVentile);
            this.Controls.Add(this.roomNumber);
            this.Controls.Add(this.roomBox);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.subButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.amountBox);
            this.Name = "OverlayStart_4_Ventile";
            this.Size = new System.Drawing.Size(560, 420);
            ((System.ComponentModel.ISupportInitialize)(this.roomNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anzahlVentile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button subButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label amountBox;
        private System.Windows.Forms.Label roomBox;
        private System.Windows.Forms.NumericUpDown roomNumber;
        private System.Windows.Forms.NumericUpDown anzahlVentile;
    }
}
