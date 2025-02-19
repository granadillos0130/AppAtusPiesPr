using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class agregarMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregarCategoria_ServerClick(object sender, EventArgs e)
        {
            string descripcionCategoria = txtDescripcion.Text.Trim();
            string nombreMarca = txtNombre.Text.Trim();

            // Crear un objeto de tipo ClCategoriaE con la descripción
            ClMarcasE oMarcaE = new ClMarcasE()
            {
                nombreMarca = nombreMarca,
                descripcion = descripcionCategoria
            };

            ClAdminL oCategoriaL = new ClAdminL();
            bool categoriaExiste = oCategoriaL.MtdValidarMarca(oMarcaE);  // Ahora pasamos un objeto ClCategoriaE

            if (categoriaExiste)
            {
                string scriptError = @"
        Swal.fire({
            icon: 'error',
            title: '¡Error!',
            text: 'Ya existe una Marca con esta descripción. Por favor, ingrese una descripción diferente.',
            confirmButtonText: 'Aceptar'
        });
        ";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlertError", scriptError, true);
            }
            else
            {
                // Si no existe, registrar la categoría
                ClMarcasE oMarca = new ClMarcasE()
                {
                    nombreMarca = nombreMarca,
                    descripcion = descripcionCategoria
                };

                bool exito = oCategoriaL.mtdAgregarMarca(oMarca);

                if (exito)
                {
                    string scriptSuccess = @"
            Swal.fire({
                icon: 'success',
                title: '¡Categoría Agregada!',
                text: 'La nueva categoría ha sido agregada y se le ha notificado a los vendedores.',
                confirmButtonText: 'Aceptar'
            });
            ";
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", scriptSuccess, true);
                }
                else
                {
                    string scriptError = @"
            Swal.fire({
                icon: 'error',
                title: '¡Error!',
                text: 'Hubo un problema al agregar la categoría. Por favor, inténtelo nuevamente.',
                confirmButtonText: 'Aceptar'
            });
            ";
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlertError", scriptError, true);
                }
            }
        }

        private void LimpiarCampos()
        {
            // Limpiar el campo de texto de descripción
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

    }
}