using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionGim
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
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