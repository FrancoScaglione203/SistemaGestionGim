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
    public partial class ConfirmacionPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["confirmacionPagoClase"] != null)
                    {
                        Pago pago = (Pago)Session["confirmacionPagoClase"];
                        // Modo clase
                        pnlClase.Visible = true;
                        pnlMensual.Visible = false;

                        // Ejemplo: Asignar valores
                        lblClase.Text = pago.inscripcionClase.clase.Descripcion;
                        lblImporte.Text = pago.Importe.ToString();
                        lblImporteFinal.Text = pago.Importe.ToString();
                    }
                    else if (Session["confirmacionPagoMensual"] != null)
                    {
                        Pago pago = (Pago)Session["confirmacionPagoMensual"];
                        // Modo mensual
                        pnlClase.Visible = false;
                        pnlMensual.Visible = true;

                        // Ejemplo: Asignar valores
                        lblMes.Text = pago.Mes.ToString();
                        lblAnio.Text = pago.Anio.ToString();
                        lblImporte.Text = pago.Importe.ToString();
                        lblImporteFinal.Text = pago.Importe.ToString();
                    }
                    else
                    {
                        pnlConfirmacionPago.Visible = false; // No hay pago en curso
                    }

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

            PagoNegocio pagoNegocio = new PagoNegocio();
            Pago pago = new Pago();
            int idCupon = 0;
            if (Session["IdCupon"] != null)
            {
                idCupon = (int)Session["IdCupon"];
            }

            int importeFinal = int.Parse(lblImporteFinal.Text);
            if (Session["confirmacionPagoClase"] != null)
            {
                pago = (Pago)Session["confirmacionPagoClase"];
                if (Session["IdCupon"] != null)
                {
                    pago.Id_cupon = idCupon;
                }
                pagoNegocio.PagarClase(pago, importeFinal);
                Session["IdCupon"] = null;
                Response.Redirect("PagosCliente.aspx");
            }
            else
            {
                pago = (Pago)Session["confirmacionPagoMensual"];
                if (Session["IdCupon"] != null)
                {
                    pago.Id_cupon = idCupon;
                }
                pagoNegocio.PagarMes(pago, importeFinal);
                Session["IdCupon"] = null;
                Response.Redirect("PagosCliente.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagosCliente.aspx");
        }



        protected void btnValidar_Click(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            CuponNegocio cuponNegocio = new CuponNegocio();
            List<Cupon> cupones = new List<Cupon>();
            cupones = cuponNegocio.listarCupones();

            PagoNegocio pagoNegocio = new PagoNegocio();
            List<Pago> pagosClases = new List<Pago>();
            List<Pago> pagosMensuales = new List<Pago>();
            pagosClases = pagoNegocio.ListarPagosClases();
            pagosMensuales = pagoNegocio.ListarPagosMensuales();
            Pago pago = new Pago();


            if (Session["confirmacionPagoClase"] != null)
            {
                pago = (Pago)Session["confirmacionPagoClase"];
            }
            else
            {
                pago = (Pago)Session["confirmacionPagoMensual"];
            }

            string codigoCupon = txtCupon.Text.Trim();
            if (string.IsNullOrEmpty(codigoCupon)) { return; }

            Cupon cuponEncontrado = cupones.FirstOrDefault(c => c.Codigo == codigoCupon);
            Pago cuponUsadoClases = pagosClases.FirstOrDefault(p => p.Id_usuario == usuarioLogueado.Id && p.Id_cupon == cuponEncontrado.Id);
            Pago cuponUsadoMensuales = pagosMensuales.FirstOrDefault(p => p.Id_usuario == usuarioLogueado.Id && p.Id_cupon == cuponEncontrado.Id);






            if (cuponEncontrado != null)
            {
                if (cuponUsadoClases != null && cuponUsadoMensuales != null)
                {
                    Session["IdCupon"] = cuponEncontrado.Id;
                    int importe = pago.Importe;
                    int descuento = 0;
                    decimal descuentoDecimal = cuponEncontrado.Descuento / 100m * importe;
                    descuento = (int)Math.Round(descuentoDecimal); // Redondear a entero
                    int importeFinal = importe - descuento;

                    lblDescuento.Text = descuento.ToString();
                    lblImporteFinal.Text = importeFinal.ToString();
                    Session["validacionCupon"] = "Cupón válido";  // Mensaje de validación
                }
                else
                {
                    Session["validacionCupon"] = "Ya utilizaste este cupon de descuento.";
                }
            }
            else
            {
                Session["validacionCupon"] = "Cupón no encontrado";  // Mensaje de error
            }

        }
    }
}