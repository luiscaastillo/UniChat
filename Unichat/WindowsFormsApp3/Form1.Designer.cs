namespace Unichat
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.labelContra = new System.Windows.Forms.Label();
            this.labelCuenta = new System.Windows.Forms.Label();
            this.linkRegistrarse = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Bconectar = new System.Windows.Forms.PictureBox();
            this.textBoxContra = new System.Windows.Forms.TextBox();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.BBconectar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bconectar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "¡Bienvenido a UniChat!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsuario.Location = new System.Drawing.Point(59, 120);
            this.labelUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(115, 31);
            this.labelUsuario.TabIndex = 3;
            this.labelUsuario.Text = "Usuario";
            // 
            // labelContra
            // 
            this.labelContra.AutoSize = true;
            this.labelContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContra.Location = new System.Drawing.Point(59, 217);
            this.labelContra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelContra.Name = "labelContra";
            this.labelContra.Size = new System.Drawing.Size(165, 31);
            this.labelContra.TabIndex = 5;
            this.labelContra.Text = "Contraseña";
            this.labelContra.Click += new System.EventHandler(this.label3_Click);
            // 
            // labelCuenta
            // 
            this.labelCuenta.AutoSize = true;
            this.labelCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCuenta.Location = new System.Drawing.Point(133, 451);
            this.labelCuenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCuenta.Name = "labelCuenta";
            this.labelCuenta.Size = new System.Drawing.Size(183, 17);
            this.labelCuenta.TabIndex = 7;
            this.labelCuenta.Text = "¿Necesitas una cuenta?";
            this.labelCuenta.Click += new System.EventHandler(this.label4_Click);
            // 
            // linkRegistrarse
            // 
            this.linkRegistrarse.AutoSize = true;
            this.linkRegistrarse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRegistrarse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.linkRegistrarse.Location = new System.Drawing.Point(322, 451);
            this.linkRegistrarse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkRegistrarse.Name = "linkRegistrarse";
            this.linkRegistrarse.Size = new System.Drawing.Size(95, 18);
            this.linkRegistrarse.TabIndex = 8;
            this.linkRegistrarse.TabStop = true;
            this.linkRegistrarse.Text = "Registrarse";
            this.linkRegistrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Bconectar);
            this.panel1.Controls.Add(this.BBconectar);
            this.panel1.Controls.Add(this.labelUsuario);
            this.panel1.Controls.Add(this.labelContra);
            this.panel1.Controls.Add(this.textBoxContra);
            this.panel1.Controls.Add(this.textBoxUsuario);
            this.panel1.Controls.Add(this.linkRegistrarse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelCuenta);
            this.panel1.Location = new System.Drawing.Point(107, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 555);
            this.panel1.TabIndex = 9;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Bconectar
            // 
            this.Bconectar.Location = new System.Drawing.Point(174, 341);
            this.Bconectar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Bconectar.Name = "Bconectar";
            this.Bconectar.Size = new System.Drawing.Size(203, 75);
            this.Bconectar.TabIndex = 11;
            this.Bconectar.TabStop = false;
            this.Bconectar.Click += new System.EventHandler(this.Bconectar_Click_1);
            // 
            // textBoxContra
            // 
            this.textBoxContra.Location = new System.Drawing.Point(104, 265);
            this.textBoxContra.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxContra.Name = "textBoxContra";
            this.textBoxContra.Size = new System.Drawing.Size(372, 22);
            this.textBoxContra.TabIndex = 10;
            this.textBoxContra.Text = "Ingresa la contraseña";
            this.textBoxContra.TextChanged += new System.EventHandler(this.textBoxContra_TextChanged);
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsuario.Location = new System.Drawing.Point(104, 171);
            this.textBoxUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(372, 23);
            this.textBoxUsuario.TabIndex = 9;
            this.textBoxUsuario.Text = "Ingresa tu usuario";
            this.textBoxUsuario.TextChanged += new System.EventHandler(this.textBoxUsuario_TextChanged);
            // 
            // BBconectar
            // 
            this.BBconectar.Location = new System.Drawing.Point(163, 331);
            this.BBconectar.Name = "BBconectar";
            this.BBconectar.Size = new System.Drawing.Size(224, 95);
            this.BBconectar.TabIndex = 12;
            this.BBconectar.Text = "button1";
            this.BBconectar.UseVisualStyleBackColor = true;
            this.BBconectar.Click += new System.EventHandler(this.BBconectar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 674);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "UniChat LogIn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bconectar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Label labelContra;
        private System.Windows.Forms.Label labelCuenta;
        private System.Windows.Forms.LinkLabel linkRegistrarse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxUsuario;
        private System.Windows.Forms.TextBox textBoxContra;
        private System.Windows.Forms.PictureBox Bconectar;
        private System.Windows.Forms.Button BBconectar;
    }
}

