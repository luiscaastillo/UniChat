using System;
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
using WindowsFormsApp3;
using Newtonsoft.Json;
using UniChat;

namespace Unichat
{
    public partial class FormNuevaSala : Form
    {
        public string NombreSala { get; private set; }
        public int IdChatCreado { get; private set; }

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

        }

        private async void BCrear_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxSala.Text))
            {
                NombreSala = textBoxSala.Text.Trim();
                IdChatCreado = await CrearNuevaSalaEnServidor(NombreSala);
                if (IdChatCreado > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Escribe un nombre para la sala.");
            }
        }

        private async Task<int> CrearNuevaSalaEnServidor(string nombreSala)
        {
            if (string.IsNullOrWhiteSpace(nombreSala))
            {
                MessageBox.Show("El nombre de la sala no puede estar vacío.");
                return -1;
            }

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

                // Enviar comando CREATE_CHAT
                var request = new ClientRequest
                {
                    Command = "CREATE_CHAT",
                    Username = CurrentUser.Username,
                    Content = nombreSala
                };

                string json = JsonConvert.SerializeObject(request);
                await writer.WriteLineAsync(json);

                // Esperar respuesta
                string responseJson = await reader.ReadLineAsync();
                var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                if (response.Type == "CHAT_CREATED" || response.Type == "CHAT_JOINED" || response.Type == "CHAT_EXISTS")
                {
                    if (response.Type == "CHAT_EXISTS")
                    {
                        MessageBox.Show("Ya eres miembro de esta sala.");
                    }
                    else if (response.Type == "CHAT_JOINED")
                    {
                        MessageBox.Show("Te has unido a la sala existente.");
                    }
                    else
                    {
                        MessageBox.Show("Sala creada exitosamente.");
                    }
                    return int.Parse(response.Content);
                }
                else
                {
                    MessageBox.Show(response.Content);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear/unirse a la sala: " + ex.Message);
                return -1;
            }
            finally
            {
                writer?.Close();
                reader?.Close();
                client?.Close();
            }
        }


        // Método eliminado: ahora se usa CrearNuevaSalaEnServidor que comunica con el servidor via JSON

        private void FormNuevaSala_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
