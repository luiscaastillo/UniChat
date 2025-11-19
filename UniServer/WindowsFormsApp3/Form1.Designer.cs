namespace Unichat
{
    partial class FormLogIn
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
            System.Windows.Forms.Label labelUsuario;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogIn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Bconectar = new System.Windows.Forms.PictureBox();
            this.BBconectar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            labelUsuario = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bconectar)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUsuario
            // 
            labelUsuario.Font = new System.Drawing.Font("Monaspace Neon NF SemiWide", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelUsuario.ForeColor = System.Drawing.SystemColors.ButtonFace;
            labelUsuario.Location = new System.Drawing.Point(104, 155);
            labelUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelUsuario.Name = "labelUsuario";
            labelUsuario.Size = new System.Drawing.Size(526, 92);
            labelUsuario.TabIndex = 3;
            labelUsuario.Text = "Encender Servidor";
            labelUsuario.Click += new System.EventHandler(this.labelUsuario_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Bconectar);
            this.panel1.Controls.Add(this.BBconectar);
            this.panel1.Controls.Add(labelUsuario);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(50, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.panel1.Size = new System.Drawing.Size(701, 521);
            this.panel1.TabIndex = 9;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Bconectar
            // 
            this.Bconectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Bconectar.Location = new System.Drawing.Point(200, 309);
            this.Bconectar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Bconectar.MaximumSize = new System.Drawing.Size(280, 110);
            this.Bconectar.Name = "Bconectar";
            this.Bconectar.Size = new System.Drawing.Size(280, 110);
            this.Bconectar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Bconectar.TabIndex = 11;
            this.Bconectar.TabStop = false;
            this.Bconectar.Click += new System.EventHandler(this.Bconectar_Click_1);
            // 
            // BBconectar
            // 
            this.BBconectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BBconectar.Location = new System.Drawing.Point(131, 309);
            this.BBconectar.Name = "BBconectar";
            this.BBconectar.Size = new System.Drawing.Size(414, 110);
            this.BBconectar.TabIndex = 12;
            this.BBconectar.Text = "button1";
            this.BBconectar.UseVisualStyleBackColor = true;
            this.BBconectar.Click += new System.EventHandler(this.BBconectar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 39);
            this.label1.TabIndex = 2;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 593);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormLogIn";
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bconectar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Bconectar;
        private System.Windows.Forms.Button BBconectar;
        private System.Windows.Forms.Label label1;
    }
}

