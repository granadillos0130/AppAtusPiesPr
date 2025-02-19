using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.IO;
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
            // No es necesario manejar postbacks aquí
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
                    // Verificar si hay múltiples roles
                    if (oUser.Roles.Count > 1)
                    {
                        // Guardar la lista de roles en el ViewState
                        ViewState["Roles"] = oUser.Roles;
                        ViewState["Usuario"] = oUser;

                        // Configurar el DropDownList con los roles
                        ddlRoles.DataSource = oUser.Roles.Select(r => r.RoleName).ToList();
                        ddlRoles.DataBind();

                        // Mostrar el modal para seleccionar el rol
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "$('#myModal').modal('show');", true);
                    }
                    else if (oUser.Roles.Count == 1)
                    {
                        // Configurar variables de sesión
                        var role = oUser.Roles[0];
                        Session["email"] = oUser.Documento; // Suponiendo que el email es igual al documento
                        Session["usuario"] = oUser.Nombres + " " + oUser.Apellidos;
                        Session["rol"] = role.RoleName;
                        Session["idUsuario"] = role.IdUsuario;

                        // Redirigir según el rol del usuario
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
                System.Diagnostics.Debug.WriteLine("Mensaje de excepción: " + ex.Message);

                if (ex.Message == "Su Cuenta esta Desactivada,¿Desea enviar una solicitud para activarla nuevamente?")
                {
                    ViewState["IdUsuario"] = obUsuarioEn.Documento;  // Guardar el documento del usuario en el ViewState
                    MostrarAlerta("error", "Error", ex.Message, true);  // Mostrar alerta con un botón para enviar solicitud
                }
                else
                {
                    MostrarAlerta("error", "Error", ex.Message);
                }
            }
        }


        protected void EnviarSolicitud()
{
    // Obtener el ID del vendedor desde ViewState
    if (ViewState["IdUsuario"] == null || !int.TryParse(ViewState["IdUsuario"].ToString(), out int idVendedor))
    {
        MostrarAlerta("error", "Error", "No se pudo obtener la información del vendedor.");
        return;
    }

    // Verificar que el ID obtenido sea el correcto
    System.Diagnostics.Debug.WriteLine($"ID Vendedor recuperado desde ViewState: {idVendedor}");

    try
    {
        ClAdminL adminLogic = new ClAdminL();
        bool exito = adminLogic.MtdSolicitudReactivacion(idVendedor);  // Enviar la solicitud de reactivación

        if (exito)
        {
            MostrarAlerta("success", "Solicitud enviada", "Tu solicitud ha sido enviada con éxito.");
        }
        else
        {
            MostrarAlerta("error", "Error en la solicitud", "Ocurrió un error al procesar la solicitud.");
        }
    }
    catch (Exception ex)
    {
        MostrarAlerta("error", "Error", ex.Message);
    }
}




        protected void btnEnviarSolicitud_Click(object sender, EventArgs e)
        {
            EnviarSolicitud();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Volver();
        }

        protected void Volver()
        {
            // Lógica para volver (si es necesario)
        }

        private void MostrarAlerta(string icon, string title, string text, bool mostrarBotonExtra = false)
        {
            string script;
            if (mostrarBotonExtra)
            {
                script = $@"Swal.fire({{
                icon: '{icon}',
                title: '{title}',
                text: '{text}',
                showCancelButton: true,
                confirmButtonText: 'Enviar Solicitud',
                cancelButtonText: 'Volver',
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            }}).then((result) => {{
                if (result.isConfirmed) {{
                    document.getElementById('{btnEnviarSolicitud.ClientID}').click();
                }} else if (result.dismiss === Swal.DismissReason.cancel) {{
                    document.getElementById('{btnVolver.ClientID}').click();
                }}
            }});";
            }
            else
            {
                script = $@"Swal.fire({{
                icon: '{icon}',
                title: '{title}',
                text: '{text}',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#3085d6'
            }});";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), title.Replace(" ", ""), script, true);
        }








        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            // Limpiar mensajes de error previos
            lblDocumentoError.Visible = false;
            lblNombreError.Visible = false;
            lblApellidoError.Visible = false;
            lblCorreoError.Visible = false;
            lblContrasenaError.Visible = false;
            lblTelefonoError.Visible = false;
            lblDireccionError.Visible = false;

            // Validación del formulario
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                lblDocumentoError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblNombreError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblApellidoError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !txtCorreo.Text.Contains("@"))
            {
                lblCorreoError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtContrasenaReg.Text))
            {
                lblContrasenaError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblTelefonoError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblDireccionError.Visible = true;
                isValid = false;
            }

            // Si la validación es exitosa, registrar al cliente
            if (isValid)
            {
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

                // Verificar si el documento o el correo ya están registrados
                string mensaje;
                int idCliente = clientoLo.RegistrarCliente(nuevoCliente, out mensaje);

                if (idCliente > 0)
                {
                    // Si el ID es mayor que 0, significa que el cliente fue registrado correctamente
                    MostrarAlerta("success", "¡Felicidades!", "Te has registrado con éxito. Ahora puedes iniciar sesión.");
                    LimpiarCampos();
                }
                else
                {
                    // Si el mensaje contiene un error, mostrarlo
                    MostrarAlerta("error", "¡Ups! Algo salió mal", mensaje);
                    LimpiarCampos();
                }
            }
            else
            {
                // Evitar cerrar la modal si hay errores
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", "$('#registerModal').modal('show');", true);
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
                    MostrarAlerta("error", "Rol no encontrado", "El rol seleccionado no existe.");
                    break;
            }
        }


        protected void btnRegistrarVendedor_Click(object sender, EventArgs e)
        {
            List<string> camposVacios = new List<string>();

            // Validar si los campos están vacíos
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

            // Validar si la foto está vacía
            if (!fuFoto.HasFile)
                camposVacios.Add("Foto");

            // Si hay campos vacíos, mostrar alerta
            if (camposVacios.Any())
            {
                string campos = string.Join(", ", camposVacios);
                MostrarAlerta("warning", "Campos vacíos", "Por favor, completa los siguientes campos: " + campos);

                // Resaltar los campos vacíos
                ResaltarCamposVacios(camposVacios);
                return;
            }

            // Crear el nuevo vendedor
            ClUsuarioE nuevoVendedor = new ClUsuarioE
            {
                Documento = txtDocumentoVend.Text,
                Nombres = txtNombreVend.Text,
                Apellidos = txtApellidoVend.Text,
                Email = txtCorreoVend.Text,
                Password = txtContrasenaVend.Text,
                Telefono = txtTelefonoVend.Text,
                Direccion = txtDireccionVend.Text
            };

            // Guardar la foto en la carpeta especificada
           
            // Mensaje de salida
            string mensaje;

            // Intentar registrar al vendedor
            int idVendedor = vendedorLo.RegistrarVendedor(nuevoVendedor, fuFoto.PostedFile, out mensaje);

            if (idVendedor > 0)
            {
                string fotoNombre = $"{nuevoVendedor.Nombres}-{nuevoVendedor.Documento}{Path.GetExtension(fuFoto.FileName)}";
                string rutaFoto = Path.Combine(Server.MapPath("~/Vista/imagenes/fotoPerfil"), fotoNombre);
                fuFoto.SaveAs(rutaFoto);

                nuevoVendedor.foto = fotoNombre;

                // Si el ID es mayor que 0, significa que se registró correctamente
                MostrarAlerta("success", "Registro exitoso", "El vendedor fue registrado correctamente. Su cuenta está en proceso de activación.");

                // Limpiar los campos de entrada
                LimpiarCamposVendedor();

                // Cerrar el modal de registro
                ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "$('#registerVendedorModal').modal('hide');", true);
            }
            else
            {
                // Si idVendedor es 0 o un valor negativo, mostrar el mensaje de error
                MostrarAlerta("error", "Error", mensaje);
            }
        }




        private void ResaltarCamposVacios(List<string> camposVacios)
        {

            txtDocumentoVend.CssClass = "form-control";
            txtNombreVend.CssClass = "form-control";
            txtApellidoVend.CssClass = "form-control";
            txtCorreoVend.CssClass = "form-control";
            txtContrasenaVend.CssClass = "form-control";
            txtTelefonoVend.CssClass = "form-control";
            txtDireccionVend.CssClass = "form-control";


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


                MostrarAlerta("Error", "Error al enviar el correo.", "Ocurrió un error al enviar el correo. Por favor, intenta nuevamente.");
            }
        }

        private void LimpiarEmail()
        {
            txtEmailRecuperar.Text = "";
        }

        protected void btnEnviarRecuperar_Click(object sender, EventArgs e)
        {
            string userEmail = txtEmailRecuperar.Text.Trim();

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                MostrarAlerta("warning", "Correo requerido", "Por favor, ingresa un correo electrónico válido para continuar con la recuperación de tu contraseña.");
                return;
            }

            if (clientoLo.IsEmailExist(userEmail))
            {
                string temporaryPassword = GenerarTemporalPassword();
                clientoLo.SaveTemporaryPassword(userEmail, temporaryPassword);
                EnviarTemporalPasswordEmail(userEmail, temporaryPassword);

                MostrarAlerta("success", "Correo enviado", "Hemos enviado un correo con una contraseña temporal. Por favor revisa tu bandeja de entrada o carpeta de spam.");
                LimpiarEmail();
            }
            else
            {
                MostrarAlerta("error", "Correo no encontrado", "El correo electrónico ingresado no está registrado en nuestro sistema. Verifica y vuelve a intentarlo.");
                LimpiarEmail();
            }

        }

        
    }
}
