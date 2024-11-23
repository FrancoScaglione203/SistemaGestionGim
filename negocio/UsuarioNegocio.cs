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
                string consulta = "SELECT Id, Nombre, Apellido, Email, ID_Plan, Activo FROM Usuarios";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["ID"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Id_plan = (int)datos.Lector["ID_Plan"];
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

        public Usuario UsuarioById(int usuarioId)
        {
            Usuario usuario = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Nombre, Apellido, Email, Clave, TipoUsuario, Id_plan, Activo FROM Usuarios WHERE Id = @UsuarioId";
                datos.setearConsulta(consulta);
                datos.setearParametro("@UsuarioId", usuarioId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["Apellido"];
                    usuario.Email = (string)datos.Lector["Email"];
                    usuario.clave = (string)datos.Lector["Clave"];
                    usuario.tipoUsuario = (TipoUsuario)(int)datos.Lector["TipoUsuario"];
                    usuario.Id_plan = (int)datos.Lector["Id_plan"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                }

                return usuario;
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


        public int InsertarNuevo(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("insertarNuevoUsuario"); // Asegúrate de que el procedimiento almacenado esté correctamente configurado
                datos.setearParametro("@clave", nuevo.clave);
                datos.setearParametro("@TipoUsuario", nuevo.tipoUsuario);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Id_plan", nuevo.Id_plan); // Agregar el ID del plan

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

        public void Modificar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("ModificarUsuario"); // Asegúrate de que el procedimiento almacenado esté correctamente configurado
                datos.setearParametro("@id", nuevo.Id); // Asegúrate de que el ID del usuario se establezca correctamente
                datos.setearParametro("@clave", nuevo.clave);
                datos.setearParametro("@TipoUsuario", nuevo.tipoUsuario);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Id_plan", nuevo.Id_plan);
                datos.setearParametro("@Activo", nuevo.Activo);

                datos.ejecutarAccion(); // Ejecuta la acción sin retorno
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el perfil del usuario", ex); // Manejo de excepciones
            }
            finally
            {
                datos.cerrarConexion(); // Asegúrate de cerrar la conexión
            }
        }



        public bool ValidacionEmail(string email)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            lista = listarUsuarios();

            Usuario useremail = lista.Find(u => u.Email == email);

            if (useremail == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool UsuarioConEmail(Usuario user)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            lista = listarUsuarios();

            Usuario useremail = lista.Find(u => u.Email == user.Email && u.Id != user.Id && u.Activo == true);

            if (useremail == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Adaptamos la consulta para la estructura de la tabla actual
                datos.setearConsulta("SELECT Id, TipoUsuario, Nombre, Apellido, Email, Id_Plan, Activo FROM Usuarios WHERE Email = @Email AND Clave = @Clave AND Activo = 1");

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
                    usuario.Id_plan = (int)datos.Lector["Id_Plan"];
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


