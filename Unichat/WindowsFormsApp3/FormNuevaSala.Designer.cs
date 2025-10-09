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
            this.textBoxSala.Location = new System.Drawing.Point(104, 159);
            this.textBoxSala.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSala.Name = "textBoxSala";
            this.textBoxSala.Size = new System.Drawing.Size(265, 22);
            this.textBoxSala.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("OCR A Extended", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Creando nueva sala";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCrear
            // 
            this.labelCrear.AutoSize = true;
            this.labelCrear.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCrear.Location = new System.Drawing.Point(84, 117);
            this.labelCrear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCrear.Name = "labelCrear";
            this.labelCrear.Size = new System.Drawing.Size(287, 23);
            this.labelCrear.TabIndex = 4;
            this.labelCrear.Text = "Escribe el nombre de tu sala";
            // 
            // BCrear
            // 
            this.BCrear.Location = new System.Drawing.Point(111, 176);
            this.BCrear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BCrear.Name = "BCrear";
            this.BCrear.Size = new System.Drawing.Size(159, 62);
            this.BCrear.TabIndex = 5;
            this.BCrear.TabStop = false;
            this.BCrear.Click += new System.EventHandler(this.BCrear_Click);
            // 
            // panelSala
            // 
            this.panelSala.Controls.Add(this.BCrear);
            this.panelSala.Controls.Add(this.label1);
            this.panelSala.Location = new System.Drawing.Point(43, 15);
            this.panelSala.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSala.Name = "panelSala";
            this.panelSala.Size = new System.Drawing.Size(408, 260);
            this.panelSala.TabIndex = 6;
            // 
            // FormNuevaSala
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 289);
            this.Controls.Add(this.labelCrear);
            this.Controls.Add(this.textBoxSala);
            this.Controls.Add(this.panelSala);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormNuevaSala";
            this.Text = "FormNuevaSala";
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