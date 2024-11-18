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
    public partial class ProductosMasVendidos : System.Web.UI.Page
    {

        ClProductoL productoL = new ClProductoL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos(DateTime.Now.AddMonths(-1), DateTime.Now);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = DateTime.Parse(txtFechaInicio.Text.Trim());
            DateTime fechaFin = DateTime.Parse(txtFechaFin.Text.Trim());
            cargarProductos(fechaInicio, fechaFin);
        }

        private void cargarProductos (DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClProductoE>productos=productoL.MtdObtenerProductosMasVendidos(fechaInicio,fechaFin);
            gvProductos.DataSource = productos;
            gvProductos.DataBind(); 
        }
    }
}