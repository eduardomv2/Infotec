using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Infotec
{
    public partial class User : Form
    {

        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            // Obtener el nombre de usuario guardado
            string usuario = Form1.LoggedInUser;
            CargarDatosUsuario(usuario);
        }

        private void CargarDatosUsuario(string usuario)
        {
            // Eliminar espacios en blanco al principio y al final del nombre de usuario
            usuario = usuario.Trim();

            string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=InfoTec;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL con TRIM para manejar los espacios en blanco
                    string query = "SELECT Nombre, Matricula, Carrera, Usuario FROM Login WHERE LTRIM(RTRIM(Usuario)) = @Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            label1.Text = reader["Nombre"].ToString();
                            label2.Text = reader["Matricula"].ToString();
                            label3.Text = reader["Carrera"].ToString();
                            label4.Text = reader["Usuario"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron datos del usuario.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

        }
    }
}
