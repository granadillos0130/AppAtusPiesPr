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

        protected void gvVendedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtén el ID del vendedor a partir del CommandArgument
            string idVendedor = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                // Redirige a una página de edición con el ID del vendedor como parámetro
                Response.Redirect($"EditarVendedor.aspx?idVendedor={idVendedor}");
            }
            else if (e.CommandName == "Eliminar")
            {
                // Elimina al vendedor usando su ID
                EliminarVendedor(idVendedor);

                // Recargar la lista después de eliminar
                CargarVendedor();
            }
        }

        private void EliminarVendedor(string idVendedor)
        {
            //ClAdminL vendedorL = new ClAdminL();
            //bool resultado = vendedorL.MtdEliminarVendedor(idVendedor);

            //if (!resultado)
            //{
            //    // Maneja el error si no se pudo eliminar el vendedor
            //    // Por ejemplo, muestra un mensaje en pantalla
            //    Response.Write("<script>alert('Error al eliminar el vendedor.');</script>");
            //}
        }
    }
}
