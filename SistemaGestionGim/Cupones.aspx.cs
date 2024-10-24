using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionGim
{
    public partial class Cupones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (Session["usuario"] != null) 
                {
                    CargarCupones();

                    if (Session["Descuento"] != null)
                        txtDescuento.Text = Session["Descuento"].ToString();
                    if (Session["Codigo"] != null)
                        txtCodigo.Text = Session["Codigo"].ToString();
                    if (Session["FechaVenc"] != null)
                        txtFechaVencimiento.Text = Session["FechaVenc"].ToString();
                }
                else
                {
                    // Si no hay un usuario en la sesión, redirigir al login o a una página de error
                    Response.Redirect("Login.aspx");
                }
            }
            

            if (Session["validacionCupon"] != null)
            {
                panelAgregarCupon.Visible = true;
                btnAgregarCupon.Visible = false;
                repeaterCupones.Visible = false;
            }


        }

        private void CargarCupones()
        {
            CuponNegocio cuponNegocio = new CuponNegocio();
            List<Cupon> listaCupones = cuponNegocio.listarCupones();

            repeaterCupones.DataSource = listaCupones;
            repeaterCupones.DataBind();
        }

        protected void btnCancelarCupon_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int cuponId = int.Parse(btn.CommandArgument);

            CuponNegocio cuponNegocio = new CuponNegocio();
            cuponNegocio.EliminarCupon(cuponId);
            // Aquí puedes desactivar el cupón en la base de datos usando el ID.

            Response.Redirect("Cupones.aspx");
        }


        protected void btnAgregarCupon_Click(object sender, EventArgs e)
        {
            panelAgregarCupon.Visible = true;
            btnAgregarCupon.Visible = false;
            repeaterCupones.Visible = false;
        }

        public bool EsCodigoValido(string codigo)
        {
            // Definir la expresión regular: ^ indica el inicio, [A-Za-z] para una letra, \d para un dígito y {3} para exactamente 3 dígitos.
            string patron = @"^[A-Za-z]\d{3}$";

            // Comprobar si el código cumple con el patrón
            return Regex.IsMatch(codigo, patron);
        }

        protected void btnGuardarCupon_Click(object sender, EventArgs e)
        {

            try
            {
                Cupon cupon = new Cupon
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Descuento = int.Parse(txtDescuento.Text.Trim()),
                    FechaVencimiento = DateTime.ParseExact(txtFechaVencimiento.Text.Trim(), "yyyy-MM-dd", null).Date, // Asegura que solo la fecha sea considerada
                    Activo = true
                };




                if (!(cupon.Descuento <= 0 || cupon.Descuento > 100))
                {
                    if (EsCodigoValido(cupon.Codigo))
                    {
                        if(cupon.FechaVencimiento > DateTime.Now) 
                        {
                            CuponNegocio cuponNegocio = new CuponNegocio();
                            cuponNegocio.InsertarNuevo(cupon);
                            Session["Descuento"] = null;
                            Session["Codigo"] = null;
                            Session["FechaVenc"] = null;
                            Session["validacionCupon"] = null;
                            Response.Redirect("Cupones.aspx");
                        }
                        else 
                        {
                            String ErrorFecha = "La fecha ingresada es invalida";
                            Session["validacionCupon"] = ErrorFecha;
                            Session["Descuento"] = cupon.Descuento;
                            Session["Codigo"] = cupon.Codigo;
                            Session["FechaVenc"] = cupon.FechaVencimiento;

                            Response.Redirect("Cupones.aspx");
                        }
                    }
                    else
                    {
                        String ErrorCodigo = "Formato de codigo invalido, debe ser 1 letra seguida de 3 numeros";
                        Session["validacionCupon"] = ErrorCodigo;
                        Session["Descuento"] = cupon.Descuento;
                        Session["Codigo"] = cupon.Codigo;
                        Session["FechaVenc"] = cupon.FechaVencimiento;

                        Response.Redirect("Cupones.aspx");
                    }
                }
                else 
                {
                        String ErrorDescuento = "El porcentaje de descuento es invalido";
                        Session["validacionCupon"] = ErrorDescuento;
                        Session["Descuento"] = cupon.Descuento;
                        Session["Codigo"] = cupon.Codigo;
                        Session["FechaVenc"] = cupon.FechaVencimiento;

                        Response.Redirect("Cupones.aspx");
                }
                
            }
            catch (Exception ex)
            {
                // Manejar errores y mostrar mensaje si es necesario.
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cupones.aspx");
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
        }
    }
}