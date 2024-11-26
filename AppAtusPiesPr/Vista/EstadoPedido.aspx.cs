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
    public partial class EstadoPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string estadoSeleccionado = ddlEstado.SelectedValue;
            CargarPedidos(estadoSeleccionado);
        }

        private void CargarPedidos(string estado)
        {
            if (HttpContext.Current.Session["idVendedor"] != null)
            {
                try
                {
                    int idVendedor = Convert.ToInt32(HttpContext.Current.Session["idVendedor"]);
                    ClAdminL adminL = new ClAdminL();
                    DataTable pedidos = adminL.MtdListarPedido(idVendedor, estado);

                    if (pedidos != null && pedidos.Rows.Count > 0)
                    {
                        gvPedidos.DataSource = pedidos;
                        gvPedidos.DataBind();
                        gvPedidos.Visible = true;
                    }
                    else
                    {
                        gvPedidos.DataSource = null;
                        gvPedidos.DataBind();
                        // SweetAlert para "No hay datos"
                        ScriptManager.RegisterStartupScript(this, GetType(), "NoData",
                            @"Swal.fire({
                                icon: 'info',
                                title: 'Sin resultados',
                                text: 'No hay pedidos con el estado seleccionado.',
                                confirmButtonColor: '#3085d6'
                            });", true);
                    }
                }
                catch (Exception ex)
                {
                    // SweetAlert para errores
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error",
                        $@"Swal.fire({{
                            icon: 'error',
                            title: 'Error',
                            text: 'Error al cargar los pedidos: {ex.Message}',
                            confirmButtonColor: '#d33'
                        }});", true);
                }
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "SessionExpired",
                    @"Swal.fire({
                        icon: 'warning',
                        title: 'Sesión Expirada',
                        text: 'Por favor, inicie sesión nuevamente.',
                        confirmButtonColor: '#3085d6'
                    }).then((result) => {
                        window.location.href = 'Login.aspx';
                    });", true);
            }
        }
    }
}