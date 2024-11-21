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
    public partial class Login : System.Web.UI.Page
    {
        private ClClienteL clientoLo = new ClClienteL();
        private ClVendedorL vendedorLo = new ClVendedorL();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validación del formulario
            if (string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtContrasenaReg.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblMensaje.Text = "Por favor, completa todos los campos.";
                return;
            }

            // Crear un nuevo cliente
            ClUsuarioE nuevoCliente = new ClUsuarioE
            {
                Documento = txtDocumento.Text,
                Nombres = txtNombre.Text,
                Apellidos = txtApellido.Text,
                Email = txtCorreo.Text,
                Password = txtContrasenaReg.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text
            };

            // Registrar el nuevo cliente
            int idCliente = clientoLo.RegistrarCliente(nuevoCliente);

            if (idCliente > 0)
            {
                lblMensaje.Text = "Registro exitoso. Por favor, inicia sesión.";
                // Limpiar los campos del formulario
                LimpiarCampos();
            }
            else
            {
                lblMensaje.Text = "Error al registrar. Intente nuevamente.";
            }
        }

        private void LimpiarCampos()
        {
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtContrasenaReg.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            // Crear una instancia del modelo de usuario con los datos ingresados
            ClUsuarioE obUsuarioEn = new ClUsuarioE
            {
                Documento = txtEmail.Text,
                Password = txtContrasena.Text
            };

            try
            {



                // Llamar al método de autenticación
                ClUsuarioE oUser = clientoLo.MtdIngreso(obUsuarioEn);

                // Verificar si el usuario existe
                if (oUser != null)
                {
                    // Configurar variables de sesión
                    Session["email"] = oUser.Email;
                    Session["usuario"] = oUser.Nombres + " " + oUser.Apellidos;
                    Session["rol"] = oUser.Rol;
                    Session["idUsuario"] = oUser.IdUsuario;

                    // Redirigir según el rol del usuario
                    switch (oUser.Rol)
                    {
                        case "Admin":
                            Response.Redirect("BlankPage.aspx");
                            break;

                        case "Vendedor":
                            Response.Redirect("BlankPage.aspx");
                            break;

                        case "Cliente":
                            Response.Redirect("../index.aspx");
                            break;

                        default:
                            lblMensaje.Text = "Rol no reconocido.";
                            break;
                    }
                }
                else
                {
                    // Mostrar mensaje de error si las credenciales no son válidas
                    lblMensaje.Text = "Credenciales incorrectas.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnRegistrarVendedor_Click(object sender, EventArgs e)
        {
            // Validación del formulario
            if (string.IsNullOrWhiteSpace(txtDocumentoVend.Text) ||
                string.IsNullOrWhiteSpace(txtNombreVend.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoVend.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoVend.Text) ||
                string.IsNullOrWhiteSpace(txtContrasenaVend.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoVend.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionVend.Text))
            {
                lblMensaje.Text = "Por favor, completa todos los campos.";
                return;
            }

            // Crear un nuevo vendedor
            ClUsuarioE nuevoVendedor = new ClUsuarioE
            {
                Documento = txtDocumentoVend.Text,
                Nombres = txtNombreVend.Text,
                Apellidos = txtApellidoVend.Text,
                Email = txtCorreoVend.Text,
                Password = txtContrasenaVend.Text,
                Telefono = txtTelefonoVend.Text,
                Direccion = txtDireccionVend.Text,
                
            };

            // Registrar el nuevo vendedor
            int idVendedor = vendedorLo.RegistrarVendedor(nuevoVendedor);

            if (idVendedor > 0)
            {
                lblMensaje.Text = "Registro de vendedor exitoso. El estado es 'PROCESO'.";
                // Limpiar los campos del formulario
                LimpiarCamposVendedor();
            }
            else
            {
                lblMensaje.Text = "Error al registrar el vendedor. Intente nuevamente.";
            }
        }

        private void LimpiarCamposVendedor()
        {
            txtDocumentoVend.Text = "";
            txtNombreVend.Text = "";
            txtApellidoVend.Text = "";
            txtCorreoVend.Text = "";
            txtContrasenaVend.Text = "";
            txtTelefonoVend.Text = "";
            txtDireccionVend.Text = "";
        }


        // Método para limpiar los campos del formulario
        


    }
}