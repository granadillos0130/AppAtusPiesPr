using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class ActualizarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            try
            {
                int? idVendedor = Convert.ToInt32(Session["idVendedor"]);

                ClProductoL productoL = new ClProductoL();
                List<ClProductoEmpresaE> productosVendedor = productoL.MtdListarProduc(idVendedor);

                if (productosVendedor.Any())
                {
                    ddlProducto.DataSource = productosVendedor;
                    ddlProducto.DataTextField = "nombreProducto";
                    ddlProducto.DataValueField = "idProducto";
                    ddlProducto.DataBind();

                    ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
                }
                else
                {
                    MostrarMensajeError("No se encontraron productos para este vendedor");
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al cargar los nombres de los productos: {ex.Message}");
            }
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProducto.SelectedValue != "0")
            {
                CargarDetallesProducto();
            }
        }

        private void CargarDetallesProducto()
        {
            try
            {
                int? idVendedor = Convert.ToInt32(Session["idVendedor"]);
                int idProducto = Convert.ToInt32(ddlProducto.SelectedValue);

                ClProductoL productoLogica = new ClProductoL();
                List<ClProductoEmpresaE> productos = productoLogica.ObtenerProductoPorVendedorYProducto(idVendedor, idProducto);

                if (productos.Count > 0)
                {
                    var producto = productos[0];

                    // Llenar los campos del formulario
                    txtNombre.Text = producto.nombreProducto;
                    txtStock.Text = producto.cantidadStock.ToString();
                    txtPrecio.Text = producto.precioVenta.ToString();
                    txtDescripcionProduc.Text = producto.descripcionProducto;
                    txtEstado.SelectedValue = producto.Estado;
                    txtDescuento.Text = producto.descuento.ToString();
                    txtReferencia.Text = producto.referencia;
                    txtCategoria.SelectedValue = producto.descripcionCategoria;
                    txtMarca.SelectedValue = producto.nombreMarca;
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al cargar los detalles del producto: {ex.Message}");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProducto.SelectedValue == "0")
                {
                    MostrarMensajeError("Por favor, seleccione un producto para actualizar.");
                    return;
                }

                string rutaImagen = ProcesarImagen();

                // Crear objeto con los datos del producto
                ClProductoEmpresaE objProductoE = new ClProductoEmpresaE
                {
                    idProducto = Convert.ToInt32(ddlProducto.SelectedValue),
                    nombreProducto = txtNombre.Text.Trim(),
                    cantidadStock = string.IsNullOrWhiteSpace(txtStock.Text) ? (int?)null : int.Parse(txtStock.Text),
                    precioVenta = string.IsNullOrWhiteSpace(txtPrecio.Text) ? (int?)null : int.Parse(txtPrecio.Text),
                    descripcionProducto = txtDescripcionProduc.Text.Trim(),
                    Estado = txtEstado.SelectedValue,
                    descuento = string.IsNullOrWhiteSpace(txtDescuento.Text) ? (int?)null : int.Parse(txtDescuento.Text),
                    referencia = txtReferencia.Text.Trim(),
                    descripcionCategoria = txtCategoria.SelectedValue,
                    nombreMarca = txtMarca.SelectedValue,
                    imagen = rutaImagen
                };

                ClProductoL objProductoL = new ClProductoL();
                ClProductoEmpresaE objDatos = objProductoL.MtdActualizacionProduc(objProductoE);

                if (objDatos != null)
                {
                    MostrarMensajeExito("Producto actualizado correctamente");
                    LimpiarFormulario();
                }
                else
                {
                    MostrarMensajeError("No se pudo actualizar el producto");
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al actualizar el producto: {ex.Message}");
            }
        }

        private string ProcesarImagen()
        {
            string rutaImagen = null;

            if (inRuta.HasFile)
            {
                string extension = Path.GetExtension(inRuta.FileName).ToLower();
                string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif" };

                if (extensionesPermitidas.Contains(extension))
                {
                    string nombreArchivo = Guid.NewGuid().ToString() + extension;
                    string directorioImagenes = Server.MapPath("~/img/Producto/");

                    // Crear directorio si no existe
                    if (!Directory.Exists(directorioImagenes))
                    {
                        Directory.CreateDirectory(directorioImagenes);
                    }

                    // Guardar la imagen
                    string rutaCompleta = Path.Combine(directorioImagenes, nombreArchivo);
                    inRuta.SaveAs(rutaCompleta);
                    rutaImagen = "~/img/Producto/" + nombreArchivo;
                }
                else
                {
                    MostrarMensajeError("Solo se permiten archivos de imagen (.jpg, .jpeg, .png, .gif)");
                }
            }

            return rutaImagen;
        }

        private void LimpiarFormulario()
        {
            ddlProducto.SelectedValue = "0";
            txtNombre.Text = "";
            txtStock.Text = "";
            txtPrecio.Text = "";
            txtDescripcionProduc.Text = "";
            txtEstado.SelectedValue = "disponible";
            txtDescuento.Text = "";
            txtReferencia.Text = "";
            txtCategoria.SelectedValue = "";
            txtMarca.SelectedValue = "";
        }

        private void MostrarMensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"Swal.fire('¡Éxito!', '{mensaje}', 'success');", true);
        }

        private void MostrarMensajeError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"Swal.fire('Error', '{mensaje}', 'error');", true);
        }
    }
}