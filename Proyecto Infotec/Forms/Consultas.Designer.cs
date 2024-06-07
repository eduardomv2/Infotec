namespace Proyecto_Infotec
{
    partial class Consultas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvReparaciones = new System.Windows.Forms.DataGridView();
            this.dateTimePickerInicio = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFin = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.numMinMaquinas = new System.Windows.Forms.NumericUpDown();
            this.numMaxMaquinas = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReparaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinMaquinas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMaquinas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReparaciones
            // 
            this.dgvReparaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReparaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReparaciones.Location = new System.Drawing.Point(13, 129);
            this.dgvReparaciones.Name = "dgvReparaciones";
            this.dgvReparaciones.Size = new System.Drawing.Size(942, 414);
            this.dgvReparaciones.TabIndex = 2;
            // 
            // dateTimePickerInicio
            // 
            this.dateTimePickerInicio.Location = new System.Drawing.Point(25, 28);
            this.dateTimePickerInicio.Name = "dateTimePickerInicio";
            this.dateTimePickerInicio.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerInicio.TabIndex = 3;
            // 
            // dateTimePickerFin
            // 
            this.dateTimePickerFin.Location = new System.Drawing.Point(245, 28);
            this.dateTimePickerFin.Name = "dateTimePickerFin";
            this.dateTimePickerFin.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFin.TabIndex = 4;
            // 
            // btnBuscar
            // 
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.Location = new System.Drawing.Point(678, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(230, 22);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "iconButton1";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // numMinMaquinas
            // 
            this.numMinMaquinas.Location = new System.Drawing.Point(460, 28);
            this.numMinMaquinas.Name = "numMinMaquinas";
            this.numMinMaquinas.Size = new System.Drawing.Size(64, 20);
            this.numMinMaquinas.TabIndex = 8;
            // 
            // numMaxMaquinas
            // 
            this.numMaxMaquinas.Location = new System.Drawing.Point(530, 28);
            this.numMaxMaquinas.Name = "numMaxMaquinas";
            this.numMaxMaquinas.Size = new System.Drawing.Size(64, 20);
            this.numMaxMaquinas.TabIndex = 9;
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(35)))), ((int)(((byte)(98)))));
            this.ClientSize = new System.Drawing.Size(966, 554);
            this.Controls.Add(this.numMaxMaquinas);
            this.Controls.Add(this.numMinMaquinas);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dateTimePickerFin);
            this.Controls.Add(this.dateTimePickerInicio);
            this.Controls.Add(this.dgvReparaciones);
            this.Name = "Consultas";
            this.Text = "Consultas";
            this.Load += new System.EventHandler(this.Consultas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReparaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinMaquinas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMaquinas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReparaciones;
        private System.Windows.Forms.DateTimePicker dateTimePickerInicio;
        private System.Windows.Forms.DateTimePicker dateTimePickerFin;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.NumericUpDown numMinMaquinas;
        private System.Windows.Forms.NumericUpDown numMaxMaquinas;
    }
}