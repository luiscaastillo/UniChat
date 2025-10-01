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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Bconectar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(193, 165);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(372, 132);
            this.listBox1.TabIndex = 0;
            // 
            // Bconectar
            // 
            this.Bconectar.Location = new System.Drawing.Point(256, 61);
            this.Bconectar.Name = "Bconectar";
            this.Bconectar.Size = new System.Drawing.Size(269, 56);
            this.Bconectar.TabIndex = 1;
            this.Bconectar.Text = "Conectar";
            this.Bconectar.UseVisualStyleBackColor = true;
            this.Bconectar.Click += new System.EventHandler(this.Bconectar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Bconectar);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Conexion a Base de datos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Bconectar;
    }
}

