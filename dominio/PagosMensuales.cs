using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class PagosMensuales
    {
        public int Mes { get; set; }
        public int Anio{ get; set; }
        public Cupon cupon { get; set; }
        public int Id_cupon { get; set; }
        public Usuario usuario { get; set; }
        public int Id_usuario { get; set; }
        public int Importe { get; set; }
        public DateTime FechaPago { get; set; }
        //public string Dni { get; set; }
        public InscripcionClase inscripcionClase { get; set; }
        public int Id_inscripcionClase { get; set; }

        public bool Pago { get; set; }
    }
}
