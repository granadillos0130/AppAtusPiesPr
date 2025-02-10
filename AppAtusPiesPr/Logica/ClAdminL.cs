using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static AppAtusPiesPr.Entidades.ClUsuarioE;

namespace AppAtusPiesPr.Logica
{
    public class ClAdminL
    {

        public DataTable MtdListarPedido(int idVendedor,string estado)
        {
            ClAdminD ListEstado = new ClAdminD();
            return ListEstado.MtdListarPedidosEstado(idVendedor,estado);
        }
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

        public List<ClUsuarioE> MtdFiltroVendedor(string docu, string estado)
        {
            ClAdminD FiltroVendedor = new ClAdminD();
            return FiltroVendedor.MtdBuscarVendedor(docu, estado);
        }


        public bool MtdValidarCategoria(ClCategoriaE oDatos)
        {
            ClAdminD Categoria = new ClAdminD();
            return Categoria.MtdValidacionCategoria(oDatos);
        }

        public List<ClEstadisticaVendedorE> MtdEstadistica(string documento,string año,string mes)
        {
            ClAdminD EstadisticaD = new ClAdminD();
            return EstadisticaD.MtdBuscarEstadisticasPorVendedor(documento,año,mes); 
        }

        public List<ClCategoriaE> MtdListaCategoria()
        {
            ClAdminD Categoria  = new ClAdminD();
            return Categoria.MtdListarCategoria();
        }

        public List<ClCategoriaE> MtdEstadisticaCategoria(string Orden)
        {
            ClAdminD Categoria = new ClAdminD();
            return Categoria.MtdEstadistcaCategoria( Orden);

        }

