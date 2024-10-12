using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace SistemaGestionGim
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;

            if(!(usuario != null && usuario.Id !=0))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Session.Add("listaUsuarios", negocio.listarUsuarios());
                dgvUsuarios.DataSource = Session["listaUsuarios"];
                dgvUsuarios.DataBind();
            }

        }
    }
}