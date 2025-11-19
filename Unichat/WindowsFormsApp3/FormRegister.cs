using BCrypt.Net;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unichat;
using Newtonsoft.Json;
using WindowsFormsApp3;

namespace UniChat
{
    public partial class FormRegister : Form
    {
        private FormLogIn _form1; // Variable para almacenar la referencia a Form1
        public FormRegister(FormLogIn form1)
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
            label1.Font = new Font("OCR A Extended", 20.25F, FontStyle.Bold);
            labelUsuario.BackColor = Color.FromArgb(25, 28, 31);
            labelUsuario.ForeColor = Color.White;
            labelUsuario.Font = new Font("OCR A Extended", 16, FontStyle.Bold);
            labelContra.BackColor = Color.FromArgb(25, 28, 31);
            labelContra.ForeColor = Color.White;
            labelContra.Font = new Font("OCR A Extended", 16, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Font = new Font("Century Gothic", 9, FontStyle.Bold);
            linkLogin.Font = new Font("Century Gothic", 9, FontStyle.Bold);

            //TextBox de usuario y contraseña
            textBoxUsuario.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            textBoxContra.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            textBoxRecontra.Font = new Font("Century Gothic", 9, FontStyle.Regular);
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
            textBoxContra.KeyPress += textBoxContra_KeyPress;

            textBoxRecontra.Text = "Ingrese nuevamente su contraseña";
            textBoxRecontra.ForeColor = Color.Gray;
            textBoxRecontra.Enter += textBoxRecontra_Enter;
            textBoxRecontra.Leave += textBoxRecontra_Leave;
            textBoxRecontra.KeyPress += textBoxRecontra_KeyPress;
        }

        private void textBoxContra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxContra.Text == "Ingrese su contraseña")
            {
                textBoxContra.Text = "";
                textBoxContra.ForeColor = Color.Black;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Bconectar_Click_1(sender, EventArgs.Empty);
            }
        }
        private void textBoxRecontra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Bconectar_Click_1(sender, EventArgs.Empty);
            }
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

        private void textBoxRecontra_Enter(object sender, EventArgs e)
        {
            if (textBoxRecontra.Text == "Ingrese nuevamente su contraseña" || textBoxRecontra.Text == "Vuelva a ingresar su contraseña")
            {
                textBoxRecontra.Text = "";
                textBoxRecontra.ForeColor = Color.Black;
            }

        }
        private void textBoxRecontra_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRecontra.Text))
            {
                textBoxRecontra.Text = "Ingrese nuevamente su contraseña";
                textBoxRecontra.ForeColor = Color.Gray;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Abrir Form1 (Login) otra vez, la que ya existe y no crear una nueva
            _form1.Show();

            this.Hide();
        }

        private async void Bconectar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string username = textBoxUsuario.Text;
                string password = textBoxContra.Text;
                string repassword = textBoxRecontra.Text;

                // Validación de campos vacíos y coincidencia de contraseñas
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

                if (string.IsNullOrWhiteSpace(repassword) || repassword == "Ingrese nuevamente su contraseña")
                {
                    MessageBox.Show("Repita la contraseña.");
                    return;
                }

                if (password != repassword)
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                    return;
                }

                // Conectar al servidor y enviar comando REGISTER
                bool registered = await RegisterOnServerAsync(username, password);

                if (registered)
                {
                    MessageBox.Show("Usuario registrado exitosamente.");
                    FormLogIn Form2 = new FormLogIn();
                    Form2.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }
        }

        private async Task<bool> RegisterOnServerAsync(string username, string password)
        {
            TcpClient client = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(ip.text, 9000);

                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Enviar comando REGISTER
                var registerRequest = new ClientRequest
                {
                    Command = "REGISTER",
                    Username = username,
                    Password = password
                };

                string json = JsonConvert.SerializeObject(registerRequest);
                await writer.WriteLineAsync(json);

                // Esperar respuesta del servidor
                string responseJson = await reader.ReadLineAsync();
                var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                if (response.Type == "REGISTER_SUCCESS")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(response.Content); // Mostrar error del servidor
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
                return false;
            }
            finally
            {
                // Cerrar conexión temporal
                writer?.Close();
                reader?.Close();
                client?.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBoxContra_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
