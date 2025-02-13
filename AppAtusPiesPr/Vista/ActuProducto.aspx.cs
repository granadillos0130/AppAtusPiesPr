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
   
    public partial class ActuProducto : System.Web.UI.Page
    {
        ClProductoL productosCategoria = new ClProductoL();

        ClProductoL productosLogica = new ClProductoL();
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    if (Session["idUsuario"] != null)
                    {
                        int idVendedor = Convert.ToInt32(Session["idUsuario"]);
                        CargarProductos(idVendedor);
                    }

                CargarCategorias();
                }

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

        private void CargarProductos(int idVendedor)
            {
                List<ClProductoE> listaProductos = productosLogica.MtdListarProductoDDL(idVendedor);

                if (listaProductos.Count > 0)
                {
                    ddlProducto.DataSource = listaProductos;
                    ddlProducto.DataTextField = "Nombre";
                    ddlProducto.DataValueField = "idProducto";
                    ddlProducto.DataBind();
                }
                else
                {
                    ddlProducto.Items.Clear();
                    ddlProducto.Items.Add(new ListItem("No hay productos disponibles", "0"));
                }

                ddlProducto.Items.Insert(0, new ListItem("--Seleccione un Producto--", "0"));
            }


            protected void btnActualizar_Click1(object sender, EventArgs e)
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
                        descripcionCategoria = string.IsNullOrWhiteSpace(ddlCategoria.Text) ? null : ddlCategoria.Text,
                        nombreMarca = string.IsNullOrWhiteSpace(txtMarca.Text) ? null : txtMarca.Text,
                        imagen = rutaImagen
                    };

                    ClProductoL objProductoL = new ClProductoL();
                    ClProductoEmpresaE objDatos = objProductoL.MtdActualizacionProduc(objProductoE);

                    if (objDatos != null)
                    {
                        MostrarMensajeExito("Producto actualizado correctamente");

                    // Recargar la lista de productos después de la actualización
                    int idVendedor = Convert.ToInt32(Session["idUsuario"]);
                    CargarProductos(idVendedor);
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
            ddlCategoria.SelectedIndex = 0;
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

