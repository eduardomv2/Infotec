using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Infotec
{
    public partial class MasEquiposAyudados : Form
    {
        public MasEquiposAyudados()
        {
            InitializeComponent();
            MostrarFotosPerfil();
        }

        private void MasEquiposAyudados_Load(object sender, EventArgs e)
        {

        }

        private void MostrarFotosPerfil()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Infotec.Properties.Settings.InfoTecConnectionString"].ConnectionString;

            // Consulta para obtener los tres primeros lugares y sus imágenes de perfil
            string query = @"SELECT TOP 3 Responsable, ImagenPerfil
                            FROM EquipoServicio
                            INNER JOIN Login ON EquipoServicio.Responsable = Login.Usuario
                            GROUP BY Responsable, ImagenPerfil
                            ORDER BY COUNT(*) DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    int pictureBoxIndex = 0; // Índice para recorrer los PictureBox

                    // Recorrer los resultados de la consulta
                    while (reader.Read())
                    {
                        string responsable = reader["Responsable"].ToString();
                        byte[] imageBytes = (byte[])reader["ImagenPerfil"];

                        // Asignar la imagen al PictureBox correspondiente
                        if (pictureBoxIndex == 0)
                        {
                            pictureBox1.Image = ByteArrayToImage(imageBytes);
                        }
                        else if (pictureBoxIndex == 1)
                        {
                            pictureBox2.Image = ByteArrayToImage(imageBytes);
                        }
                        else if (pictureBoxIndex == 2)
                        {
                            pictureBox3.Image = ByteArrayToImage(imageBytes);
                        }

                        pictureBoxIndex++; // Incrementar el índice
                    }

                    // Cerrar la conexión y liberar recursos
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar las imágenes de perfil: {ex.Message}");
                }
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
