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
            this.textBoxSala.Location = new System.Drawing.Point(78, 129);
            this.textBoxSala.Name = "textBoxSala";
            this.textBoxSala.Size = new System.Drawing.Size(200, 20);
            this.textBoxSala.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("OCR A Extended", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Creando nueva sala";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCrear
            // 
            this.labelCrear.AutoSize = true;
            this.labelCrear.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCrear.Location = new System.Drawing.Point(63, 95);
            this.labelCrear.Name = "labelCrear";
            this.labelCrear.Size = new System.Drawing.Size(226, 19);
            this.labelCrear.TabIndex = 4;
            this.labelCrear.Text = "Escribe el nombre de tu sala";
            // 
            // BCrear
            // 
            this.BCrear.Location = new System.Drawing.Point(83, 143);
            this.BCrear.Name = "BCrear";
            this.BCrear.Size = new System.Drawing.Size(119, 50);
            this.BCrear.TabIndex = 5;
            this.BCrear.TabStop = false;
            // 
            // panelSala
            // 
            this.panelSala.Controls.Add(this.BCrear);
            this.panelSala.Controls.Add(this.label1);
            this.panelSala.Location = new System.Drawing.Point(32, 12);
            this.panelSala.Name = "panelSala";
            this.panelSala.Size = new System.Drawing.Size(306, 211);
            this.panelSala.TabIndex = 6;
            // 
            // FormNuevaSala
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 235);
            this.Controls.Add(this.labelCrear);
            this.Controls.Add(this.textBoxSala);
            this.Controls.Add(this.panelSala);
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