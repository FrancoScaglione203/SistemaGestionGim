﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string clave { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public int Id_tipoUsuario { get; set; }
        //public string Dni { get; set; }
        public Plan plan { get; set; }
        public int Id_plan { get; set; }

        public bool Activo { get; set; }
    }
}
