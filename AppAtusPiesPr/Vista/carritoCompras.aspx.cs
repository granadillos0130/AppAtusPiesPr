using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class carritoCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                cargarCategorias();

            }
        }

        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }

        [WebMethod]
        public static bool VerificarSesion()
        {
            try
            {
                return HttpContext.Current.Session["usuario"] != null;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en VerificarSesion: " + ex.Message);
                throw;
            }
        }


    }

}