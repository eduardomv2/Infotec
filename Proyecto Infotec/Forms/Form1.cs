using Proyecto_Infotec.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Configuration;
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
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;
            string usuario = txtUser.Text.Trim();  // El nombre de usuario ingresado, eliminando espacios en blanco
            string contraseña = txtPassword.Text.Trim();  // La contraseña ingresada, eliminando espacios en blanco

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.");
                return;
            }

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();  // Abre la conexión

                    // Consulta para obtener la contraseña del usuario ingresado
                    string query = "SELECT Contraseña FROM Login WHERE LTRIM(RTRIM(Usuario)) = @Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = usuario;  // Agrega parámetro con tipo de dato específico

                        // Mensaje de depuración
                        MessageBox.Show($"Consultando usuario: '{usuario}'");

                        SqlDataReader reader = command.ExecuteReader();  // Ejecuta la consulta
                        if (reader.Read())  // Si se encontró el usuario
                        {
                            string storedPassword = reader["Contraseña"].ToString();
                            // Mensaje de depuración

                            //MOSTRAR CONTRASEÑA DEL USUARIO INGRESADO POR SI SE OLVIDA
                            //MessageBox.Show($"Contraseña en la base de datos: '{storedPassword}'");

                            if (storedPassword == contraseña)  // Compara contraseñas
                            {
                                MessageBox.Show("Acceso concedido.");
                                //close this form
                                this.Hide();
                                //next form
                                Registros f3 = new Registros();
                                f3.Show();
                            }
                            else
                            {
                                MessageBox.Show("Contraseña incorrecta.");
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
