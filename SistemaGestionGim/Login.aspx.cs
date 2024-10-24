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
        public Plan Plan { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClaseNegocio claseNegocio = new ClaseNegocio();
            claseNegocio.ActualizarEstadoClases();
            CuponNegocio cuponNegocio = new CuponNegocio();
            cuponNegocio.ActualizarEstadoCupones();
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
                //Aca deberia ir el UsuarioById


                if (negocio.loguear(usuario))
                {                  
                    usuario.plan = PlanById(usuario.Id_plan);
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



        protected Plan PlanById(int planId)
        {
            PlanNegocio planNegocio = new PlanNegocio();

            Plan = planNegocio.GetPlanById(planId);
            return Plan;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        

    }
}