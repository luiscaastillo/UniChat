using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //Conexion con la Base de datos
using Mysqlx.Crud;
using Unichat;
using WindowsFormsApp3;

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

        private void BCrear_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxSala.Text))
            {
                NombreSala = textBoxSala.Text.Trim();
                IdChatCreado = CrearNuevaSala(NombreSala); // Guarda el id
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
                    string query = @"INSERT INTO chats (chatname, admin, n_msg, n_members)
                             VALUES (@chatname, @admin, @n_msg, @n_members);
                             SELECT LAST_INSERT_ID();"; //Query para poder ponerle un tagsito al treeview jiji

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@chatname", nombreSala);
                        cmd.Parameters.AddWithValue("@admin", CurrentUser.IdUser);
                        cmd.Parameters.AddWithValue("@n_msg", 0);
                        cmd.Parameters.AddWithValue("@n_members", 1);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int idChat))
                        {
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
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al crear la sala: " + ex.Message);
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
