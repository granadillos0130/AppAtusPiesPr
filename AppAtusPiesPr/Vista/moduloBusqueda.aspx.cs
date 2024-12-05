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
    public partial class moduloBusqueda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();

                string busqueda = Request.QueryString["busqueda"];
                if (!string.IsNullOrEmpty(busqueda))
                {
                    busquedas(busqueda);
                }
                else
                {
                }
            }
        }

        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }

        private void busquedas(string busqueda)
        {
            ClProductoL oLogica = new ClProductoL();
            DataTable dtProductos = oLogica.mtdBuscarProducto(busqueda);
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