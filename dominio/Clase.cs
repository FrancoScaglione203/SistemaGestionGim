using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Clase
    {
        public int Id { get; set; }
        public DateTime FechaHorario { get; set; }
        public int Capacidad { get; set; }
        public int Importe { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
