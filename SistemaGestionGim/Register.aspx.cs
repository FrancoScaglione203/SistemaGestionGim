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
    public partial class Register : System.Web.UI.Page
    {
        public List<Plan> listaPlanes = new List<Plan>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlanNegocio planNegocio = new PlanNegocio();
                listaPlanes = planNegocio.listarPlanes();
                outerRepeater.DataSource = listaPlanes;
                outerRepeater.DataBind();
            }
            
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                
                user.Email = txtEmail.Text;
                user.clave = txtClave.Text;
                user.tipoUsuario = TipoUsuario.NORMAL;
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.Id_plan = int.Parse(hiddenFieldPlanId.Value);
                user.Activo = true;

                if (!usuarioNegocio.ValidacionEmail(user.Email))
                {
                    if (txtClave.Text == txtClave2.Text)
                    {
                        int id = usuarioNegocio.InsertarNuevo(user);
                        if (id == 0)
                        {
                            Response.Redirect("ErrorLog.aspx");

                        }
                        else
                        {
                            Session["validacionRegister"] = null;
                            Response.Redirect("RegisterExitoso.aspx");
                        }
                    }
                    else 
                    {
                        String errorClave = "Las contraseñas no coinciden";
                        Session["validacionRegister"] = errorClave;
                        Response.Redirect("Register.aspx");
                    }
                }
                else
                {
                    String errorMail = "Ya existe un usuario con esa direccion de email";
                    Session["validacionRegister"] = errorMail;
                    Response.Redirect("Register.aspx");
                }  

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }

        }
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            // Obtener el botón que fue clickeado
            Button btn = (Button)sender;

            // Obtener la descripción y el ID del plan seleccionado
            string planSeleccionado = btn.CommandArgument; // Este debe ser el ID del plan
            string descripcionPlan = btn.Text; // Suponiendo que el texto del botón es la descripción

            // Actualizar el TextBox con la descripción del plan seleccionado
            txtPlanSeleccionado.Text = descripcionPlan;

            // Asignar el ID del plan al campo oculto
            hiddenFieldPlanId.Value = planSeleccionado; // Asegúrate de que planSeleccionado contiene el ID

            // Aquí puedes también marcar el control como válido si deseas
            // Esto evita que el validator del plan muestre un error si se selecciona un plan
            Page.Validate("GrupoDeValidacion"); // Asegúrate de tener el grupo de validación configurado
            vPlan.IsValid = true; // Marcar el control del plan como válido
        }

    }
}