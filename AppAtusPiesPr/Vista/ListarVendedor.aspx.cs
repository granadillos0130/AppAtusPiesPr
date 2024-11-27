using AppAtusPiesPr.Logica;
using System;
using System.Data;
using System.Web.UI;
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

        protected void gvVendedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el valor de "estado" de forma segura
                var estado = DataBinder.Eval(e.Row.DataItem, "estado");

                if (estado != null)
                {
                    // Si "estado" no es null, lo convertimos a string
                    string estadoStr = estado.ToString().ToLower();

                    // Buscar el botón de inactivación
                    Button btnInactivar = (Button)e.Row.FindControl("btnInactivar");

                    if (estadoStr == "activo")
                    {
                        btnInactivar.Visible = true;  // Muestra el botón si el estado es 'activo'
                    }
                    else
                    {
                        btnInactivar.Visible = false;  // Oculta el botón si el estado no es 'activo'
                    }
                }
                else
                {
                    // Si "estado" es null, ocultar el botón de inactivar (o manejar el caso de otra forma)
                    Button btnInactivar = (Button)e.Row.FindControl("btnInactivar");
                    btnInactivar.Visible = false;
                }
            }
        }



        protected void btnInactivar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int vendedorId = Convert.ToInt32(e.CommandArgument);

                GridViewRow fila = (GridViewRow)((Button)sender).NamingContainer;
                string email = fila.Cells[4].Text;

                ClAdminL inactivar = new ClAdminL();
                bool exito = inactivar.MtdInactivarVendedor(vendedorId, email);

                if (exito)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaExito",
                           "Swal.fire({ " +
                           "title: 'Solicitud Aceptada', " +
                           "text: 'La solicitud ha sido aceptada correctamente y se ha enviado un correo al vendedor.', " +
                           "icon: 'success', " +
                           "confirmButtonText: 'Aceptar' " +
                           "});", true);

                    CargarVendedor();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError",
                        "Swal.fire({ " +
                        "title: 'Error', " +
                        "text: 'No se pudo aceptar la solicitud. Intenta de nuevo.', " +
                        "icon: 'error', " +
                        "confirmButtonText: 'Aceptar' " +
                        "});", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaExcepcion",
                        $"Swal.fire({{ " +
                        $"title: 'Error', " +
                        $"text: 'Error al aceptar la solicitud: {ex.Message}', " +
                        $"icon: 'error', " +
                        $"confirmButtonText: 'Aceptar' " +
                        $"}});", true);
            }
        }
    }
}

