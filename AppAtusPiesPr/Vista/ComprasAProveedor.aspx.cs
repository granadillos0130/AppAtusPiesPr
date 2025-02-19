using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class ComprasAProveedor : System.Web.UI.Page
    {
        private ClVendedorL proveedorLogica = new ClVendedorL();
        private ClVendedorL compraLogica = new ClVendedorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    int idVendedor = Convert.ToInt32(Session["idUsuario"]);
                    CargarProveedores(idVendedor);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CargarProveedores(int idVendedor)
        {
            List<ClProveedorE> listaProveedores = proveedorLogica.MtdListarProveedorVendedor(idVendedor);

            ddlProveedores.DataSource = listaProveedores;
            ddlProveedores.DataTextField = "Nombre";
            ddlProveedores.DataValueField = "IdProveedor";
            ddlProveedores.DataBind();

            ddlProveedores.Items.Insert(0, new ListItem("-- Seleccione un proveedor --", "0"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlProveedores.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtNumeroFactura.Text) || string.IsNullOrWhiteSpace(txtTotalCompra.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "mostrarAlerta('error', 'Todos los campos son obligatorios');", true);
                return;
            }

            int idProveedor = Convert.ToInt32(ddlProveedores.SelectedValue);
            string numeroFactura = txtNumeroFactura.Text;
            decimal totalCompra = Convert.ToDecimal(txtTotalCompra.Text);
            int idVendedor = Convert.ToInt32(Session["idUsuario"]);
            DateTime fechaCompra = DateTime.Now;

            bool resultado = compraLogica.GuardarCompra(numeroFactura, totalCompra, idProveedor, idVendedor);

            if (resultado)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "mostrarAlerta('success', 'Compra registrada exitosamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "mostrarAlerta('error', 'Error al registrar la compra');", true);
            }
        }
    }
}