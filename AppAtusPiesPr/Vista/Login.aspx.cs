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
    }
        }