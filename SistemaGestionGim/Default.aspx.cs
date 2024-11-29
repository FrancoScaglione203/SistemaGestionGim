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
                InscripcionClaseNegocio inscripcionClaseNegocio = new InscripcionClaseNegocio();

                ClaseNegocio claseNegocio = new ClaseNegocio();
                List<Clase> clases = new List<Clase>();
                clases = claseNegocio.listarClases();

                List<Clase> clasesHoy = new List<Clase>();
                List<Clase> clasesMan = new List<Clase>();
                DateTime fechaHoy = DateTime.Today;
                DateTime fechaManana = fechaHoy.AddDays(1);

                clasesHoy = clases.Where(c => c.FechaHorario.Date == fechaHoy).ToList();
                clasesMan = clases.Where(c => c.FechaHorario.Date == fechaManana).ToList();

                var clasesDisponiblesHoy = clasesHoy
               .Select(clase => new
               {
                   Id = clase.Id,
                   Descripcion = clase.Descripcion,
                   FechaHorario = clase.FechaHorario,
                   Capacidad = clase.Capacidad,
                   Importe = clase.Importe,
                   Activo = clase.Activo,
                   Inscriptos = inscripcionClaseNegocio.InscriptosXclase(clase.Id) // Obtener la cantidad de inscriptos para cada clase.
               })
               .ToList();

                var clasesDisponiblesMan = clasesMan
               .Select(clase => new
               {
                   Id = clase.Id,
                   Descripcion = clase.Descripcion,
                   FechaHorario = clase.FechaHorario,
                   Capacidad = clase.Capacidad,
                   Importe = clase.Importe,
                   Activo = clase.Activo,
                   Inscriptos = inscripcionClaseNegocio.InscriptosXclase(clase.Id) // Obtener la cantidad de inscriptos para cada clase.
               })
               .ToList();


                repeaterClasesHoy.DataSource = clasesDisponiblesHoy;
                repeaterClasesHoy.DataBind();

                repeaterClasesMañana.DataSource = clasesDisponiblesMan;
                repeaterClasesMañana.DataBind();
            }

        }
    }
}