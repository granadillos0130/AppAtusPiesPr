using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
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
                cargarMarcas();

            }
            if (Session["idUsuario"] != null)
            {
                Response.Write("<script>console.log('idUsuario en sesión: " + Session["idUsuario"] + "');</script>");
            }
            else
            {
                Response.Write("<script>console.log('idUsuario en sesión es NULL');</script>");
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

        [WebMethod(EnableSession = true)]

        public static object GuardarPedido(ClPedidosE pedido)
        {
            try
            {
                if (pedido == null || pedido.Detalles == null || pedido.Detalles.Count == 0)
                {
                    return new { success = false, message = "El pedido o los detalles están vacíos." };
                }

                // Recuperar usuario de la sesión
                var usuario = HttpContext.Current.Session["usuario"] as ClUsuarioE;

                // Si el pedido tiene un IdCliente válido, úsalo. Si no, intenta obtenerlo desde la sesión.
                pedido.IdCliente = pedido.IdCliente > 0 ? pedido.IdCliente : (usuario?.IdUsuario ?? 0);

                if (pedido.IdCliente == 0)
                {
                    return new { success = false, message = "No se ha iniciado sesión o el ID de usuario es inválido." };
                }

                pedido.FechaPedido = DateTime.Now;
                pedido.Estado = "Pendiente";

                // Guardar el pedido en la base de datos
                ClPedidosL logicaPedido = new ClPedidosL();
                int idPedido = logicaPedido.MtdGuardarPedido(pedido, pedido.Detalles);

                return idPedido > 0
                    ? new { success = true, message = "Pedido guardado exitosamente con ID: " + idPedido }
                    : new { success = false, message = "No se pudo guardar el pedido." };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error al guardar el pedido: " + ex.Message };
            }
        }

        private void cargarMarcas()
        {
            ClProductoL oLogica = new ClProductoL();
            RepeaterMarca.DataSource = oLogica.MtdListarMarcas();
            RepeaterMarca.DataBind();
        }


    }

}