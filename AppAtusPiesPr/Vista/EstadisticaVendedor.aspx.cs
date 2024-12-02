using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AppAtusPiesPr.Entidades.ClUsuarioE;

namespace AppAtusPiesPr.Vista
{
    public partial class EstadisticaVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string documento = txtDocumento.Text;
            string año = ddlAño.Text;
            string mes = ddlMes.Text;

            ClAdminL EstadisticaL = new ClAdminL();

            List<ClEstadisticaVendedorE> estadisticas = EstadisticaL.MtdEstadistica(documento, año, mes);

            gvEstadisticasVendedor.DataSource = estadisticas;
            gvEstadisticasVendedor.DataBind();

        }
    }
}