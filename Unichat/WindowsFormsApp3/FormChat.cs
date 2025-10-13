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

        public FormChat()
        {
            InitializeComponent();
            string user = CurrentUser.Username;
            MessageBox.Show("Usuario actual: " + user);

            //Colores-fuentes de ventanas y botones
            //AQUI NO MUEVAN NADA, PORFA
            this.BackgroundImage = Image.FromFile("back.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.FromArgb(25, 28, 31);

            //Colores de los labels
            labelUsername.ForeColor = Color.White;
            labelSalas.ForeColor = Color.White;
            labelChats.ForeColor = Color.FromArgb(25, 28, 31);

            //Colores de los paneles
            panelUser.BackColor = Color.FromArgb(166, 166, 166);
            panelSalas.BackColor = Color.FromArgb(166, 166, 166);
            panelName.BackColor = Color.FromArgb(25, 28, 31);
            panelEmoji.BackColor = Color.FromArgb(25, 28, 31);
            panelSalasName.BackColor = Color.FromArgb(25, 28, 31);
            treeViewChats.BackColor = Color.FromArgb(25, 28, 31);

            //Botones e iconos 
            pictureUser.Image = Image.FromFile("user.png");
            pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureSala.Image = Image.FromFile("group.png");
            pictureSala.SizeMode = PictureBoxSizeMode.StretchImage;
            BEnviarMsj.Image = Image.FromFile("send.png");
            BEnviarMsj.SizeMode = PictureBoxSizeMode.StretchImage;
            BEmoji.Image = Image.FromFile("emoji.png");
            BEmoji.SizeMode = PictureBoxSizeMode.StretchImage;
            BLogOut.Image = Image.FromFile("logout.png");
            BLogOut.SizeMode = PictureBoxSizeMode.StretchImage;
            BNewChat.Image = Image.FromFile("mas.png");
            BNewChat.SizeMode = PictureBoxSizeMode.StretchImage;
            BDeleteChat.Image = Image.FromFile("menos.png");
            BDeleteChat.SizeMode = PictureBoxSizeMode.StretchImage;
            treeViewChats.Font = new Font("Century Gothic", 9, FontStyle.Bold);
            treeViewChats.ForeColor = Color.White;
            labelUsername.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            //TextBox de enviar mensaje
            RichMessage.Font = new Font("Century Gothic", 9, FontStyle.Regular);

            //Aplicar cuando se cierre el FormChat, se cierre toda la aplicación
            this.FormClosed += (s, e) => Application.Exit();

            //NO ME MUEVAN ESTAS
            //Enter y Level es para las instrucciones de los textBox
            //KeyDown si quiero enviar un mensaje con enter en vez de presionar BEnviarMsj

            //PA QUE NO SE EQUIVOQUEN 
            //En BEnviarMsj_Click esta lo de enviar mensajes ya para que solo lo guarden en la base
            //Con panelUser cambian todo lo de los chats (ahi estan en conjunto, todos los botones de ahi) lo pueden conectar con cada dif. chat
            //Conectar las salas con cada dif chat

            //COSAS QUE FALTAN A REGI
            //acomodar pa q cuando se expanda la ventana se acomoden

            //COSAS FALTAN
            //Cuando al darle click a un chat de la treeview se carguen los mensajes de ese chat

        }

        private void FormChat_Load(object sender, EventArgs e)
        {
            // Cambiar el texto y color de los RichMessage al iniciar
            RichMessage.Text = "Escribe un mensaje";
            RichMessage.ForeColor = Color.Gray;

            // Cargar las funciones de RichMessage
            RichMessage.Enter += RichMessage_Enter;
            RichMessage.Leave += RichMessage_Leave;
            RichMessage.KeyDown += RichMessage_KeyDown;

            // Implementación del TreeView
            CargarChatsUsuario(treeViewChats);
            labelUsername.Text = CurrentUser.Username;

            // Ocultar el panel de emojis al iniciar
            panelEmoji.Visible = false;

        }
        private void textBoxMessage_TextChanged(object sender, EventArgs e)
        {

        }

        //Con esta función se puede dar la tecla Enter y se hace el envió de los mensajes
        private void RichMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BEnviarMsj_Click(BEnviarMsj, EventArgs.Empty); // Llama a la función del botón
                e.SuppressKeyPress = true; // Evita que se agregue un salto de línea en el TextBox
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelUser_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureUser_Click(object sender, EventArgs e)
        {

        }

        /*
        private void BEnviarMsj_Click(object sender, EventArgs e)
        {
            //implementar que cuando haga clic sustraiga el contenido de textBoxMessage
            //y genere un label

            //Aqui se generan los chats
            string mensaje = RichMessage.Text.Trim();
            //Mostrar el emoji si se agrega

            if (!string.IsNullOrEmpty(mensaje) && mensaje != "Escribe un mensaje")
            {
                Label nuevoLabel = new Label();
                nuevoLabel.Text = mensaje;
                nuevoLabel.AutoSize = true;
                nuevoLabel.ForeColor = Color.White;
                nuevoLabel.BackColor = Color.FromArgb(25, 28, 31);
                nuevoLabel.Font = new Font("Century Gothic", 9, FontStyle.Regular);

                panelUser.Controls.Add(nuevoLabel);

                // Reposiciona todos los labels desde abajo hacia arriba
                int padding = 30;
                int y = panelUser.Height - padding;
                foreach (Control ctrl in panelUser.Controls.OfType<Label>().Reverse())
                {
                    y -= ctrl.Height + padding;
                    ctrl.Location = new Point(panelUser.Width - ctrl.Width - padding, y); // Usa Width en vez de PreferredWidth
                }
            }

            // Limpia el textbox para el siguiente mensaje
            RichMessage.Text = "";
            RichMessage.ForeColor = Color.Gray;
        }*/


        private void BEnviarMsj_Click(object sender, EventArgs e)
        {
            string mensaje = RichMessage.Text.Trim();

            if (!string.IsNullOrEmpty(mensaje) && mensaje != "Escribe un mensaje")
            {
                //modifica por el Unicode
                foreach (var entry in EmojiMap)
                {
                    // Reemplaza todas las instancias del código de texto por el carácter Unicode
                    RichMessage.Text = RichMessage.Text.Replace(entry.Key, entry.Value);
                }

                RichMessage.SelectAll();
                RichMessage.SelectionColor = Color.White;
                RichMessage.DeselectAll(); // Opcional, para quitar la selección

                // Crear un nuevo RichTextBox (la "burbuja" del mensaje)
                RichTextBox nuevoRTB = CrearNuevoRTB(RichMessage, panelUser.Width - 40);

                panelUser.Controls.Add(nuevoRTB);

                //Posicionamiento
                int padding = 30;
                int y = panelUser.Height - padding;

                // Iteramos en orden inverso sobre todos los RichTextBox que NO son el RichMessage de entrada
                foreach (Control ctrl in panelUser.Controls.OfType<RichTextBox>().Where(c => c != RichMessage).Reverse())
                {
                    y -= ctrl.Height + padding;
                    // Posiciona el mensaje a la derecha (para simular el mensaje del usuario actual)
                    ctrl.Location = new Point(panelUser.Width - ctrl.Width - padding, y);

                    // Asegura que el texto sea blanco (aunque ya se hizo en CrearNuevoRTB)
                    ctrl.ForeColor = Color.White;
                }
            }

            RichMessage.Text = "";
            RichMessage.ForeColor = Color.Gray;

            // Devolver el foco al RichTextBox si lo deseas
            RichMessage.Focus();
        }


        // Función para extraer todo el contenido de un RichTextBox en uno nuevo
        private RichTextBox CrearNuevoRTB(RichTextBox original, int maxWidth)
        {
            RichTextBox nuevoRTB = new RichTextBox();
            nuevoRTB.BackColor = Color.FromArgb(25, 28, 31);
            nuevoRTB.ForeColor = Color.White;
            nuevoRTB.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            nuevoRTB.BorderStyle = BorderStyle.None;
            nuevoRTB.ReadOnly = true;
            nuevoRTB.Multiline = true;
            nuevoRTB.WordWrap = true;

            // Copiar texto + emojis
            nuevoRTB.Rtf = original.Rtf;

            // Ajustar tamaño según contenido
            nuevoRTB.Width = Math.Min(maxWidth, nuevoRTB.PreferredSize.Width);
            nuevoRTB.Height = nuevoRTB.GetPositionFromCharIndex(nuevoRTB.TextLength).Y + 20;

            return nuevoRTB;
        }


        private void BEmoji_Click(object sender, EventArgs e)
        {
            //Abrir una tipo panel con emojis para seleccionar
            //\U0001F60A es una carita feliz
            //Mostrar el panel de emoji cuando se seleccione el botón de emoji

            panelEmoji.Visible = true; // Alterna la visibilidad del panel de emojis

            //hacer un switch para cada emoji que se agregue
            //ejemplo: richMessage.Text += "\U0001F60A"; // Agrega un emoji de ejemplo

            happy.Image = Image.FromFile("happy.png");
            happy.SizeMode = PictureBoxSizeMode.StretchImage;

            sad.Image = Image.FromFile("sad.png");
            sad.SizeMode = PictureBoxSizeMode.StretchImage;

            angry.Image = Image.FromFile("angry.png");
            angry.SizeMode = PictureBoxSizeMode.StretchImage;

            cry.Image = Image.FromFile("cry.png");
            cry.SizeMode = PictureBoxSizeMode.StretchImage;

            eww.Image = Image.FromFile("eww.png");
            eww.SizeMode = PictureBoxSizeMode.StretchImage;

            like.Image = Image.FromFile("like.png");
            like.SizeMode = PictureBoxSizeMode.StretchImage;

            corazon.Image = Image.FromFile("corazon.png");
            corazon.SizeMode = PictureBoxSizeMode.StretchImage;

            lover.Image = Image.FromFile("lover.png");
            lover.SizeMode = PictureBoxSizeMode.StretchImage;

            kiss.Image = Image.FromFile("kiss.png");
            kiss.SizeMode = PictureBoxSizeMode.StretchImage;

            pray.Image = Image.FromFile("pray.png");
            pray.SizeMode = PictureBoxSizeMode.StretchImage;

            ajajaja.Image = Image.FromFile("risa.png");
            ajajaja.SizeMode = PictureBoxSizeMode.StretchImage;

            cool.Image = Image.FromFile("cool.png");
            cool.SizeMode = PictureBoxSizeMode.StretchImage;

            close.Image = Image.FromFile("salirEmoji.png");
            close.SizeMode = PictureBoxSizeMode.StretchImage;
            //textBoxMessage.Text += "\U0001F60A"; // Agrega un emoji de ejemplo
        }

        private void labelUsername_Click(object sender, EventArgs e)
        {
            //Implementar que el usuario que tecleo se cambie por el que este en la base de datos

        }

        private void pictureSala_Click(object sender, EventArgs e)
        {

        }

        //Este es el Boton para Salir
        private void BLogOut_Click(object sender, EventArgs e)
        {
            //Aplicar cuando se cierre el FormChat, se cierre toda la aplicación
            Application.Exit();
        }


        //Boton para crear un nuevo chat (Sala)
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
                        nodo.Tag = id_chat; // Guarda el id en el nodo
                        treeViewChats.Nodes.Add(nodo);
                    }
                }
            }
        }

        private void treeViewChats_AfterSelect(object sender, TreeViewEventArgs e) //Afterselect del treeview w
        {
            if (e.Node.Tag != null)
            {
                int idChatSeleccionado = (int)e.Node.Tag;
                // Ahora puedes usar idChatSeleccionado para consultar la base de datos
                // y cargar los mensajes de ese chat
            }
        }

        //Metodo para cargar los chats del treeview al cargar el formulario bro


        public void CargarChatsUsuario(TreeView treeView)
        {
            treeView.Nodes.Clear();
            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    string query = "SELECT id_chat, chatname FROM chats WHERE admin = @admin";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@admin", CurrentUser.IdUser);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idChat = Convert.ToInt32(reader["id_chat"]);
                                string nombreChat = reader["chatname"].ToString();

                                TreeNode nodo = new TreeNode(nombreChat);
                                nodo.Tag = idChat; // Guarda el id en el nodo
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
            //Implementar
            // -> el elemento que este seleccionado en el treeview
            // -> eliminar de la base de datos
            // -> validar que el admin del chat solo pueda eliminar el chat

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
                            string query = "DELETE FROM chats WHERE id_chat = @idChat AND admin = @admin";
                            using (var cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@idChat", idChatSeleccionado);
                                cmd.Parameters.AddWithValue("@admin", CurrentUser.IdUser);
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

        private void panelEmoji_Paint(object sender, PaintEventArgs e)
        {
            //si toca este boton quiero que llame al Dictionary y busque el emoji que coincida con el nombre del picturebox
            // y lo agregue al richtextbox

        }

        private void RichMessage_TextChanged(object sender, EventArgs e)
        {

        }

        /*
        private void Happy_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath = "happy.png"; // ruta relativa
                if (!System.IO.File.Exists(imagePath))
                {
                    MessageBox.Show("No se encontró el archivo del emoji: " + imagePath);
                    return;
                }

                Image emoji = Image.FromFile(imagePath);
                InsertImageIntoRichTextBox(RichMessage, emoji);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar emoji: " + ex.Message);
            }

            panelEmoji.Visible = false; // Oculta el panel después de seleccionar un emoji
        }

        private void InsertImageIntoRichTextBox(RichTextBox rtb, Image image)
        {
            if (image == null || rtb == null) return;

            // 🔹 Ajusta estos valores a gusto
            int customWidth = 15;  // ancho deseado (puedes probar 25, 30, 50…)
            int customHeight = 15; // alto deseado (igual al ancho para mantenerlo cuadrado)
            /*
            Image resized = new Bitmap(image, new Size(customWidth, customHeight));

            Clipboard.SetImage(resized);
            rtb.Paste();

            resized.Dispose();
            
            using (Image resized = new Bitmap(image, new Size(customWidth, customHeight)))
            {
                bool wasReadOnly = rtb.ReadOnly;
                rtb.ReadOnly = false;  // Permitir edición temporalmente
                rtb.Focus();           // Asegurar que el control tenga el foco

                Clipboard.SetImage(resized);
                rtb.Paste();

                rtb.ReadOnly = wasReadOnly;
            }
        }
        */

        private void happy_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":happy:") ? EmojiMap[":happy:"] : "😀";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }
        private void sad_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":sad:") ? EmojiMap[":sad:"] : "😔";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void angry_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":angry:") ? EmojiMap[":angry:"] : "😡";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }
        private void close_Click(object sender, EventArgs e)
        {
            panelEmoji.Visible = false; // Oculta el panel de emojis
        }

        private void cry_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":cry:") ? EmojiMap[":cry:"] : "😭";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void eww_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":eww:") ? EmojiMap[":eww:"] : "🤢";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void like_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":like:") ? EmojiMap[":like:"] : "👍";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void corazon_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":corazon:") ? EmojiMap[":corazon:"] : "❤️";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void lover_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":lover:") ? EmojiMap[":lover:"] : "🥰";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void kiss_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":kiss:") ? EmojiMap[":kiss:"] : "😘";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void pray_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":pray:") ? EmojiMap[":pray:"] : "🙏";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void ajajaja_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":ajajaj:") ? EmojiMap[":ajajaj:"] : "🤣";
        
            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }

        private void cool_Click(object sender, EventArgs e)
        {
            // Obtiene el emoji Unicode desde el diccionario
            string emojiUnicode = EmojiMap.ContainsKey(":cool:") ? EmojiMap[":cool:"] : "😎";

            // Inserta el emoji en la posición actual del cursor
            int pos = RichMessage.SelectionStart;
            RichMessage.Text = RichMessage.Text.Insert(pos, emojiUnicode);
            RichMessage.SelectionStart = pos + emojiUnicode.Length;
            RichMessage.Focus();

            panelEmoji.Visible = false;
        }


        /*
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
         */
    }
}
