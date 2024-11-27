using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;

namespace AppAtusPiesPr.Vista
{
    public partial class ProductosMasVendidos : System.Web.UI.Page
    {
        ClVendedorL productoL = new ClVendedorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar productos con rango predeterminado
                cargarProductos(DateTime.Now.AddMonths(-1), DateTime.Now);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener fechas desde los campos ocultos
                string fechaInicioStr = hfFechaInicio.Value;
                string fechaFinStr = hfFechaFin.Value;

                if (string.IsNullOrEmpty(fechaInicioStr) || string.IsNullOrEmpty(fechaFinStr))
                {
                    lblMensaje.Text = "Por favor selecciona las fechas de inicio y fin.";
                    lblMensaje.Visible = true;
                    return;
                }

                DateTime fechaInicio = DateTime.Parse(fechaInicioStr);
                DateTime fechaFin = DateTime.Parse(fechaFinStr);

                if (fechaInicio > fechaFin)
                {
                    lblMensaje.Text = "La fecha de inicio no puede ser mayor que la fecha de fin.";
                    lblMensaje.Visible = true;
                    return;
                }

                cargarProductos(fechaInicio, fechaFin);
                lblMensaje.Visible = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }
        private void cargarProductos(DateTime fechaInicio, DateTime fechaFin)
        {
            if (Session["idUsuario"] == null)
            {
                lblMensaje.Text = "No se ha encontrado la información del vendedor en la sesión.";
                lblMensaje.Visible = true;
                return;
            }

            int idVendedor;
            if (!int.TryParse(Session["idUsuario"].ToString(), out idVendedor))
            {
                lblMensaje.Text = "El ID del vendedor en la sesión no es válido.";
                lblMensaje.Visible = true;
                return;
            }



            List<ClProductoE> productos = productoL.MtdObtenerProductosMasVendidosPorVendedor(idVendedor, fechaInicio, fechaFin);

            if (productos.Count > 0)
            {
                gvProductos.DataSource = productos;
                gvProductos.DataBind();
            }
            else
            {
                gvProductos.DataSource = null;
                gvProductos.DataBind();
                lblMensaje.Text = "No se encontraron productos para el rango de fechas especificado.";
                lblMensaje.Visible = true;
            }
        }






    }
}
