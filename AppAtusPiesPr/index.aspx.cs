using AppAtusPiesPr.Logica;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1)); // Fecha de expiración pasada
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                cargarProductos();
                cargarCategorias();
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

    }
}