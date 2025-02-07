using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace AppAtusPiesPr.Vista
{
    public partial class EstadisticaCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar la tabla con un orden por defecto (Ascendente)
                CargarEstadistica("DESC");
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el valor seleccionado en el DropDownList y recargar la tabla
                string ordenSeleccionado = ddlOrden.SelectedValue;
                CargarEstadistica(ordenSeleccionado);
            }
            catch (Exception ex)
            {
                // Puedes mostrar un mensaje de error aquí, o loguearlo si es necesario
                // Ejemplo de log: LogError(ex.Message);
                lblError.Text = "Ocurrió un error al filtrar los datos: " + ex.Message;
            }
        }

        public void CargarEstadistica(string orden)
        {
            try
            {
                // Llamar a la capa lógica para obtener los datos
                ClAdminL oCategoria = new ClAdminL();
                List<ClCategoriaE> categoria = oCategoria.MtdEstadisticaCategoria(orden);

                // Verificar si hay datos y asignarlos al GridView
                if (categoria != null && categoria.Count > 0)
                {
                    gvEstadisticasCategoria.DataSource = categoria;
                    gvEstadisticasCategoria.DataBind();
                }
                else
                {
                    gvEstadisticasCategoria.DataSource = null;
                    gvEstadisticasCategoria.DataBind();
                    lblError.Text = "No se encontraron datos para mostrar.";
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: puedes agregar un mensaje o log para depuración
                lblError.Text = "Ocurrió un error al cargar las estadísticas: " + ex.Message;
            }
        }
    }
}
