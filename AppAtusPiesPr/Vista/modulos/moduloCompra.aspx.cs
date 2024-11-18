using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista.modulos
{
    public partial class moduloCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
                cargarCategorias();

                string productoId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(productoId) && int.TryParse(productoId, out int id))
                {
                    cargarProducto(id);

                }
            }
        }
    }
}