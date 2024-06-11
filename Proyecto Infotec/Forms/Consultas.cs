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
    public partial class Consultas : Form
    {
        public Consultas()
        {
            InitializeComponent();

            
        }

        private void Consultas_Load(object sender, EventArgs e)
        {
          

        }




        private void btnBuscar_Click(object sender, EventArgs e)
        {


            // Validar que los NumericUpDown no estén vacíos
            if (numMinMaquinas.Value == 0 && numMaxMaquinas.Value == 0)
            {
                MessageBox.Show("Por favor, ingrese un rango de cantidad de máquinas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener las fechas y el rango de cantidad de máquinas desde los controles
            DateTime fechaInicio = dateTimePickerInicio.Value;
            DateTime fechaFin = dateTimePickerFin.Value;
            int minMaquinas = (int)numMinMaquinas.Value;
            int maxMaquinas = (int)numMaxMaquinas.Value;

            // Cargar registros según los criterios de búsqueda
            CargarReparacionesPersonalizadas(fechaInicio, fechaFin, minMaquinas, maxMaquinas);
        }

        private void CargarReparacionesPersonalizadas(DateTime fechaInicio, DateTime fechaFin, int minMaquinas, int maxMaquinas)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;
            string query = $@"
                SELECT 
                    Responsable, 
                    Login.Nombre, 
                    Login.Matricula, 
                    Login.Carrera, 
                    COUNT(*) AS CantidadMaquinas
                FROM EquipoServicio
                INNER JOIN Login ON EquipoServicio.Responsable = Login.Usuario
                WHERE FechaActual >= @FechaInicio AND FechaEntrega <= @FechaFin
                GROUP BY Responsable, Login.Nombre, Login.Matricula, Login.Carrera
                HAVING COUNT(*) BETWEEN @MinMaquinas AND @MaxMaquinas";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    command.Parameters.AddWithValue("@MinMaquinas", minMaquinas);
                    command.Parameters.AddWithValue("@MaxMaquinas", maxMaquinas);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvReparaciones.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }
    }
}
