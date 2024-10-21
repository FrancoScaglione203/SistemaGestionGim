using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClaseNegocio
    {

        public List<Clase> listarClases()
        {
            List<Clase> lista = new List<Clase>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, FechaHorario, Capacidad, Importe, Descripcion, Activo FROM Clases";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Clase aux = new Clase();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.FechaHorario = (DateTime)datos.Lector["FechaHorario"];
                    aux.Capacidad = (int)datos.Lector["Capacidad"];
                    aux.Importe = (int)datos.Lector["Importe"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    if (aux.Activo)
                    {
                        lista.Add(aux);
                    }
                }

                return lista;
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

        public void ActualizarEstadoClases() 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Asegúrate de que el procedimiento almacenado esté correctamente configurado en la base de datos.
                datos.setearProcedimiento("ActualizarEstadoClases");
                datos.ejecutarAccion();
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

        public int InsertarNuevo(Clase nuevaClase)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Asegúrate de que el procedimiento almacenado esté correctamente configurado en la base de datos.
                datos.setearProcedimiento("insertarNuevaClase");
                datos.setearParametro("@FechaHorario", nuevaClase.FechaHorario);
                datos.setearParametro("@Capacidad", nuevaClase.Capacidad);
                datos.setearParametro("@Importe", nuevaClase.Importe);
                datos.setearParametro("@Descripcion", nuevaClase.Descripcion);
                datos.setearParametro("@Activo", true);

                // Ejecutar la acción y obtener el ID de la clase insertada.
                return datos.ejecutarAccionScalar();
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


        public Clase ClaseById(int claseId)
        {
            Clase clase = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, FechaHorario, Capacidad, Importe, Descripcion, Activo FROM Clases WHERE Id = @ClaseId";
                datos.setearConsulta(consulta);
                datos.setearParametro("@ClaseId", claseId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    clase = new Clase();
                    clase.Id = (int)datos.Lector["Id"];
                    clase.FechaHorario = (DateTime)datos.Lector["FechaHorario"];
                    clase.Capacidad = (int)datos.Lector["Capacidad"];
                    clase.Importe = (int)datos.Lector["Importe"];
                    clase.Descripcion = datos.Lector["Descripcion"] as string; // Usamos "as" para permitir nulls
                    clase.Activo = (bool)datos.Lector["Activo"];
                }

                return clase;
            }
            catch (Exception ex)
            {
                throw ex; // Considera registrar el error en lugar de solo lanzar
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Clase nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("ModificarClase"); // Asegúrate de que el procedimiento almacenado esté correctamente configurado
                datos.setearParametro("@id", nueva.Id); // Asegúrate de que el ID de la clase se establezca correctamente
                datos.setearParametro("@FechaHorario", nueva.FechaHorario);
                datos.setearParametro("@Capacidad", nueva.Capacidad);
                datos.setearParametro("@Importe", nueva.Importe);
                datos.setearParametro("@Descripcion", nueva.Descripcion);
                datos.setearParametro("@Activo", nueva.Activo);

                datos.ejecutarAccion(); // Ejecuta la acción sin retorno
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la clase", ex); // Manejo de excepciones
            }
            finally
            {
                datos.cerrarConexion(); // Asegúrate de cerrar la conexión
            }
        }

    }
}
