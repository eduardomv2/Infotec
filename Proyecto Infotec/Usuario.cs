using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Infotec
{
    public class EquipoServicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }

        public string Carrera { get; set; }
        public string Contacto { get; set; }
        public string Problemas { get; set; }
        public string Solucion { get; set; }
        public string Equipo { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaActual { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
