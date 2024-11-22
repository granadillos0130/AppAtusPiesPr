using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClAdminL
    {

        public DataTable MtdListarVendedores()
        {
            ClAdminD Lista = new ClAdminD();
            return Lista.MtdListarVendedores();
        }

        public DataTable MtdListarUsuarios()
        {
            ClAdminD ListaUsuario = new ClAdminD();
            return ListaUsuario.MtdListarUsuarios();
        }

        public DataTable MtdObtenerSolicitudes()
        {
            ClAdminD ListaSolicitudes = new ClAdminD();
            return ListaSolicitudes.MtdListarSolicitudes();
        }

        public bool MtdAceptarSolicitud(int idVendedor, string email)
        {
            ClAdminD AceptarSoli = new ClAdminD();
            bool exito = AceptarSoli.MtdAceptarSolicitud(idVendedor);

            if (exito)
            {
                // Si la solicitud se aceptó correctamente, enviar correo
                try
                {
                    EnviarCorreo(email);
                }
                catch (Exception ex)
                {
                    throw new Exception("La solicitud fue aceptada, pero ocurrió un error al enviar el correo: " + ex.Message);
                }
            }

            return exito;
        }


        //ENVIAR CORREO AL ACEPTAR
        private void EnviarCorreo(string email)
        {
            try
            {
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("granadossantiago2006@gmail.com"); // Dirección del remitente
                mensaje.To.Add(email); // Dirección del destinatario
                mensaje.Subject = "¡Tu cuenta ha sido activada en A Tus Pies!";
                mensaje.Body = @"
    <h1 style='color: #4CAF50;'>¡Felicidades!</h1>
    <p>Tu cuenta ha sido activada correctamente en <strong>A Tus Pies</strong>.</p>
    <p>Ahora puedes acceder a nuestra plataforma para comenzar a subir tus productos y ofrecerlos a miles de compradores interesados en lo que tienes para ofrecer.</p>
    <h3>¿Qué puedes hacer ahora?</h3>
    <ul>
        <li>Iniciar sesión en tu cuenta.</li>
        <li>Subir tus productos con fotos, descripciones y precios.</li>
        <li>Gestionar tus ventas y recibir pedidos directamente desde nuestra plataforma.</li>
    </ul>
    <p>Estamos aquí para ayudarte a crecer. Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos.</p>
    <p><strong>Visítanos en:</strong> <a href='https://www.atuspies.com' style='color: #4CAF50;'>A Tus Pies</a></p>
    <p>¡Gracias por ser parte de nuestra comunidad!</p>
    <p>Atentamente,<br>El equipo de A Tus Pies</p>";
                mensaje.IsBodyHtml = true; // Importante para soportar HTML en el mensaje.


                // Configuración del servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new System.Net.NetworkCredential("granadossantiago2006@gmail.com", "degj jfht juau nzbv"); // Password de Aplicación
                smtp.EnableSsl = true; // Habilitar conexión segura (TLS)

                smtp.Send(mensaje); // Enviar el correo
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Error SMTP al enviar el correo: " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al enviar el correo: " + ex.Message);
            }
        }

    }


}