namespace DynAbg.gui.Overlay
{
    partial class OverlayStart_3_Raum
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
            this.amountBox = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.subButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.roomCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.roomCount)).BeginInit();
            this.SuspendLayout();
            // 
            // amountBox
            // 
            this.amountBox.Location = new System.Drawing.Point(27, 112);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(101, 80);
            this.amountBox.TabIndex = 0;
            this.amountBox.Text = "# Räume";
            this.amountBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(171, 88);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(103, 78);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // subButton
            // 
            this.subButton.Location = new System.Drawing.Point(172, 172);
            this.subButton.Name = "subButton";
            this.subButton.Size = new System.Drawing.Size(101, 78);
            this.subButton.TabIndex = 2;
            this.subButton.Text = "-";
            this.subButton.UseVisualStyleBackColor = true;
            this.subButton.Click += new System.EventHandler(this.subButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(281, 131);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(147, 78);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // roomCount
            // 
            this.roomCount.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.roomCount.Location = new System.Drawing.Point(30, 172);
            this.roomCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.roomCount.Name = "roomCount";
            this.roomCount.Size = new System.Drawing.Size(120, 20);
            this.roomCount.TabIndex = 4;
            this.roomCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // OverlayStart_3_Raum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roomCount);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.subButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.amountBox);
            this.Name = "OverlayStart_3_Raum";
            this.Size = new System.Drawing.Size(516, 351);
            ((System.ComponentModel.ISupportInitialize)(this.roomCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label amountBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button subButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.NumericUpDown roomCount;
    }
}
