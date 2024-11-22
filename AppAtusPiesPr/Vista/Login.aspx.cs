using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        protected void btnEnviarRecuperar_Click(object sender, EventArgs e)
        {
            // Captura el correo ingresado en el campo del formulario
            string userEmail = txtEmailRecuperar.Text.Trim();
            // Verifica si el correo electrónico existe en la base de datos utilizando la capa lógica
            if (clientoLo.IsEmailExist(userEmail))
            {
                // Genera una contraseña temporal
                string temporaryPassword = GenerateTemporaryPassword();
                // Guarda la contraseña temporal en la base de datos
                clientoLo.SaveTemporaryPassword(userEmail, temporaryPassword);
                // Envía el correo con la contraseña temporal
                SendTemporaryPasswordEmail(userEmail, temporaryPassword);
                // Muestra un mensaje de éxito en la interfaz
                lblMensajeRecuperar.Text = "Se ha enviado un correo con la contraseña temporal.";
                lblMensajeRecuperar.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                // Si el correo no está registrado, muestra un mensaje de error
                lblMensajeRecuperar.Text = "Correo electrónico no encontrado.";
                lblMensajeRecuperar.ForeColor = System.Drawing.Color.Red;
            }

        }

        private string GenerateTemporaryPassword()
        {
            const int length = 10; // Longitud de la contraseña temporal
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?";

            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }




        // Método para enviar el correo de restablecimiento
        private void SendTemporaryPasswordEmail(string email, string temporaryPassword)
        {
            // Contenido del correo electrónico
            string body = $@"
    <html>
    <body>
        <p>Hola,</p>
        <p>Has solicitado restablecer tu contraseña. Tu nueva contraseña temporal es: <strong>{temporaryPassword}</strong></p>
        <p>Por favor, utiliza esta contraseña para iniciar sesión y asegúrate de cambiarla inmediatamente después de iniciar sesión.</p>
        <p>Gracias,</p>
        <p>El equipo de soporte</p>
    </body>
    </html>";

            // Configuración del correo
            MailMessage message = new MailMessage("96ferney@gmail.com", email)
            {
                Subject = "Contraseña Temporal",
                Body = body,
                IsBodyHtml = true
            };

            // Configuración del cliente SMTP
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Puerto para conexiones STARTTLS
                Credentials = new System.Net.NetworkCredential("ssferney@gmail.com", "ircu qqav zkjw quhg"), // Credenciales del servidor de correo
                EnableSsl = true // Habilita SSL para mayor seguridad
            };

            try
            {
                // Intenta enviar el correo
                client.Send(message);
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de fallo en el envío del correo
                lblMensajeRecuperar.Text = "Error al enviar el correo: " + ex.Message;
                lblMensajeRecuperar.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}
