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
        ClProductoL productosCategoria = new ClProductoL();
        ClProductoL productosMarca = new ClProductoL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    int idVendedor = Convert.ToInt32(Session["idUsuario"]);
                    CargarCategorias();
                    CargarMarca();  
                }
            }

        }

        private void CargarMarca()
        {
            List<ClMarcasE> listaProductos = productosMarca.MtdlistarMarcaActua();

            if (listaProductos.Count > 0)
            {
                ddlMarca.DataSource = listaProductos;
                ddlMarca.DataTextField = "nombreMarca";
                ddlMarca.DataValueField = "idMarca";
                ddlMarca.DataBind();
            }
            else
            {
                ddlMarca.Items.Clear();
                ddlMarca.Items.Add(new ListItem("No hay marcas disponibles", "0"));
            }

            ddlMarca.Items.Insert(0, new ListItem("Seleccione una marca", "0"));
        }


        private void CargarCategorias()
        {
            List<ClCategoriaE> listaProductos = productosCategoria.MtdlistarCategoriasActua();

            if (listaProductos.Count > 0)
            {
                ddlCategoria.DataSource = listaProductos;
                ddlCategoria.DataTextField = "descripcion";
                ddlCategoria.DataValueField = "idcategoria";
                ddlCategoria.DataBind();
            }
            else
            {
                ddlCategoria.Items.Clear();
                ddlCategoria.Items.Add(new ListItem("No hay categorias disponibles", "0"));
            }

            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoria", "0"));
        }


        
           protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                ClProductoEmpresaE objProduE = new ClProductoEmpresaE();

                objProduE.idVendedor = int.Parse(Session["idUsuario"].ToString());

                // Validación de campos obligatorios
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcionProduc.Text) ||
                    string.IsNullOrWhiteSpace(txtReferencia.Text) ||
                    string.IsNullOrWhiteSpace(ddlCategoria.Text) ||
                   !chkTallas.Items.Cast<ListItem>().Any(item => item.Selected) ||
                    string.IsNullOrWhiteSpace(ddlMarca.Text))


                {
                    MostrarMensajeError("Todos los campos son requeridos");
                    return;
                }

                objProduE.nombreProducto = txtNombre.Text.Trim();
                objProduE.descripcionProducto = txtDescripcionProduc.Text.Trim();
                objProduE.referencia = txtReferencia.Text.Trim();
                objProduE.descripcionCategoria = ddlCategoria.Text.Trim();
                objProduE.nombreMarca = ddlMarca.Text.Trim();

                List<string> tallasSeleccionadas = new List<string>();
                foreach (ListItem item in chkTallas.Items)
                {
                    if (item.Selected)
                    {
                        tallasSeleccionadas.Add(item.Value);
                    }
                }
    
                            // Convertir la lista de strings en una lista de objetos ClTallaE
                  objProduE.TallasDisponibles = tallasSeleccionadas
                 .Select(t => new ClTallaE { idTalla = int.Parse(t) })
                 .ToList();

                // Validación de stock
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

                // Validación de precio
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

                // Verificar si el campo descuento está vacío
                if (string.IsNullOrWhiteSpace(txtDescuento.Text))
                {
                    objProduE.descuento = null;  // Si no se ingresa descuento, se asigna null
                }
                else
                {
                    // Si el campo no está vacío, verificar que sea un número válido
                    if (!int.TryParse(txtDescuento.Text, out int descuento))
                    {
                        MostrarMensajeError("Por favor ingrese un número válido para el Descuento.");
                        return;
                    }

                    // Validar que el descuento sea un número positivo
                    if (descuento < 0)
                    {
                        MostrarMensajeError("El descuento debe ser un número positivo.");
                        return;
                    }
                    objProduE.descuento = descuento;  // Asignar descuento
                }

                // Verificar si se ha cargado una imagen
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
                    string directorioDestino = Server.MapPath("~/Vista/imagenes/");

                    // Verificar si existe el directorio
                    if (!Directory.Exists(directorioDestino))
                    {
                        Directory.CreateDirectory(directorioDestino);
                    }

                    string rutaCompleta = Path.Combine(directorioDestino, nombreUnico);
                    inRuta.SaveAs(rutaCompleta);
                    objProduE.imagen = "~/Vista/imagenes/" + nombreUnico;
                }

                // Registrar producto en la base de datos
                ClProductoL objProductoL = new ClProductoL();
                objProductoL.MtdRegistroProd(objProduE);

                // Primero limpiamos los campos
                LimpiarCampos();

                // Luego mostramos el mensaje de éxito usando setTimeout
                ScriptManager.RegisterStartupScript(this, GetType(), "sweetAlertSuccess",
                    $"setTimeout(function() {{ Swal.fire({{ icon: 'success', title: 'Éxito', text: 'Producto registrado exitosamente' }}); }}, 100);", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "sweetAlertError",
                    $"setTimeout(function() {{ Swal.fire({{ icon: 'error', title: 'Error', text: '{ex.Message.Replace("'", "\\'")}' }}); }}, 100);", true);
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
                $"setTimeout(function() {{ Swal.fire({{ icon: 'error', title: 'Error', text: '{mensaje.Replace("'", "\\'")}' }}); }}, 100);", true);
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcionProduc.Text = "";
            txtReferencia.Text = "";
            ddlCategoria.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            txtStock.Text = "";
            txtPrecio.Text = "";
            txtDescuento.Text = "";
            foreach (ListItem item in chkTallas.Items)
            {
                item.Selected = false;
            }
        }
    }
}