using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Web.UI;

namespace AppAtusPiesPr.Vista
{
    public partial class RegistrarVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string documento = txtDocumento.Text;
            string correo = txtCorreo.Text;
            string password = txtPassword.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;
            string descripcion = txtDescripcion.Text;

            //Alerta Para los campos vacios
            if (string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(documento) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(direccion) ||
                string.IsNullOrWhiteSpace(descripcion))
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({title: 'Advertencia!', text: 'Todos los campos son obligatorios', icon: 'warning', confirmButtonText: 'Aceptar'});", true);
                return;
            }

            try
            {
                
                ClAdminL oDatos = new ClAdminL();
                ClUsuarioE oDatosE = new ClUsuarioE
                {
                    Nombres = nombres,
                    Apellidos = apellidos,
                    Documento = documento,
                    Email = correo,
                    Password = password,
                    Telefono = telefono,
                    Direccion = direccion,
                    Descripcion = descripcion
                };

                
                ClUsuarioE respuestaVuelta = oDatos.MtdRegistrarVendedor(oDatosE);

                //Alerta para saber si se registro correctamente
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({title: 'Éxito!', text: 'Usuario registrado correctamente', icon: 'success', confirmButtonText: 'Aceptar'});", true);

                // Limpiar campos del formulario
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                // Manejar errores
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"Swal.fire({{title: 'Error!', text: 'Ocurrió un error: {ex.Message}', icon: 'error', confirmButtonText: 'Aceptar'}});", true);
            }
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }
    }
}
