using BCrypt.Net;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            try
            {
                string connectionString = DbConfig.connectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string username = textBoxUsuario.Text;
                    string password = textBoxContra.Text;

                    // Validates input fields
                    if (string.IsNullOrWhiteSpace(username) || username == "Ingrese nombre de usuario")
                    {
                        MessageBox.Show("Ingrese un nombre de usuario válido.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(password) || password == "Ingrese su contraseña")
                    {
                        MessageBox.Show("Ingrese una contraseña válida.");
                        return;
                    }

                    connection.Open();

                    // Check if the username already exists
                    string queryCheck = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection))
                    {
                        cmdCheck.Parameters.AddWithValue("@username", username);
                        int userCount = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("El usuario ya existe.");
                            return;
                        }
                    }

                    // Hash the password before storing it
                    string hashedPassword = PasswordManager.HashPassword(password);

                    // Insert new user into the database
                    string queryInsert = "INSERT INTO users (username, passwd) VALUES (@username, @passwd)";
                    using (MySqlCommand cmdInsert = new MySqlCommand(queryInsert, connection))
                    {
                        cmdInsert.Parameters.AddWithValue("@username", username);
                        cmdInsert.Parameters.AddWithValue("@passwd", hashedPassword);

                        int result = cmdInsert.ExecuteNonQuery();

                        // Check if the insert was successful
                        if (result > 0)
                        {
                            MessageBox.Show("Usuario registrado exitosamente.");

                            // Open FormChat
                            FormChat chatForm = new FormChat();
                            chatForm.Show();

                            // Hide FormRegister when FormChat opens
                            this.Hide();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }
        }
    }
}
