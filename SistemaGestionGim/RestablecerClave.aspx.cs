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
    public partial class RestablecerClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            string email = Request.QueryString["email"];

            //FALTA VALIDACION POR SI NO SON IGUALES

            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios = negocio.listarUsuarios();

            Usuario usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));


            if (txtClave1.Text != txtClave2.Text)
            {
                string validacion = "";
                validacion = "Las contraseñas ingresadas deben coincidir";
                Session["validacionClave"] = validacion;
            }
            else
            {
                usuarioEncontrado.clave = txtClave1.Text;
                negocio.Modificar(usuarioEncontrado);
                Response.Redirect("default.aspx", false);
            }
            return;
        }
    }
}