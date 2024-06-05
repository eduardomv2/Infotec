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
            // Ajusta el tamaño de las columnas y filas
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvUsuarios.AutoResizeColumns();
            dgvUsuarios.AutoResizeRows();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            string nombre = txtNombre.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string numeroContacto = txtNumeroContacto.Text.Trim();
            string problemas = txtProblemasTexto.Text.Trim();
            string solucion = txtSolucionRecomendacion.Text.Trim();
            string nombreModeloEquipo = txtNombreModeloEquipo.Text.Trim();
            string Carrera = txtCarrera.Text.Trim();
            string responsable = Form1.LoggedInUser;
            DateTime fechaActual = dtpFechaActual.Value;
            DateTime fechaEntrega = dtpFechaEntrega.Value;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(matricula) || string.IsNullOrEmpty(numeroContacto) ||
                string.IsNullOrEmpty(problemas) || string.IsNullOrEmpty(solucion) || string.IsNullOrEmpty(nombreModeloEquipo))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Verificar si la matrícula existe en la tabla Usuarios
                    string queryCheck = "SELECT COUNT(*) FROM Usuarios WHERE Matricula = @Matricula";
                    using (SqlCommand commandCheck = new SqlCommand(queryCheck, connection, transaction))
                    {
                        commandCheck.Parameters.AddWithValue("@Matricula", matricula);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count == 0)
                        {
                            // Insertar en la tabla Usuarios si la matrícula no existe
                            string queryInsertUsuarios = "INSERT INTO Usuarios (Nombre, Matricula) VALUES (@Nombre, @Matricula)";
                            using (SqlCommand commandInsertUsuarios = new SqlCommand(queryInsertUsuarios, connection, transaction))
                            {
                                commandInsertUsuarios.Parameters.AddWithValue("@Nombre", nombre);
                                commandInsertUsuarios.Parameters.AddWithValue("@Matricula", matricula);

                                commandInsertUsuarios.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insertar en la tabla EquipoServicio
                    string queryInsertEquipoServicio = "INSERT INTO EquipoServicio (Nombre, Matricula, NumeroContacto, Problemas, Solucion, NombreModeloEquipo, Responsable, FechaActual, FechaEntrega, Carrera) " +
                                                       "VALUES (@Nombre, @Matricula, @NumeroContacto, @Problemas, @Solucion, @NombreModeloEquipo, @Responsable, @FechaActual, @FechaEntrega, @Carrera)";
                    using (SqlCommand commandInsertEquipoServicio = new SqlCommand(queryInsertEquipoServicio, connection, transaction))
                    {
                        commandInsertEquipoServicio.Parameters.AddWithValue("@Nombre", nombre);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@Matricula", matricula);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@NumeroContacto", numeroContacto);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@Problemas", problemas);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@Solucion", solucion);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@NombreModeloEquipo", nombreModeloEquipo);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@Responsable", responsable);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@Carrera", Carrera);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@FechaActual", fechaActual);
                        commandInsertEquipoServicio.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);

                        commandInsertEquipoServicio.ExecuteNonQuery();
                    }

                    // Confirmar la transacción
                    transaction.Commit();
                    MessageBox.Show("Datos guardados exitosamente.");
                }
                catch (Exception ex)
                {
                    // Revertir la transacción en caso de error
                    transaction.Rollback();
                    MessageBox.Show($"Error al guardar los datos: {ex.Message}");
                }
            }
        }
        

        private void NuevoRegistro_Load(object sender, EventArgs e)
        {
            // Actualizar el Label con el nombre del usuario logueado
            lblLoggedInUser.Text = Form1.LoggedInUser;

            CargarDatosUsuarios();
        }

        private void CargarDatosUsuarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Nombre, Matricula FROM Usuarios";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvUsuarios.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los datos de los usuarios: {ex.Message}");
                }
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}