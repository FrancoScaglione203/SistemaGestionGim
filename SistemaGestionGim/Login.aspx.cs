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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public String Email()
        {
            Usuario usuario;
            UsuarioNegocio negocio = new UsuarioNegocio();

            usuario = (Usuario)Session["usuario"];
            string email = usuario.Email;
            return email;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario = new Usuario();
                usuario.Email = txtEmail.Text;
                usuario.clave = txtClave.Text;

                if (negocio.loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    string nombreUsuario = usuario.Nombre + " " + usuario.Apellido;
                    Session.Add("nombreUsuario", nombreUsuario);
                    Response.Redirect("default.aspx", false);
                }
                else
                {

                    int x = 1;
                    Session["validacionLogin"] = x;
                    return;
                }

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("ErrorLog.aspx");
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}