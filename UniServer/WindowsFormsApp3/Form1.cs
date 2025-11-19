using BCrypt.Net;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using UniChat;
using WindowsFormsApp3;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Unichat
{
    public partial class FormLogIn : Form
    {
        private bool serverStarted = false;

        public FormLogIn()
        {
            InitializeComponent();

            //Colores-fuentes de ventanas y botones
            this.BackgroundImage = Image.FromFile("back3.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            panel1.BackColor = Color.FromArgb(25, 28, 31);

            //Colores y fuentes de los labels
            label1.Font = new Font("OCR A Extended", 20.25F, FontStyle.Bold);
            Bconectar.Image = Image.FromFile("iniciar.png");
            Bconectar.SizeMode = PictureBoxSizeMode.StretchImage;

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

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBoxUsuario_TextChanged(object sender, EventArgs e) { }

        private async void Bconectar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!serverStarted)
                {
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await Server.Main(new string[0]);
                        }
                        catch (Exception ex)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                MessageBox.Show($"Error en el servidor: {ex.Message}");
                            });
                        }
                    });
                    serverStarted = true;
                    
                    // Esperar un momento para que el servidor inicie
                    await Task.Delay(1000);
                    
                    MessageBox.Show("Servidor iniciado correctamente en el puerto 9000.");
                }

                MessageBox.Show("LogIn Exitoso como administrador del servidor.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void BBconectar_Click(object sender, EventArgs e) { Bconectar_Click_1(sender, e); }

        private void labelUsuario_Click(object sender, EventArgs e) { }
    }
}           