namespace DynAbg.gui.Overlay
{
    partial class OverlayStart_6_AnlernenModule
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
            this.anlernenButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // anlernenButton
            // 
            this.anlernenButton.Location = new System.Drawing.Point(172, 169);
            this.anlernenButton.Name = "anlernenButton";
            this.anlernenButton.Size = new System.Drawing.Size(229, 87);
            this.anlernenButton.TabIndex = 3;
            this.anlernenButton.Text = "Anlernen";
            this.anlernenButton.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(169, 91);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(232, 23);
            this.textBox.TabIndex = 2;
            this.textBox.Text = "Module Anlernen";
            // 
            // OverlayStart_6_AnlernenModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.anlernenButton);
            this.Controls.Add(this.textBox);
            this.Name = "OverlayStart_6_AnlernenModule";
            this.Size = new System.Drawing.Size(571, 347);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label textBox;
        public System.Windows.Forms.Button anlernenButton;
    }
}
