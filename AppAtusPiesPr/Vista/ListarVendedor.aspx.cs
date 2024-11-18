using AppAtusPiesPr.Logica;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class ListarVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVendedor();
            }
        }

        private void CargarVendedor()
        {
            ClAdminL vendedorL = new ClAdminL();
            DataTable vendedor = vendedorL.MtdListarVendedores();

            gvVendedores.DataSource = vendedor;
            gvVendedores.DataBind();
        }
    }
}
