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
                ClProductoE objProduE = new ClProductoE();
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtTalla.Text))
                {
                    MostrarMensaje("Todos los campos son requeridos");
                    return;
                }
                objProduE.Nombre = txtNombre.Text.Trim();
                objProduE.Codigo = txtCodigo.Text.Trim();
                objProduE.Talla = txtTalla.Text.Trim();


                if (!int.TryParse(txtStock.Text, out int cantidad))
                {
                    MostrarMensaje("Por favor ingrese un número válido para el stock");
                    return;
                }
                if (cantidad < 0)
                {
                    MostrarMensaje("El stock no puede ser negativo");
                    return;
                }
                objProduE.CantidadStock = cantidad;



                if (!int.TryParse(txtPrecio.Text, out int money))
                {
                    MostrarMensaje("Por favor ingrese un valor válido para el precio");
                    return;
                }
                if (money < 0)
                {
                    MostrarMensaje("El precio no puede ser negativo");
                    return;
                }
                objProduE.Precio = money;



                if (!int.TryParse(txtVendedor.Text, out int Vendedor))
                {
                    MostrarMensaje("Por favor ingrese un número válido para el ID del vendedor");
                    return;
                }
                if (Vendedor <= 0)
                {
                    MostrarMensaje("El ID del vendedor debe ser un número positivo");
                    return;
                }
                objProduE.idVendedor = Vendedor;

                if (inRuta.HasFile)
                {
                    string extension = Path.GetExtension(inRuta.FileName).ToLower();
                    // Validar extensión
                    if (!extension.Equals(".jpg") && !extension.Equals(".png") &&
                        !extension.Equals(".jpeg") && !extension.Equals(".gif"))
                    {
                        MostrarMensaje("Solo se permiten archivos de imagen (jpg, png, jpeg, gif)");
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
                    objProduE.Presentacion = "~/Vista/img/" + nombreUnico;
                }

                ClProductoL objProductoL = new ClProductoL();
                objProductoL.MtdRegistroProd(objProduE);

                MostrarMensaje("Producto registrado exitosamente");
                LimpiarCampos();

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al registrar el producto: " + ex.Message);
            }
        }
        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"alert('{mensaje.Replace("'", "\\'")}')", true);
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCodigo.Text = "";
            txtTalla.Text = "";
            txtStock.Text = "";
            txtPrecio.Text = "";
            txtVendedor.Text = "";
        }
    }
    }
