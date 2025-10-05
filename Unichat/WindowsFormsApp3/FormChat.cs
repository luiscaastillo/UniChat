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

namespace UniChat
{
    public partial class FormChat : Form
    {
        public FormChat()
        {
            InitializeComponent();

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
            treeViewChats.Font = new Font("Century Gothic", 9, FontStyle.Bold);
            treeViewChats.ForeColor = Color.White;

            //TextBox de enviar mensaje
            textBoxMessage.Font = new Font("Century Gothic", 9, FontStyle.Regular);

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
            //Cambiar el texto y color de los TextBox al iniciar
            textBoxMessage.Text = "Escribe un mensaje";
            textBoxMessage.ForeColor = Color.Gray;

            //Cargar las funciones de textBoxMessage
            textBoxMessage.Enter += textBoxMessage_Enter;
            textBoxMessage.Leave += textBoxMessage_Leave;
            textBoxMessage.KeyDown += textBoxMessage_KeyDown;

            //Implementación del TreeView
            treeViewChats.Nodes.Clear(); // Limpiar nodos existentes
            
            // Agregar un nodo raíz con hijos
          
        }
        private void textBoxMessage_TextChanged(object sender, EventArgs e)
        {

        }
        //Con esta función se puede dar la tecla Enter y se hace el envió de los mensajes
        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BEnviarMsj_Click(BEnviarMsj, EventArgs.Empty); // Llama a la función del botón
                e.SuppressKeyPress = true; // Evita que se agregue un salto de línea en el TextBox
            }
        }
        private void textBoxMessage_Enter(object sender, EventArgs e)
        {
            if (textBoxMessage.Text == "Escribe un mensaje")
            {
                textBoxMessage.Text = "";
                textBoxMessage.ForeColor = Color.Black;
            }
        }

        private void textBoxMessage_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMessage.Text))
            {
                textBoxMessage.Text = "Escribe un mensaje";
                textBoxMessage.ForeColor = Color.Gray;
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

        private void BEnviarMsj_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aqui implementar el envio de mensajes");

            //implementar que cuando haga clic sustraiga el contenido de textBoxMessage
            //y genere un label

            //Aqui se generan los chats
            string mensaje = textBoxMessage.Text.Trim();
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
            textBoxMessage.Text = "";
            textBoxMessage.ForeColor = Color.Gray;
        }

        private void BEmoji_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aqui implementar lo de los emojis");
        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

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

        private void treeViewChats_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Agregar elementos
            
        }

        private void BNewChat_Click(object sender, EventArgs e)
        {
            //Aqui es para crear un nuevo chat o sala
            using (var formSala = new FormNuevaSala())
            {
                if (formSala.ShowDialog() == DialogResult.OK)
                {
                    string nombreSala = formSala.NombreSala;
                    if (!string.IsNullOrEmpty(nombreSala))
                    {
                        treeViewChats.Nodes.Add(nombreSala);
                    }
                }
            }
        }
    }
}
