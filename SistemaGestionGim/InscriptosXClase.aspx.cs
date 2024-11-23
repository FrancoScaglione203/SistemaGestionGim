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
    public partial class InscriptosXClase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;

            if (!(usuario != null && usuario.Id != 0))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                int idClase = (int)Session["IdClasesInscriptos"];

                ClaseNegocio claseNegocio = new ClaseNegocio();
                Clase clase = claseNegocio.ClaseById(idClase);
                lblTituloClase.Text = clase.Descripcion;

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                List<Usuario> usuarios = new List<Usuario>();
                usuarios = usuarioNegocio.listarUsuarios();

                InscripcionClaseNegocio inscripcionClaseNegocio = new InscripcionClaseNegocio();
                List<InscripcionClase> inscripcionesClases = new List<InscripcionClase>();
                inscripcionesClases = inscripcionClaseNegocio.listarInscripcionesClases();


                List<Usuario> usuariosClase = new List<Usuario>();

                foreach (InscripcionClase inscripcion in inscripcionesClases)
                {
                    if(inscripcion.Id_clase == idClase && inscripcion.Cancelado == false)
                    {
                        Usuario usuarioEncontrado = usuarios.FirstOrDefault(u => u.Id == inscripcion.Id_usuario);
                        usuariosClase.Add(usuarioEncontrado);
                    }
                }


                dgvUsuarios.DataSource = usuariosClase;
                dgvUsuarios.DataBind();

                    
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Session["IdClasesInscriptos"] = null;
            Response.Redirect("ClasesAdmin.aspx");
        }
    }
}