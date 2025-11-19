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
using MySql.Data.MySqlClient; //Conexion con la Base de datos
using Mysqlx.Crud;
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


        public static int CrearNuevaSala(string nombreSala)
        {
            if (string.IsNullOrWhiteSpace(nombreSala))
            {
                MessageBox.Show("El nombre de la sala no puede estar vacío.");
                return -1;
            }

            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    // First check if a chat with this name already exists
                    string checkQuery = "SELECT id_chat FROM chats WHERE chat_name = @chat_name";
                    using (var checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@chat_name", nombreSala);
                        object existingid_chat = checkCmd.ExecuteScalar();

                        // If the chat already exists
                        if (existingid_chat != null)
                        {
                            int id_chat = Convert.ToInt32(existingid_chat);
                            
                            // Check if the user is already a member of this chat
                            string memberCheckQuery = "SELECT COUNT(*) FROM chat_members WHERE id_chat = @id_chat AND id_user = @id_user";
                            using (var memberCheckCmd = new MySqlCommand(memberCheckQuery, connection))
                            {
                                memberCheckCmd.Parameters.AddWithValue("@id_chat", id_chat);
                                memberCheckCmd.Parameters.AddWithValue("@id_user", CurrentUser.IdUser);
                                int memberCount = Convert.ToInt32(memberCheckCmd.ExecuteScalar());

                                if (memberCount > 0)
                                {
                                    MessageBox.Show("Ya eres miembro de esta sala.");
                                    return id_chat;
                                }
                                else
                                {
                                    // Add the user as a member to the existing chat
                                    string joinQuery = "INSERT INTO chat_members (id_chat, id_user) VALUES (@id_chat, @id_user)";
                                    using (var joinCmd = new MySqlCommand(joinQuery, connection))
                                    {
                                        joinCmd.Parameters.AddWithValue("@id_chat", id_chat);
                                        joinCmd.Parameters.AddWithValue("@id_user", CurrentUser.IdUser);
                                        joinCmd.ExecuteNonQuery();
                                        
                                        MessageBox.Show("Te has unido a la sala existente.");
                                        return id_chat;
                                    }
                                }
                            }
                        }
                        // If the chat doesn't exist, create a new one
                        else
                        {
                            string insertQuery = @"INSERT INTO chats (chat_name, admin_id)
                                        VALUES (@chat_name, @admin_id);
                                        SELECT LAST_INSERT_ID();";

                            using (var insertCmd = new MySqlCommand(insertQuery, connection))
                            {
                                insertCmd.Parameters.AddWithValue("@chat_name", nombreSala);
                                insertCmd.Parameters.AddWithValue("@admin_id", CurrentUser.IdUser);

                                object result = insertCmd.ExecuteScalar();
                                if (result != null && int.TryParse(result.ToString(), out int idChat))
                                {
                                    // Add the creator as a member in chat_members table
                                    string addMemberQuery = "INSERT INTO chat_members (id_chat, id_user) VALUES (@id_chat, @id_user)";
                                    using (var addMemberCmd = new MySqlCommand(addMemberQuery, connection))
                                    {
                                        addMemberCmd.Parameters.AddWithValue("@id_chat", idChat);
                                        addMemberCmd.Parameters.AddWithValue("@id_user", CurrentUser.IdUser);
                                        addMemberCmd.ExecuteNonQuery();
                                    }
                                    
                                    MessageBox.Show("Sala creada exitosamente.");
                                    return idChat;
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo crear la sala.");
                                    return -1;
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al procesar la sala: " + ex.Message);
                return -1;
            }
        }

        private void FormNuevaSala_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
