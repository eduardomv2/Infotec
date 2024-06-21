using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Infotec
{
    public partial class User : Form
    {

        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            // Obtener el nombre de usuario guardado
            string usuario = Form1.LoggedInUser;
            CargarDatosUsuario(usuario);
            CargarEquiposArreglados(usuario);

        }

        #region Metodo para que se vean los datos del usuario
        private void CargarDatosUsuario(string usuario)
        {
            // Eliminar espacios en blanco al principio y al final del nombre de usuario
            usuario = usuario.Trim();

            string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=InfoTec;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL con TRIM para manejar los espacios en blanco
                    string query = "SELECT Nombre, Matricula, Carrera, Usuario, ImagenPerfil FROM Login WHERE LTRIM(RTRIM(Usuario)) = @Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            label1.Text = reader["Nombre"].ToString();
                            label2.Text = reader["Matricula"].ToString();
                            label3.Text = reader["Carrera"].ToString();
                            label4.Text = reader["Usuario"].ToString();

                            if (reader["ImagenPerfil"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["ImagenPerfil"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    pictureBoxProfile.Image = Image.FromStream(ms);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron datos del usuario.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

        }
        #endregion

        #region Boton para guardar imagen
        private void button1_Click(object sender, EventArgs e)
        {
            // Mostrar el cuadro de diálogo de selección de archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Cargar la imagen seleccionada en el PictureBox
                pictureBoxProfile.Image = new Bitmap(openFileDialog.FileName);

                // Convertir la imagen a un arreglo de bytes
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBoxProfile.Image.Save(ms, pictureBoxProfile.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }

                // Guardar la imagen en la base de datos
                string connectionString = "Data Source=eduardomv\\SQLEXPRESS;Initial Catalog=InfoTec;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Login SET ImagenPerfil = @ImagenPerfil WHERE Usuario = @Usuario";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@ImagenPerfil", SqlDbType.VarBinary).Value = imageBytes;
                            command.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = Form1.LoggedInUser;

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Imagen de perfil guardada correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo actualizar la imagen de perfil.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar la imagen de perfil: {ex.Message}");
                    }
                }

            }
            #endregion
        }

        private void CargarEquiposArreglados(string usuario)
        {
            // Eliminar espacios en blanco al principio y al final del nombre de usuario
            usuario = usuario.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para obtener los equipos arreglados por el usuario
                    string query = "SELECT * FROM EquipoServicio WHERE Responsable = @Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dgvEquiposArreglados.DataSource = dataTable;

                            // Ajustar el tamaño de las columnas
                            foreach (DataGridViewColumn column in dgvEquiposArreglados.Columns)
                            {
                                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            }
                        }
                     
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los equipos arreglados: {ex.Message}");
                }
            }
        }

        private void pictureBoxProfile_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
