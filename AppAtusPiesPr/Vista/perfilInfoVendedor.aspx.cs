using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class perfilInfoVendedor : System.Web.UI.Page
    {
        private ClVendedorL oLogica = new ClVendedorL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();
                cargarMarcas();

                int idVendedor = Convert.ToInt32(Request.QueryString["id"]); // Obtener idCategoria desde la URL

                    cargarVendedor(idVendedor);
                    cargarProductos(idVendedor);

            }

        }
        private void cargarProductos(int idVendedor)
        {
            ClProductoL objProductoL = new ClProductoL();
            DataTable dt = objProductoL.MtdListarPorVendedor(idVendedor);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }

        private void cargarVendedor(int idVendedor)
        {
            ClUsuarioE oUsuario = oLogica.mtdPerfilVendedor(idVendedor);
            if (oUsuario != null)
            {
                nombreVendedor.Text = oUsuario.Nombres;
                apellidoVendedor.Text = oUsuario.Apellidos;
                telVendedor.Text = oUsuario.Telefono;
                descVendedor.Text = oUsuario.Descripcion;
                totalProductos.Text = oUsuario.numProducto.ToString();
            }
            else
            {
                lblMensaje.InnerText = "No se encontró la información del Producto.";
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