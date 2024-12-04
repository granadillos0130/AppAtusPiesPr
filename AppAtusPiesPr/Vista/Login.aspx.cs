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

        private void MostrarMensajeDeError(TextBox textBox, string mensaje)
        {
            string script = $@"
    var input = document.getElementById('{textBox.ClientID}');
    input.setAttribute('placeholder', '{mensaje}');
    input.classList.add('is-invalid');
    
    // Eliminar mensaje de error y cambiar borde al comenzar a escribir
    input.addEventListener('input', function() {{
        if (input.value !== '') {{
            input.classList.remove('is-invalid');
            input.style.borderColor = ''; // Restablecer el color del borde
            input.removeAttribute('placeholder');
        }}
    }});
";
            ScriptManager.RegisterStartupScript(this, GetType(), $"{textBox.ID}Error", script, true);
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
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MostrarAlerta("warning", "Campos vacíos", "Por favor, complete todos los campos.");
                return;
            }

            ClUsuarioE obUsuarioEn = new ClUsuarioE
            {
                Documento = txtEmail.Text,
                Password = txtContrasena.Text
            };

            try
            {
                ClUsuarioE oUser = clientoLo.MtdIngreso(obUsuarioEn);

                if (oUser != null)
                {
                    if (oUser.Roles.Count > 1)
                    {
                        ViewState["Roles"] = oUser.Roles;
                        ViewState["Usuario"] = oUser;

                        ddlRoles.DataSource = oUser.Roles.Select(r => r.RoleName).ToList();
                        ddlRoles.DataBind();

                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#myModal').modal('show');", true);
                    }
                    else if (oUser.Roles.Count == 1)
                    {
                        var role = oUser.Roles[0];
                        Session["email"] = oUser.Documento;
                        Session["usuario"] = $"{oUser.Nombres} {oUser.Apellidos}";
                        Session["rol"] = role.RoleName;
                        Session["idUsuario"] = role.IdUsuario;

                        RedirigirSegunRol(role.RoleName);
                    }
                }
                else
                {
                    MostrarAlerta("error", "Error de autenticación", "Credenciales incorrectas.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error", ex.Message);
            }
        }


        protected void btnSeleccionarRol_Click(object sender, EventArgs e)
        {
            string rolSeleccionado = ddlRoles.SelectedValue;
            List<ClRolE> roles = (List<ClRolE>)ViewState["Roles"];
            ClUsuarioE oUser = (ClUsuarioE)ViewState["Usuario"]; // Recuperar el usuario del ViewState
            ClRolE role = roles.FirstOrDefault(r => r.RoleName == rolSeleccionado);
            if (role != null)
            {
                // Configurar variables de sesión
                Session["email"] = oUser.Documento; // Suponiendo que el email es igual al documento
                Session["usuario"] = oUser.Nombres + " " + oUser.Apellidos;
                Session["rol"] = role.RoleName;
                Session["idUsuario"] = role.IdUsuario;


                // Redirigir según el rol seleccionado
                RedirigirSegunRol(role.RoleName);
            }
        }


        private void RedirigirSegunRol(string rol)
        {
            switch (rol)
            {
                case "Admin":
                    Response.Redirect("Peticiones.aspx");
                    break;

                case "Vendedor":
                    Response.Redirect("ProductosMasVendidos.aspx");

                    break;

                case "Cliente":
                    Response.Redirect("../index.aspx");
                    break;

                default:
                    lblMensaje.Text = "Rol no reconocido.";
                    MostrarAlerta("error","Rol no encontrado","El rol seleccionado no existe.");
                    break;
            }
        }


        protected void btnRegistrarVendedor_Click(object sender, EventArgs e)
        {
            // Inicializar variable para los campos vacíos
            List<string> camposVacios = new List<string>();

            // Verificar si hay campos vacíos
            if (string.IsNullOrWhiteSpace(txtDocumentoVend.Text))
                camposVacios.Add("Documento");
            if (string.IsNullOrWhiteSpace(txtNombreVend.Text))
                camposVacios.Add("Nombre");
            if (string.IsNullOrWhiteSpace(txtApellidoVend.Text))
                camposVacios.Add("Apellido");
            if (string.IsNullOrWhiteSpace(txtCorreoVend.Text))
                camposVacios.Add("Correo");
            if (string.IsNullOrWhiteSpace(txtContrasenaVend.Text))
                camposVacios.Add("Contraseña");
            if (string.IsNullOrWhiteSpace(txtTelefonoVend.Text))
                camposVacios.Add("Teléfono");
            if (string.IsNullOrWhiteSpace(txtDireccionVend.Text))
                camposVacios.Add("Dirección");

            // Si hay campos vacíos, mostrar alerta y marcar los campos
            if (camposVacios.Any())
            {
                string campos = string.Join(", ", camposVacios);
                MostrarAlerta("warning", "Campos vacíos", "Por favor, completa los siguientes campos: " + campos);

                // Resaltar los campos vacíos
                ResaltarCamposVacios(camposVacios);
                return;
            }

            // Si todos los campos están llenos, continuar con el registro
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
                // Registro exitoso: muestra la alerta de éxito
                MostrarAlerta("success", "Registro exitoso", "El vendedor fue registrado correctamente.");

                // Limpiar los campos
                LimpiarCamposVendedor();

                // Cerrar el modal
                ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "$('#registerVendedorModal').modal('hide');", true);
            }
            else
            {
                // Registro fallido: muestra un mensaje de error
                MostrarAlerta("error", "Error", "No se pudo registrar al vendedor. Intenta nuevamente.");
            }
        }


        private void ResaltarCamposVacios(List<string> camposVacios)
        {
            // Remover clases previas de resaltar
            txtDocumentoVend.CssClass = "form-control";
            txtNombreVend.CssClass = "form-control";
            txtApellidoVend.CssClass = "form-control";
            txtCorreoVend.CssClass = "form-control";
            txtContrasenaVend.CssClass = "form-control";
            txtTelefonoVend.CssClass = "form-control";
            txtDireccionVend.CssClass = "form-control";

            // Agregar la clase de resaltar a los campos vacíos
            foreach (var campo in camposVacios)
            {
                switch (campo)
                {
                    case "Documento":
                        txtDocumentoVend.CssClass += " is-invalid";
                        break;
                    case "Nombre":
                        txtNombreVend.CssClass += " is-invalid";
                        break;
                    case "Apellido":
                        txtApellidoVend.CssClass += " is-invalid";
                        break;
                    case "Correo":
                        txtCorreoVend.CssClass += " is-invalid";
                        break;
                    case "Contraseña":
                        txtContrasenaVend.CssClass += " is-invalid";
                        break;
                    case "Teléfono":
                        txtTelefonoVend.CssClass += " is-invalid";
                        break;
                    case "Dirección":
                        txtDireccionVend.CssClass += " is-invalid";
                        break;
                }
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

           
            MailMessage message = new MailMessage("pratuspies@gmail.com", email)
            {
                Subject = "Contraseña Temporal",
                Body = body,
                IsBodyHtml = true
            };

           
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, 
                Credentials = new System.Net.NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"), // Credenciales del servidor de correo
                EnableSsl = true 
            };

            try
            {
                
                client.Send(message);
            }
            catch (Exception ex)
            {
               

                MostrarAlerta("Error", "Error al enviar el correo.","Ocurrió un error al enviar el correo. Por favor, intenta nuevamente.");
            }
        }

    }
}
