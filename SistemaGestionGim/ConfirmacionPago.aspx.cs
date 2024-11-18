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

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            PagoNegocio pagoNegocio = new PagoNegocio();
            Pago pago = new Pago();
            int importeFinal = int.Parse(lblImporteFinal.Text);
            if (Session["confirmacionPagoClase"] != null)
            {
                pago = (Pago)Session["confirmacionPagoClase"];
                pagoNegocio.PagarClase(pago, importeFinal);
                Response.Redirect("PagosCliente.aspx");
            }
            else
            {
                pago = (Pago)Session["confirmacionPagoMensual"];
                pagoNegocio.PagarMes(pago, importeFinal);
                Response.Redirect("PagosCliente.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagosCliente.aspx");
        }



        protected void btnValidar_Click(object sender, EventArgs e)
        {
            CuponNegocio cuponNegocio = new CuponNegocio();
            List<Cupon> cupones = new List<Cupon>();
            cupones = cuponNegocio.listarCupones();
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
            Cupon cuponEncontrado = cupones.FirstOrDefault(c => c.Codigo == codigoCupon);
            if (cuponEncontrado != null)
            {


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
                Session["validacionCupon"] = "Cupón no encontrado";  // Mensaje de error
            }
        }
    }
}