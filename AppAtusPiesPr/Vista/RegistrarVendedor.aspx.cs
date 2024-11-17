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

            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos) || string.IsNullOrWhiteSpace(documento) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(descripcion))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({title: 'Advertencia!', text: 'Todos los campos son obligatorios', icon: 'warning', confirmButtonText: 'Aceptar'});", true);
                return;
            }

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

        }
    }
}