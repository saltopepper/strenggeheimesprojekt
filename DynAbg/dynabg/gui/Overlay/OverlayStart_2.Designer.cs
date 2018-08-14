namespace DynAbg.gui.Overlay
{
    partial class OverlayStart_2
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
            this.neuButton = new System.Windows.Forms.Button();
            this.ladenButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // neuButton
            // 
            this.neuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.neuButton.Location = new System.Drawing.Point(39, 69);
            this.neuButton.Name = "neuButton";
            this.neuButton.Size = new System.Drawing.Size(101, 90);
            this.neuButton.TabIndex = 0;
            this.neuButton.Text = "Neu";
            this.neuButton.UseVisualStyleBackColor = true;
            // 
            // ladenButton
            // 
            this.ladenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ladenButton.Location = new System.Drawing.Point(173, 69);
            this.ladenButton.Name = "ladenButton";
            this.ladenButton.Size = new System.Drawing.Size(101, 90);
            this.ladenButton.TabIndex = 1;
            this.ladenButton.Text = "Laden";
            this.ladenButton.UseVisualStyleBackColor = true;
            // 
            // restoreButton
            // 
            this.restoreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreButton.Location = new System.Drawing.Point(309, 69);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(101, 90);
            this.restoreButton.TabIndex = 2;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            // 
            // OverlayStart_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.ladenButton);
            this.Controls.Add(this.neuButton);
            this.Name = "OverlayStart_2";
            this.Size = new System.Drawing.Size(447, 258);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button neuButton;
        private System.Windows.Forms.Button ladenButton;
        private System.Windows.Forms.Button restoreButton;
    }
}
