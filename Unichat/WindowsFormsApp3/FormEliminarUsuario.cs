using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp3;
using Unichat;

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
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(400, 450);
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

        private void CargarUsuariosDelChat()
        {
            listViewUsuarios.Items.Clear();

            try
            {
                using (var connection = DbConfig.GetOpenConnection())
                {
                    // Query to get users in the current chat
                    string query = @"
                    SELECT u.id_user, u.username 
                    FROM users u 
                    INNER JOIN chat_members cm ON u.id_user = cm.id_user
                    WHERE cm.id_chat = @id_chat AND u.id_user <> @current_user_id";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_chat", id_chat);
                        cmd.Parameters.AddWithValue("@current_user_id", CurrentUser.IdUser);  // Exclude current user

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

        private void BtnEliminar_Click(object sender, EventArgs e)
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
                    using (var connection = DbConfig.GetOpenConnection())
                    {
                        // Remove user from the chat
                        string deleteQuery = "DELETE FROM chat_members WHERE id_chat = @id_chat AND id_user = @id_user";
                        using (var cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@id_chat", id_chat);
                            cmd.Parameters.AddWithValue("@id_user", id_user);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Usuario {username} eliminado del chat correctamente.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al eliminar usuario del chat: " + ex.Message);
                }
            }
        }
    }
}