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
    public partial class Peticiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPeticiones();
            }
        }

        private void CargarPeticiones()
        {
            ClAdminL solicitud = new ClAdminL();
            DataTable dtSolicitudes = solicitud.MtdObtenerSolicitudes();

            gvSolicitudes.DataSource = dtSolicitudes;
            gvSolicitudes.DataBind();
        }


        protected void btnAceptar_Command(object sender, CommandEventArgs e)
        {

        }

        protected void btnDenegar_Command(object sender, CommandEventArgs e)
        {

        }
    }
}