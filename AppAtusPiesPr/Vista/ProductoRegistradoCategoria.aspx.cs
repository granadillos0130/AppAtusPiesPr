using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class ProductoRegistradoCategoria : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos(); // Cargar productos al iniciar
            }
        }

        private void CargarCategorias()
        {
            try
            {
                // Instancia de la capa lógica para obtener las categorías
                ClProductoL categoriaLogica = new ClProductoL();
                List<ClCategoriaE> categorias = categoriaLogica.MtdListarCategorias();

                // Asignar las categorías al DropDownList
                ddlCategorias.DataSource = categorias;
                ddlCategorias.DataTextField = "descripcion"; // Nombre de la categoría
                ddlCategorias.DataValueField = "idCategoria"; // ID de la categoría
                ddlCategorias.DataBind();

                // Agregar la opción "Todas las categorías" como predeterminada
                ddlCategorias.Items.Insert(0, new ListItem("Todas las categorías", "0"));
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar las categorías: {ex.Message}');</script>");
            }
        }

        protected void CargarProductos()
        {
            try
            {
                // Recuperar el id del vendedor desde la sesión
                int? idVendedor = Convert.ToInt32(Session["idUsuario"]);
                // Obtener la categoría seleccionada, si es "0" significa "todas"
                int? idCategoria = ddlCategorias.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlCategorias.SelectedValue) : null;

                // Instanciar la lógica del vendedor
                ClVendedorL vendedorLogica = new ClVendedorL();

                // Llamar al método para obtener los productos
                List<ClProductoE> productos = vendedorLogica.ObtenerProductosPorVendedorYCategoria(idVendedor, idCategoria);

                // Vincular los productos al GridView
                gvProductos.DataSource = productos;
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar los productos: {ex.Message}');</script>");
            }
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recargar los productos cuando se cambia de categoría
            CargarProductos();
        }
    }
}
