using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;


namespace Proyecto_Infotec
{
    public partial class Registros : Form
    {

        // Cadena de conexión a la base de datos
        private string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

        public Registros()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Registros_Load(object sender, EventArgs e)
        {
            // Cargar datos de la tabla Login al DataGridView al cargar el formulario
            CargarDatos();
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
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Nombre, Matricula, Carrera, Usuario FROM Login", connection);

                    // Crear DataTable para almacenar los datos
                    DataTable table = new DataTable();

                    // Llenar DataTable con los datos de la consulta
                    adapter.Fill(table);

                    // Asignar DataTable al DataGridView
                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Consultar los datos de la tabla Login
            List<Usuario> usuarios = ConsultarUsuarios();

            // Serializar la lista de usuarios a formato JSON
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);

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

        private List<Usuario> ConsultarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();

                    // Crear adaptador de datos
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Nombre, Matricula, Carrera, Usuario FROM Login", connection);

                    // Crear DataTable para almacenar los datos
                    DataTable table = new DataTable();

                    // Llenar DataTable con los datos de la consulta
                    adapter.Fill(table);

                    // Convertir DataTable a lista de objetos Usuario
                    foreach (DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Nombre = Convert.ToString(row["Nombre"]),
                            Matricula = Convert.ToString(row["Matricula"]),
                            Carrera = Convert.ToString(row["Carrera"]),
                            Usuarios = Convert.ToString(row["Usuario"])
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar datos: {ex.Message}");
            }

            return usuarios;
        }
    }
    
}
