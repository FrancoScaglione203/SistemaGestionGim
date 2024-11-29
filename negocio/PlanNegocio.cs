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

        public Plan GetPlanById(int planId)
        {
            Plan plan = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Descripcion, Importe, Maquinas, Seguimiento, Locker, DescuentoClases FROM Planes WHERE Id = @PlanId";
                datos.setearConsulta(consulta);
                datos.setearParametro("@PlanId", planId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    plan = new Plan();
                    plan.Id = (int)datos.Lector["Id"];
                    plan.Descripcion = (string)datos.Lector["Descripcion"];
                    plan.Importe = (int)datos.Lector["Importe"];
                    plan.Maquinas = (bool)datos.Lector["Maquinas"];
                    plan.Seguimiento = (bool)datos.Lector["Seguimiento"];
                    plan.Locker = (bool)datos.Lector["Locker"];
                    plan.DescuentoClases = (int)datos.Lector["DescuentoClases"];
                }

                return plan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
