using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Nombre, Apellido, Activo FROM Usuarios";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Activo = (bool)(datos.Lector["Activo"]);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Adaptamos la consulta para la estructura de la tabla actual
                datos.setearConsulta("select Id, TipoUsuario, Nombre, Apellido, Email, Activo from Usuarios where Email = @Email and Clave = @Clave and Activo = 1 ");

                // Seteamos los parámetros necesarios
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Clave", usuario.clave);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // Mapear los resultados a la clase Usuario
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.tipoUsuario = (int)(datos.Lector["TipoUsuario"]) == 2 ? TipoUsuario.ADMIN : TipoUsuario.NORMAL;
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    return true;
                }
                return false;
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


