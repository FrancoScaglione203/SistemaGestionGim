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
    public partial class Clases : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    CargarClasesDisponibles();
                    CargarClasesInscriptas();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        private void CargarClasesDisponibles()
        {
            ClaseNegocio claseNegocio = new ClaseNegocio();
            InscripcionClaseNegocio inscripcionClaseNegocio = new InscripcionClaseNegocio();

            // Obtener la lista completa de clases.
            List<Clase> listaClases = claseNegocio.listarClases();

            // Obtener el usuario logueado desde la sesión.
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            // Obtener la lista de clases en las que el usuario está inscrito.
            List<Clase> listaClasesInscriptas = claseNegocio.ListarClasesPorUsuario(usuarioLogueado.Id);

            // Filtrar las clases disponibles excluyendo aquellas en las que el usuario está inscrito.
            var clasesDisponibles = listaClases
                .Where(clase => !listaClasesInscriptas.Any(c => c.Id == clase.Id))
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

            // Asignar la lista filtrada al Repeater
            repeaterClasesDisponibles.DataSource = clasesDisponibles;
            repeaterClasesDisponibles.DataBind();
        }

        private void CargarClasesInscriptas()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            ClaseNegocio ClaseNegocio = new ClaseNegocio();

            // Obtener las clases en las que el usuario está inscrito.
            List<Clase> listaClasesInscriptas = ClaseNegocio.ListarClasesPorUsuario(usuarioLogueado.Id);

            // Asignar la lista al Repeater
            repeaterClasesInscriptas.DataSource = listaClasesInscriptas;
            repeaterClasesInscriptas.DataBind();
        }

        protected void btnInscribirse_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idClase = Convert.ToInt32(btn.CommandArgument);



            //Aquí iría la lógica para inscribir al usuario en la clase
            //Ejemplo:
            InscripcionClaseNegocio inscripcionClaseNegocio = new InscripcionClaseNegocio();
            InscripcionClase inscripcionClase = new InscripcionClase();

            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            inscripcionClase.Id_usuario = usuarioLogueado.Id;
            inscripcionClase.Id_clase = idClase;
            inscripcionClase.Id_plan = usuarioLogueado.Id_plan;
            inscripcionClase.DescuentoPlan = usuarioLogueado.plan.DescuentoClases;
            inscripcionClase.Cancelado = false;



            ClaseNegocio claseNegocio = new ClaseNegocio();
            Clase clase = claseNegocio.ClaseById(idClase);

            int cantInscriptos = inscripcionClaseNegocio.InscriptosXclase(clase.Id);

            if(!(cantInscriptos >= clase.Capacidad))
            {
                int resultado = inscripcionClaseNegocio.InsertarNuevo(inscripcionClase);
                Session["validacionInscripcion"] = null;
                Response.Redirect("Clases.aspx");
            }
            else
            {
                String errorInscripcion = "Todos los cupos para la clase seleccionada estan ocupados";
                Session["validacionInscripcion"] = errorInscripcion;
                Response.Redirect("Clases.aspx");
            }
        }

        protected void btnCancelarSuscripcion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idClase = Convert.ToInt32(btn.CommandArgument);

            // Obtener la información del usuario logueado
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            // Instanciar la lógica de negocio para manejar inscripciones
            InscripcionClaseNegocio inscripcionClaseNegocio = new InscripcionClaseNegocio();

            // Cancelar la inscripción de la clase para el usuario
            bool resultado = inscripcionClaseNegocio.CancelarInscripcion(usuarioLogueado.Id, idClase);

            if (resultado)
            {
                // Si la cancelación fue exitosa, recargar la lista de clases inscritas
                Session["validacionInscripcion"] = null;
                CargarClasesInscriptas();
            }
            else
            {
                // Si hubo un problema, establecer un mensaje de error en la sesión
                Session["validacionInscripcion"] = "Hubo un problema al intentar cancelar la inscripción.";
            }

            // Redirigir nuevamente a la página para actualizar la información mostrada
            Response.Redirect("Clases.aspx");
        }
    }
}