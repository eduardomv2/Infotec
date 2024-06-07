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
using System.Configuration;

namespace Proyecto_Infotec
{
    public partial class NuevoRegistro : Form
    {
        public NuevoRegistro()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verificar si hay campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtMatricula.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroContacto.Text) ||
                string.IsNullOrWhiteSpace(txtProblemasTexto.Text) ||
                string.IsNullOrWhiteSpace(txtSolucionRecomendacion.Text) ||
                string.IsNullOrWhiteSpace(txtNombreModeloEquipo.Text) ||
                string.IsNullOrWhiteSpace(txtCarrera.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryEquipoServicio = "INSERT INTO EquipoServicio (Nombre, Matricula, NumeroContacto, Problemas, Solucion, NombreModeloEquipo, Responsable, FechaActual, FechaEntrega, Carrera) " +
                                             "VALUES (@Nombre, @Matricula, @NumeroContacto, @Problemas, @Solucion, @NombreModeloEquipo, @Responsable, @FechaActual, @FechaEntrega, @Carrera)";

                string queryUsuarios = "INSERT INTO Usuarios (Nombre, Matricula, Carrera, Contacto) " +
                                       "VALUES (@Nombre, @Matricula, @Carrera, @Contacto)";

                using (SqlCommand commandEquipoServicio = new SqlCommand(queryEquipoServicio, connection))
                using (SqlCommand commandUsuarios = new SqlCommand(queryUsuarios, connection))
                {
                    commandEquipoServicio.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    commandEquipoServicio.Parameters.AddWithValue("@Matricula", txtMatricula.Text.Trim());
                    commandEquipoServicio.Parameters.AddWithValue("@NumeroContacto", txtNumeroContacto.Text.Trim());
                    commandEquipoServicio.Parameters.AddWithValue("@Problemas", txtProblemasTexto.Text.Trim());
                    commandEquipoServicio.Parameters.AddWithValue("@Solucion", txtSolucionRecomendacion.Text.Trim());
                    commandEquipoServicio.Parameters.AddWithValue("@NombreModeloEquipo", txtNombreModeloEquipo.Text.Trim());
                    commandEquipoServicio.Parameters.AddWithValue("@Responsable", Form1.LoggedInUser);
                    commandEquipoServicio.Parameters.AddWithValue("@FechaActual", dtpFechaActual.Value);
                    commandEquipoServicio.Parameters.AddWithValue("@FechaEntrega", dtpFechaEntrega.Value);
                    commandEquipoServicio.Parameters.AddWithValue("@Carrera", txtCarrera.Text.Trim());


                    commandUsuarios.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    commandUsuarios.Parameters.AddWithValue("@Matricula", txtMatricula.Text.Trim());
                    commandUsuarios.Parameters.AddWithValue("@Carrera", txtCarrera.Text.Trim());
                    commandUsuarios.Parameters.AddWithValue("@Contacto", txtNumeroContacto.Text.Trim());

                    try
                    {
                        connection.Open();

                        // Verificar si la matrícula ya existe en la tabla Usuarios
                        string queryVerificarMatricula = "SELECT COUNT(*) FROM Usuarios WHERE Matricula = @Matricula";
                        using (SqlCommand commandVerificar = new SqlCommand(queryVerificarMatricula, connection))
                        {
                            commandVerificar.Parameters.AddWithValue("@Matricula", txtMatricula.Text.Trim());
                            int count = (int)commandVerificar.ExecuteScalar();

                            if (count == 0)
                            {
                                // Insertar en la tabla Usuarios si la matrícula no existe
                                commandUsuarios.ExecuteNonQuery();
                            }
                        }

                        // Insertar en la tabla EquipoServicio
                        commandEquipoServicio.ExecuteNonQuery();

                        MessageBox.Show("Datos guardados exitosamente.");
                        // Limpiar los campos del formulario
                        txtNombre.Clear();
                        txtMatricula.Clear();
                        txtNumeroContacto.Clear();
                        txtProblemasTexto.Clear();
                        txtSolucionRecomendacion.Clear();
                        txtNombreModeloEquipo.Clear();
                        txtCarrera.Clear();
                        dtpFechaActual.Value = DateTime.Now;
                        dtpFechaEntrega.Value = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar los datos: {ex.Message}");
                    }
                }
            }
        }
        

        private void NuevoRegistro_Load(object sender, EventArgs e)
        {
            // Actualizar el Label con el nombre del usuario logueado
            lblLoggedInUser.Text = Form1.LoggedInUser;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;
            string matricula = txtMatricula.Text.Trim();

            if (string.IsNullOrEmpty(matricula))
            {
                MessageBox.Show("Por favor, ingrese una matrícula.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryBuscarUsuario = "SELECT Nombre, Carrera, Contacto FROM Usuarios WHERE Matricula = @Matricula";

                using (SqlCommand commandBuscar = new SqlCommand(queryBuscarUsuario, connection))
                {
                    commandBuscar.Parameters.AddWithValue("@Matricula", matricula);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = commandBuscar.ExecuteReader();

                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtCarrera.Text = reader["Carrera"].ToString();
                            txtNumeroContacto.Text = reader["Contacto"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un usuario con esa matrícula.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al buscar la matrícula: {ex.Message}");
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dtpFechaActual_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}