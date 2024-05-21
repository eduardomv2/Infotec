﻿using System;
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

namespace Proyecto_Infotec.Forms
{
    public partial class InicioSesion : Form

    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Cadena de conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            // Obtener los datos del formulario
            string nombre = txtNombre.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string carrera = comboBoxCarrera.SelectedItem?.ToString(); // Obtener el valor seleccionado del ComboBox
            string usuario = txtUsuario.Text.Trim();
            string contraseña = PasswordTxt.Text.Trim();
            string confirmarContraseña = PasswordConfirmTxt.Text.Trim();

            // Validar que todos los campos estén completos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(matricula) ||
                string.IsNullOrEmpty(carrera) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(confirmarContraseña))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Validar que las contraseñas coincidan
            if (contraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                // Abrir conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Crear la consulta SQL con parámetros
                    string query = "INSERT INTO Login (Nombre, Matricula, Carrera, Usuario, Contraseña) " +
                                   "VALUES (@Nombre, @Matricula, @Carrera, @Usuario, @Contraseña)";

                    // Crear y configurar el comando SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Matricula", matricula);
                        command.Parameters.AddWithValue("@Carrera", carrera);
                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Contraseña", contraseña);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Verificar si se insertaron filas
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Datos agregados correctamente.");
                            // Limpiar los campos del formulario
                            txtNombre.Clear();
                            txtMatricula.Clear();
                            comboBoxCarrera.SelectedIndex = -1;
                            txtUsuario.Clear();
                            PasswordTxt.Clear();
                            PasswordConfirmTxt.Clear();
                            // Redirigir a la ventana de inicio de sesión
                            this.Hide();
                            Form1 f1 = new Form1();  
                            f1.Show();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar los datos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //back to login form
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
            
        }
    }
} 



