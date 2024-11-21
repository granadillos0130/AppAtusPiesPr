using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Web.UI;

namespace AppAtusPiesPr.Vista
{
    public partial class Maestra : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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



                }
                else if (rol == "Vendedor")
                {
                    LblPuesto.Text = "Vendedor";
                    menuListarUsuarios.Visible = false;
                    menuRegistrarVendedor.Visible = false;
                    menuListarVendedores.Visible = false;
                   




                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }
    }
}
