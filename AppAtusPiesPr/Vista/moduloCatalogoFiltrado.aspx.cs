using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class moduloCatalogoFiltrado2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCategoria = Convert.ToInt32(Request.QueryString["id"]); // Obtener idCategoria desde la URL
                cargarProductos(idCategoria);
                cargarCategorias();

            }
        }

        private void cargarProductos(int idCategoria)
        {
            ClProductoL objProductoL = new ClProductoL();
            DataTable dt = objProductoL.MtdListarPorFiltro(idCategoria);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }
        private void busquedas(string busqueda)
        {
            ClProductoL oDatos = new ClProductoL();

            DataTable dtProductos = oDatos.mtdBuscarProducto(busqueda);
            if (dtProductos.Rows.Count > 0)
            {
                Repeater1.DataSource = dtProductos;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
        }
    }
}