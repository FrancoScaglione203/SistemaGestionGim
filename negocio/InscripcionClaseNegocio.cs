﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class InscripcionClaseNegocio
    {

        public int InsertarNuevo(InscripcionClase nuevaInscripcion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Asegúrate de que el procedimiento almacenado esté correctamente configurado en la base de datos.
                datos.setearProcedimiento("insertarNuevaInscripcionClase");
                datos.setearParametro("@ID_Usuario", nuevaInscripcion.Id_usuario);
                datos.setearParametro("@ID_Clase", nuevaInscripcion.Id_clase);
                datos.setearParametro("@ID_Plan", nuevaInscripcion.Id_plan);
                datos.setearParametro("@DescuentoPlan", nuevaInscripcion.DescuentoPlan);
                datos.setearParametro("@Cancelado", false);

                // Ejecutar la acción y obtener el ID de la inscripción insertada.
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


        public int InscriptosXclase(int idClase)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Configurar el procedimiento almacenado para contar los inscriptos de una clase.
                datos.setearProcedimiento("contarInscriptosPorClase");
                datos.setearParametro("@ID_Clase", idClase);

                // Ejecutar y obtener la cantidad de inscriptos.
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

        public bool UsuarioYaInscripto(int idClase, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Configurar el procedimiento almacenado para verificar la inscripción de un usuario en una clase.
                datos.setearProcedimiento("verificarInscripcionClase");
                datos.setearParametro("@ID_Clase", idClase);
                datos.setearParametro("@ID_Usuario", idUsuario);

                // Ejecutar el procedimiento y obtener la cantidad de registros que cumplen con los criterios.
                int resultado = datos.ejecutarAccionScalar();

                // Si el resultado es mayor a 0, significa que el usuario ya está inscrito.
                if (resultado > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool CancelarInscripcion(int idUsuario, int idClase)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_CancelarInscripcionClase");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@IdClase", idClase);

                int filasAfectadas = datos.ejecutarAccionScalar();

                return filasAfectadas > 0; // Devuelve true si se actualizó al menos una fila
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

        public List<InscripcionClase> listarInscripcionesClases()
        {
            List<InscripcionClase> lista = new List<InscripcionClase>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT ID, ID_Usuario, ID_Clase, ID_Plan, DescuentoPlan, Cancelado FROM InscripcionesClases";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    InscripcionClase aux = new InscripcionClase();

                    aux.Id = (int)datos.Lector["ID"];
                    aux.Id_usuario = (int)datos.Lector["ID_Usuario"];
                    aux.Id_clase = (int)datos.Lector["ID_Clase"];
                    aux.Id_plan = (int)datos.Lector["ID_Plan"];
                    aux.DescuentoPlan = (int)datos.Lector["DescuentoPlan"];
                    aux.Cancelado = (bool)datos.Lector["Cancelado"];

        
                    ClaseNegocio claseNegocio = new ClaseNegocio();
                    aux.clase = claseNegocio.ClaseById(aux.Id_clase);

                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    aux.usuario = usuarioNegocio.UsuarioById(aux.Id_usuario);

                    PlanNegocio planNegocio = new PlanNegocio();
                    aux.plan = planNegocio.GetPlanById(aux.Id_plan);

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

    }
}
