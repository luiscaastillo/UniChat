using MySql.Data.MySqlClient;
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
using Unichat;
using WindowsFormsApp3;

namespace UniChat
{
    public partial class FormChat : Form
    {
        // 🌟 MAPEO DE EMOJIS (códigos de texto a Unicode)
        private readonly Dictionary<string, string> EmojiMap = new Dictionary<string, string>
        {
            { ":happy:", "😀"},
            { ":sad:", "😔" },
            { ":angry:", "😡" },
            { ":cry:", "😭" },
            { ":eww:", "🤢" },
            { ":like:", "👍" },
            { ":corazon:", "❤️" },
            { ":lover:", "🥰" },
            { ":kiss:", "😘" },
            { ":pray:", "🙏" },
            { ":ajajaj:", "🤣" },
            { ":cool:", "😎" }
        };

        private readonly Dictionary<string, Image> EmojiImages = new Dictionary<string, Image>();

        // Asignar imágenes a EmojiImages solo una vez

        public FormChat()
        {
            InitializeComponent();
            string user = CurrentUser.Username;
            MessageBox.Show("Usuario actual: " + user);

            //Colores-fuentes de ventanas y botones
            this.BackgroundImage = Image.FromFile("back.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.FromArgb(25, 28, 31);

            //Colores de los labels
            labelUsername.ForeColor = Color.White;
            labelSalas.ForeColor = Color.White;

            //Colores de los paneles
            panelUser.BackColor = Color.FromArgb(166, 166, 166);
            panelSalas.BackColor = Color.FromArgb(166, 166, 166);
            panelName.BackColor = Color.FromArgb(25, 28, 31);
            panelEmoji.BackColor = Color.FromArgb(25, 28, 31);
            panelSalasName.BackColor = Color.FromArgb(25, 28, 31);
            treeViewChats.BackColor = Color.FromArgb(25, 28, 31);
            treeViewUsers.BackColor = Color.FromArgb(25, 28, 31);

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



            this.FormClosed += (s, e) => Application.Exit();
        }

        private void FormChat_Load(object sender, EventArgs e)
        {
            RichMessage.Text = "Escribe un mensaje";
            RichMessage.ForeColor = Color.Gray;

            RichMessage.Enter += RichMessage_Enter;
            RichMessage.Leave += RichMessage_Leave;
            RichMessage.KeyDown += RichMessage_KeyDown;

            CargarChatsUsuario(treeViewChats);
            labelUsername.Text = CurrentUser.Username;

            panelEmoji.Visible = false;
        }

        private void textBoxMessage_TextChanged(object sender, EventArgs e) { }

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

        private void label2_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panelUser_Paint(object sender, PaintEventArgs e) { }
        private void pictureUser_Click(object sender, EventArgs e) { }

        private void BEnviarMsj_Click(object sender, EventArgs e)
        {
            string mensaje = RichMessage.Text.Trim();

            if (!string.IsNullOrEmpty(mensaje) && mensaje != "Escribe un mensaje")
            {
                if (treeViewChats.SelectedNode?.Tag != null)
                {
                    int id_chat = (int)treeViewChats.SelectedNode.Tag;
                    SaveMessageWithMentions(id_chat, mensaje);

                    string username = CurrentUser.Username;
                    DateTime fecha = DateTime.Now;

                    // Mensaje propio: azul Discord
                    RichTextBox rtb = new RichTextBox
                    {
                        BackColor = Color.FromArgb(88, 101, 242), // Azul Discord
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

                    // Bordes redondeados (opcional)
                    rtb.Region = System.Drawing.Region.FromHrgn(
                        NativeMethods.CreateRoundRectRgn(0, 0, rtb.Width, rtb.Height, 16, 16));

                    int padding = 30;
                    int y = 10;
                    foreach (Control ctrl in panelUser.Controls.OfType<Control>().Where(c => (string)c.Tag == "mensaje"))
                    {
                        y = Math.Max(y, ctrl.Location.Y + ctrl.Height + padding);
                    }

                    // Alinear a la derecha (usuario actual)
                    int x = panelUser.Width - rtb.Width - padding;
                    rtb.Location = new Point(x, y);
                    panelUser.Controls.Add(rtb);
                    rtb.BringToFront();

                    panelUser.ScrollControlIntoView(rtb);
                }
            }

            RichMessage.Text = "";
            RichMessage.ForeColor = Color.Gray;
            RichMessage.Focus();
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
            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    string msgQuery = "INSERT INTO messages (content, sendingDate, id_user, id_chat ) VALUES  (@message, NOW(), @senderId, @id_chat)";
                    using (var cmd = new MySqlCommand(msgQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_chat", id_chat);
                        cmd.Parameters.AddWithValue("@senderId", CurrentUser.IdUser);
                        cmd.Parameters.AddWithValue("@message", message);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar el mensaje: " + ex.Message);
            }
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

        private void labelUsername_Click(object sender, EventArgs e) { }
        private void pictureSala_Click(object sender, EventArgs e) { }

        private void BLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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

        private void treeViewChats_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                int idChatSeleccionado = (int)e.Node.Tag;
                CargarMensajesChat(idChatSeleccionado);
                CargarChatMembers(idChatSeleccionado);
            }
        }
        private void CargarChatMembers(int id_chat)
        {
            treeViewUsers.Nodes.Clear();

            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    string query = @"
                SELECT u.username
                FROM users u
                INNER JOIN chat_members cm ON u.id_user = cm.id_user
                WHERE cm.id_chat = @id_chat";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_chat", id_chat);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string username = reader["username"].ToString();
                                TreeNode userNode = new TreeNode(username);
                                treeViewUsers.Nodes.Add(userNode);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar los miembros del chat: " + ex.Message);
            }
        }

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

