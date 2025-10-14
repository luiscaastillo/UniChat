using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp3;
using Unichat;


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
            this.Text = "Añadir Usuario al Chat";
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
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Name = "FormAnadirUsuario";
            this.ResumeLayout(false);
        }

        private void ConfigureFormUI()
        {
            this.Size = new Size(400, 450);
            this.BackColor = Color.FromArgb(25, 28, 31);

            // Create controls
            Label lblTitulo = new Label
            {
                Text = "Selecciona un usuario para añadir al chat",
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

            Button btnAnadir = new Button
            {
                Text = "Añadir",
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

        private void CargarUsuarios()
        {
            listViewUsuarios.Items.Clear();

            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    // Query to get users not in the current chat
                    string query = @"
                    SELECT id_user, username 
                    FROM users 
                    WHERE id_user NOT IN (
                        SELECT id_user 
                        FROM chat_members 
                        WHERE id_chat = @id_chat
                    )";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_chat", id_chat);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_user = Convert.ToInt32(reader["id_user"]);
                                string username = reader["username"].ToString();

                                ListViewItem item = new ListViewItem(id_user.ToString());
                                item.SubItems.Add(username);
                                listViewUsuarios.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void BtnAnadir_Click(object sender, EventArgs e)
        {
            if (listViewUsuarios.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario para añadir.");
                return;
            }

            int id_user = Convert.ToInt32(listViewUsuarios.SelectedItems[0].Text);

            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    // Add user to the chat
                    string insertQuery = "INSERT INTO chat_members (id_chat, id_user) VALUES (@id_chat, @id_user)";
                    using (var cmd = new MySqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_chat", id_chat);
                        cmd.Parameters.AddWithValue("@id_user", id_user);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Usuario añadido al chat correctamente.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al añadir usuario al chat: " + ex.Message);
            }
        }
    }
}