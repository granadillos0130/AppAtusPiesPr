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
                int? idVendedor = Convert.ToInt32(Session["idUsuario"]);

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
                int? idVendedor = Convert.ToInt32(Session["idUsuario"]);
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
                    nombreProducto = string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text, //Verifica si el campo esta vacio o contiene solo espacios en blanco
                    cantidadStock = string.IsNullOrWhiteSpace(txtStock.Text) ? (int?)null : int.Parse(txtStock.Text),//El int?null indica que el tipo de datos es anulable lo que significa que acepta el valor null
                    precioVenta = string.IsNullOrWhiteSpace(txtPrecio.Text) ? (int?)null : int.Parse(txtPrecio.Text),
                    descripcionProducto = string.IsNullOrWhiteSpace(txtDescripcionProduc.Text) ? null : txtDescripcionProduc.Text,
                    Estado = string.IsNullOrWhiteSpace(txtEstado.Text) ? null : txtEstado.Text,
                    descuento = string.IsNullOrWhiteSpace(txtDescuento.Text) ? (int?)null : int.Parse(txtDescuento.Text),
                    referencia = string.IsNullOrWhiteSpace(txtReferencia.Text) ? null : txtReferencia.Text,
                    descripcionCategoria = string.IsNullOrWhiteSpace(txtCategoria.Text) ? null : txtCategoria.Text,
                    nombreMarca = string.IsNullOrWhiteSpace(txtMarca.Text) ? null : txtMarca.Text,
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