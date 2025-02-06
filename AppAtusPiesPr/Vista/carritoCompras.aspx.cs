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

        [WebMethod]
        public static string GuardarPedido(ClPedidosE pedido, List<DetallePedido> detalles)
        {
            try
            {
                if (pedido == null || detalles == null || detalles.Count == 0)
                    throw new ArgumentException("Los datos del pedido o los detalles son inválidos.");

                // Recuperar el usuario desde la sesión
                var usuario = HttpContext.Current.Session["usuario"] as ClUsuarioE;

                if (usuario == null)
                    throw new UnauthorizedAccessException("No se ha iniciado sesión.");

                pedido.IdCliente = usuario.IdUsuario;
                pedido.FechaPedido = DateTime.Now;
                pedido.Estado = "Pendiente"; // Estado inicial del pedido

                ClPedidosL logicaPedido = new ClPedidosL();
                int idPedido = logicaPedido.MtdGuardarPedido(pedido, detalles);

                return idPedido > 0
                    ? "Pedido guardado exitosamente con ID: " + idPedido
                    : "No se pudo guardar el pedido. Intenta nuevamente.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en GuardarPedido: " + ex.Message);
                return "Error al guardar el pedido: " + ex.Message;
            }
        }

        [WebMethod]
        public static string ObtenerIdCliente()
        {
            try
            {
                var usuario = HttpContext.Current.Session["usuario"] as ClUsuarioE;

                if (usuario == null)
                    throw new UnauthorizedAccessException("No se ha iniciado sesión.");

                return usuario.IdUsuario.ToString(); // Enviar el IdCliente como string
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


    }

}