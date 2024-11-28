using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Web;
using System.Web.UI;

namespace AppAtusPiesPr.Vista
{
    public partial class Maestra : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1)); // Fecha de expiración pasada
            Response.Cache.SetNoStore();

            // Redirigir al Login si no hay sesión activa
            if (Session["Usuario"] == null) // Cambia "Usuario" por el nombre de tu variable de sesión
            {
                Response.Redirect("Login.aspx");
            }
            ClAdminL user = new ClAdminL();
            
            if (Session["usuario"] == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }

            lblUsuario.Text = Session["usuario"].ToString();

            if (!IsPostBack)
            {
                string rol = Session["Rol"]?.ToString();

                if (rol == "Admin")
                {
                    LblPuesto.Text = "Administrador";
                    menuRegistrarProducto.Visible = false;
                    menuMasVendidos.Visible = false;
                    menuProductosPorCategoria.Visible = false;
                    menuEstadoPedido.Visible = false;
                    menuActualizarProducto.Visible = false;

                }
                else if (rol == "Vendedor")
                {
                    LblPuesto.Text = "Vendedor";
                    menuCategoria.Visible = false;
                    menuPeticiones.Visible = false;
                    menuListarVendedores.Visible = false;
                   




                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar las variables de sesión
            Session.Clear();
            Session.Abandon();

            // Opcional: Eliminar cookies de autenticación si estás utilizando Forms Authentication
            HttpCookie authCookie = Request.Cookies[".ASPXAUTH"];
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }

            // Redirigir al usuario a la página de inicio o de login
            Response.Redirect("Login.aspx");
        }
    }
}
