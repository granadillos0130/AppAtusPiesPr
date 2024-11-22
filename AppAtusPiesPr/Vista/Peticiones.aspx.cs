using AppAtusPiesPr.Datos;
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
                // Cargar las solicitudes solo en la primera carga
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
            try
            {
                int vendedorId = Convert.ToInt32(e.CommandArgument);

                // Obtener el correo electrónico del vendedor
                GridViewRow fila = (GridViewRow)((Button)sender).NamingContainer;
                string email = fila.Cells[4].Text;

                // Llamar a la lógica para aceptar la solicitud
                ClAdminL logicaSolicitud = new ClAdminL();
                bool exito = logicaSolicitud.MtdAceptarSolicitud(vendedorId, email);

                if (exito)
                {
                    // Mostrar SweetAlert de éxito
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaExito",
                        "Swal.fire({ " +
                        "title: 'Solicitud Aceptada', " +
                        "text: 'La solicitud ha sido aceptada correctamente y se ha enviado un correo al vendedor.', " +
                        "icon: 'success', " +
                        "confirmButtonText: 'Aceptar' " +
                        "});", true);

                    CargarPeticiones(); // Recargar la tabla
                }
                else
                {
                    // Mostrar SweetAlert de error
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
                // Mostrar SweetAlert de error al capturar la excepción
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


      
