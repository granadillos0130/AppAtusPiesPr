using AppAtusPiesPr.Logica;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class ListarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuario();
            }
        }

        private void CargarUsuario()
        {
            ClAdminL usuarioL = new ClAdminL();
            DataTable usuarios = usuarioL.MtdListarUsuarios();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();
        }
    }
}
