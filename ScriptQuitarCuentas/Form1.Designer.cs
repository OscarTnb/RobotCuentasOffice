namespace ScriptQuitarCuentas
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Licencias = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Licencias
            // 
            this.Licencias.FormattingEnabled = true;
            this.Licencias.Location = new System.Drawing.Point(1, 2);
            this.Licencias.Name = "Licencias";
            this.Licencias.Size = new System.Drawing.Size(250, 69);
            this.Licencias.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 75);
            this.Controls.Add(this.Licencias);
            this.Name = "Form2";
            this.Text = "RemoveLics";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Licencias;
    }
}