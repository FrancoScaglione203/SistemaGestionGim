using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PlanNegocio
    {

        public List<Plan> listarPlanes()
        {
            List<Plan> lista = new List<Plan>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Descripcion, Maquinas, Seguimiento, Locker, DescuentoClases, Importe FROM Planes";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Plan aux = new Plan();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Maquinas = (bool)datos.Lector["Maquinas"];
                    aux.Seguimiento = (bool)datos.Lector["Seguimiento"];
                    aux.Locker = (bool)datos.Lector["Locker"];
                    aux.DescuentoClases = (int)datos.Lector["DescuentoClases"];
                    aux.Importe = (int)datos.Lector["Importe"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
