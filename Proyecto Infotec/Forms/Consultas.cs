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
           CargarReparaciones();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Cargar registros de personas que arreglaron 4 máquinas o menos
            CargarReparacionesPorCantidad(4);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            // Cargar registros de personas que arreglaron exactamente 5 máquinas
            CargarReparacionesPorCantidad(5);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            // Cargar registros de personas que arreglaron más de 5 máquinas
            CargarReparacionesPorCantidad(6); // Se usa 6 porque incluirá 6 o más
        }

        private void CargarReparaciones()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;
            string query = @"SELECT Responsable, COUNT(*) AS CantidadMaquinas
                            FROM EquipoServicio
                            GROUP BY Responsable";

            CargarDatos(query, connectionString);
        }

        private void CargarReparacionesPorCantidad(int cantidad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;
            string query;
            if (cantidad < 5)
            {
                query = $@"SELECT Responsable, COUNT(*) AS CantidadMaquinas
                    FROM EquipoServicio
                    GROUP BY Responsable
                    HAVING COUNT(*) < {cantidad}";
            }
            else
            {
                query = $@"SELECT Responsable, COUNT(*) AS CantidadMaquinas
                    FROM EquipoServicio
                    GROUP BY Responsable
                    HAVING COUNT(*) >= {cantidad}";
            }
            CargarDatos(query, connectionString);
        }

        private void CargarDatos(string query, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
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
