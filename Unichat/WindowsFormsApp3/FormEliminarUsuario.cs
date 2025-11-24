using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;
using Unichat;
using Newtonsoft.Json;

namespace UniChat
{
    public partial class FormEliminarUsuario : Form
    {
        private int id_chat;
        private ListView listViewUsuarios;

        public FormEliminarUsuario(int id_chat)
        {
            InitializeComponent();
            this.id_chat = id_chat;
            this.Text = "Eliminar Usuario del Chat";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Configure the form UI
            ConfigureFormUI();
            
            // Load users from the database
            CargarUsuariosDelChat();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEliminarUsuario));
            this.SuspendLayout();
            // 
            // FormEliminarUsuario
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEliminarUsuario";
            this.ResumeLayout(false);

        }

        private void ConfigureFormUI()
        {
            this.Size = new Size(400, 450);
            this.BackColor = Color.FromArgb(25, 28, 31);

            // Create controls
            Label lblTitulo = new Label
            {
                Text = "Selecciona un usuario para eliminar",
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

            Button btnEliminar = new Button
            {
                Text = "Eliminar",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(150, 370),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(237, 66, 69),  // Color rojo para eliminar
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Click += BtnEliminar_Click;

            // Add controls to the form
            this.Controls.Add(lblTitulo);
            this.Controls.Add(listViewUsuarios);
            this.Controls.Add(btnEliminar);
        }

        private async void CargarUsuariosDelChat()
        {
            listViewUsuarios.Items.Clear();

            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ip.text, 9000);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Solicitar miembros del chat
                var request = new ClientRequest
                {
                    Command = "GET_CHAT_MEMBERS",
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

                if (response.Type == "MEMBERS_RESPONSE" && response.Messages != null)
                {
                    foreach (var member in response.Messages)
                    {
                        // member.Content tiene formato "id_user|username"
                        string[] parts = member.Content.Split('|');
                        if (parts.Length == 2)
                        {
                            int id_user = int.Parse(parts[0]);
                            string username = parts[1];

                            // Excluir al usuario actual
                            if (id_user != CurrentUser.IdUser)
                            {
                                ListViewItem item = new ListViewItem(id_user.ToString());
                                item.SubItems.Add(username);
                                listViewUsuarios.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (listViewUsuarios.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un usuario para eliminar.");
                return;
            }

            int id_user = Convert.ToInt32(listViewUsuarios.SelectedItems[0].Text);
            string username = listViewUsuarios.SelectedItems[0].SubItems[1].Text;

            // Confirm removal
            var result = MessageBox.Show(
                $"¿Estás seguro que deseas eliminar a {username} del chat?", 
                "Confirmar eliminación", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    await client.ConnectAsync(ip.text, 9000);

                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                    // Enviar comando REMOVE_USER_FROM_CHAT
                    var request = new ClientRequest
                    {
                        Command = "REMOVE_USER_FROM_CHAT",
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

                    if (response.Type == "USER_REMOVED")
                    {
                        MessageBox.Show($"Usuario {username} eliminado del chat correctamente.");
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
                    MessageBox.Show("Error al eliminar usuario del chat: " + ex.Message);
                }
            }
        }
    }
}