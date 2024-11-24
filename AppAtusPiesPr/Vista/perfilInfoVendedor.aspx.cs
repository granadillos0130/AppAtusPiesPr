using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
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

                string vendedorId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(vendedorId) && int.TryParse(vendedorId, out int id))
                {
                    cargarVendedor(id);

                }
            }

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
    }
}