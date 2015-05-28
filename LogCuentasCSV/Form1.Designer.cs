namespace LogCuentasCSV
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnError = new System.Windows.Forms.Button();
            this.btnCuentaIncorrecta = new System.Windows.Forms.Button();
            this.btnCuentaValida = new System.Windows.Forms.Button();
            this.btnCopiarPwd = new System.Windows.Forms.Button();
            this.btnCopiarCuenta = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblNotificaciones = new System.Windows.Forms.Label();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(366, 173);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(75, 23);
            this.btnError.TabIndex = 29;
            this.btnError.Text = "&Error";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // btnCuentaIncorrecta
            // 
            this.btnCuentaIncorrecta.Location = new System.Drawing.Point(188, 173);
            this.btnCuentaIncorrecta.Name = "btnCuentaIncorrecta";
            this.btnCuentaIncorrecta.Size = new System.Drawing.Size(107, 23);
            this.btnCuentaIncorrecta.TabIndex = 28;
            this.btnCuentaIncorrecta.Text = "Cuenta &Incorrecta";
            this.btnCuentaIncorrecta.UseVisualStyleBackColor = true;
            this.btnCuentaIncorrecta.Click += new System.EventHandler(this.btnCuentaIncorrecta_Click);
            // 
            // btnCuentaValida
            // 
            this.btnCuentaValida.Location = new System.Drawing.Point(36, 173);
            this.btnCuentaValida.Name = "btnCuentaValida";
            this.btnCuentaValida.Size = new System.Drawing.Size(108, 23);
            this.btnCuentaValida.TabIndex = 27;
            this.btnCuentaValida.Text = "Cuenta &Validada";
            this.btnCuentaValida.UseVisualStyleBackColor = true;
            this.btnCuentaValida.Click += new System.EventHandler(this.btnCuentaValida_Click);
            // 
            // btnCopiarPwd
            // 
            this.btnCopiarPwd.Location = new System.Drawing.Point(390, 128);
            this.btnCopiarPwd.Name = "btnCopiarPwd";
            this.btnCopiarPwd.Size = new System.Drawing.Size(75, 23);
            this.btnCopiarPwd.TabIndex = 26;
            this.btnCopiarPwd.Text = "Copiar &Pass";
            this.btnCopiarPwd.UseVisualStyleBackColor = true;
            this.btnCopiarPwd.Click += new System.EventHandler(this.btnCopiarPwd_Click);
            // 
            // btnCopiarCuenta
            // 
            this.btnCopiarCuenta.Location = new System.Drawing.Point(390, 65);
            this.btnCopiarCuenta.Name = "btnCopiarCuenta";
            this.btnCopiarCuenta.Size = new System.Drawing.Size(75, 23);
            this.btnCopiarCuenta.TabIndex = 25;
            this.btnCopiarCuenta.Text = "Copiar &User";
            this.btnCopiarCuenta.UseVisualStyleBackColor = true;
            this.btnCopiarCuenta.Click += new System.EventHandler(this.btnCopiarUser_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Enabled = false;
            this.txtPwd.Location = new System.Drawing.Point(17, 131);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.ReadOnly = true;
            this.txtPwd.Size = new System.Drawing.Size(367, 20);
            this.txtPwd.TabIndex = 24;
            // 
            // txtCuenta
            // 
            this.txtCuenta.Enabled = false;
            this.txtCuenta.Location = new System.Drawing.Point(17, 67);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.ReadOnly = true;
            this.txtCuenta.Size = new System.Drawing.Size(367, 20);
            this.txtCuenta.TabIndex = 23;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(17, 115);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(61, 13);
            this.Label3.TabIndex = 22;
            this.Label3.Text = "Contraseña";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(14, 51);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(43, 13);
            this.Label2.TabIndex = 21;
            this.Label2.Text = "Usuario";
            // 
            // lblNotificaciones
            // 
            this.lblNotificaciones.AutoSize = true;
            this.lblNotificaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificaciones.Location = new System.Drawing.Point(12, 9);
            this.lblNotificaciones.Name = "lblNotificaciones";
            this.lblNotificaciones.Size = new System.Drawing.Size(122, 20);
            this.lblNotificaciones.TabIndex = 20;
            this.lblNotificaciones.Text = "Notificaciones";
            this.lblNotificaciones.Click += new System.EventHandler(this.lblNotificaciones_Click);
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Location = new System.Drawing.Point(12, 227);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(51, 13);
            this.lblRegistros.TabIndex = 30;
            this.lblRegistros.Text = "Registros";
            this.lblRegistros.Click += new System.EventHandler(this.lblRegistros_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 249);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnCuentaIncorrecta);
            this.Controls.Add(this.btnCuentaValida);
            this.Controls.Add(this.btnCopiarPwd);
            this.Controls.Add(this.btnCopiarCuenta);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lblNotificaciones);
            this.Name = "Form1";
            this.Text = "Log Cuentas CSV";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnError;
        internal System.Windows.Forms.Button btnCuentaIncorrecta;
        internal System.Windows.Forms.Button btnCuentaValida;
        internal System.Windows.Forms.Button btnCopiarPwd;
        internal System.Windows.Forms.Button btnCopiarCuenta;
        internal System.Windows.Forms.TextBox txtPwd;
        internal System.Windows.Forms.TextBox txtCuenta;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lblNotificaciones;
        private System.Windows.Forms.Label lblRegistros;
    }
}

