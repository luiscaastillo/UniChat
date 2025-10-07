using MySql.Data.MySqlClient; //Conexion con la Base de datos
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using UniChat;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BCrypt.Net;

namespace Unichat
{

    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();

            //Colores-fuentes de ventanas y botones
            this.BackgroundImage = Image.FromFile("back3.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            panel1.BackColor = Color.FromArgb(25, 28, 31);
            //panel1.BackColor = Color.FromArgb(39, 45, 159); ESTE COLOR??

            //Colores y fuentes de los labels
            labelUsuario.ForeColor = Color.White;
            labelUsuario.BackColor = Color.FromArgb(25, 28, 31);
            labelContra.ForeColor = Color.White;
            labelContra.BackColor = Color.FromArgb(25, 28, 31);
            labelCuenta.ForeColor = Color.White;
            labelCuenta.BackColor = Color.FromArgb(25, 28, 31);
            linkRegistrarse.BackColor = Color.FromArgb(25, 28, 31);

            //Config de los textBox Usuario y Contra
            textBoxUsuario.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            textBoxContra.Font = new Font("Century Gothic", 9, FontStyle.Regular);

            //Boton Conectar
            Bconectar.Image = Image.FromFile("iniciar.png");
            Bconectar.SizeMode = PictureBoxSizeMode.StretchImage;

        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(83, 73, 76);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister registroForm = new FormRegister(this);
            registroForm.Show();

            //Oculta Form1 cuando se abre el FormRegister
            this.Hide();
        }

        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {
         
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

        private void textBoxContra_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bconectar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string connectionString = DbConfig.connectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    //Open connection
                    connection.Open();
                    string username = textBoxUsuario.Text;
                    string password = textBoxContra.Text;

                    // Prepare the SQL query to get the hashed password for the given username
                    string query = "SELECT passwd FROM users WHERE username = @username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            string storedHashedPassword = result.ToString();
                            
                            // Check if it's a valid BCrypt hash (starts with $)
                            if (!storedHashedPassword.StartsWith("$"))
                            {
                                MessageBox.Show("Su contraseña debe ser actualizada. Contacte al administrador.");
                                return;
                            }
                            
                            if (PasswordManager.VerifyPassword(password, storedHashedPassword))
                            {
                                MessageBox.Show("LogIn Exitoso");
                                //Open chat window
                                FormChat chatForm = new FormChat();
                                chatForm.Show();
                                //Hide Form1 when FormChat opens
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Contraseña incorrecta.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El usuario no existe.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }


            MessageBox.Show("LogIn Exitoso"); //Implementar la validación de user y 

            //Oculta Form1 cuando se abre el FormRegister
            this.Hide();
        }
    }
}
