using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Plan
    {
        public int Id { get; set; }
        public string Descrípcion { get; set; }
        public bool Maquinas { get; set; }
        public bool Seguimiento { get; set; }
        public bool DescuentoClases { get; set; }
        public bool Locker { get; set; }
        public int Importe { get; set; }
    }
}
