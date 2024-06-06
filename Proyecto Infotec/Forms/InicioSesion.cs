using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            // Verificar si el usuario ya existe en la base de datos
            if (!VerificarUsuarioUnico(usuario))
            {
                MessageBox.Show("El nombre de usuario ya está en uso. Por favor, elija otro.");
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

        // Método para verificar si el usuario es único en la base de datos
        private bool VerificarUsuarioUnico(string usuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;
            string query = "SELECT COUNT(*) FROM Login WHERE LTRIM(RTRIM(Usuario)) = @Usuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count == 0; // Devuelve true si el usuario es único, false si ya existe
                }
            }
        }


        private void iconButton2_Click(object sender, EventArgs e)
        {
            //back to login form
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
            
        }

        private void InicioSesion_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath borderPath = new GraphicsPath();
            int borderRadius = 30; // Ajusta el radio de las esquinas redondeadas aquí

            borderPath.AddArc(0, 0, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
            borderPath.AddArc(this.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
            borderPath.AddArc(this.Width - borderRadius, this.Height - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
            borderPath.AddArc(0, this.Height - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
            borderPath.CloseFigure();

            this.Region = new Region(borderPath);
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
           
            



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordConfirmTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
} 



