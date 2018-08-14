namespace DynAbg.gui.Overlay
{
    partial class OverlayStart_5_AnlernenTCM
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
            this.textBox = new System.Windows.Forms.Label();
            this.anlernenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(61, 40);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(232, 23);
            this.textBox.TabIndex = 0;
            this.textBox.Text = "TCM Anlernen";
            // 
            // anlernenButton
            // 
            this.anlernenButton.Location = new System.Drawing.Point(64, 118);
            this.anlernenButton.Name = "anlernenButton";
            this.anlernenButton.Size = new System.Drawing.Size(229, 87);
            this.anlernenButton.TabIndex = 1;
            this.anlernenButton.Text = "Anlernen";
            this.anlernenButton.UseVisualStyleBackColor = true;
            this.anlernenButton.Click += new System.EventHandler(this.anlernenButton_Click);
            // 
            // OverlayStart_5_AnlernenTCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.anlernenButton);
            this.Controls.Add(this.textBox);
            this.Name = "OverlayStart_5_AnlernenTCM";
            this.Size = new System.Drawing.Size(360, 276);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label textBox;
        public System.Windows.Forms.Button anlernenButton;
    }
}
