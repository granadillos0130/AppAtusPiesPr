using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                cargarComentarios();
                cargarMarcas();

                string productoId = Request.QueryString["id"];
                
                if (!string.IsNullOrEmpty(productoId) && int.TryParse(productoId, out int idProducto))
                {
                    cargarProducto(idProducto);
                    cargarValoracion(idProducto);

                }
            }
        }

        private void cargarMarcas()
        {
            ClProductoL oLogica = new ClProductoL();
            RepeaterMarca.DataSource = oLogica.MtdListarMarcas();
            RepeaterMarca.DataBind();
        }
        private void cargarValoracion(int idProducto)
        {
            decimal promedioValoracion = oLogica.ObtenerValoracionPromedio(idProducto);

            lblPromedio.Text = promedioValoracion.ToString("F1");
            lblPromedio.Attributes["data-promedio"] = promedioValoracion.ToString("F1");
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

                ClProductoL logica = new ClProductoL();

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

        private void LimpiarCampos()
        {
            txtComentario.Text = "";
        }

        protected void enviarComentario_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar el idProducto
                string idProducto = Request.QueryString["id"];
                int idProduct;
                if (!int.TryParse(idProducto, out idProduct))
                {
                    lblMensaje.InnerText = "El ID del producto no es válido.";
                    return; // Detener la ejecución si el ID del producto no es válido
                }

                // Verificar el idUsuario
                string usuario = Session["idUsuario"]?.ToString();
                int idUsuario;
                if (!int.TryParse(usuario, out idUsuario))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "Swal.fire({ icon: 'error', title: '¡Debes estar logueado!', text: 'Inicia sesión para poder comentar.', toast: true, position: 'bottom-end', background: '#ff6b6b', color: '#fff', timer: 2000, timerProgressBar: true });", true);
                    return;
                }
                int valoracion = 0;

                string valoracionStr = Request.Form[hdnValoracion.UniqueID]; // Obtener desde Request.Form
                System.Diagnostics.Debug.WriteLine("Valoración desde Request.Form: " + valoracionStr); // Verificar en consola
                int valr = Convert.ToInt32(valoracionStr);

                // Crear el objeto comentario
                ClComentarioE comentario = new ClComentarioE
                {
                    comentario = txtComentario.Text,
                    idProducto = idProduct,  // Asignar el id del producto
                    idCliente = idUsuario,
                    valoracion = valr  // Asegurar que la valoración se está enviando
                };



                if (!int.TryParse(valoracionStr, out valoracion) || valoracion < 1 || valoracion > 5)

                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "Swal.fire({ icon: 'warning', title: '¡Atención!', text: 'Por favor, selecciona una valoración.', toast: true, position: 'bottom-end', background: '#ffc107', color: '#000', timer: 2000, timerProgressBar: true });", true);
                    return;
                }

                // Guardar el comentario
                ClProductoL oL = new ClProductoL();
                ClComentarioE comentarioGuardado = oL.MtdGuardarComentario(comentario);

                // Si el comentario se guardó correctamente
                if (comentarioGuardado != null)
                {
                    txtComentario.Text = string.Empty;

                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "Swal.fire({ icon: 'success', title: '¡Comentario enviado!', text: 'Gracias por tu opinión.', showConfirmButton: false, timer: 1500, timerProgressBar: true, position: 'bottom-end', toast: true, background: '#4CAF50', color: '#fff', iconColor: '#fff' });", true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "recargarPagina",
                "setTimeout(function() { window.location.href = window.location.href; }, 1500);", true);
                

            }
                else
                {
                    // Si no se guardó correctamente, mostrar un mensaje de error
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "Swal.fire({ icon: 'error', title: '¡Error!', text: 'Hubo un problema al enviar el comentario. Intenta nuevamente.', showConfirmButton: false, timer: 2000, timerProgressBar: true, position: 'bottom-end', toast: true, background: '#ff6b6b', color: '#fff', iconColor: '#fff' });", true);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.InnerText = "Ocurrió un error: " + ex.Message;
            }

            LimpiarCampos();
        }

        private void cargarComentarios()
        {
            string idProducto = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(idProducto) && int.TryParse(idProducto, out int idProduct))
            {
                ClProductoL objL = new ClProductoL();
                Repeater3.DataSource = objL.mtdListarComentarios(idProduct);
                Repeater3.DataBind();
            } else
            {
                // Si no se guardó correctamente, mostrar un mensaje de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: '¡Error!', text: 'Hubo un problema al listar los comentarios. Intenta nuevamente.', showConfirmButton: false, timer: 2000, timerProgressBar: true, position: 'bottom-end', toast: true, background: '#ff6b6b', color: '#fff', iconColor: '#fff' });", true);
            }
        }

        private string GenerarCorazonesHTML(decimal promedio)
        {
            // Asegúrate de que el promedio no exceda 5
            promedio = Math.Min(5, promedio);

            // Calcula los corazones llenos (parte entera del promedio)
            int corazonesLlenos = (int)Math.Floor(promedio);

            // Calcula si necesitamos un medio corazón
            decimal parteDecimal = promedio - corazonesLlenos;
            bool medioCorazon = parteDecimal >= 0.5m;

            StringBuilder html = new StringBuilder();
            html.Append("<div class='hearts-rating2'>");

            // Corazones llenos
            for (int i = 0; i < corazonesLlenos; i++)
            {
                html.Append("<span class='heart-icon2' style='color: #ff0000;'>&#10084;</span>");
            }

            // Medio corazón si es necesario
            if (medioCorazon)
            {
                html.Append("<span class='heart-icon2' style='color: #ff0000; opacity: 0.5;'>&#10084;</span>");
            }

            // Corazones vacíos
            int corazonesVacios = 5 - corazonesLlenos - (medioCorazon ? 1 : 0);
            for (int i = 0; i < corazonesVacios; i++)
            {
                html.Append("<span class='heart-icon2' style='color: #ccc;'>&#10084;</span>");
            }

            // Agregar el promedio numérico
            html.Append($"<span class='promedio-texto'>({promedio:F1})</span>");
            html.Append("</div>");

            return html.ToString();
        }

    }
}