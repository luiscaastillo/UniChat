using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MySql.Data.MySqlClient; //Conexion con la Base de datos
using Mysqlx.Crud;
using UniChat;

namespace Unichat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MySqlConnection conn;
            MySqlCommand comand;
            MySqlDataReader reader = null; // Para evitar conflictos con la variable.

            conn = new MySql.Data.MySqlClient.MySqlConnection("server=127.0.0.1;uid=root;pwd=rootroot;database=unichat"); //Crea el objeto
            conn.Open();

            comand = new MySqlCommand("Select * from users order by username", conn);
            reader = comand.ExecuteReader();

            if (reader != null && reader.HasRows) // Check if reader is not null before accessing HasRows
            {
                while (reader.Read())
                {
                    string item = reader["id_user"].ToString() + " - " +
                        reader["username"].ToString() + " - " + reader["passwd"] + " - " + reader["creationDate"];

                    listBox1.Items.Add(item);
                }
            }

            reader?.Close(); // Use null-conditional operator to safely close the reader
            conn.Close();
        }
        private void Bconectar_Click(object sender, EventArgs e) //boton para abrir otro form (chat)
        {
            MessageBox.Show("La base de datos ha sido conectada"); //aun en pruebas
            
            FormChat chatForm = new FormChat();
            chatForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
