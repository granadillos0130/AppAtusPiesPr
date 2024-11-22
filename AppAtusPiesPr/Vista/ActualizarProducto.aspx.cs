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

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                // Validar el ID del producto
                if (!int.TryParse(txtProducto.Text, out int idProducto))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire('Error', 'El ID del producto debe ser un número válido', 'error');", true);
                    return;
                }
                string rutaImagen = null;

                // Procesar la imagen si se ha subido una nueva
                if (inRuta.HasFile)
                {
                    //Extrae la extension y la convierte en minuscula
                    string extension = System.IO.Path.GetExtension(inRuta.FileName).ToLower();
                    string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (extensionesPermitidas.Contains(extension))
                    {                       //Evita nombres duplicados
                        string nombreArchivo = Guid.NewGuid().ToString() + extension;
                        rutaImagen = Server.MapPath("~/img/Producto/") + nombreArchivo;

                        // Crear directorio si no existe
                        if (!Directory.Exists(Server.MapPath("~/img/Producto/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/img/Producto/"));
                        }

                        // Guardar la imagen
                        inRuta.SaveAs(rutaImagen);
                        rutaImagen = "~/img/Producto/" + nombreArchivo;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                            "Swal.fire('Error', 'Solo se permiten archivos de imagen (.jpg, .jpeg, .png, .gif)', 'error');", true);
                        return;
                    }
                }

                // Crear objeto con los datos del producto
                ClProductoEmpresaE objProductoE = new ClProductoEmpresaE
                {
                    idProducto = int.Parse(txtProducto.Text),
                    nombreProducto = string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text,
                    cantidadStock = string.IsNullOrWhiteSpace(txtStock.Text) ? 0 : int.Parse(txtStock.Text),
                    precioVenta = string.IsNullOrWhiteSpace(txtPrecio.Text) ? 0 : int.Parse(txtPrecio.Text),
                    descripcionProducto = string.IsNullOrWhiteSpace(txtDescripcionProduc.Text) ? null : txtDescripcionProduc.Text,
                    Estado = string.IsNullOrWhiteSpace(txtEstado.Text) ? null : txtEstado.Text,
                    descuento = string.IsNullOrWhiteSpace(txtDescuento.Text) ? 0 : int.Parse(txtDescuento.Text),
                    referencia = string.IsNullOrWhiteSpace(txtReferencia.Text) ? null : txtReferencia.Text,
                    descripcionCategoria = string.IsNullOrWhiteSpace(txtCategoria.Text) ? null : txtCategoria.Text,
                    nombreMarca = string.IsNullOrWhiteSpace(txtMarca.Text) ? null : txtMarca.Text,
                    imagen = rutaImagen
                };


                ClProductoL objProductoL = new ClProductoL();
                ClProductoEmpresaE objDatos = objProductoL.MtdActualizacionProduc(objProductoE);

                if (objDatos != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire('¡Éxito!', 'Producto actualizado correctamente', 'success');", true);

                    // Limpiar campos
                    txtProducto.Text = "";
                    txtNombre.Text = "";
                    txtStock.Text = "";
                    txtPrecio.Text = "";
                    txtDescripcionProduc.Text = "";
                    txtEstado.Text = "";
                    txtDescuento.Text = "";
                    txtReferencia.Text = "";
                    txtCategoria.Text = "";
                    txtMarca.Text = "";

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire('Error', 'No se pudo actualizar el producto', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                    $"Swal.fire('Error', 'Error al actualizar el producto: {ex.Message}', 'error');", true);
            }
        }
    }
}