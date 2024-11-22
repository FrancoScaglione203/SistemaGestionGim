using dominio;
using System;
using System.Collections.Generic;
using System.IO;
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
                imgPerfilMini.Visible = true;
                PlanesLink.Visible = true;
                ClasesLink.Visible = true;
                PerfilLink.Visible = true;
                PagosLink.Visible = true;

                string rutaImg = " ";
                rutaImg = Server.MapPath("~/Imagenes/perfiles/perfil-" + usuario.Id + ".jpg");

                if (File.Exists(rutaImg))
                {
                    imgPerfilMini.ImageUrl = "~/Imagenes/perfiles/perfil-" + usuario.Id + ".jpg";
                }
                else 
                {
                    imgPerfilMini.ImageUrl = "~/Imagenes/perfiles/perfil-default.jpg";
                }

   

                if (!dominio.Seguridad.esAdmin(Session["usuario"]))
                {
                    CuponesLink.Visible = true;
                    ClasesLink.NavigateUrl = "ClasesAdmin.aspx";
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