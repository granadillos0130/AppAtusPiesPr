using System;
using System.Web.UI;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;

namespace AppAtusPiesPr.Vista
{
    public partial class EditarPerfilVendeyAdmin : System.Web.UI.Page
    {
        private ClUsuarioL usuarioLo = new ClUsuarioL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosUsuario();

            }
        }

        private void CargarDatosUsuario()
        {
            // Obtener el IdUsuario y el rol de la sesión
            int idUsuario = (int)Session["idUsuario"];
            bool esVendedor = Session["rol"].ToString() == "Vendedor";

            // Obtener los datos del usuario
            ClUsuarioE usuario = usuarioLo.ObtenerUsuarioPorId(idUsuario, esVendedor);

            if (usuario != null)
            {
                // Cargar los datos en los campos del formulario
                txtNombres.Text = usuario.Nombres;
                txtApellidos.Text = usuario.Apellidos;
                txtEmail.Text = usuario.Email;
                if (esVendedor)
                {
                    txtTelefono.Text = usuario.Telefono;
                    txtDireccion.Text = usuario.Direccion;
                    txtDescripcion.Text = usuario.Descripcion;
                }
                else
                {
                    txtTelefono.Visible = false;
                    txtDireccion.Visible = false;
                    txtDescripcion.Visible = false;
                    
                    
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar la contraseña actual
            string contrasenaActual = txtContrasenaActual.Text;
            string nuevaContrasena = txtNuevaContrasena.Text;
            bool esVendedor = Session["rol"].ToString() == "Vendedor";

            if (!usuarioLo.ValidarContrasena((int)Session["idUsuario"], contrasenaActual, esVendedor))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ContrasenaIncorrecta", "Swal.fire('Error', 'La contraseña actual es incorrecta.', 'error');", true);
                return;
            }

            // Actualizar los datos del usuario
            int idUsuario = (int)Session["idUsuario"];
            ClUsuarioE usuario = new ClUsuarioE
            {
                IdUsuario = idUsuario,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Email = txtEmail.Text,
                Password = string.IsNullOrEmpty(nuevaContrasena) ? contrasenaActual : nuevaContrasena // Si no se ingresó una nueva contraseña, mantener la actual
            };

            if (esVendedor)
            {
                usuario.Telefono = txtTelefono.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.Descripcion = txtDescripcion.Text;
            }

            bool exito = usuarioLo.ActualizarUsuario(usuario, esVendedor);

            if (exito)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PerfilActualizado", "Swal.fire('Éxito', 'Perfil actualizado correctamente.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorActualizacion", "Swal.fire('Error', 'Error al actualizar el perfil.', 'error');", true);
            }
        }
    }
}
