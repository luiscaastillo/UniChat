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

namespace UniChat
{
    public partial class FormRegister : Form
    {
        private Form1 _form1; // Variable para almacenar la referencia a Form1
        public FormRegister(Form1 form1)
        {
            InitializeComponent();

            _form1 = form1; //Para llamar a la FormPrincipal

            //Colores-fuentes de ventanas y botones
            this.BackgroundImage = Image.FromFile("back3.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
         
            //Panel de Fondo
            panel1.BackColor = Color.FromArgb(25, 28, 31);

            //Colores de los labels
            label1.BackColor = Color.FromArgb(25, 28, 31);
            label1.ForeColor = Color.White;
            labelUsuario.BackColor = Color.FromArgb(25, 28, 31);
            labelUsuario.ForeColor = Color.White;
            labelContra.BackColor = Color.FromArgb(25, 28, 31);
            labelContra.ForeColor = Color.White;
            label4.ForeColor = Color.White;

            //TextBox de usuario y contraseña
            textBoxUsuario.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            textBoxContra.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            Bconectar.Image = Image.FromFile("registrar.png");
            Bconectar.SizeMode = PictureBoxSizeMode.StretchImage;

            //Aplicar cuando se cierre el FormRegister, se cierre toda la aplicación
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            //Cambiar el texto y color de los TextBox al iniciar
            textBoxUsuario.Text = "Ingrese nombre de usuario";
            textBoxUsuario.ForeColor = Color.Gray;

            textBoxUsuario.Enter += textBoxUsuario_Enter;
            textBoxUsuario.Leave += textBoxUsuario_Leave;

            textBoxContra.Text = "Ingrese su contraseña";
            textBoxContra.ForeColor = Color.Gray;
            textBoxContra.Enter += textBoxContra_Enter;
            textBoxContra.Leave += textBoxContra_Leave;
        }

        private void textBoxUsuario_Enter(object sender, EventArgs e)
        {
            if (textBoxUsuario.Text == "Ingrese nombre de usuario")
            {
                textBoxUsuario.Text = "";
                textBoxUsuario.ForeColor = Color.Black;
            }
        }

        private void textBoxUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsuario.Text))
            {
                textBoxUsuario.Text = "Ingrese nombre de usuario";
                textBoxUsuario.ForeColor = Color.Gray;
            }
        }

        private void textBoxContra_Enter(object sender, EventArgs e)
        {
            if (textBoxContra.Text == "Ingrese su contraseña")
            {
                textBoxContra.Text = "";
                textBoxContra.ForeColor = Color.Black;
            }
        }

        private void textBoxContra_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxContra.Text))
            {
                textBoxContra.Text = "Ingrese su contraseña";
                textBoxContra.ForeColor = Color.Gray;
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Abrir Form1 (Login) otra vez, la que ya existe y no crear una nueva
            _form1.Show();

            this.Hide(); //Para ocultar la ventana
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxContra_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bconectar_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Registro Exitoso"); //Se registra el usuario

            //Aqui se abre la ventana de chat
            FormChat chatForm = new FormChat();
            chatForm.Show();

            //Oculta Form1 cuando se abre el FormRegister
            this.Hide();
        }
    }
}
