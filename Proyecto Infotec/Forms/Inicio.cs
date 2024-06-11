using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_Infotec
{
    public partial class Inicio : Form
    {
        // Cadena de conexión a la base de datos
        private string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            CargarDatos();

            dgvInicio.RowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); // Gris claro para el fondo de las filas
            dgvInicio.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220); // Gris más oscuro para las filas impares

            foreach (DataGridViewColumn column in dgvInicio.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

           
          
        }

        private void CargarDatos()
        {
            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();

                    // Crear adaptador de datos
                    // RETIRÉ ID HASTA SOLUCUONARLO
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Nombre, Matricula, Carrera, Contacto, Problemas, Solucion, Equipo, Responsable, FechaActual, FechaEntrega FROM EquipoServicio", connection);

                    // Crear DataTable para almacenar los datos
                    DataTable table = new DataTable();

                    // Llenar DataTable con los datos de la consulta
                    adapter.Fill(table);

                    // Asignar DataTable al DataGridView
                    dgvInicio.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Consultar los datos de la tabla EquipoServicio
            List<EquipoServicio> equipos = ConsultarEquipos();

            // Serializar la lista de equipos a formato JSON
            string json = JsonConvert.SerializeObject(equipos, Formatting.Indented);

            try
            {
                // Mostrar el cuadro de diálogo de selección de archivos
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
                saveFileDialog.Title = "Guardar como archivo JSON";
                saveFileDialog.ShowDialog();

                // Si el usuario elige una ubicación y hace clic en "Guardar"
                if (saveFileDialog.FileName != "")
                {
                    // Guardar el JSON en el archivo seleccionado por el usuario
                    File.WriteAllText(saveFileDialog.FileName, json);
                    MessageBox.Show("Datos guardados correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el archivo JSON: {ex.Message}");
            }
        }
        private List<EquipoServicio> ConsultarEquipos()
        {
            List<EquipoServicio> equipos = new List<EquipoServicio>();

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();

                    // Crear adaptador de datos
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Nombre, Matricula, Carrera, Contacto, Problemas, Solucion, Equipo, Responsable, FechaActual, FechaEntrega FROM EquipoServicio", connection);

                    // Crear DataTable para almacenar los datos
                    DataTable table = new DataTable();

                    // Llenar DataTable con los datos de la consulta
                    adapter.Fill(table);

                    // Convertir DataTable a lista de objetos EquipoServicio
                    foreach (DataRow row in table.Rows)
                    {
                        EquipoServicio equipo = new EquipoServicio
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Nombre = Convert.ToString(row["Nombre"]),
                            Matricula = Convert.ToString(row["Matricula"]),
                            Carrera = Convert.ToString(row["Carrera"]),
                            //NumeroContacto = Convert.ToString(row["NumeroContacto"]),
                            Problemas = Convert.ToString(row["Problemas"]),
                            Solucion = Convert.ToString(row["Solucion"]),
                            Equipo = Convert.ToString(row["Equipo"]),
                            Responsable = Convert.ToString(row["Responsable"]),
                            FechaActual = Convert.ToDateTime(row["FechaActual"]),
                            FechaEntrega = Convert.ToDateTime(row["FechaEntrega"])
                        };
                        equipos.Add(equipo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar datos: {ex.Message}");
            }

            return equipos;
        }

        private void dgvInicio_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }
    }
}
