using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionGim
{
    public partial class ClasesAdmin : System.Web.UI.Page
    {
        public List<Clase> listaClases = new List<Clase>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    CargarClases();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }



            if (!IsPostBack)
            {
                if (Session["Descripcion"] != null)
                    txtDescripcion.Text = Session["Descripcion"].ToString();
                if (Session["Fecha"] != null)
                    txtFecha.Text = Session["Fecha"].ToString();
                if (Session["Hora"] != null)
                    txtHora.Text = Session["Hora"].ToString();
                if (Session["Capacidad"] != null)
                    txtCapacidad.Text = Session["Capacidad"].ToString();
                if (Session["Importe"] != null)
                    txtImporte.Text = Session["Importe"].ToString();
            }


            if (Session["validacionClase"] != null)
            {
                repeaterClases.Visible = false;
                btnAgregarClase.Visible = false;
                panelFormularioClase.Visible = true;
                lblTxtImagenClase.Visible = true;
                txtImagenClase.Style["display"] = "inline-block";



            }
        }

        private void CargarClases()
        {
            ClaseNegocio claseNegocio = new ClaseNegocio();
            listaClases = claseNegocio.listarClases(); // Método que obtiene todas las clases
            repeaterClases.DataSource = listaClases;
            repeaterClases.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idClase = int.Parse(((Button)sender).CommandArgument);
            Session["IdClasesInscriptos"] = idClase;
            Response.Redirect("InscriptosXClase.aspx");
        }


        protected void btnAgregarClase_Click(object sender, EventArgs e)
        {
            repeaterClases.Visible = false;
            btnAgregarClase.Visible = false;
            panelFormularioClase.Visible = true;
            lblTxtImagenClase.Visible = true;
            txtImagenClase.Style["display"] = "inline-block";
        }
        protected String validarFechaHora(DateTime fechaHora)
        {
            if(fechaHora > DateTime.Now)
            {
                if(fechaHora.Hour >= 8 && fechaHora.Hour <= 21)
                {
                    if (!FechaHoraOcupada(fechaHora))
                    {
                        return null;
                    }
                    else
                    {return "La fecha/hora se encuentra ocupada";}
                }
                else {return "La fecha/hora ingresada es invalida, los horarios son de 8hrs a 21hrs";}
            }
            else {return "La fecha/hora ingresada es invalida"; }
        }


        protected bool FechaHoraOcupada(DateTime fechaHora)
        {
            List<Clase> listaClases = new List<Clase>();

            ClaseNegocio claseNegocio = new ClaseNegocio();
            listaClases = claseNegocio.listarClases();

            foreach (Clase clase in listaClases)
            {
                // Comparar el día, mes, año y la hora (sin los minutos y segundos) de cada clase con la fechaHora pasada
                if (clase.FechaHorario.Date == fechaHora.Date &&
                    clase.FechaHorario.Hour == fechaHora.Hour)
                {
                    return true; // La fecha y hora coinciden con alguna clase existente
                }
            }

            return false;
        }


        protected void btnGuardarClase_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener y combinar la fecha y la hora ingresadas
                DateTime fecha = DateTime.Parse(txtFecha.Text);
                TimeSpan hora = TimeSpan.Parse(txtHora.Text);
                DateTime fechaHorario = fecha.Add(hora);


                // Validar que la fecha y hora no sean anteriores al momento actual
                String valFechaHora = validarFechaHora(fechaHorario);

                if (valFechaHora == null)
                {


                    // Crear una nueva instancia de la clase para guardar
                    Clase nuevaClase = new Clase
                    {
                        Descripcion = txtDescripcion.Text,
                        FechaHorario = fechaHorario,
                        Capacidad = int.Parse(txtCapacidad.Text),
                        Importe = int.Parse(txtImporte.Text),
                    };

                    // Lógica para guardar la nueva clase (insertar en la base de datos)
                    ClaseNegocio claseNegocio = new ClaseNegocio();


                    claseNegocio.InsertarNuevo(nuevaClase);
                    guardarImagenClase();

                    // Mostrar nuevamente el Repeater y ocultar el formulario
                    panelFormularioClase.Visible = false;
                    repeaterClases.Visible = true;
                    lblTxtImagenClase.Visible=true;
                    txtImagenClase.Style["display"] = "none";
                    // Refrescar la lista de clases
                    //CargarClases();

                    Session["validacionClase"] = null;
                    Session["Descripcion"] = null;
                    Session["Fecha"] = null;
                    Session["Hora"] = null;
                    Session["Capacidad"] = null;
                    Session["Importe"] = null;
                    Session["validacionClase"] = null;
                    Response.Redirect("ClasesAdmin.aspx");


                }
                else
                {
                    Session["validacionClase"] = valFechaHora;
                    Session["Descripcion"] = txtDescripcion.Text;
                    Session["Fecha"] = txtFecha.Text;
                    Session["Hora"] = txtHora.Text;
                    Session["Capacidad"] = txtCapacidad.Text;
                    Session["Importe"] = txtImporte.Text;
                    Session["validacionClase"] = valFechaHora;
                    Response.Redirect("ClasesAdmin.aspx");

                }
                
            }
            catch (Exception ex)
            {
                // Manejar el error
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e) 
        {
            Session["validacionClase"] = null;
            Session["Descripcion"] = null;
            Session["Fecha"] = null;
            Session["Hora"] = null;
            Session["Capacidad"] = null;
            Session["Importe"] = null;
            Session["validacionClase"] = null;
            Response.Redirect("ClasesAdmin.aspx");
        }
        protected void btnCancelarEliminar_Click(object sender, EventArgs e) { Response.Redirect("ClasesAdmin.aspx"); }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnAgregarClase.Visible = false;
            Button btn = (Button)sender;
            int claseId = int.Parse(btn.CommandArgument);

            // Aquí buscarías la clase en la base de datos por su ID para obtener sus detalles.
            ClaseNegocio claseNegocio = new ClaseNegocio();
            Clase clase = claseNegocio.ClaseById(claseId);

            // Llenar los labels con la información de la clase a eliminar.
            lblDescripcionEliminar.Text = clase.Descripcion;
            lblFechaHoraEliminar.Text = clase.FechaHorario.ToString("dd/MM/yyyy HH:mm");
            lblCapacidadEliminar.Text = clase.Capacidad.ToString();
            lblImporteEliminar.Text = clase.Importe.ToString();

            // Almacenar el ID de la clase que se desea eliminar en una variable de sesión o ViewState.
            Session["ClaseIdEliminar"] = claseId;

            // Ocultar el repetidor y mostrar el panel de confirmación de eliminación.
            repeaterClases.Visible = false;
            panelConfirmarEliminar.Visible = true;
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int claseId = (int)Session["ClaseIdEliminar"];

            // Aquí puedes eliminar la clase de la base de datos.
            ClaseNegocio claseNegocio = new ClaseNegocio();
            Clase clase = claseNegocio.ClaseById(claseId);

            clase.Activo = false;
            claseNegocio.Modificar(clase);

            Response.Redirect("ClasesAdmin.aspx");
        }

        protected void guardarImagenClase()
        {
            ClaseNegocio claseNegocio = new ClaseNegocio();
            listaClases = claseNegocio.listarClases();

            int nuevoId = listaClases.Count > 0 ? listaClases.Max(c => c.Id) : 1;

            string ruta = Server.MapPath("Imagenes/clases/");
            txtImagenClase.PostedFile.SaveAs(ruta + "clase-" + nuevoId + ".jpg");

        }

        public string GetImageUrl(string claseId)
        {
            string rutaImg = Server.MapPath("~/Imagenes/clases/clase-" + claseId + ".jpg");
            if (File.Exists(rutaImg))
            {
                return "/Imagenes/clases/clase-" + claseId + ".jpg";
            }
            else
            {
                return "/Imagenes/clases/clase-default.jpg";
            }
        }
    }
}