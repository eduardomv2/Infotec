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
using FontAwesome.Sharp;
using System.Diagnostics;


namespace Proyecto_Infotec
{
    public partial class Registros : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public Registros()
        {
            InitializeComponent();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panel1.Controls.Add(leftBorderBtn);
        }

        #region ANIMACION BOTONES   
        //structs 
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();            
            }
        }
       

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        #endregion

        private void Registros_Load(object sender, EventArgs e)
        {
            
        }

        #region Pannel
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        #region METODO PARA ABRIR FORMULARIOS   
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        #endregion

        #region BotoneS menu lateral
        private void iconButton1_Click(object sender, EventArgs e)
        {
            //open form user
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new User());

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //open form nuevo registro
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new NuevoRegistro());
        }
        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Inicio());
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            
        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new Consultas());
        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);

        }

        private void iconButton5_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new MasEquiposAyudados());
        }
        #endregion

        private void iconButton6_Click(object sender, EventArgs e)
        {
            // URL de la página web que quieres abrir
            string url = "https://github.com/eduardomv2/Infotec";

            // Abre la URL en el navegador predeterminado
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }

}
