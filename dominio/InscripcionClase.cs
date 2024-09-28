using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class InscripcionClase
    {
        public int Id { get; set; }
        public Usuario usuario { get; set; }
        public int Id_usuario { get; set; }
        public Clase clase { get; set; }
        public int Id_clase { get; set; }
        public Plan plan { get; set; }
        public int Id_plan { get; set; }
        public int DescuentoPlan { get; set; }
        public bool Cancelado { get; set; }
    }
}
