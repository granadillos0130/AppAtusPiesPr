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
                    string extension = System.IO.Path.GetExtension(inRuta.FileName).ToLower();
                    string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (extensionesPermitidas.Contains(extension))
                    {
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
                ClProductoE objProductoE = new ClProductoE
                {
                    idProducto = int.TryParse(txtProducto.Text, out int id) ? id : 0,
                    Nombre = txtNombre.Text,
                    Codigo = txtCodigo.Text,
                    CantidadStock = int.TryParse(txtStock.Text, out int stock) ? stock : 0,
                    Estado = txtEstado.Text,
                    Precio = int.TryParse(txtPrecio.Text, out int precio) ? precio : 0,
                    Talla = txtTalla.Text,
                    Presentacion = rutaImagen// Solo se actualiza si se subió una nueva imagen
                };

                // Llamar al método de actualización
                ClProductoL objProductoL = new ClProductoL();
                ClProductoE objDatos = objProductoL.MtdActualizacionProduc(objProductoE);

                if (objDatos != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                        "Swal.fire('¡Éxito!', 'Producto actualizado correctamente', 'success');", true);

                    // Limpiar campos
                    txtProducto.Text = "";
                    txtNombre.Text = "";
                    txtCodigo.Text = "";
                    txtStock.Text = "";
                    txtEstado.Text = "";
                    txtPrecio.Text = "";
                    txtTalla.Text = "";
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