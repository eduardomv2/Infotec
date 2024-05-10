using Proyecto_Infotec.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto_Infotec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = ("Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=InfoTec;Integrated Security=True;TrustServerCertificate=True");  // Tu conexión a SQL Server
            string usuario = txtUser.Text;  // El nombre de usuario ingresado
            string contraseña = txtPassword.Text;  // La contraseña ingresada

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();  // Abre la conexión

                    // Consulta para obtener la contraseña del usuario ingresado
                    string query = "SELECT Contraseña FROM Login WHERE Usuario = @Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);  // Agrega parámetro

                        SqlDataReader reader = command.ExecuteReader();  // Ejecuta la consulta
                        if (reader.Read())  // Si se encontró el usuario
                        {
                            string storedPassword = reader["Contraseña"].ToString();
                            if (storedPassword == contraseña)  // Compara contraseñas
                            {
                                MessageBox.Show("Acceso concedido.");  
                            }
                            else
                            {
                                MessageBox.Show("Contraseña incorrecta.");  // Fallo
                            }
                        }
                        else
                        {
                            MessageBox.Show("Usuario no encontrado.");  // Usuario no existe
                        }
                    }
                }
                catch (Exception ex)  // Manejo de excepciones
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //close this form
            this.Hide();
            //next form
            InicioSesion f3 = new InicioSesion();
            f3.Show();
        }
    }
}
