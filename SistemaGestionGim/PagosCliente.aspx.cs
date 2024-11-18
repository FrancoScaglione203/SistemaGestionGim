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
    public partial class PagosCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["usuario"] != null)
            //{
            if (!IsPostBack)
            {
                CargarPagosMensuales(); // Cargar pagos mensuales por defecto
                CargarPagosPorClase();  // Cargar pagos por clase por defecto
                CargarPagosNoMensuales(); // Cargar pagos mensuales por defecto
                CargarPagosNoPorClase();  // Cargar pagos por clase por defecto

            }
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}

        }

        private void CargarPagosMensuales()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosMensuales();
            pagos = CargarDatos(pagos);

            pagos = pagos.Where(p => p.usuario.Id == usuarioLogueado.Id && p.Pagado == true).ToList();

            var datosGrid = pagos.Select(p => new
            {

                Mes = p.Mes,
                Anio = p.Anio, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosMensualesPagados.DataSource = datosGrid;
            gvPagosMensualesPagados.DataBind();
        }

        private void CargarPagosNoMensuales()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosMensuales();
            pagos = CargarDatos(pagos);
            pagos = pagos.Where(p => p.usuario.Id == usuarioLogueado.Id && p.Pagado == false).ToList();

            var datosGrid = pagos.Select(p => new
            {
                IdUsuario = p.Id_usuario,
                Mes = p.Mes,
                Anio = p.Anio, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosMensualesNoPagados.DataSource = datosGrid;
            gvPagosMensualesNoPagados.DataBind();
        }

        private void CargarPagosPorClase()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosClases();
            pagos = CargarDatos(pagos);
            pagos = pagos.Where(p => p.usuario.Id == usuarioLogueado.Id && p.Pagado == true).ToList();


            var datosGrid = pagos.Select(p => new
            {
                FechaClase = p.inscripcionClase.clase.FechaHorario,
                NombreClase = p.inscripcionClase.clase.Descripcion, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosClasePagados.DataSource = datosGrid;
            gvPagosClasePagados.DataBind();
        }


        private void CargarPagosNoPorClase()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagos = pagoNegocio.ListarPagosClases();
            pagos = CargarDatos(pagos);
            pagos = pagos.Where(p => p.usuario.Id == usuarioLogueado.Id && p.Pagado == false).ToList();

            var datosGrid = pagos.Select(p => new
            {
                IdUsuario = p.Id_usuario,
                IdClase = p.inscripcionClase.Id_clase,
                FechaClase = p.inscripcionClase.clase.FechaHorario,
                NombreClase = p.inscripcionClase.clase.Descripcion, // Nombre de la Clase
                ApellidoUsuario = p.usuario.Apellido, // Apellido del Usuario
                Importe = p.Importe,
                FechaPago = p.FechaPago,
                Pagado = p.Pagado ? "Si" : "No"
            }).ToList();

            gvPagosClaseNoPagados.DataSource = datosGrid;
            gvPagosClaseNoPagados.DataBind();
        }


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


        protected void GvPagosMensualesNoPagados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pagar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPagosMensualesNoPagados.Rows[index];


                string idUsuario = gvPagosMensualesNoPagados.DataKeys[index].Value.ToString();
                string mes = row.Cells[1].Text;
                string anio = row.Cells[2].Text;

                int mesInt = int.Parse(mes);
                int anioInt = int.Parse(anio);
                int idUsuarioInt = int.Parse(idUsuario);

                PagoNegocio pagoNegocio = new PagoNegocio();
                List<Pago> pagos = new List<Pago>();

                pagos = pagoNegocio.ListarPagosMensuales();

                Pago pagoEncontrado = pagos.FirstOrDefault(p =>
                    p.Id_usuario == idUsuarioInt &&
                    p.Mes == mesInt &&
                    p.Anio == anioInt);
                Session["confirmacionPagoMensual"] = pagoEncontrado;
                Response.Redirect("ConfirmacionPago.aspx");

            }
        }



        protected void gvPagosClaseNoPagados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pagar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPagosClaseNoPagados.Rows[index];


                string idUsuario = gvPagosClaseNoPagados.DataKeys[index].Values["IdUsuario"].ToString();
                string idClase = gvPagosClaseNoPagados.DataKeys[index].Values["IdClase"].ToString();

                int idUsuarioInt = int.Parse(idUsuario);
                int idClaseInt = int.Parse(idClase);
                

                PagoNegocio pagoNegocio = new PagoNegocio();
                List<Pago> pagos = new List<Pago>();

                pagos = pagoNegocio.ListarPagosClases();
                pagos = CargarDatos(pagos); 

                Pago pagoEncontrado = pagos.FirstOrDefault(p =>
                    p.Id_usuario == idUsuarioInt &&
                    p.inscripcionClase.Id_clase == idClaseInt);

                Session["confirmacionPagoClase"] = pagoEncontrado;
                Response.Redirect("confirmacionPago.aspx");
            }
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {

        }
    }
}