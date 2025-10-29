namespace Unichat
{
    partial class FormNuevaSala
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
            this.textBoxSala = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCrear = new System.Windows.Forms.Label();
            this.BCrear = new System.Windows.Forms.PictureBox();
            this.panelSala = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.BCrear)).BeginInit();
            this.panelSala.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSala
            // 
            this.textBoxSala.Location = new System.Drawing.Point(156, 248);
            this.textBoxSala.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxSala.Name = "textBoxSala";
            this.textBoxSala.Size = new System.Drawing.Size(396, 31);
            this.textBoxSala.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(474, 55);
            this.label1.TabIndex = 3;
            this.label1.Text = "Creando nueva sala";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCrear
            // 
            this.labelCrear.AutoSize = true;
            this.labelCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCrear.Location = new System.Drawing.Point(126, 183);
            this.labelCrear.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelCrear.Name = "labelCrear";
            this.labelCrear.Size = new System.Drawing.Size(450, 37);
            this.labelCrear.TabIndex = 4;
            this.labelCrear.Text = "Escribe el nombre de tu sala";
            // 
            // BCrear
            // 
            this.BCrear.Location = new System.Drawing.Point(166, 275);
            this.BCrear.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.BCrear.Name = "BCrear";
            this.BCrear.Size = new System.Drawing.Size(238, 97);
            this.BCrear.TabIndex = 5;
            this.BCrear.TabStop = false;
            this.BCrear.Click += new System.EventHandler(this.BCrear_Click);
            // 
            // panelSala
            // 
            this.panelSala.Controls.Add(this.BCrear);
            this.panelSala.Controls.Add(this.label1);
            this.panelSala.Location = new System.Drawing.Point(64, 23);
            this.panelSala.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelSala.Name = "panelSala";
            this.panelSala.Size = new System.Drawing.Size(612, 406);
            this.panelSala.TabIndex = 6;
            // 
            // FormNuevaSala
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 452);
            this.Controls.Add(this.labelCrear);
            this.Controls.Add(this.textBoxSala);
            this.Controls.Add(this.panelSala);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FormNuevaSala";
            this.ShowIcon = false;
            this.Text = "Crear Sala";
            this.Load += new System.EventHandler(this.FormNuevaSala_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BCrear)).EndInit();
            this.panelSala.ResumeLayout(false);
            this.panelSala.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSala;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCrear;
        private System.Windows.Forms.PictureBox BCrear;
        private System.Windows.Forms.Panel panelSala;
    }
}