using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
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

            List<ClUsuarioE> objVendedores = new List<ClUsuarioE>();

            foreach (DataRow fila in vendedor.Rows) {

                objVendedores.Add(new ClUsuarioE{
                
                    IdUsuario = int.Parse( fila["idVendedor"].ToString()),
                    Nombres = fila["nombres"].ToString(),
                    Apellidos = fila["apellidos"].ToString(),
                    Documento = fila["documento"].ToString(),
                    Email = fila["email"].ToString(),
                    Telefono = fila["telefono"].ToString(),
                    Direccion = fila["direccion"].ToString(),
                    estado = fila["estado"].ToString()

                });
            
            
            }

            gvVendedores.DataSource = objVendedores;
            gvVendedores.DataBind();
        }

        protected void gvVendedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                var estado = DataBinder.Eval(e.Row.DataItem, "estado");

                if (estado != null)
                {
                   
                    string estadoStr = estado.ToString().ToLower();

                   
                    Button btnInactivar = (Button)e.Row.FindControl("btnInactivar");

                    if (estadoStr == "activo")
                    {
                        btnInactivar.Visible = true; 
                    }
                    else
                    {
                        btnInactivar.Visible = false; 
                    }
                }
                else
                {

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
                string nombreVendedor = fila.Cells[1].Text;
                string apellidoVendedor = fila.Cells[2].Text;

                ClAdminL inactivar = new ClAdminL();
                bool exito = inactivar.MtdInactivarVendedor(vendedorId, email,nombreVendedor,apellidoVendedor);

                if (exito)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaExito",
                           "Swal.fire({ " +
                           "title: 'Solicitud Aceptada', " +
                           "text: 'La solicitud ha sido Rechazada correctamente y se ha enviado un correo al vendedor.', " +
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
                        "text: 'No se pudo Rechazar la solicitud. Intenta de nuevo.', " +
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
                        $"text: 'Error al Rechazar la solicitud: {ex.Message}', " +
                        $"icon: 'error', " +
                        $"confirmButtonText: 'Aceptar' " +
                        $"}});", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            string Documento = string.IsNullOrWhiteSpace(txtDocumento.Text) ? null : txtDocumento.Text;
            string estado = string.IsNullOrWhiteSpace(ddlEstado.SelectedValue) ? null : ddlEstado.SelectedValue;
          

            ClAdminL vendedorL = new ClAdminL();
            List<ClUsuarioE> vendedores = vendedorL.MtdFiltroVendedor(Documento,estado);

            if (vendedores != null && vendedores.Count > 0)
            {

                gvVendedores.DataSource = vendedores;
                gvVendedores.DataBind();
            }
            else
            {

                gvVendedores.DataSource = null;
                gvVendedores.DataBind();

                // Mostrar mensaje de alerta usando SweetAlert
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('No se encontraron vendedores.');", true);
            }
        }


    }
}

