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
    public partial class RegistroProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                ClProductoEmpresaE objProduE = new ClProductoEmpresaE();

                objProduE.idVendedor = int.Parse( Session["idUsuario"].ToString());
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionProduc.Text) ||
                string.IsNullOrWhiteSpace(txtReferencia.Text) ||
                string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                 string.IsNullOrWhiteSpace(txtDescuento.Text) ||
                 string.IsNullOrWhiteSpace(txtMarca.Text)) 

                {
                    MostrarMensajeError("Todos los campos son requeridos");
                    return;
                }
                objProduE.nombreProducto = txtNombre.Text.Trim();
                objProduE.descripcionProducto = txtDescripcionProduc.Text.Trim();
                objProduE.referencia = txtReferencia.Text.Trim();
                objProduE.descripcionCategoria = txtCategoria.Text.Trim();
                objProduE.descuento = int.Parse( txtDescuento.Text.Trim());
                objProduE.nombreMarca = txtMarca.Text.Trim();


                if (!int.TryParse(txtStock.Text, out int cantidad))
                {
                    MostrarMensajeError("Por favor ingrese un número válido para el stock");
                    return;
                }
                if (cantidad < 0)
                {
                    MostrarMensajeError("El stock no puede ser negativo");
                    return;
                }
                objProduE.cantidadStock = cantidad;



                if (!int.TryParse(txtPrecio.Text, out int money))
                {
                    MostrarMensajeError("Por favor ingrese un valor válido para el precio");
                    return;
                }
                if (money < 0)
                {
                    MostrarMensajeError("El precio no puede ser negativo");
                    return;
                }
                objProduE.precioVenta = money;



                if (!int.TryParse(txtDescuento.Text, out int descuento))
                {
                    MostrarMensajeError("Por favor ingrese un número válido para el Descuento");
                    return;
                }
                if (descuento <= 0)
                {
                    MostrarMensajeError("El descuento debe ser un número positivo");
                    return;
                }
                objProduE.descuento = descuento;

                if (inRuta.HasFile)
                {
                    string extension = Path.GetExtension(inRuta.FileName).ToLower();
                    // Validar extensión
                    if (!extension.Equals(".jpg") && !extension.Equals(".png") &&
                        !extension.Equals(".jpeg") && !extension.Equals(".gif"))
                    {
                        MostrarMensajeError("Solo se permiten archivos de imagen (jpg, png, jpeg, gif)");
                        return;
                    }

                    string nombreImg = Path.GetFileName(inRuta.FileName);
                    // Crear nombre único para evitar sobrescritura
                    string nombreUnico = Guid.NewGuid().ToString() + extension;
                    string directorioDestino = Server.MapPath("~/Vista/img/");

                    // Verificar si existe el directorio
                    if (!Directory.Exists(directorioDestino))
                    {
                        Directory.CreateDirectory(directorioDestino);
                    }

                    string rutaCompleta = Path.Combine(directorioDestino, nombreUnico);
                    inRuta.SaveAs(rutaCompleta);
                    objProduE.imagen = "~/Vista/img/" + nombreUnico;
                }

                ClProductoL objProductoL = new ClProductoL();
                objProductoL.MtdRegistroProd(objProduE);

                MostrarMensajeExito("Producto registrado exitosamente");
                LimpiarCampos();

            }
            catch (Exception ex)
            {
                MostrarMensajeError("Error al registrar el producto: " + ex.Message);
            }

        }
        private void MostrarMensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "sweetAlertSuccess",
                $"Swal.fire({{ icon: 'success', title: 'Éxito', text: '{mensaje.Replace("'", "\\'")}' }})", true);
        }

        private void MostrarMensajeError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "sweetAlertError",
                $"Swal.fire({{ icon: 'error', title: 'Error', text: '{mensaje.Replace("'", "\\'")}' }})", true);
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcionProduc.Text = "";
            txtReferencia.Text = "";
            txtCategoria.Text = "";
            txtMarca.Text = "";
            txtStock.Text = "";
            txtPrecio.Text = "";
            txtDescuento.Text = "";
        }
    }
    }
