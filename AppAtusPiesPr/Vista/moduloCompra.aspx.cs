using AppAtusPiesPr.Entidades;
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
    public partial class moduloCompra2 : System.Web.UI.Page
    {
        private ClProductoL oLogica = new ClProductoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();


                string productoId = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(productoId) && int.TryParse(productoId, out int idProducto))
                {
                    cargarProducto(idProducto);  // Cargar el producto
                }

            }
        }

        private void cargarProducto(int idProducto)
        {
            ClProductoEmpresaE oProductoE = oLogica.MtdInfoProductos(idProducto);
            int idVendedor = 0;
            if (oProductoE != null)
            {
                lblTituloProducto.Text = oProductoE.nombreProducto;
                ImgProducto.ImageUrl = oProductoE.imagen;

                // Conversión de int a string para asignar a las etiquetas de texto
                stockProducto.Text = oProductoE.cantidadStock.ToString();
                referenciaProducto.Text = oProductoE.referencia;
                descuento.Text = oProductoE.descuento.ToString();
                nombreVendedor.Text = oProductoE.nombres;
                apellidoVendedor.Text = oProductoE.apellidoVendedor;
                nombreV.Text = oProductoE.nombres;
                apellidoV.Text = oProductoE.apellidoVendedor;
                PrecioProducto.Text = oProductoE.precioVenta.ToString();
                marcaProducto.Text = oProductoE.nombreMarca;
                productoDescripcion.Text = oProductoE.descripcionProducto;

                idVendedor = oProductoE.idVendedor;
                Console.WriteLine($"ID Vendedor: {idVendedor}");
               

                if (oProductoE.TallasDisponibles != null && oProductoE.TallasDisponibles.Count > 0)
                {
                    ddlTallas.DataSource = oProductoE.TallasDisponibles;
                    ddlTallas.DataTextField = "descripcionTalla";
                    ddlTallas.DataTextField = "idTalla";
                    ddlTallas.DataBind();
                    ddlTallas.Items.Insert(0, new ListItem("Tallas Disponibles", "0"));
                }
                else
                {
                    ddlTallas.Items.Clear();
                    ddlTallas.Items.Insert(0, new ListItem("No Hay Tallas Disponibles", "0"));
                }


            }
            else
            {
                lblMensaje.InnerText = "No se encontró la información del Producto.";
            }
            cargarProductos(idVendedor);
        }

        private void cargarProductos(int idVendedor)
        {
            try
            {
                Console.WriteLine($"ID Vendedor pasado a cargarProductos: {idVendedor}");

                ClProductoL objProductoL = new ClProductoL();

                
                DataTable dt = objProductoL.MtdListarPorVendedor(idVendedor);
                int conta = dt.Rows.Count;

                if (dt == null)
                {
                    lblMensaje.InnerText = "Error: La consulta no ha devuelto datos.";
                    return;
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    lblMensaje.InnerText = "Este vendedor no tiene productos disponibles.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.InnerText = "Error al mostrar la información: " + ex.Message;
            }
        }


        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {

        }
    }
}