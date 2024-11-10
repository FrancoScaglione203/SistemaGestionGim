using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PagoNegocio
    {
        public List<Pago> filtrarPagosMensuales(List<Pago> lista, string campo, string criterio, string filtro, string activo)
        {
            if (!string.IsNullOrEmpty(filtro))
            {
                if (campo == "Nombre clase")
                {
                    if (criterio == "Contiene")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.Descripcion.Contains(filtro)).ToList();
                    }
                    else if (criterio == "Comienza con")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.Descripcion.StartsWith(filtro)).ToList();
                    }
                    else if (criterio == "Termina con")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.Descripcion.EndsWith(filtro)).ToList();
                    }
                }
                else if (campo == "Apellido usuario")
                {
                    if (criterio == "Contiene")
                    {
                        lista = lista.Where(a => a.usuario.Apellido.Contains(filtro)).ToList();
                    }
                    else if (criterio == "Comienza con")
                    {
                        lista = lista.Where(a => a.usuario.Apellido.StartsWith(filtro)).ToList();
                    }
                    else if (criterio == "Termina con")
                    {
                        lista = lista.Where(a => a.usuario.Apellido.EndsWith(filtro)).ToList();
                    }
                }
                else if (campo == "Fecha")
                {
                    if (criterio == "Mes")
                    {
                        lista = lista.Where(a => a.Mes == Convert.ToInt32(filtro)).ToList();
                    }
                    else if (criterio == "Año")
                    {
                        lista = lista.Where(a => a.Anio == Convert.ToInt32(filtro)).ToList();
                    }
                    else if (criterio == "Mes/Año")
                    {
                        var partes = filtro.Split('/');
                        if (partes.Length == 2)
                        {
                            lista = lista.Where(a =>
                                a.Mes == Convert.ToInt32(partes[0]) &&
                                a.Anio == Convert.ToInt32(partes[1])
                            ).ToList();
                        }
                    }
                }

                
            }

            if (activo == "Si")
            {
                bool fil = true;
                lista = lista.Where(a => a.Pagado == fil).ToList();
            }
            else if (activo == "No")
            {
                bool fil = false;
                lista = lista.Where(a => a.Pagado == fil).ToList();
            }
            return lista;
        }

        public List<Pago> filtrarPagosClases(List<Pago> lista, string campo, string criterio, string filtro, string activo)
        {

            if (!string.IsNullOrEmpty(filtro))
            {
                if (campo == "Nombre clase")
                {
                    if (criterio == "Contiene")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.Descripcion.Contains(filtro)).ToList();
                    }
                    else if (criterio == "Comienza con")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.Descripcion.StartsWith(filtro)).ToList();
                    }
                    else if (criterio == "Termina con")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.Descripcion.EndsWith(filtro)).ToList();
                    }
                }
                else if (campo == "Apellido usuario")
                {
                    if (criterio == "Contiene")
                    {
                        lista = lista.Where(a => a.usuario.Apellido.Contains(filtro)).ToList();
                    }
                    else if (criterio == "Comienza con")
                    {
                        lista = lista.Where(a => a.usuario.Apellido.StartsWith(filtro)).ToList();
                    }
                    else if (criterio == "Termina con")
                    {
                        lista = lista.Where(a => a.usuario.Apellido.EndsWith(filtro)).ToList();
                    }
                }
                else if (campo == "Fecha")
                {
                    if (criterio == "Mes")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.FechaHorario.Month == Convert.ToInt32(filtro)).ToList();
                    }
                    else if (criterio == "Año")
                    {
                        lista = lista.Where(a => a.inscripcionClase.clase.FechaHorario.Year == Convert.ToInt32(filtro)).ToList();
                    }
                    else if (criterio == "Mes/Año")
                    {
                        var partes = filtro.Split('/');
                        if (partes.Length == 2)
                        {
                            lista = lista.Where(a =>
                                a.inscripcionClase.clase.FechaHorario.Month == Convert.ToInt32(partes[0]) &&
                                a.inscripcionClase.clase.FechaHorario.Year == Convert.ToInt32(partes[1])
                            ).ToList();
                        }
                    }
                }

                
            }
            if (activo == "Si")
            {
                bool fil = true;
                lista = lista.Where(a => a.Pagado == fil).ToList();
            }
            else if (activo == "No")
            {
                bool fil = false;
                lista = lista.Where(a => a.Pagado == fil).ToList();
            }
            return lista;
        }




        public List<Pago> ListarPagosClases()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("ObtenerPagosPorClase");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pago aux = new Pago();

                    aux.Id_cupon = (int)datos.Lector["ID_Cupon"];
                    aux.Id_usuario = (int)datos.Lector["ID_Usuario"];
                    aux.Importe = (int)datos.Lector["Importe"];
                    aux.FechaPago = (DateTime)datos.Lector["FechaPago"];
                    aux.Id_inscripcionClase = (int)datos.Lector["ID_InscripcionClase"];
                    aux.Pagado = (bool)datos.Lector["Pago"];

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

        public List<Pago> ListarPagosMensuales()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("ObtenerPagosMensuales");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pago aux = new Pago();

                    aux.Id_cupon = (int)datos.Lector["ID_Cupon"];
                    aux.Id_usuario = (int)datos.Lector["ID_Usuario"];
                    aux.Importe = (int)datos.Lector["Importe"];
                    aux.FechaPago = (DateTime)datos.Lector["FechaPago"];
                    aux.Mes = (int)datos.Lector["Mes"];
                    aux.Anio = (int)datos.Lector["Anio"];
                    aux.Pagado = (bool)datos.Lector["Pago"];

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


        public List<Pago> ObtenerPagosPorClase()
        {



            List<Pago> pagos = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM Pagos WHERE Mes IS NULL AND Anio IS NULL");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pago pago = new Pago
                    {
                        Id_inscripcionClase = (int)(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ID_InscripcionClase")) ? (int?)null : datos.Lector.GetInt32(datos.Lector.GetOrdinal("ID_InscripcionClase"))),
                        Id_cupon = (int)(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ID_Cupon")) ? (int?)null : datos.Lector.GetInt32(datos.Lector.GetOrdinal("ID_Cupon"))),
                        Id_usuario = (int)(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ID_Usuario")) ? (int?)null : datos.Lector.GetInt32(datos.Lector.GetOrdinal("ID_Usuario"))),
                        Importe = datos.Lector.GetInt32(datos.Lector.GetOrdinal("Importe")),
                        FechaPago = datos.Lector.GetDateTime(datos.Lector.GetOrdinal("FechaPago")),
                        Pagado = datos.Lector.GetBoolean(datos.Lector.GetOrdinal("Pago")),
                        // Aquí puedes agregar más propiedades o mapeos si es necesario
                    };
                    pagos.Add(pago);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los pagos por clase", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return pagos;
        }

        public List<Pago> listarPagos()
        {
            List<Pago> lista = new List<Pago>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Mes, Anio, ID_Cupon, ID_Usuario, Importe, FechaPago, ID_InscripcionClase, Pago FROM Pagos";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pago aux = new Pago();
                    aux.Mes = datos.Lector["Mes"] != DBNull.Value ? (int)datos.Lector["Mes"] : 0;
                    aux.Anio = datos.Lector["Anio"] != DBNull.Value ? (int)datos.Lector["Anio"] : 0;
                    //if (datos.Lector["Mes"] != null){ aux.Mes = (int)datos.Lector["Mes"];}
                    //if (datos.Lector["Anio"] != null) { aux.Anio = (int)datos.Lector["Anio"]; }
                    aux.Id_cupon = datos.Lector["ID_Cupon"] != DBNull.Value ? (int)datos.Lector["ID_Cupon"] : 0;
                    //aux.Id_usuario = (int)datos.Lector["ID_Usuario"];
                    aux.Id_usuario = 0;
                    aux.Importe = (int)datos.Lector["Importe"];
                    aux.FechaPago = (DateTime)datos.Lector["FechaPago"];
                    aux.Id_inscripcionClase = datos.Lector["ID_InscripcionClase"] != DBNull.Value ? (int)datos.Lector["ID_InscripcionClase"] : 0;
                    aux.Pagado = (bool)datos.Lector["Pago"];

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
                datos.cerrarConexion(); // Asegúrate de cerrar la conexión
            }
        }


    }
}
