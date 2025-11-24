using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;
using Unichat;
using Newtonsoft.Json;

namespace UniChat
{
    public partial class FormAnadirUsuario : Form
    {
        private int id_chat;
        private ListView listViewUsuarios;

        public FormAnadirUsuario(int id_chat)
        {
            InitializeComponent();
            this.id_chat = id_chat;
            this.Text = "A�adir Usuario al Chat";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Configure the form UI
            ConfigureFormUI();
            
            // Load users from the database
            CargarUsuarios();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormAnadirUsuario
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Name = "FormAnadirUsuario";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        private void ConfigureFormUI()
        {
            this.Size = new Size(400, 450);
            this.BackColor = Color.FromArgb(25, 28, 31);

            // Create controls
            Label lblTitulo = new Label
            {
                Text = "Selecciona un usuario para a�adir al chat",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(360, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            listViewUsuarios = new ListView
            {
                Location = new Point(20, 60),
                Size = new Size(360, 300),
                View = View.Details,
                FullRowSelect = true,
                MultiSelect = false,
                BackColor = Color.FromArgb(54, 57, 63),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            listViewUsuarios.Columns.Add("ID", 0);
            listViewUsuarios.Columns.Add("Usuario", 355);
            listViewUsuarios.DoubleClick += (s, e) => BtnAnadir_Click(s, e);

            Button btnAnadir = new Button
            {
                Text = "A�adir",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(150, 370),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(88, 101, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAnadir.FlatAppearance.BorderSize = 0;
            btnAnadir.Click += BtnAnadir_Click;

            // Add controls to the form
            this.Controls.Add(lblTitulo);
            this.Controls.Add(listViewUsuarios);
            this.Controls.Add(btnAnadir);
        }

        private async void CargarUsuarios()
        {
            listViewUsuarios.Items.Clear();

            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ip.text, 9000);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Solicitar miembros del chat para excluirlos
                var membersRequest = new ClientRequest
                {
                    Command = "GET_CHAT_MEMBERS",
                    ChatId = id_chat
                };

                string jsonMembers = JsonConvert.SerializeObject(membersRequest);
                await writer.WriteLineAsync(jsonMembers);

                string membersResponseJson = await reader.ReadLineAsync();
                var membersResponse = JsonConvert.DeserializeObject<ServerResponse>(membersResponseJson);

                HashSet<string> membersInChat = new HashSet<string>();
                if (membersResponse.Type == "MEMBERS_RESPONSE" && membersResponse.Messages != null)
                {
                    foreach (var member in membersResponse.Messages)
                    {
                        string[] parts = member.Content.Split('|');
                        if (parts.Length == 2)
                        {
                            membersInChat.Add(parts[0]); // id_user
                        }
                    }
                }

                // Solicitar todos los usuarios
                var usersRequest = new ClientRequest
                {
                    Command = "GET_ALL_USERS"
                };

                string jsonUsers = JsonConvert.SerializeObject(usersRequest);
                await writer.WriteLineAsync(jsonUsers);

                string usersResponseJson = await reader.ReadLineAsync();
                var usersResponse = JsonConvert.DeserializeObject<ServerResponse>(usersResponseJson);

                if (usersResponse.Type == "USERS_RESPONSE" && usersResponse.Messages != null)
                {
                    foreach (var user in usersResponse.Messages)
                    {
                        string[] parts = user.Content.Split('|');
                        if (parts.Length == 2)
                        {
                            string id_user = parts[0];
                            string username = parts[1];

                            // Excluir usuarios que ya están en el chat
                            if (!membersInChat.Contains(id_user))
                            {
                                ListViewItem item = new ListViewItem(id_user);
                                item.SubItems.Add(username);
                                listViewUsuarios.Items.Add(item);
                            }
                        }
                    }
                }

                writer.Close();
                reader.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private async void BtnAnadir_Click(object sender, EventArgs e)
        {
            if (listViewUsuarios.SelectedItems.Count == 0 || listViewUsuarios.SelectedItems[0] == null)
            {
                MessageBox.Show("Selecciona un usuario para añadir.");
                return;
            }

            var selectedItem = listViewUsuarios.SelectedItems[0];
            
            if (selectedItem.SubItems.Count < 2)
            {
                MessageBox.Show("Error: datos de usuario incompletos.");
                return;
            }

            int id_user;
            if (!int.TryParse(selectedItem.Text, out id_user))
            {
                MessageBox.Show("Error: ID de usuario inválido.");
                return;
            }
            
            string username = selectedItem.SubItems[1].Text;

            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ip.text, 9000);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Enviar comando ADD_USER_TO_CHAT
                var request = new ClientRequest
                {
                    Command = "ADD_USER_TO_CHAT",
                    Username = username,
                    ChatId = id_chat
                };

                string json = JsonConvert.SerializeObject(request);
                await writer.WriteLineAsync(json);

                // Esperar respuesta
                string responseJson = await reader.ReadLineAsync();
                var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                writer.Close();
                reader.Close();
                client.Close();

                if (response.Type == "USER_ADDED")
                {
                    MessageBox.Show("Usuario añadido al chat correctamente.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(response.Content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir usuario al chat: " + ex.Message);
            }
        }
    }
}