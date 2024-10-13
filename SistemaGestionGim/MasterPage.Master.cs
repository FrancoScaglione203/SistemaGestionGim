using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionGim
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!(Page is Login))
            //{
            //    if (!Seguridad.sesionActiva(Session["usuario"]))
            //        Response.Redirect("Login.aspx", false);
            //}

            if (Session["usuario"] != null)
            {

                Usuario usuario;
                usuario = (Usuario)Session["usuario"];
                string nombreUsuario = usuario.Nombre + " " + usuario.Apellido;
                lblNomreUsuario.Text = nombreUsuario;
                lblNomreUsuario.Visible = true;
                PlanesLink.Visible = true;
                ClasesLink.Visible = true;
                PerfilLink.Visible = true;
                PagosLink.Visible = true;

                if (!dominio.Seguridad.esAdmin(Session["usuario"]))
                {
                    CuponesLink.Visible = true;
                }
            }

            
        }


        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
     
    }
}