        private void CargarMensajesChat(int id_chat)
        {
            InicializarEmojiImages();
            foreach (Control ctrl in panelUser.Controls.OfType<Control>().Where(c => (string)c.Tag == "mensaje").ToList())
            {
                panelUser.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    string query = @"
                SELECT m.content, m.sendingDate, m.id_user, u.username
                FROM messages m
                INNER JOIN users u ON m.id_user = u.id_user
                WHERE m.id_chat = @id_chat
                ORDER BY m.sendingDate ASC";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_chat", id_chat);

                        using (var reader = cmd.ExecuteReader())
                        {
                            int padding = 30;
                            int y = 10;

                            while (reader.Read())
                            {
                                string contenido = reader["content"].ToString();
                                DateTime fecha = Convert.ToDateTime(reader["sendingDate"]);
                                int idUser = Convert.ToInt32(reader["id_user"]);
                                string username = reader["username"].ToString();

                                bool esPropio = (idUser == CurrentUser.IdUser);

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
                                RenderMessageWithEmojis(rtb, contenido);
                                rtb.AppendText($"\n{fecha:HH:mm}");
                                HighlightMentions(rtb);
                                rtb.Width = Math.Min(panelUser.Width - 80, rtb.PreferredSize.Width);
                                rtb.Height = rtb.GetPositionFromCharIndex(rtb.TextLength).Y + 20;

                                // Bordes redondeados (opcional)
                                rtb.Region = System.Drawing.Region.FromHrgn(
                                    NativeMethods.CreateRoundRectRgn(0, 0, rtb.Width, rtb.Height, 16, 16));

                                int x = esPropio
                                    ? panelUser.Width - rtb.Width - padding
                                    : padding;

                                rtb.Location = new Point(x, y);
                                panelUser.Controls.Add(rtb);
                                rtb.BringToFront();

                                y += rtb.Height + padding;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar los mensajes: " + ex.Message);
            }
        }

        public void CargarChatsUsuario(TreeView treeView)
        {
            treeView.Nodes.Clear();
            panelUser.Controls.Clear();
            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    string query = @"
                SELECT c.id_chat, c.chat_name
                FROM chats c
                INNER JOIN chat_members cm ON c.id_chat = cm.id_chat
                WHERE cm.id_user = @id_user";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_user", CurrentUser.IdUser);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idChat = Convert.ToInt32(reader["id_chat"]);
                                string nombreChat = reader["chat_name"].ToString();

                                TreeNode nodo = new TreeNode(nombreChat);
                                nodo.Tag = idChat;
                                treeView.Nodes.Add(nodo);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar los chats: " + ex.Message);
            }
        }

        private void BDeleteChat_Click(object sender, EventArgs e)
        {
            if (treeViewChats.SelectedNode != null && treeViewChats.SelectedNode.Tag != null)
            {
                int idChatSeleccionado = (int)treeViewChats.SelectedNode.Tag;
                var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este chat?", "Confirmar eliminación", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = DbConfig.GetOpenConnection())
                        {
                            string query = "DELETE FROM chats WHERE id_chat = @id_chat AND admin_id = @admin_id";
                            using (var cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@id_chat", idChatSeleccionado);
                                cmd.Parameters.AddWithValue("@admin_id", CurrentUser.IdUser);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    treeViewChats.Nodes.Remove(treeViewChats.SelectedNode);
                                    MessageBox.Show("Chat eliminado correctamente.");
                                }
                                else
                                {
                                    MessageBox.Show("No tienes permiso para eliminar este chat o el chat no existe.");
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
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

        // Método auxiliar para insertar el código en la posición del cursor
        private void InsertEmojiCode(string code)
        {
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, code);
            RichMessage.SelectionStart = pos + code.Length;
            RichMessage.Focus();
            panelEmoji.Visible = false;
        }

        // Renderiza los emojis como imágenes en el RichTextBox
        private void RenderMessageWithEmojis(RichTextBox rtb, string message)
        {
            int idx = 0;
            bool wasReadOnly = rtb.ReadOnly;
            rtb.ReadOnly = false; // Permitir pegar imágenes

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

            rtb.ReadOnly = wasReadOnly; // Restaurar estado original
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
                    // Recargar la lista de miembros del chat
                    CargarChatMembers(id_chat);
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
                    // Recargar la lista de miembros del chat
                    CargarChatMembers(id_chat);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelSalasName_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

internal static class NativeMethods
{
    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
}
