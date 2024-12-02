using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Web.UI;
namespace AppAtusPiesPr.Vista
{
    public partial class ProductosMasVendidos : System.Web.UI.Page
    {
        ClVendedorL productoL = new ClVendedorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar productos con rango predeterminado
                cargarProductos(DateTime.Now.AddMonths(-1), DateTime.Now);
            }
        }

        private void MostrarAlerta(string icon, string title, string text)
        {
            string script = $@"Swal.fire({{
    icon: '{icon}',
    title: '{title}',
    text: '{text}',
    confirmButtonColor: '#3085d6'
}});";
            ScriptManager.RegisterStartupScript(this, GetType(), title.Replace(" ", ""), script, true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener fechas desde los campos ocultos
                string fechaInicioStr = hfFechaInicio.Value;
                string fechaFinStr = hfFechaFin.Value;

                if (string.IsNullOrEmpty(fechaInicioStr) || string.IsNullOrEmpty(fechaFinStr))
                {
                    MostrarAlerta("warning", "Fechas incompletas", "Por favor selecciona las fechas de inicio y fin.");
                    return;
                }

                DateTime fechaInicio = DateTime.Parse(fechaInicioStr);
                DateTime fechaFin = DateTime.Parse(fechaFinStr);

                if (fechaInicio > fechaFin)
                {
                    MostrarAlerta("error", "Fechas inválidas", "La fecha de inicio no puede ser mayor que la fecha de fin.");
                    return;
                }

                cargarProductos(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error inesperado", "Ocurrió un error al procesar la solicitud. Detalles: " + ex.Message);
            }
        }

        private void cargarProductos(DateTime fechaInicio, DateTime fechaFin)
{
    if (Session["idUsuario"] == null)
    {
        MostrarAlerta("error", "Sesión no válida", "No se encontró la información del vendedor en la sesión. Por favor, inicia sesión nuevamente.");
        return;
    }

    int idVendedor;
    if (!int.TryParse(Session["idUsuario"].ToString(), out idVendedor))
    {
        MostrarAlerta("error", "ID de Vendedor inválido", "El ID del vendedor en la sesión no es válido.");
        return;
    }

    try
    {
        List<ClProductoEmpresaE> productos = productoL.MtdObtenerProductosMasVendidosPorVendedor(idVendedor, fechaInicio, fechaFin);

        // Asignar imagen predeterminada si no existe una imagen
        foreach (var producto in productos)
        {
            if (string.IsNullOrEmpty(producto.imagen))
            {
                producto.imagen = "~/Images/no-image-found.png"; // Ruta de la imagen predeterminada
            }
        }

        if (productos.Count > 0)
        {
            gvProductos.DataSource = productos;
            gvProductos.DataBind();
            MostrarAlerta("success", "Productos cargados", "No se encontraron registro de ventas.");
        }
        else
        {
            gvProductos.DataSource = null;
            gvProductos.DataBind();
            MostrarAlerta("info", "Sin resultados", "No se  encontraron registro de ventas.");
        }
    }
    catch (Exception ex)
    {
        MostrarAlerta("error", "Error al cargar productos", "Ocurrió un error al intentar cargar los productos. Detalles: " + ex.Message);
    }
}

    }
}
