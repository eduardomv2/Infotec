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
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO EquipoServicio (Nombre, Matricula, NumeroContacto, Problemas, Solucion, NombreModeloEquipo, Responsable) " +
                               "VALUES (@Nombre, @Matricula, @NumeroContacto, @Problemas, @Solucion, @NombreModeloEquipo, @Responsable)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    command.Parameters.AddWithValue("@Matricula", txtMatricula.Text.Trim());
                    command.Parameters.AddWithValue("@NumeroContacto", txtNumeroContacto.Text.Trim());
                    command.Parameters.AddWithValue("@Problemas", txtProblemasTexto.Text.Trim());
                    command.Parameters.AddWithValue("@Solucion", txtSolucionRecomendacion.Text.Trim());
                    command.Parameters.AddWithValue("@NombreModeloEquipo", txtNombreModeloEquipo.Text.Trim()); // Agrega el nuevo parámetro
                    command.Parameters.AddWithValue("@Responsable", Form1.LoggedInUser); // Agrega el responsable

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Datos guardados exitosamente.");
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
    }
}