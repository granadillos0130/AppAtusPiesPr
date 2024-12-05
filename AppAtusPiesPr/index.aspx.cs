using AppAtusPiesPr.Logica;
using AppAtusPiesPr.Vista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr
{
    public partial class Index2 : System.Web.UI.Page
    {
        ClProductoL oDatos = new ClProductoL();
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1)); // Fecha de expiración pasada
            Response.Cache.SetNoStore();
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

                    cargarProductos();
                }
            }

        }

        private void cargarProductos()
        {
            ClProductoL objProductoL = new ClProductoL();
            DataTable dt = objProductoL.MtdListarProductos();
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