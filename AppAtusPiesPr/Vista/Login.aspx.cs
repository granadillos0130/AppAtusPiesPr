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
                    Session["idUsuario"] = oUser.IdUsuario;  // Almacena el idUsuario (idCliente si usas otro nombre)
                    Session["email"] = oUser.Email;
                    Session["usuario"] = oUser.Nombres + " " + oUser.Apellidos;
                    Session["rol"] = oUser.Rol;
                    Session["idVendedor"] = oUser.IdUsuario;

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
          
            string userEmail = txtEmailRecuperar.Text.Trim();
          
            if (clientoLo.IsEmailExist(userEmail))
            {
               
                string temporaryPassword = GenerarTemporalPassword();
               
                clientoLo.SaveTemporaryPassword(userEmail, temporaryPassword);
               
                EnviarTemporalPasswordEmail(userEmail, temporaryPassword);
               
                lblMensajeRecuperar.Text = "Se ha enviado un correo con la contraseña temporal.";
                lblMensajeRecuperar.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
           
                lblMensajeRecuperar.Text = "Correo electrónico no encontrado.";
                lblMensajeRecuperar.ForeColor = System.Drawing.Color.Red;
            }

        }

        private string GenerarTemporalPassword()
        {
            const int length = 10; 
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?";

            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }




        
        private void EnviarTemporalPasswordEmail(string email, string temporaryPassword)
        {
           
            string body = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Restablecimiento de Contraseña</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f7f7f7;
            color: #333333;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            border: 1px solid #dddddd;
        }}
        .email-header {{
            background-color: #4a90e2;
            padding: 20px;
            text-align: center;
            color: #ffffff;
        }}
        .email-header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .email-body {{
            padding: 20px;
            line-height: 1.6;
            color: #555555;
        }}
        .email-body p {{
            margin: 10px 0;
        }}
        .email-body strong {{
            color: #4a90e2;
        }}
        .email-footer {{
            text-align: center;
            padding: 20px;
            font-size: 12px;
            color: #aaaaaa;
            background-color: #f7f7f7;
            border-top: 1px solid #dddddd;
        }}
        .email-button {{
            display: inline-block;
            margin: 20px 0;
            padding: 10px 20px;
            background-color: #4a90e2;
            color: #ffffff;
            text-decoration: none;
            font-size: 16px;
            border-radius: 5px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            transition: background-color 0.3s ease;
        }}
        .email-button:hover {{
            background-color: #357ab7;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <!-- Header -->
        <div class='email-header'>
            <h1>Restablecimiento de Contraseña</h1>
        </div>

        <!-- Body -->
        <div class='email-body'>
            <p>Hola,</p>
            <p>Has solicitado restablecer tu contraseña. Tu nueva contraseña temporal es:</p>
            <p style='text-align: center; font-size: 18px; font-weight: bold; color: #4a90e2; border: 1px dashed #4a90e2; padding: 10px; border-radius: 5px;'>
                {temporaryPassword}
            </p>
            <p>Por favor, utiliza esta contraseña para iniciar sesión y asegúrate de cambiarla inmediatamente después de iniciar sesión para mantener tu cuenta segura.</p>
            <p>
                Si no solicitaste este cambio, por favor, <a href='#' style='color: #4a90e2; text-decoration: none;'>contacta a nuestro soporte</a> de inmediato.
            </p>
        </div>

        <!-- Footer -->
        <div class='email-footer'>
            <p>Gracias,<br>El equipo de soporte</p>
        </div>
    </div>
</body>
</html>";

           
            MailMessage message = new MailMessage("96ferney@gmail.com", email)
            {
                Subject = "Contraseña Temporal",
                Body = body,
                IsBodyHtml = true
            };

           
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, 
                Credentials = new System.Net.NetworkCredential("ssferney@gmail.com", "ircu qqav zkjw quhg"), // Credenciales del servidor de correo
                EnableSsl = true 
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
