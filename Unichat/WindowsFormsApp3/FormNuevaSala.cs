using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //Conexion con la Base de datos
using Mysqlx.Crud;
using Unichat;

namespace Unichat
{
    public partial class FormNuevaSala : Form
    {
        public string NombreSala { get; private set; }
        public FormNuevaSala()
        {
            InitializeComponent();

            //Colores-fuentes de ventanas y botones
            this.BackgroundImage = Image.FromFile("back.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            panelSala.BackColor = Color.FromArgb(25, 28, 31);
            label1.BackColor = Color.FromArgb(25, 28, 31);
            label1.ForeColor = Color.White;
            labelCrear.BackColor = Color.FromArgb(25, 28, 31);
            labelCrear.ForeColor = Color.White;
            BCrear.Image = Image.FromFile("crear.png");
            BCrear.SizeMode = PictureBoxSizeMode.StretchImage;
            BCrear.BackColor = Color.FromArgb(25, 28, 31);


            BCrear.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(textBoxSala.Text))
                {
                    NombreSala = textBoxSala.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Escribe un nombre para la sala.");
                }
            };
        }
    
    
        private void FormNuevaSala_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
