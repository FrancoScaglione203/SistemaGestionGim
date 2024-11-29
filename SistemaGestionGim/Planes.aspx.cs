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
    public partial class Planes : System.Web.UI.Page
    {
        public int IdPlanUsuarioActual = 0;
        //public int IdPlanUsuarioNuevo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;

            if (!(usuario != null && usuario.Id != 0))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarPlanes();
                CargarPlanActual();

            }

        }

        private void CargarPlanes()
        {
            // Simulación de la carga de planes desde la base de datos.
            PlanNegocio planNegocio = new PlanNegocio();
            List<Plan> planes = planNegocio.listarPlanes(); // Método que devuelve la lista de todos los planes

            // Vincular la lista de planes al Repeater
            repeaterPlanes.DataSource = planes;
            repeaterPlanes.DataBind();
        }

        private void CargarPlanActual()
        {
            if (Session["usuario"] != null)
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                lblDescripcionPlanActual.Text = "Descripción: " + usuarioLogueado.plan.Descripcion;
                lblImportePlanActual.Text = "Importe: $" + usuarioLogueado.plan.Importe.ToString("N2");
            }
        }

        protected void btnCambiarPlan_Click(object sender, EventArgs e)
        {

            Usuario user = new Usuario();
            user = (Usuario)Session["usuario"];

            Button btn = (Button)sender;
            int idPlan = Convert.ToInt32(btn.CommandArgument);

            if (idPlan == user.Id_plan) 
            {
                String errorPlan = "Seleccionaste tu plan actual";
                Session["validacionPlan"] = errorPlan;

                Response.Redirect("Planes.aspx");
            }


            Session.Add("idPlanNuevo", idPlan);
            lblValidacion.Visible = false;
            panelNuevoPlan.Visible = true;

            PlanNegocio negocio = new PlanNegocio();
            Plan plan = new Plan();

            plan = negocio.GetPlanById(idPlan);
            lblDescripcionPlanNuevo.Text = "Descripción: " + plan.Descripcion;
            lblImportePlanNuevo.Text = "Importe: $" + plan.Importe.ToString("N2");
            btnConfirmarCambio.Visible = true;

        }

        protected void btnConfirmarCambio_Click(object sender, EventArgs e)
        {
            int idPlan = (int)Session["idPlanNuevo"];

            PlanNegocio negocio = new PlanNegocio();
            Plan plan = new Plan();

            plan = negocio.GetPlanById(idPlan);
            
            Usuario usuarioCambioPlan = (Usuario)Session["usuario"];
            usuarioCambioPlan.Id_plan = idPlan;
            usuarioCambioPlan.plan = plan;

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.Modificar(usuarioCambioPlan);

            Session["usuario"] = usuarioCambioPlan;
            Session["idPlanNuevo"] = null;
            Session["validacionPlan"] = null;
            Response.Redirect("Planes.aspx");

        }

    }
}