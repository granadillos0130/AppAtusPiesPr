using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class IndexMaestra : System.Web.UI.MasterPage
    {
        ClClienteL oClienteL = new ClClienteL();
        protected HtmlGenericControl notificacionBadge;
        protected HtmlGenericControl listaNotificaciones;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                Session["ModuloActual"] = "index"; // Cambiar según el módulo

                menuConSesion.Visible = false;
                menuSinSesion.Visible = false;

                if (Session["usuario"] != null)
                {
                    int usuarioID = Convert.ToInt32(Session["idUsuario"]); // Obtener el ID del cliente
                    string rol = Session["Rol"]?.ToString();
                    nombreCliente.Text = Session["usuario"].ToString();

                    if (!string.IsNullOrEmpty(rol) && rol == "Cliente")
                    {
                        menuConSesion.Visible = true;
                        CargarNotificaciones(usuarioID);

                    }
                    else
                    {
                        menuSinSesion.Visible = true;
                    }
                }
                else
                {
                    menuSinSesion.Visible = true;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            HttpCookie cookies = Request.Cookies[".ASPXAUTH"];
            if (cookies != null)
            {
                cookies.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookies);
            }

            Response.Redirect("~/index.aspx");

        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            // Identifica el control que dispara el evento
            string controlId = (sender as Control)?.ID;

            if (controlId == "txtBusqueda")
            {
                string busqueda = txtBusqueda.Text.Trim();
                if (!string.IsNullOrEmpty(busqueda))
                {
                    string rutaBusqueda = ResolveUrl("~/Vista/moduloBusqueda.aspx");
                    Response.Redirect($"{rutaBusqueda}?busqueda={Server.UrlEncode(busqueda)}");
                }
            }
            else if (controlId == "txtBusqueda2")
            {
                string busqueda2 = txtBusqueda2.Text.Trim();
                if (!string.IsNullOrEmpty(busqueda2))
                {
                    string rutaBusqueda2 = ResolveUrl("~/Vista/moduloBusqueda.aspx");
                    Response.Redirect($"{rutaBusqueda2}?busqueda={Server.UrlEncode(busqueda2)}");
                }
            }
        }

        private void CargarNotificaciones(int usuarioID)
        {
            try
            {
                // Obtener notificaciones desde la capa de lógica
                ClProductoL oL = new ClProductoL();
                List<ClProductoEmpresaE> notificaciones = oL.mtdNotificaciones(usuarioID);

                // Construir el HTML de las notificaciones
                StringBuilder sb = new StringBuilder();

                if (notificaciones != null && notificaciones.Count > 0)
                {
                    foreach (var notificacion in notificaciones)
                    {
                        // Validar que los campos no sean null antes de usarlos
                        string nombreProducto = notificacion.nombreProducto ?? "Producto sin nombre";
                        string nombreVendedor = notificacion.nombreVendedor ?? "Vendedor desconocido";
                        string apellidoVendedor = notificacion.apellidoVendedor ?? "Vendedor desconocido";

                        sb.Append("<div class='notificacion-item'>");
                        sb.Append("<div class='notificacion-contenido'>");
                        // Escapar los valores para evitar problemas de XSS
                        sb.Append($"<strong>{Server.HtmlEncode(nombreProducto)}</strong>");
                        sb.Append($"<p>Publicado por <strong>{Server.HtmlEncode(nombreVendedor)}</strong><strong>{Server.HtmlEncode(apellidoVendedor)}</strong></p>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                    }

                    // Actualizar el contador y mostrar el badge
                    if (notificacionBadge != null)
                    {
                        notificacionBadge.InnerText = notificaciones.Count.ToString();
                        notificacionBadge.Style["display"] = "inline";
                    }

                    // Mostrar el panel de notificaciones
                    if (panelNotificaciones != null)
                    {
                        panelNotificaciones.Style["display"] = "block";
                    }
                }
                else
                {
                    // Mensaje cuando no hay notificaciones
                    sb.Append("<div class='notificacion-item'>");
                    sb.Append("<p>No hay notificaciones nuevas</p>");
                    sb.Append("</div>");

                    // Ocultar el badge
                    if (notificacionBadge != null)
                    {
                        notificacionBadge.Style["display"] = "none";
                    }

                    // Ocultar el panel de notificaciones
                    if (panelNotificaciones != null)
                    {
                        panelNotificaciones.Style["display"] = "none";
                    }
                }

                // Actualizar el contenedor de notificaciones
                if (listaNotificaciones != null)
                {
                    listaNotificaciones.InnerHtml = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                // Manejar el error de manera apropiada
                Console.WriteLine($"Error al cargar notificaciones: {ex.Message}");

                // Mostrar mensaje de error al usuario
                if (listaNotificaciones != null)
                {
                    listaNotificaciones.InnerHtml = "<div class='notificacion-error'>Error al cargar las notificaciones</div>";
                }
            }
        }
        protected void txtBusqueda2_TextChanged(object sender, EventArgs e)
        {
            string busqueda2 = txtBusqueda2.Text.Trim();

            string moduloActual = Session["ModuloActual"] as string;

            if (!string.IsNullOrEmpty(busqueda2))
            {

                string rutaBusqueda2 = ResolveUrl("~/Vista/moduloBusqueda.aspx");
                Response.Redirect($"{rutaBusqueda2}?busqueda={Server.UrlEncode(busqueda2)}");

            }
        }
    }
}
