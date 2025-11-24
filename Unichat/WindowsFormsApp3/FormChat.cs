using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unichat;
using WindowsFormsApp3;
using Newtonsoft.Json;

namespace UniChat
{
    public partial class FormChat : Form
    {
        private readonly Dictionary<string, string> EmojiMap = new Dictionary<string, string>
        {
            { ":happy:", "😀"}, { ":sad:", "😔" }, { ":angry:", "😡" }, 
            { ":cry:", "😭" }, { ":eww:", "🤢" }, { ":like:", "👍" }, 
            { ":corazon:", "❤️" }, { ":lover:", "🥰" }, { ":kiss:", "😘" }, 
            { ":pray:", "🙏" }, { ":ajajaj:", "🤣" }, { ":cool:", "😎" }
        };

        private readonly Dictionary<string, Image> EmojiImages = new Dictionary<string, Image>();

        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private bool isConnected = false;

        public FormChat(TcpClient tcpClient = null, StreamReader streamReader = null, StreamWriter streamWriter = null)
        {
            InitializeComponent();

            if (tcpClient != null && streamReader != null && streamWriter != null)
            {
                client = tcpClient;
                reader = streamReader;
                writer = streamWriter;
                isConnected = true;

                // Start listening for messages
                _ = Task.Run(() => ReceiveMessages());
            }

            string user = CurrentUser.Username;
            MessageBox.Show("Usuario actual: " + user);

            //Colores-fuentes de ventanas y botones
            this.BackgroundImage = Image.FromFile("back.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.FromArgb(25, 28, 31);

            //Colores de los labels
            labelUsername.ForeColor = labelSalas.ForeColor = Color.White;

            //Colores de los paneles
            Color BackColor = Color.FromArgb(166, 166, 166);

            panelUser.BackColor = BackColor;
            panelSalas.BackColor = BackColor;
            panelName.BackColor = BackColor;
            panelEmoji.BackColor = BackColor;
            panelSalasName.BackColor = BackColor;
            treeViewChats.BackColor = BackColor;
            treeViewUsers.BackColor = BackColor;

            //Botones e iconos 
            pictureUser.Image = Image.FromFile("user.png");
            pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureSala.Image = Image.FromFile("group.png");
            pictureSala.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureSala1.Image = Image.FromFile("group.png");
            pictureSala1.SizeMode = PictureBoxSizeMode.StretchImage;
            BEnviarMsj.Image = Image.FromFile("send.png");
            BEnviarMsj.SizeMode = PictureBoxSizeMode.StretchImage;
            BEmoji.Image = Image.FromFile("emoji.png");
            BEmoji.SizeMode = PictureBoxSizeMode.StretchImage;
            BLogOut.Image = Image.FromFile("logout.png");
            BLogOut.SizeMode = PictureBoxSizeMode.StretchImage;
            BNewChat.Image = Image.FromFile("mas.png");
            BNewChat.SizeMode = PictureBoxSizeMode.StretchImage;
            addUser.Image = Image.FromFile("mas.png");
            addUser.SizeMode = PictureBoxSizeMode.StretchImage;
            BDeleteChat.Image = Image.FromFile("menos.png");
            BDeleteChat.SizeMode = PictureBoxSizeMode.StretchImage;
            deleteUser.Image = Image.FromFile("menos.png");
            deleteUser.SizeMode = PictureBoxSizeMode.StretchImage;
            labelUsername.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            RichMessage.Font = new Font("Century Gothic", 9, FontStyle.Regular);

            // Panel lateral
            panelSalas.BackColor = Color.FromArgb(44, 47, 51);

            // Chats
            treeViewChats.BorderStyle = BorderStyle.None;
            treeViewChats.BackColor = Color.FromArgb(54, 57, 63);
            treeViewChats.ForeColor = Color.WhiteSmoke;
            treeViewChats.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            treeViewChats.ItemHeight = 28;

            // Users
            treeViewUsers.BorderStyle = BorderStyle.None;
            treeViewUsers.BackColor = Color.FromArgb(54, 57, 63);
            treeViewUsers.ForeColor = Color.WhiteSmoke;
            treeViewUsers.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            treeViewUsers.ItemHeight = 28;

            // Títulos
            labelChats.Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
            labelChats.ForeColor = Color.White;
            label1.Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
            label1.ForeColor = Color.White;

            // En el constructor, después de InitializeComponent();
            panelUser.BackColor = Color.FromArgb(54, 57, 63); // Fondo tipo Discord
            panelUser.BorderStyle = BorderStyle.None; // Si tienes esta propiedad
            panelUser.Padding = new Padding(16, 16, 16, 16);



            this.FormClosing += FormChat_FormClosing;
            this.FormClosed += (s, e) => Application.Exit();
        }

        private async void ReceiveMessages()
        {
            try
            {
                while (isConnected && reader != null)
                {
                    string messageJson = await reader.ReadLineAsync();
                    
                    if (string.IsNullOrEmpty(messageJson))
                    {
                        // Connection closed
                        isConnected = false;
                        break;
                    }

                    // Deserializar respuesta JSON
                    var response = JsonConvert.DeserializeObject<ServerResponse>(messageJson);
                    
                    if (response.Type == "NEW_MESSAGE")
                    {
                        // Solo mostrar si no es del usuario actual
                        if (response.Username != CurrentUser.Username)
                        {
                            // Mostrar si es el chat activo
                            bool isCurrentChat = false;
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    isCurrentChat = treeViewChats.SelectedNode?.Tag != null &&
                                                   (int)treeViewChats.SelectedNode.Tag == response.ChatId;
                                }));
                            }
                            else
                            {
                                isCurrentChat = treeViewChats.SelectedNode?.Tag != null &&
                                               (int)treeViewChats.SelectedNode.Tag == response.ChatId;
                            }

                            // Display if it's the active chat
                            if (isCurrentChat)
                            {
                                DateTime timestamp = DateTime.Parse(response.Timestamp);
                                DisplayMessage(response.Username, response.Content, timestamp, false);
                            }
                        }
                    }
                    else if (response.Type == "MESSAGES_RESPONSE")
                    {
                        // Cargar mensajes históricos del servidor
                        if (response.Messages != null)
                        {
                            this.Invoke(new Action(() =>
                            {
                                panelUser.Controls.Clear(); // Limpiar mensajes anteriores
                                
                                // Mostrar en orden inverso (más antiguos primero)
                                for (int i = response.Messages.Count - 1; i >= 0; i--)
                                {
                                    var msg = response.Messages[i];
                                    DateTime timestamp = DateTime.Parse(msg.Timestamp);
                                    bool esPropio = msg.Username == CurrentUser.Username;
                                    DisplayMessage(msg.Username, msg.Content, timestamp, esPropio);
                                }
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (isConnected)
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Error al recibir mensajes: " + ex.Message);
                        }));
                    }
                    catch (ObjectDisposedException)
                    {
                        // Form already closed
                    }
                    isConnected = false;
                }
            }
        }

        private async void BEnviarMsj_Click(object sender, EventArgs e)
        {
            string mensaje = RichMessage.Text.Trim();

            if (!string.IsNullOrEmpty(mensaje) && mensaje != "Escribe un mensaje")
            {
                if (treeViewChats.SelectedNode?.Tag != null)
                {
                    int id_chat = (int)treeViewChats.SelectedNode.Tag;
                    
                    try
                    {
                        // 1. Mostrar localmente primero
                        string username = CurrentUser.Username;
                        DateTime fecha = DateTime.Now;
                        DisplayMessage(username, mensaje, fecha, true);
                        
                        // 2. Enviar al servidor usando JSON
                        if (isConnected && writer != null)
                        {
                            var request = new ClientRequest
                            {
                                Command = "SEND_MESSAGE",
                                Username = CurrentUser.Username,
                                Content = mensaje,
                                ChatId = id_chat
                            };
                            
                            string json = JsonConvert.SerializeObject(request);
                            await writer.WriteLineAsync(json);
                            await writer.FlushAsync();
                        }
                        
                        // 3. Limpiar input
                        RichMessage.Clear();
                        RichMessage.Text = "";
                        RichMessage.ForeColor = Color.Gray;
                        RichMessage.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al enviar mensaje: " + ex.Message);
                        isConnected = false;
                    }
                }
            }

            RichMessage.Text = "";
            RichMessage.ForeColor = Color.Gray;
            RichMessage.Focus();
        }

        public async Task<bool> ConnectToServer(string serverIP, int port)
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(serverIP, port);
                
                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                
                isConnected = true;
                
                // Enviar LOGIN usando JSON
                var loginRequest = new ClientRequest
                {
                    Command = "LOGIN",
                    Username = CurrentUser.Username,
                    Password = "" // No enviamos la contraseña porque ya autenticamos localmente
                };
                
                string json = JsonConvert.SerializeObject(loginRequest);
                await writer.WriteLineAsync(json);
                
                // Esperar respuesta
                string responseJson = await reader.ReadLineAsync();
                var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);
                
                if (response.Type == "LOGIN_SUCCESS" || response.Type == "ERROR")
                {
                    // Iniciar recepción de mensajes
                    _ = Task.Run(() => ReceiveMessages());
                    
                    // Cargar mensajes del servidor
                    await LoadMessagesFromServer(50);
                    
                    return response.Type == "LOGIN_SUCCESS";
                }
                
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
                return false;
            }
        }

        private async Task LoadMessagesFromServer(int count)
        {
            if (!isConnected || writer == null) return;
            
            try
            {
                var request = new ClientRequest
                {
                    Command = "LOAD_MESSAGES",
                    Count = count
                };
                
                string json = JsonConvert.SerializeObject(request);
                await writer.WriteLineAsync(json);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar mensajes: " + ex.Message);
            }
        }

        private async void DisconnectFromServer()
        {
            if (!isConnected) return;
            
            isConnected = false;
            
            if (writer != null)
            {
                try
                {
                    var request = new ClientRequest { Command = "DISCONNECT" };
                    string json = JsonConvert.SerializeObject(request);
                    await writer.WriteLineAsync(json);
                    await writer.FlushAsync();
                    writer.Dispose();
                }
                catch { }
                writer = null;
            }
            
            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }
            
            if (client != null)
            {
                client.Close();
                client = null;
            }
        }

        private void SaveReceivedMessageFromServer(string username, string content)
        {
            // El servidor ya guarda los mensajes, el cliente solo muestra
            // No es necesario guardar en la base de datos local
        }

        // ... [Mantén todos los demás métodos existentes: FormChat_Load, RichMessage_KeyDown, 
        //      HighlightMentions, SaveMessageWithMentions, BEmoji_Click, ResizeImage, 
        //      ShowImageDialog, BLogOut_Click, BNewChat_Click, treeViewChats_AfterSelect,
        //      CargarChatMembers, InicializarEmojiImages, CargarMensajesChat, DisplayMessage,
        //      CargarChatsUsuario, BDeleteChat_Click, addUser_Click, deleteUser_Click, etc.] ...

        private void FormChat_Load(object sender, EventArgs e)
        {
            RichMessage.Text = "Escribe un mensaje";
            RichMessage.ForeColor = Color.Gray;
            RichMessage.Enter += RichMessage_Enter;
            RichMessage.Leave += RichMessage_Leave;
            RichMessage.KeyDown += RichMessage_KeyDown;
            
            // Cargar chats desde el servidor
            CargarChatsUsuario(treeViewChats);
            
            labelUsername.Text = CurrentUser.Username;
            panelEmoji.Visible = false;
        }

        private void RichMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BEnviarMsj_Click(BEnviarMsj, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private void RichMessage_Enter(object sender, EventArgs e)
        {
            if (RichMessage.Text == "Escribe un mensaje")
            {
                RichMessage.Text = "";
                RichMessage.ForeColor = Color.Black;
            }
        }

        private void RichMessage_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RichMessage.Text))
            {
                RichMessage.Text = "Escribe un mensaje";
                RichMessage.ForeColor = Color.Gray;
            }
        }

        private void HighlightMentions(RichTextBox rtb)
        {
            string text = rtb.Text;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"@\w+");
            foreach (System.Text.RegularExpressions.Match match in regex.Matches(text))
            {
                rtb.Select(match.Index, match.Length);
                rtb.SelectionColor = Color.LightSkyBlue;
                rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
            }
            rtb.DeselectAll();
        }

        private void SaveMessageWithMentions(int id_chat, string message)
        {
            // El servidor ya guarda los mensajes cuando se envían
            // Este método ya no es necesario con la arquitectura cliente-servidor
        }

        private void BEmoji_Click(object sender, EventArgs e)
        {
            //Se elimina el texto por defecto al hacer click en el boton de emojis
            if (RichMessage.Text == "Escribe un mensaje")
            {
                RichMessage.Text = "";
                RichMessage.ForeColor = Color.Gray;
            }

            panelEmoji.Visible = true;
            panelEmoji.BringToFront();
            happy.SizeMode = PictureBoxSizeMode.StretchImage;
            sad.SizeMode = PictureBoxSizeMode.StretchImage;
            angry.SizeMode = PictureBoxSizeMode.StretchImage;
            cry.SizeMode = PictureBoxSizeMode.StretchImage;
            eww.SizeMode = PictureBoxSizeMode.StretchImage;
            like.SizeMode = PictureBoxSizeMode.StretchImage;
            corazon.SizeMode = PictureBoxSizeMode.StretchImage;
            lover.SizeMode = PictureBoxSizeMode.StretchImage;
            kiss.SizeMode = PictureBoxSizeMode.StretchImage;
            pray.SizeMode = PictureBoxSizeMode.StretchImage;
            ajajaja.SizeMode = PictureBoxSizeMode.StretchImage;
            cool.SizeMode = PictureBoxSizeMode.StretchImage;
            close.Image = Image.FromFile("salirEmoji.png");
            close.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }

            //ShowImageDialog(EmojiImages[":cool:"]);

        //Metodo metodo para asignar tamaño
        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        //Metodo para messagebox con imagen para verificar si hay una imagen.
        public void ShowImageDialog(Image img)
        {
            Form form = new Form();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = new Size(200, 200);
            PictureBox pb = new PictureBox();
            pb.Image = img;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Dock = DockStyle.Fill;
            form.Controls.Add(pb);
            form.ShowDialog();
        }

        private void BLogOut_Click(object sender, EventArgs e) { Application.Exit(); }

        private void BNewChat_Click(object sender, EventArgs e)
        {
            using (var formSala = new FormNuevaSala())
            {
                if (formSala.ShowDialog() == DialogResult.OK)
                {
                    string nombreSala = formSala.NombreSala;
                    int id_chat = formSala.IdChatCreado;
                    if (!string.IsNullOrEmpty(nombreSala) && id_chat > 0)
                    {
                        TreeNode nodo = new TreeNode(nombreSala);
                        nodo.Tag = id_chat;
                        treeViewChats.Nodes.Add(nodo);
                    }
                }
            }
        }

        private async void treeViewChats_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                int idChatSeleccionado = (int)e.Node.Tag;
                // Cargar mensajes y miembros desde el servidor
                await CargarMensajesDesdeServidor(idChatSeleccionado);
                await CargarChatMembersDesdeServidor(idChatSeleccionado);
            }
        }

        private async Task CargarChatMembersDesdeServidor(int id_chat)
        {
            try
            {
                // Usar conexión temporal para consultas GET
                using (TcpClient tempClient = new TcpClient())
                {
                    await tempClient.ConnectAsync(ip.text, 9000);
                    NetworkStream stream = tempClient.GetStream();
                    StreamReader tempReader = new StreamReader(stream, Encoding.UTF8);
                    StreamWriter tempWriter = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                    var request = new ClientRequest
                    {
                        Command = "GET_CHAT_MEMBERS",
                        ChatId = id_chat
                    };

                    string json = JsonConvert.SerializeObject(request);
                    await tempWriter.WriteLineAsync(json);

                    // Esperar respuesta
                    string responseJson = await tempReader.ReadLineAsync();
                    var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                    if (response.Type == "MEMBERS_RESPONSE" && response.Messages != null)
                    {
                        this.Invoke(new Action(() =>
                        {
                            treeViewUsers.Nodes.Clear();
                            foreach (var member in response.Messages)
                            {
                                // member.Content tiene formato "id_user|username"
                                string[] parts = member.Content.Split('|');
                                if (parts.Length == 2)
                                {
                                    string username = parts[1];
                                    TreeNode userNode = new TreeNode(username);
                                    treeViewUsers.Nodes.Add(userNode);
                                }
                            }
                        }));
                    }

                    tempWriter.Close();
                    tempReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar miembros: " + ex.Message);
            }
        }

        // Método eliminado: usar CargarChatMembersDesdeServidor en su lugar

        private void InicializarEmojiImages()
        {
            if (EmojiImages.Count == 0)
            {
                happy.Image = Image.FromFile("happy.png");
                sad.Image = Image.FromFile("sad.png");
                angry.Image = Image.FromFile("angry.png");
                cry.Image = Image.FromFile("cry.png");
                eww.Image = Image.FromFile("eww.png");
                like.Image = Image.FromFile("like.png");
                corazon.Image = Image.FromFile("corazon.png");
                lover.Image = Image.FromFile("lover.png");
                kiss.Image = Image.FromFile("kiss.png");
                pray.Image = Image.FromFile("pray.png");
                ajajaja.Image = Image.FromFile("risa.png");
                cool.Image = Image.FromFile("cool.png");

                EmojiImages[":happy:"] = ResizeImage(Image.FromFile("happy.png"), 22, 22);
                EmojiImages[":sad:"] = ResizeImage(Image.FromFile("sad.png"), 22, 22);
                EmojiImages[":angry:"] = ResizeImage(Image.FromFile("angry.png"), 22, 22);
                EmojiImages[":cry:"] = ResizeImage(Image.FromFile("cry.png"), 22, 22);
                EmojiImages[":eww:"] = ResizeImage(Image.FromFile("eww.png"), 22, 22);
                EmojiImages[":like:"] = ResizeImage(like.Image, 22, 22);
                EmojiImages[":corazon:"] = ResizeImage(corazon.Image, 22, 22);
                EmojiImages[":lover:"] = ResizeImage(lover.Image, 22, 22);
                EmojiImages[":kiss:"] = ResizeImage(kiss.Image, 22, 22);
                EmojiImages[":pray:"] = ResizeImage(pray.Image, 22, 22);
                EmojiImages[":ajajaj:"] = ResizeImage(ajajaja.Image, 22, 22);
                EmojiImages[":cool:"] = ResizeImage(cool.Image, 22, 22);
            }
        }

        private async Task CargarMensajesDesdeServidor(int id_chat)
        {
            try
            {
                InicializarEmojiImages();
                panelUser.Controls.Clear();

                // Usar conexión temporal para consultas GET
                using (TcpClient tempClient = new TcpClient())
                {
                    await tempClient.ConnectAsync(ip.text, 9000);
                    NetworkStream stream = tempClient.GetStream();
                    StreamReader tempReader = new StreamReader(stream, Encoding.UTF8);
                    StreamWriter tempWriter = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                    var request = new ClientRequest
                    {
                        Command = "GET_MESSAGES",
                        ChatId = id_chat,
                        Count = 100
                    };

                    string json = JsonConvert.SerializeObject(request);
                    await tempWriter.WriteLineAsync(json);

                    // Esperar respuesta
                    string responseJson = await tempReader.ReadLineAsync();
                    var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                    if (response.Type == "MESSAGES_RESPONSE" && response.Messages != null)
                    {
                        foreach (var msg in response.Messages)
                        {
                            bool esPropio = (msg.Username == CurrentUser.Username);
                            DateTime timestamp = DateTime.Parse(msg.Timestamp);
                            DisplayMessage(msg.Username, msg.Content, timestamp, esPropio);
                        }
                    }

                    tempWriter.Close();
                    tempReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar mensajes: " + ex.Message);
            }
        }

        // Método eliminado: usar CargarMensajesDesdeServidor en su lugar

        private void DisplayMessage(string username, string mensaje, DateTime fecha, bool esPropio)
        {
            Action displayAction = () =>
            {
                RichTextBox rtb = new RichTextBox
                {
                    BackColor = esPropio ? Color.FromArgb(88, 101, 242) : Color.FromArgb(64, 68, 75),
                    ForeColor = Color.WhiteSmoke,
                    Font = new Font("Segoe UI", 11, FontStyle.Regular),
                    BorderStyle = BorderStyle.None,
                    ReadOnly = true,
                    Multiline = true,
                    WordWrap = true,
                    Tag = "mensaje",
                    Padding = new Padding(12, 8, 12, 8),
                    Margin = new Padding(0, 0, 0, 12)
                };
                
                rtb.AppendText($"{username}: ");
                RenderMessageWithEmojis(rtb, mensaje);
                rtb.AppendText($"\n{fecha:HH:mm}");
                HighlightMentions(rtb);
                rtb.Width = Math.Min(panelUser.Width - 80, rtb.PreferredSize.Width);
                rtb.Height = rtb.GetPositionFromCharIndex(rtb.TextLength).Y + 20;

                rtb.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, rtb.Width, rtb.Height, 16, 16));

                int padding = 30;
                int y = 10;
                foreach (Control ctrl in panelUser.Controls.OfType<Control>().Where(c => (string)c.Tag == "mensaje"))
                {
                    y = Math.Max(y, ctrl.Location.Y + ctrl.Height + padding);
                }

                int x = esPropio ? panelUser.Width - rtb.Width - padding : padding;
                rtb.Location = new Point(x, y);
                
                panelUser.Controls.Add(rtb);
                rtb.BringToFront();
                panelUser.ScrollControlIntoView(rtb);
            };

            if (panelUser.InvokeRequired)
            {
                try { panelUser.Invoke(displayAction); }
                catch (ObjectDisposedException) { }
            }
            else
            {
                displayAction();
            }
        }

        public async void CargarChatsUsuario(TreeView treeView)
        {
            try
            {
                // Usar conexión temporal para consultas GET
                using (TcpClient tempClient = new TcpClient())
                {
                    await tempClient.ConnectAsync(ip.text, 9000);
                    NetworkStream stream = tempClient.GetStream();
                    StreamReader tempReader = new StreamReader(stream, Encoding.UTF8);
                    StreamWriter tempWriter = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                    var request = new ClientRequest
                    {
                        Command = "GET_CHATS",
                        Username = CurrentUser.Username
                    };

                    string json = JsonConvert.SerializeObject(request);
                    await tempWriter.WriteLineAsync(json);

                    // Esperar respuesta
                    string responseJson = await tempReader.ReadLineAsync();
                    var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                    if (response.Type == "CHATS_RESPONSE" && response.Messages != null)
                    {
                        treeView.Nodes.Clear();
                        panelUser.Controls.Clear();

                        foreach (var chat in response.Messages)
                        {
                            // chat.Content tiene formato "id_chat|chat_name"
                            string[] parts = chat.Content.Split('|');
                            if (parts.Length == 2)
                            {
                                int idChat = int.Parse(parts[0]);
                                string nombreChat = parts[1];
                                TreeNode nodo = new TreeNode(nombreChat);
                                nodo.Tag = idChat;
                                treeView.Nodes.Add(nodo);
                            }
                        }
                    }

                    tempWriter.Close();
                    tempReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar chats: " + ex.Message);
            }
        }

        // Método eliminado: usar CargarChatsUsuario en su lugar

        private async void BDeleteChat_Click(object sender, EventArgs e)
        {
            if (treeViewChats.SelectedNode != null && treeViewChats.SelectedNode.Tag != null)
            {
                int idChatSeleccionado = (int)treeViewChats.SelectedNode.Tag;
                var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este chat?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        // Usar conexión temporal para DELETE
                        using (TcpClient tempClient = new TcpClient())
                        {
                            await tempClient.ConnectAsync(ip.text, 9000);
                            NetworkStream stream = tempClient.GetStream();
                            StreamReader tempReader = new StreamReader(stream, Encoding.UTF8);
                            StreamWriter tempWriter = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                            var request = new ClientRequest
                            {
                                Command = "DELETE_CHAT",
                                Username = CurrentUser.Username,
                                ChatId = idChatSeleccionado
                            };

                            string json = JsonConvert.SerializeObject(request);
                            await tempWriter.WriteLineAsync(json);

                            // Esperar respuesta
                            string responseJson = await tempReader.ReadLineAsync();
                            var response = JsonConvert.DeserializeObject<ServerResponse>(responseJson);

                            if (response.Type == "CHAT_DELETED")
                            {
                                treeViewChats.Nodes.Remove(treeViewChats.SelectedNode);
                                MessageBox.Show("Chat eliminado correctamente.");
                            }
                            else
                            {
                                MessageBox.Show(response.Content);
                            }

                            tempWriter.Close();
                            tempReader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el chat: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un chat para eliminar.");
            }
        }

        private void textBoxMessage_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panelUser_Paint(object sender, PaintEventArgs e) { }
        private void pictureUser_Click(object sender, EventArgs e) { }
        private void labelUsername_Click(object sender, EventArgs e) { }
        private void pictureSala_Click(object sender, EventArgs e) { }
        private void panelEmoji_Paint(object sender, PaintEventArgs e) { }
        private void RichMessage_TextChanged(object sender, EventArgs e) { }
        private void kiss_Click(object sender, EventArgs e) { InsertEmojiCode(":kiss:"); }
        private void happy_Click(object sender, EventArgs e) { InsertEmojiCode(":happy:"); }
        private void sad_Click(object sender, EventArgs e) { InsertEmojiCode(":sad:"); }
        private void angry_Click(object sender, EventArgs e) { InsertEmojiCode(":angry:"); }
        private void cry_Click(object sender, EventArgs e) { InsertEmojiCode(":cry:"); }
        private void eww_Click(object sender, EventArgs e) { InsertEmojiCode(":eww:"); }
        private void like_Click(object sender, EventArgs e) { InsertEmojiCode(":like:"); }
        private void corazon_Click(object sender, EventArgs e) { InsertEmojiCode(":corazon:"); }
        private void lover_Click(object sender, EventArgs e) { InsertEmojiCode(":lover:"); }
        private void pray_Click(object sender, EventArgs e) { InsertEmojiCode(":pray:"); }
        private void ajajaja_Click(object sender, EventArgs e) { InsertEmojiCode(":ajajaj:"); }
        private void cool_Click(object sender, EventArgs e) { InsertEmojiCode(":cool:"); }
        private void close_Click(object sender, EventArgs e) { panelEmoji.Visible = false; }

        private void InsertEmojiCode(string code)
        {
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, code);
            RichMessage.SelectionStart = pos + code.Length;
            RichMessage.Focus();
            panelEmoji.Visible = false;
        }

        private void RenderMessageWithEmojis(RichTextBox rtb, string message)
        {
            int idx = 0;
            bool wasReadOnly = rtb.ReadOnly;
            rtb.ReadOnly = false;

            while (idx < message.Length)
            {
                bool found = false;
                foreach (var entry in EmojiMap)
                {
                    if (message.IndexOf(entry.Key, idx, StringComparison.Ordinal) == idx)
                    {
                        if (EmojiImages.ContainsKey(entry.Key) && EmojiImages[entry.Key] != null)
                        {
                            Clipboard.SetImage(EmojiImages[entry.Key]);
                            rtb.Select(rtb.TextLength, 0);
                            rtb.Paste();
                        }
                        else
                        {
                            rtb.AppendText(entry.Key);
                        }
                        idx += entry.Key.Length;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    rtb.AppendText(message[idx].ToString());
                    idx++;
                }
            }
            rtb.ReadOnly = wasReadOnly;
        }

        private void addUser_Click(object sender, EventArgs e) 
        {
            if (treeViewChats.SelectedNode?.Tag == null)
            {
                MessageBox.Show("Por favor, selecciona un chat primero.");
                return;
            }
            int id_chat = (int)treeViewChats.SelectedNode.Tag;
            using (var formAnadirUsuario = new FormAnadirUsuario(id_chat))
            {
                if (formAnadirUsuario.ShowDialog() == DialogResult.OK)
                {
                    CargarChatMembersDesdeServidor(id_chat);
                }
            }
        }

        private void deleteUser_Click(object sender, EventArgs e)
        {
            if (treeViewChats.SelectedNode?.Tag == null)
            {
                MessageBox.Show("Por favor, selecciona un chat primero.");
                return;
            }
            int id_chat = (int)treeViewChats.SelectedNode.Tag;
            using (var formEliminarUsuario = new FormEliminarUsuario(id_chat))
            {
                if (formEliminarUsuario.ShowDialog() == DialogResult.OK)
                {
                    CargarChatMembersDesdeServidor(id_chat);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e){ }
        private void panel1_Paint(object sender, PaintEventArgs e){ }
        private void panelSalasName_Paint(object sender, PaintEventArgs e){ }
        private void FormChat_FormClosing(object sender, FormClosingEventArgs e) { DisconnectFromServer(); }
    }
}

internal static class NativeMethods
{
    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
}
