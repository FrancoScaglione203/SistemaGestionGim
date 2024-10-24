using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CuponNegocio
    {

        public List<Cupon> listarCupones()
        {
            List<Cupon> lista = new List<Cupon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Codigo, Descuento, FechaVencimiento, Activo FROM Cupones WHERE Activo = 1;";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cupon aux = new Cupon();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Descuento = (int)datos.Lector["Descuento"];
                    aux.FechaVencimiento = (DateTime)datos.Lector["FechaVencimiento"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
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

        public int InsertarNuevo(Cupon nuevoCupon)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Asegúrate de que el procedimiento almacenado esté correctamente configurado en la base de datos.
                datos.setearProcedimiento("insertarNuevoCupon");
                datos.setearParametro("@Codigo", nuevoCupon.Codigo);
                datos.setearParametro("@Descuento", nuevoCupon.Descuento);
                datos.setearParametro("@FechaVencimiento", nuevoCupon.FechaVencimiento);
                datos.setearParametro("@Activo", true);

                // Ejecutar la acción y obtener el ID del cupón insertado.
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


        public void ActualizarEstadoCupones()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Asegúrate de que el procedimiento almacenado esté correctamente configurado en la base de datos.
                datos.setearProcedimiento("ActualizarEstadoCupones");
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

        //public void ActualizarEstadoCupones()
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        // Actualiza el estado de los cupones según tu lógica
        //        string consulta = @"
        //    UPDATE Cupones
        //    SET Activo = 0 
        //    WHERE FechaVencimiento < GETDATE()"; // Establece Activo en 0 para los cupones vencidos

        //        datos.setearConsulta(consulta);
        //        datos.ejecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al actualizar el estado de los cupones", ex);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public void EliminarCupon(int cuponId)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Establecer el nombre del procedimiento almacenado
                datos.setearProcedimiento("DesactivarCuponPorId");

                // Configurar el parámetro para el procedimiento
                datos.setearParametro("@CuponId", cuponId);

                // Ejecutar la acción (sin necesidad de leer resultados)
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                // Lanza la excepción para manejarla en niveles superiores o para registrar el error
                throw ex;
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                datos.cerrarConexion();
            }
        }

    }
}