        public bool MtdRegistrarCategoria(ClCategoriaE oDatos)
        {
            ClAdminD registrar = new ClAdminD();

            bool exito = registrar.MtdAgregarCategoria(oDatos);

            if (exito)
            {
                try
                {
                    
                    ClAdminD adminD = new ClAdminD();
                    List<ClUsuarioE> vendedores = adminD.MtdObtenerVendedorEmail();

                    if (vendedores.Count > 0)
                    {
                        foreach (var vendedor in vendedores)
                        {
                            EnviarCorreoVendedores(vendedor.Email, vendedor.Nombres, vendedor.Apellidos, oDatos.descripcion);
                        }
                    }
                    else
                    {
                        throw new Exception("No hay vendedores activos para notificar.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("La Categoria fue registrada, pero ocurrió un error al enviar el correo: " + ex.Message);
                }
            }

            return exito;
        }


        public bool MtdAceptarSolicitud(int idVendedor, string email)
        {
            ClAdminD AceptarSoli = new ClAdminD();
            bool exito = AceptarSoli.MtdAceptarSolicitud(idVendedor);

            if (exito)
            {
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

        public bool MtdSolicitudReactivacion(int documento)
        {
            ClAdminD AceptarSoli = new ClAdminD();
            bool exito = AceptarSoli.MtdReactivacionVendedor(documento);

            return exito;
        }




        public bool MtDenegarSolicitud(int idVendedor, string email, string nombreVendedor, string apellidoVendedor)
        {
            ClAdminD DenegarSoli = new ClAdminD();
            bool exito = DenegarSoli.MtdDenegarSolicitud(idVendedor);

            if (exito)
            {
                try
                {
                    EnviarCorreoRechazado(email, nombreVendedor, apellidoVendedor);
                }
                catch (Exception ex)
                {
                    throw new Exception("La solicitud fue Rechazada, pero ocurrió un error al enviar el correo: " + ex.Message);
                }
            }

            return exito;
        }


        public bool MtdInactivarVendedor(int idVendedor, string email,string nombreVendedor,string apellidoVendedor)
        {
            ClAdminD Inactivar = new ClAdminD();
            bool exito = Inactivar.MtdInactivarVendedor(idVendedor);

            if (exito)
            {
                try
                {
                    EnviarCorreoInactivar(email,nombreVendedor,apellidoVendedor);
                }
                catch (Exception ex)
                {
                    throw new Exception("Se desactivo la cuenta, pero ocurrió un error al enviar el correo: " + ex.Message);
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
                mensaje.From = new MailAddress("pratuspies@gmail.com"); // Dirección del remitente
                mensaje.To.Add(email); // Dirección del destinatario
                mensaje.Subject = "¡Tu cuenta ha sido activada en A Tus Pies!";
                mensaje.Body = @"<!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>¡Felicidades! Tu cuenta ha sido activada</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f7f7f7;
                            color: #333333;
                        }
                        .email-container {
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                            border: 1px solid #dddddd;
                        }
                        .email-header {
                            background-color: #4A90E2; /* Azul */
                            padding: 20px;
                            text-align: center;
                            color: #ffffff;
                        }
                        .email-header h1 {
                            margin: 0;
                            font-size: 24px;
                        }
                        .email-body {
                            padding: 20px;
                            line-height: 1.6;
                            color: #555555;
                        }
                        .email-body p {
                            margin: 10px 0;
                        }
                        .email-body strong {
                            color: #4A90E2; /* Azul */
                        }
                        .email-footer {
                            text-align: center;
                            padding: 20px;
                            font-size: 12px;
                            color: #aaaaaa;
                            background-color: #f7f7f7;
                            border-top: 1px solid #dddddd;
                        }
                        .email-button {
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            background-color: #4A90E2; /* Azul */
                            color: #ffffff;
                            text-decoration: none;
                            font-size: 16px;
                            border-radius: 5px;
                            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                            transition: background-color 0.3s ease;
                        }
                        .email-button:hover {
                            background-color: #357ab7; /* Azul oscuro al hacer hover */
                        }
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <!-- Header -->
                        <div class='email-header'>
                            <h1>¡Felicidades! Tu cuenta ha sido activada</h1>
                        </div>

                        <!-- Body -->
                        <div class='email-body'>
                            <p>Hola,</p>
                            <p>Tu cuenta ha sido activada correctamente en <strong>A Tus Pies</strong>.</p>
                            <p>Ahora puedes acceder a nuestra plataforma para comenzar a subir tus productos y ofrecerlos a miles de compradores interesados en lo que tienes para ofrecer.</p>
                            <h3>¿Qué puedes hacer ahora?</h3>
                            <ul>
                                <li>Iniciar sesión en tu cuenta.</li>
                                <li>Subir tus productos con fotos, descripciones y precios.</li>
                                <li>Gestionar tus ventas y recibir pedidos directamente desde nuestra plataforma.</li>
                            </ul>
                            <p>Estamos aquí para ayudarte a crecer. Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos.</p>
                            <p><strong>Visítanos en:</strong> <a href='https://www.atuspies.com' style='color: #4A90E2;'>A Tus Pies</a></p>
                            <p>¡Gracias por ser parte de nuestra comunidad!</p>
                        </div>

                        <!-- Footer -->
                        <div class='email-footer'>
                            <p>Atentamente,<br>El equipo de A Tus Pies</p>
                        </div>
                    </div>
                </body>
                </html>

                ";
                mensaje.IsBodyHtml = true; // Importante para soportar HTML en el mensaje.


                // Configuración del servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new System.Net.NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"); // Password de Aplicación
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

        //ENVIAR CORREO DE INACTIVAR
        private void EnviarCorreoInactivar(string email,string nombreVendedor,string apellidoVendedor)
        {
            try
            {
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("pratuspies@gmail.com"); // Dirección del remitente
                mensaje.To.Add(email); // Dirección del destinatario
                mensaje.Subject = "¡Tu cuenta ha sido Inactivada!";
                mensaje.Body = @"<!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Cuenta Desactivada</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f7f7f7;
                            color: #333333;
                        }
                        .email-container {
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                            border: 1px solid #dddddd;
                        }
                        .email-header {
                            background-color: #4A90E2; /* Azul */
                            padding: 20px;
                            text-align: center;
                            color: #ffffff;
                        }
                        .email-header h1 {
                            margin: 0;
                            font-size: 24px;
                        }
                        .email-body {
                            padding: 20px;
                            line-height: 1.6;
                            color: #555555;
                        }
                        .email-body p {
                            margin: 10px 0;
                        }
                        .email-body strong {
                            color: #4A90E2; /* Azul */
                        }
                        .email-footer {
                            text-align: center;
                            padding: 20px;
                            font-size: 12px;
                            color: #aaaaaa;
                            background-color: #f7f7f7;
                            border-top: 1px solid #dddddd;
                        }
                        .email-button {
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            background-color: #4A90E2; /* Azul */
                            color: #ffffff;
                            text-decoration: none;
                            font-size: 16px;
                            border-radius: 5px;
                            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                            transition: background-color 0.3s ease;
                        }
                        .email-button:hover {
                            background-color: #357ab7; /* Azul oscuro al hacer hover */
                        }
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <!-- Header -->
                        <div class='email-header'>
                            <h1>¡Tu cuenta ha sido desactivada!</h1>
                        </div>

                        <!-- Body -->
                        <div class='email-body'>
                            <p>Hola {nombreVendedor} {apellidoVendedor},</p>
                            <p>Lamentablemente, tu cuenta en <strong>A Tus Pies</strong> ha sido desactivada.</p>
                            <p>Si crees que esto ha ocurrido por error o deseas más información sobre la desactivación de tu cuenta, por favor no dudes en ponerte en contacto con nosotros. Estamos aquí para ayudarte.</p>
                            <p>Si deseas reactivar tu cuenta, por favor visita el siguiente enlace:</p>
                            <p><a href='https://www.atuspies.com/soporte' class='email-button'>Contacta con soporte</a></p>
                            <p>Te agradecemos por ser parte de nuestra comunidad y esperamos poder ayudarte a resolver cualquier inconveniente.</p>
                        </div>

                        <!-- Footer -->
                        <div class='email-footer'>
                            <p>Atentamente,<br>El equipo de A Tus Pies</p>
                        </div>
                    </div>
                </body>
                </html>
                       ";
                mensaje.IsBodyHtml = true; // Importante para soportar HTML en el mensaje.


                // Configuración del servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new System.Net.NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"); // Password de Aplicación
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


        //ENVIAR CORREO A VENDEDORES PARA CATEGORIAS
        private void EnviarCorreoVendedores(string email, string nombreVendedor, string apellidoVendedor, string nombreCategoria)
        {
            try
            {
                // Crear el mensaje
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("pratuspies@gmail.com"); // Dirección del remitente
                mensaje.To.Add(email); // Dirección del destinatario
                mensaje.Subject = "Nueva Categoria de Zapatos!!!!: "; // Asunto personalizado
                mensaje.Body = $@"<!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Notificación de Nueva Categoría</title>
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
                            background-color: #3498db; /* Azul */
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
                            color: #3498db; /* Azul */
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
                            background-color: #3498db; /* Azul */
                            color: #ffffff;
                            text-decoration: none;
                            font-size: 16px;
                            border-radius: 5px;
                            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                            transition: background-color 0.3s ease;
                        }}
                        .email-button:hover {{
                            background-color: #2980b9; /* Azul oscuro al hacer hover */
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <!-- Header -->
                        <div class='email-header'>
                            <h1>¡Nueva Categoría Disponible!</h1>
                        </div>

                        <!-- Body -->
                        <div class='email-body'>
                            <p>Hola {nombreVendedor} {apellidoVendedor},</p>
                            <p>Nos complace informarte que se ha abierto una nueva categoría en <strong>A Tus Pies</strong>: <strong>{nombreCategoria}</strong>!</p>
                            <p>Te invitamos a explorar esta nueva categoría y, si estás interesado, a unirte para ofrecer tus productos en ella. Sabemos que tu experiencia y dedicación serán un aporte valioso para nuestra comunidad.</p>
                            <p>Si tienes alguna pregunta o necesitas más detalles sobre cómo unirte a esta categoría, no dudes en ponerte en contacto con nosotros.</p>
                            <p>¡Esperamos contar contigo en esta nueva aventura!</p>
                        </div>

                        <!-- Footer -->
                        <div class='email-footer'>
                            <p>Atentamente,<br>El equipo de A Tus Pies</p>
                        </div>
                    </div>
                </body>
                </html>
                ";
                mensaje.IsBodyHtml = true; // Importante para soportar HTML en el mensaje.

                // Configuración del servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new System.Net.NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"); // Password de Aplicación
                smtp.EnableSsl = true; // Habilitar conexión segura (TLS)

                // Enviar el correo
                smtp.Send(mensaje);
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
        private void EnviarCorreoRechazado(string email, string nombreVendedor, string apellidoVendedor)
        {
            try
            {
                // Crear el mensaje
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("pratuspies@gmail.com"); // Dirección del remitente
                mensaje.To.Add(email); // Dirección del destinatario
                mensaje.Subject = "Solicitud Rechazada: " ; // Asunto personalizado
                mensaje.Body = "Solicitud Rechazada en ATusPies: "; // Asunto personalizado
                mensaje.Body = $@"<!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Notificación de rechazo de solicitud</title>
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
                            background-color: #e74c3c; /* Rojo */
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
                            color: #e74c3c; /* Rojo */
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
                            background-color: #e74c3c; /* Rojo */
                            color: #ffffff;
                            text-decoration: none;
                            font-size: 16px;
                            border-radius: 5px;
                            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                            transition: background-color 0.3s ease;
                        }}
                        .email-button:hover {{
                            background-color: #c0392b; /* Rojo oscuro al hacer hover */
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <!-- Header -->
                        <div class='email-header'>
                            <h1>Solicitud de Vendedor Rechazada</h1>
                        </div>

                        <!-- Body -->
                        <div class='email-body'>
                            <p>Hola {nombreVendedor} {apellidoVendedor},</p>
                            <p>Lamentablemente, tu solicitud para ser parte de nuestro equipo de vendedores en <strong>A Tus Pies</strong> ha sido rechazada en esta ocasión.</p>
                            <p>Sin embargo, te animamos a mejorar algunos aspectos y volver a intentarlo en el futuro. Estamos siempre en busca de vendedores con pasión y dedicación, y creemos que puedes lograrlo.</p>
                            <p>Si necesitas algún consejo o más detalles sobre cómo puedes mejorar tu solicitud, no dudes en ponerte en contacto con nosotros.</p>
                            <p>Gracias por tu interés y esfuerzo. ¡Esperamos verte nuevamente en el futuro!</p>
                        </div>

                        <!-- Footer -->
                        <div class='email-footer'>
                            <p>Atentamente,<br>El equipo de A Tus Pies</p>
                        </div>
                    </div>
                </body>
                </html>

                "; ;
                mensaje.IsBodyHtml = true; // Importante para soportar HTML en el mensaje.

                // Configuración del servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new System.Net.NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"); // Password de Aplicación
                smtp.EnableSsl = true; // Habilitar conexión segura (TLS)

                // Enviar el correo
                smtp.Send(mensaje);
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