using dominio;
using negocio;
using System;
using System.Collections.Generic;
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
            }


            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    // Convertir la variable de sesión a un objeto Usuario
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];

                    // Ahora puedes acceder a las propiedades del usuarioLogueado
                    txtEmail.Text = usuarioLogueado.Email;
                    txtApellido.Text = usuarioLogueado.Apellido;
                    txtNombre.Text = usuarioLogueado.Nombre;
                    txtPassword.Attributes["value"] = usuarioLogueado.clave;
                    txtConfirmarPassword.Attributes["value"] = usuarioLogueado.clave;
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
        }

        protected void GuardarCambios_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario
            {
                Email = txtEmail.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                // Aquí asigna el plan seleccionado si corresponde
                //Activo = txtActivo.Text == "Activo"
            };

            //usuarioNegocio.ActualizarUsuario(usuario);
            Response.Redirect("PerfilActualizado.aspx");
        }

        protected void btnCancelarUsuario_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Perfil.aspx");
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


            if (txtPassword.Text == txtConfirmarPassword.Text)
            {

                // Aquí llama al método para modificar el usuario en la base de datos
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuarioNegocio.Modificar(usuarioActualizado); // Asegúrate de que este método esté implementado correctamente

                Session["usuario"] = usuarioActualizado;
                Session["validacionModificacion"] = null;
                Response.Redirect("Perfil.aspx");
            }
            else
            {
                String errorClave = "Las contraseñas no coinciden";
                Session["validacionModificacion"] = errorClave;
                Response.Redirect("Perfil.aspx");
            }


        }

        protected void btnCambiarPlan_Click(object sender, EventArgs e)
        {
            Response.Redirect("Planes.aspx");
        }

    }
}