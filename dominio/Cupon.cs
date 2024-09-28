using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cupon
    {
        public int Id { get; set; }
        public string Codigo { get; set; }  //El codigo va a estar compuesto por 1 letra al comienzo del string seguido de 3 numeros
        public int Descuento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Activo { get; set; }
    }
}
