using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionGim
{
    public partial class Perfil : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["validacionModificacion"] != null)
            {

                txtNombre.ReadOnly = false;
                txtApellido.ReadOnly = false;
                txtEmail.ReadOnly = false;
                txtPassword.ReadOnly = false;
                txtConfirmarPassword.ReadOnly = false;

                txtConfirmarPassword.Style["display"] = "inline-block";
                btnCambiarPlan.Style["display"] = "none";
                btnModificarPerfil.Style["display"] = "none";
                btnCancelarUsuario.Style["display"] = "none";
                btnCancelar.Style["display"] = "inline-block";
                btnConfirmarCambios.Style["display"] = "inline-block";

                txtImagen.Style["display"] = "inline-block";
                lblTxtImagen.Visible = true;
            }


            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    // Convertir la variable de sesión a un objeto Usuario
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];

                    string rutaImg = " ";
                    rutaImg = Server.MapPath("~/Imagenes/perfiles/perfil-" + usuarioLogueado.Id + ".jpg");

                    if (File.Exists(rutaImg))
                    {
                        imgPerfil.ImageUrl = "~/Imagenes/perfiles/perfil-" + usuarioLogueado.Id + ".jpg";
                    }
                    else
                    {
                        imgPerfil.ImageUrl = "~/Imagenes/perfiles/perfil-default.jpg";
                    }
                    // Ahora puedes acceder a las propiedades del usuarioLogueado


                    if (Session["validacionModificacion"] != null)
                    {
                        txtConfirmarPassword.Attributes["value"] = (String)Session["Clave2"];
                        txtEmail.Attributes["value"] = (String)Session["Email"];
                        txtNombre.Attributes["value"] = (String)Session["Nombre"];
                        txtApellido.Attributes["value"] = (String)Session["Apellido"];
                        txtPassword.Attributes["value"] = (String)Session["Clave"];

                    }
                    else
                    { 
                        txtConfirmarPassword.Attributes["value"] = usuarioLogueado.clave;
                        txtEmail.Attributes["value"] = usuarioLogueado.Email;
                        txtNombre.Attributes["value"] = usuarioLogueado.Nombre;
                        txtApellido.Attributes["value"] = usuarioLogueado.Apellido;
                        txtPassword.Attributes["value"] = usuarioLogueado.clave;
                    }
                    // Y así con las otras propiedades que necesites

                    // Asignar los datos del plan a los controles de la tarjeta
                    lblDescripcionPlan.Text = "Descripción: " + usuarioLogueado.plan.Descripcion;
                    lblImportePlan.Text = "Importe: $" + usuarioLogueado.plan.Importe.ToString("N2");

                    

                }
                else
                {
                    // Si no hay un usuario en la sesión, redirigir al login o a una página de error
                    Response.Redirect("Login.aspx");
                }
            }
        }


        protected void btnModificarPerfil_Click(object sender, EventArgs e)
        {
            // Cambiar a modo edición
            txtNombre.ReadOnly = false;
            txtApellido.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtPassword.ReadOnly = false;
            txtConfirmarPassword.ReadOnly = false;
            // Agrega otros campos que el usuario debería poder editar

            txtConfirmarPassword.Style["display"] = "inline-block";

            // lblConfirmarPassword.Style["display"] = "inline-block";
            btnCambiarPlan.Style["display"] = "none";
            btnModificarPerfil.Style["display"] = "none";
            btnCancelarUsuario.Style["display"] = "none";
            btnCancelar.Style["display"] = "inline-block";
            btnConfirmarCambios.Style["display"] = "inline-block";
            txtImagen.Style["display"] = "inline-block";
            lblTxtImagen.Visible = true;

        }

        protected void GuardarCambios_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario
            {
                Email = txtEmail.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
            };

            //usuarioNegocio.ActualizarUsuario(usuario);
            Response.Redirect("PerfilActualizado.aspx");
        }

        protected void btnCancelarUsuario_Click(object sender, EventArgs e)
        {
            btnModificarPerfil.Style["display"] = "none";
            btnConfirmarCancelacion.Style["display"] = "inline-block";
            btnCancelarUsuario.Style["display"] = "none";
            btnCancelar2.Style["display"] = "inline-block";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["validacionModificacion"] = null;
            Response.Redirect("Perfil.aspx");
        }

        protected void btnCancelar2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
        }


        protected void btnConfirmarCancelacion_Click(object sender, EventArgs e) 
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            usuarioLogueado.Activo = false;
            usuarioNegocio.Modificar(usuarioLogueado);
            Response.Redirect("Login.aspx");

        }

        protected void btnConfirmarCambios_Click(object sender, EventArgs e)
        {

            // Convertir la variable de sesión a un objeto Usuario
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            // Crear un nuevo objeto Usuario con los datos actualizados
            Usuario usuarioActualizado = new Usuario
            {
                Id = usuarioLogueado.Id,  // Preservar el ID original
                Email = txtEmail.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                clave = txtPassword.Text, // Asigna la nueva contraseña
                tipoUsuario = usuarioLogueado.tipoUsuario, // Preservar el tipo de usuario actual
                Id_plan = usuarioLogueado.Id_plan, // Preservar el ID del plan actual
                plan = usuarioLogueado.plan,
                Activo = true
            };

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            if (!(usuarioNegocio.UsuarioConEmail(usuarioActualizado)))
            {
                Session["Email"] = null;
                if (txtPassword.Text == txtConfirmarPassword.Text)
                {

                    // Aquí llama al método para modificar el usuario en la base de datos

                    usuarioNegocio.Modificar(usuarioActualizado); // Asegúrate de que este método esté implementado correctamente

                    Session["usuario"] = usuarioActualizado;
                    Session["Nombre"] = null;
                    Session["Apellido"] = null;
                    Session["Clave2"] = null;
                    Session["Clave"] = null;
                    Session["Email"] = null;
                    Session["validacionModificacion"] = null;
                    guardarImagenPerfil();
                    Response.Redirect("Perfil.aspx");
                }
                else
                {
                    String errorClave = "Las contraseñas no coinciden";
                    Session["Nombre"] = txtNombre.Text;
                    Session["Apellido"] = txtApellido.Text;
                    Session["Clave2"] = txtConfirmarPassword.Text;
                    Session["Clave"] = txtPassword.Text;
                    Session["Email"] = txtEmail.Text;
                    Session["validacionModificacion"] = errorClave;
                    Response.Redirect("Perfil.aspx");
                }
            }
            else
            {
                String errorEmail = "Ya existe usuario con esa direccion de email";
                Session["Nombre"] = txtNombre.Text;
                Session["Apellido"] = txtApellido.Text;
                Session["Clave2"] = txtConfirmarPassword.Text;
                Session["Clave"] = txtPassword.Text;
                Session["Email"] = txtEmail.Text;
                Session["validacionModificacion"] = errorEmail;
                Response.Redirect("Perfil.aspx");
            }

            


        }

        protected void btnCambiarPlan_Click(object sender, EventArgs e)
        {
            Response.Redirect("Planes.aspx");
        }

        protected void guardarImagenPerfil()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            try
            {
                string ruta = Server.MapPath("Imagenes/perfiles/");
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");

                Image img = (Image)Master.FindControl("imgPerfilMini");
                img.ImageUrl = "~/Imagenes/perfiles/perfil-" + usuario.Id + ".jpg";
                imgPerfil.ImageUrl = "~/Imagenes/perfiles/perfil-" + usuario.Id + ".jpg";

                Response.Redirect("Perfil.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}