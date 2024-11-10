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
    public partial class Pagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    CargarPagosMensuales(); // Cargar pagos mensuales por defecto
                    CargarPagosPorClase();  // Cargar pagos por clase por defecto

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            
        }


        //Pagos Mensuales -----------------------------

        private void CargarPagosMensuales()
        {
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosMensuales();
            pagos = CargarDatos(pagos);


            var datosGrid = pagos.Select(p => new
            {
                Mes = p.Mes,
                Anio = p.Anio, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosMensuales.DataSource = datosGrid;
            gvPagosMensuales.DataBind();
        }

        protected void btnBuscarMensual_Click(object sender, EventArgs e)
        {
            FiltrarPagosMensuales();
        }

        protected void btnBorrarFiltroMensual_Click(object sender, EventArgs e)
        {
            LimpiarFiltrosMensual();
            CargarPagosMensuales();
        }

        protected void btnCancelarFiltrosMensual_Click(object sender, EventArgs e)
        {
            panelFiltros.Visible = false;

            LimpiarFiltrosMensual();
            CargarPagosMensuales();
        }

        private void FiltrarPagosMensuales()
        {
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosMensuales();
            pagos = CargarDatos(pagos);

            String filtro = "";

            if (ddlCampoMensual.Text == "Fecha")
            {
                if (ddlCriterioMensual.Text == "Mes")
                {
                    filtro = ddlMesMensual.Text;
                }
                else if (ddlCriterioMensual.Text == "Año")
                {
                    filtro = ddlAnioMensual.Text;
                }
                else if (ddlCriterioMensual.Text == "Mes/Año")
                {
                    filtro = ddlMesMensual.Text + "/" + ddlAnioMensual.Text;
                }
            }
            else if(ddlCampoMensual.Text == "Nombre clase" || ddlCampoMensual.Text == "Apellido usuario")
            {
                filtro = txtFiltroAvanzadoMensual.Text;
            }

            pagos = pagoNegocio.filtrarPagosMensuales(pagos, ddlCampoMensual.Text, ddlCriterioMensual.Text, filtro, ddlPagosMensual.Text);

            var datosGrid = pagos.Select(p => new
            {
                Mes = p.Mes,
                Anio = p.Anio, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosMensuales.DataSource = datosGrid;
            gvPagosMensuales.DataBind();
        }


        private void LimpiarFiltrosMensual()
        {
            ddlCriterioMensual.Enabled = true;
            ddlCampoMensual.Enabled = true;
            ddlCampoMensual.SelectedIndex = 0;
            txtFiltroAvanzadoMensual.Text = "";

            txtFiltroAvanzadoMensual.Visible = false;
            lblTxtFiltroAvanzadoMensual.Visible = false;

            lblDdlMesMensual.Visible = false;
            ddlMesMensual.Visible = false;

            lblDdlAnioMensual.Visible = false;
            ddlAnioMensual.Visible = false;

            lblDdlCriterioMensual.Visible = false;
            ddlCriterioMensual.Visible = false;
        }

        protected void ddlCampoMensual_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlCriterioMensual.Items.Clear();
            ddlCampoMensual.Enabled = false;
            if (ddlCampoMensual.SelectedItem.ToString() == "Apellido usuario" || ddlCampoMensual.SelectedItem.ToString() == "Nombre clase")
            {
                lblDdlCriterioMensual.Visible = true;
                ddlCriterioMensual.Visible = true;
                lblTxtFiltroAvanzadoMensual.Visible = true;
                txtFiltroAvanzadoMensual.Visible = true;
                ddlCriterioMensual.Items.Add("-");
                ddlCriterioMensual.Items.Add("Contiene");
                ddlCriterioMensual.Items.Add("Comienza con");
                ddlCriterioMensual.Items.Add("Termina con");
            }
            else
            {
                lblDdlCriterioMensual.Visible = true;
                ddlCriterioMensual.Visible = true;
                lblDdlMesMensual.Visible = true;
                ddlMesMensual.Visible = true;
                lblDdlAnioMensual.Visible = true;
                ddlAnioMensual.Visible = true;
                lblTxtFiltroAvanzadoMensual.Visible = false;
                txtFiltroAvanzadoMensual.Visible = false;
                ddlCriterioMensual.Items.Add("Mes/Año");
                ddlCriterioMensual.Items.Add("Mes");
                ddlCriterioMensual.Items.Add("Año");



            }
        }




        protected void ddlCriterioMensual_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterioMensual.Enabled = false;
            ddlAnioMensual.Items.Clear();
            ddlMesMensual.Items.Clear();
            if (ddlCriterioMensual.SelectedItem.ToString() == "Mes/Año")
            {
                lblDdlMesMensual.Visible = true;
                ddlMesMensual.Visible = true;

                lblDdlAnioMensual.Visible = true;
                ddlAnioMensual.Visible = true;

                ddlMesMensual.Items.Add("1");
                ddlMesMensual.Items.Add("2");
                ddlMesMensual.Items.Add("3");
                ddlMesMensual.Items.Add("4");
                ddlMesMensual.Items.Add("5");
                ddlMesMensual.Items.Add("6");
                ddlMesMensual.Items.Add("7");
                ddlMesMensual.Items.Add("8");
                ddlMesMensual.Items.Add("9");
                ddlMesMensual.Items.Add("10");
                ddlMesMensual.Items.Add("11");
                ddlMesMensual.Items.Add("12");

                ddlAnioMensual.Items.Add("2024");
                ddlAnioMensual.Items.Add("2023");
                ddlAnioMensual.Items.Add("2022");
                ddlAnioMensual.Items.Add("2021");
                ddlAnioMensual.Items.Add("2020");
            }
            else if (ddlCriterioMensual.SelectedItem.ToString() == "Mes")
            {
                lblDdlAnioMensual.Visible = false;
                ddlAnioMensual.Visible = false;
                lblDdlMesMensual.Visible = true;
                ddlMesMensual.Visible = true;

                ddlMesMensual.Items.Add("1");
                ddlMesMensual.Items.Add("2");
                ddlMesMensual.Items.Add("3");
                ddlMesMensual.Items.Add("4");
                ddlMesMensual.Items.Add("5");
                ddlMesMensual.Items.Add("6");
                ddlMesMensual.Items.Add("7");
                ddlMesMensual.Items.Add("8");
                ddlMesMensual.Items.Add("9");
                ddlMesMensual.Items.Add("10");
                ddlMesMensual.Items.Add("11");
                ddlMesMensual.Items.Add("12");
            }
            else if (ddlCriterioMensual.SelectedItem.ToString() == "Año")
            {
                lblDdlMesMensual.Visible = false;
                ddlMesMensual.Visible = false;
                lblDdlAnioMensual.Visible = true;
                ddlAnioMensual.Visible = true;



                ddlAnioMensual.Items.Add("2024");
                ddlAnioMensual.Items.Add("2023");
                ddlAnioMensual.Items.Add("2022");
                ddlAnioMensual.Items.Add("2021");
                ddlAnioMensual.Items.Add("2020");

            }
        }


        protected void btnMostrarFiltrosMensual_Click(object sender, EventArgs e)
        {
            panelFiltros.Visible = true;  // Mostrar el panel de filtros
            btnCancelarFiltrosMensual.Visible = true;
        }

        protected void btnCancelarFiltroMensual_Click(object sender, EventArgs e)
        {
            ddlCriterioMensual.Enabled = true;
            ddlCampoMensual.Enabled = true;
            btnCancelarFiltrosMensual.Visible = false;
            panelFiltros.Visible = false;  // Ocultar el panel de filtros
            LimpiarFiltrosMensual();              // Método para limpiar los filtros
        }

        //Pagos Clases -----------------------------
        private void CargarPagosPorClase()
        {
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosClases();
            pagos = CargarDatos(pagos);

            var datosGrid = pagos.Select(p => new
            {
                FechaClase = p.inscripcionClase.clase.FechaHorario,
                NombreClase = p.inscripcionClase.clase.Descripcion, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosClase.DataSource = datosGrid;
            gvPagosClase.DataBind();
        }

        protected void btnBuscarClase_Click(object sender, EventArgs e)
        {
            FiltrarPagosClases();
        }

        protected void btnBorrarFiltroClase_Click(object sender, EventArgs e)
        {
            LimpiarFiltrosClase();
            CargarPagosPorClase();
        }

        protected void btnCancelarFiltrosClase_Click(object sender, EventArgs e)
        {
            panelFiltrosClase.Visible = false;

            LimpiarFiltrosClase();
            CargarPagosPorClase();
        }

        private void FiltrarPagosClases()
        {
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosClases();
            pagos = CargarDatos(pagos);

            String filtro = "";

            if (ddlCampoClase.Text == "Fecha")
            {
                if (ddlCriterioClase.Text == "Mes")
                {
                    filtro = ddlMesClase.Text;
                }
                else if (ddlCriterioClase.Text == "Año")
                {
                    filtro = ddlAnioClase.Text;
                }
                else if (ddlCriterioClase.Text == "Mes/Año")
                {
                    filtro = ddlMesClase.Text + "/" + ddlAnioClase.Text;
                }
            }
            else if (ddlCampoClase.Text == "Nombre clase" || ddlCampoClase.Text == "Apellido usuario")
            {
                filtro = txtFiltroAvanzadoClase.Text;
            }

            pagos = pagoNegocio.filtrarPagosClases(pagos, ddlCampoClase.Text, ddlCriterioClase.Text, filtro, ddlPagosClase.Text);

            var datosGrid = pagos.Select(p => new
            {
                FechaClase = p.inscripcionClase.clase.FechaHorario,
                NombreClase = p.inscripcionClase.clase.Descripcion, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosClase.DataSource = datosGrid;
            gvPagosClase.DataBind();
        }


        private void LimpiarFiltrosClase()
        {
            ddlCriterioClase.Enabled = true;
            ddlCampoClase.Enabled = true;
            ddlCampoClase.SelectedIndex = 0;
            txtFiltroAvanzadoClase.Text = "";

            txtFiltroAvanzadoClase.Visible = false;
            lblTxtFiltroAvanzadoClase.Visible = false;

            lblDdlMesClase.Visible = false;
            ddlMesClase.Visible = false;

            lblDdlAnioClase.Visible = false;
            ddlAnioClase.Visible = false;

            lblDdlCriterioClase.Visible = false;
            ddlCriterioClase.Visible = false;
        }

        protected void ddlCampoClase_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlCriterioClase.Items.Clear();
            ddlCampoClase.Enabled = false;
            if (ddlCampoClase.SelectedItem.ToString() == "Apellido usuario" || ddlCampoClase.SelectedItem.ToString() == "Nombre clase")
            {
                lblDdlCriterioClase.Visible = true;
                ddlCriterioClase.Visible = true;
                lblTxtFiltroAvanzadoClase.Visible = true;
                txtFiltroAvanzadoClase.Visible = true;
                ddlCriterioClase.Items.Add("-");
                ddlCriterioClase.Items.Add("Contiene");
                ddlCriterioClase.Items.Add("Comienza con");
                ddlCriterioClase.Items.Add("Termina con");
            }
            else
            {
                lblDdlCriterioClase.Visible = true;
                ddlCriterioClase.Visible = true;
                lblDdlMesClase.Visible = true;
                ddlMesClase.Visible = true;
                lblDdlAnioClase.Visible = true;
                ddlAnioClase.Visible = true;
                lblTxtFiltroAvanzadoClase.Visible = false;
                txtFiltroAvanzadoClase.Visible = false;
                ddlCriterioClase.Items.Add("Mes/Año");
                ddlCriterioClase.Items.Add("Mes");
                ddlCriterioClase.Items.Add("Año");
            }
        }




        protected void ddlCriterioClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterioClase.Enabled = false;
            ddlAnioClase.Items.Clear();
            ddlMesClase.Items.Clear();
            if (ddlCriterioClase.SelectedItem.ToString() == "Mes/Año")
            {
                lblDdlMesClase.Visible = true;
                ddlMesClase.Visible = true;

                lblDdlAnioClase.Visible = true;
                ddlAnioClase.Visible = true;

                ddlMesClase.Items.Add("1");
                ddlMesClase.Items.Add("2");
                ddlMesClase.Items.Add("3");
                ddlMesClase.Items.Add("4");
                ddlMesClase.Items.Add("5");
                ddlMesClase.Items.Add("6");
                ddlMesClase.Items.Add("7");
                ddlMesClase.Items.Add("8");
                ddlMesClase.Items.Add("9");
                ddlMesClase.Items.Add("10");
                ddlMesClase.Items.Add("11");
                ddlMesClase.Items.Add("12");

                ddlAnioClase.Items.Add("2024");
                ddlAnioClase.Items.Add("2023");
                ddlAnioClase.Items.Add("2022");
                ddlAnioClase.Items.Add("2021");
                ddlAnioClase.Items.Add("2020");
            }
            else if (ddlCriterioMensual.SelectedItem.ToString() == "Mes")
            {
                lblDdlAnioMensual.Visible = false;
                ddlAnioClase.Visible = false;
                lblDdlMesClase.Visible = true;
                ddlMesClase.Visible = true;

                ddlMesClase.Items.Add("1");
                ddlMesClase.Items.Add("2");
                ddlMesClase.Items.Add("3");
                ddlMesClase.Items.Add("4");
                ddlMesClase.Items.Add("5");
                ddlMesClase.Items.Add("6");
                ddlMesClase.Items.Add("7");
                ddlMesClase.Items.Add("8");
                ddlMesClase.Items.Add("9");
                ddlMesClase.Items.Add("10");
                ddlMesClase.Items.Add("11");
                ddlMesClase.Items.Add("12");
            }
            else if (ddlCriterioMensual.SelectedItem.ToString() == "Año")
            {
                lblDdlMesClase.Visible = false;
                ddlMesClase.Visible = false;
                lblDdlAnioClase.Visible = true;
                ddlAnioClase.Visible = true;



                ddlAnioClase.Items.Add("2024");
                ddlAnioClase.Items.Add("2023");
                ddlAnioClase.Items.Add("2022");
                ddlAnioClase.Items.Add("2021");
                ddlAnioClase.Items.Add("2020");

            }
        }


        protected void btnMostrarFiltrosClase_Click(object sender, EventArgs e)
        {
            panelFiltrosClase.Visible = true;  // Mostrar el panel de filtros
            btnCancelarFiltrosClase.Visible = true;
        }

        protected void btnCancelarFiltroClase_Click(object sender, EventArgs e)
        {
            ddlCriterioClase.Enabled = true;
            ddlCampoClase.Enabled = true;
            btnCancelarFiltrosClase.Visible = false;
            panelFiltrosClase.Visible = false;  // Ocultar el panel de filtros
            LimpiarFiltrosClase();              // Método para limpiar los filtros
        }


        //Funciones generales -----------------------------


        private List<Pago> CargarDatos(List<Pago> listaPagos)
        {


            InscripcionClaseNegocio inscripcionClaseNegocio = new InscripcionClaseNegocio();
            List<InscripcionClase> listaInscripcionesClases = new List<InscripcionClase>();
            listaInscripcionesClases = inscripcionClaseNegocio.listarInscripcionesClases();

            CuponNegocio cuponNegocio = new CuponNegocio();
            List<Cupon> listaCupones = new List<Cupon>();
            listaCupones = cuponNegocio.listarTodosCupones();

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios = usuarioNegocio.listarUsuarios();

            foreach (Pago pago in listaPagos)
            {
                InscripcionClase inscripcion = listaInscripcionesClases.FirstOrDefault(i => i.Id == pago.Id_inscripcionClase);
                
                if (inscripcion != null)
                {
                    pago.inscripcionClase = inscripcion;
                    pago.usuario = inscripcion.usuario;
                    if (pago.Id_cupon != null)
                    {
                        pago.cupon = listaCupones.FirstOrDefault(c => c.Id == pago.Id_cupon);
                    }
                }
                else 
                {
                    Usuario usuario = listaUsuarios.FirstOrDefault(u => u.Id == pago.Id_usuario);
                    pago.usuario = usuario;
                }
            }

            return listaPagos;

        }


        private List<Usuario> ObtenerUsuarios()
        {
            //obtención de usuarios desde la base de datos
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> usuarios = usuarioNegocio.listarUsuarios();
            return usuarios;
        }

        private List<Clase> ObtenerClases()
        {
            //obtención de clases desde la base de datos
            ClaseNegocio claseNegocio = new ClaseNegocio();
            List<Clase> clases = claseNegocio.listarClases();
            return clases;
        }


        

       



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Aplica el filtro basado en los criterios seleccionados

        }

        

        
    }
}