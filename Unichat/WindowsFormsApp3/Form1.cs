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
using WindowsFormsApp3;

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
            label1.Font = new Font("OCR A Extended", 20.25F, FontStyle.Bold);

            labelUsuario.ForeColor = Color.White;
            labelUsuario.BackColor = Color.FromArgb(25, 28, 31);
            labelUsuario.Font = new Font("OCR A Extended", 16, FontStyle.Bold);

            labelContra.ForeColor = Color.White;
            labelContra.BackColor = Color.FromArgb(25, 28, 31);
            labelContra.Font = new Font("OCR A Extended", 16, FontStyle.Bold);

            labelCuenta.ForeColor = Color.White;
            labelCuenta.BackColor = Color.FromArgb(25, 28, 31);
            labelCuenta.Font = new Font("Century Gothic", 9, FontStyle.Bold);

            linkRegistrarse.BackColor = Color.FromArgb(25, 28, 31);
            linkRegistrarse.Font = new Font("Century Gothic", 9, FontStyle.Bold);

            //Config de los textBox Usuario y Contra
            textBoxUsuario.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            textBoxContra.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            textBoxContra.PasswordChar = '●'; 

            //Imagen  BConectar
            Bconectar.Image = Image.FromFile("iniciar.png");
            Bconectar.SizeMode = PictureBoxSizeMode.StretchImage;

            //Boton BBconectar
            BBconectar.FlatStyle = FlatStyle.Flat;
            BBconectar.FlatAppearance.BorderSize = 0;
            BBconectar.BackColor = Color.Transparent;
            BBconectar.ForeColor = Color.Transparent;
            BBconectar.Text = "";
            BBconectar.TabStop = false;
            BBconectar.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BBconectar.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BBconectar.TabStop = true; // Permite el foco con Tab


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

            textBoxContra.KeyPress += textBoxContra_KeyPress;

            int radio = 20; // Radio de las esquinas
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = BBconectar.ClientRectangle;
            path.AddArc(rect.X, rect.Y, radio, radio, 180, 90);
            path.AddArc(rect.Right - radio, rect.Y, radio, radio, 270, 90);
            path.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radio, radio, radio, 90, 90);
            path.CloseAllFigures();
            BBconectar.Region = new Region(path);
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
            if (textBoxContra.Text == "Ingrese su contraseña" || textBoxContra.Text == "Vuelva a ingresar su contraseña")
            {
                textBoxContra.Text = "";
                textBoxContra.ForeColor = Color.Black;
                textBoxContra.PasswordChar = '●'; // Activar ocultamiento
            }
        }

        private void textBoxContra_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxContra.Text))
            {
                textBoxContra.PasswordChar = '\0'; // Mostrar texto normal
                textBoxContra.Text = "Ingrese su contraseña";
                textBoxContra.ForeColor = Color.Gray;
            }
        }

        private void textBoxContra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxContra.Text == "Ingrese su contraseña" || textBoxContra.Text == "Vuelva a ingresar su contraseña")
            {
                textBoxContra.Text = "";
                textBoxContra.ForeColor = Color.Black;
            }
        }


        private void textBoxContra_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bconectar_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    string username = textBoxUsuario.Text;
                    string password = textBoxContra.Text;

                    string query = "SELECT id_user, passwd FROM users WHERE username = @username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idObtenido = Convert.ToInt32(reader["id_user"]);
                                string storedHashedPassword = reader["passwd"].ToString();

                                if (!storedHashedPassword.StartsWith("$"))
                                {
                                    MessageBox.Show("Su contraseña debe ser actualizada. Contacte al administrador.");
                                    return;
                                }

                                if (PasswordManager.VerifyPassword(password, storedHashedPassword))
                                {
                                    // Guarda el usuario actual (Static Class)
                                    CurrentUser.SetCurrentUser(idObtenido, username);

                                    MessageBox.Show("LogIn Exitoso");
                                    FormChat chatForm = new FormChat();
                                    chatForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta.");
                                    textBoxContra.Text = "Vuelva a ingresar su contraseña";
                                    textBoxContra.ForeColor = Color.Gray;
                                }
                            }
                            else
                            {
                                MessageBox.Show("El usuario no existe.");
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }
        }

        private void BBconectar_Click(object sender, EventArgs e)
        {
            Bconectar_Click_1(sender,e);
        }
    }
}
