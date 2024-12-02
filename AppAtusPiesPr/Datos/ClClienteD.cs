using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;

namespace AppAtusPiesPr.Datos
{
    public class ClClienteD
    {
        private ClConexion conexion = new ClConexion();

        public ClUsuarioE mtdPerfilCliente(int idCliente)
        {
            ClUsuarioE cliente = new ClUsuarioE();
            ClConexion oConex = new ClConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("spListarPerfil", oConex.MtdAbrirConexion());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new ClUsuarioE
                    {
                        IdUsuario = Convert.ToInt32(reader["idCliente"]),
                        Documento = reader["documento"].ToString(),
                        Nombres = reader["nombres"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        estado = reader["estado"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar la información: " + ex.Message);
            }

            oConex.MtdCerrarConexion();

            return cliente;

        }

        public ClUsuarioE mtdActualiarInfoCliente(ClUsuarioE objData)
        {

                ClConexion conex = new ClConexion();
                SqlConnection connection = conex.MtdAbrirConexion();

                using (SqlCommand cmd = new SqlCommand("spActualizarDatosPerfilCliente", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", objData.IdUsuario);
                    cmd.Parameters.AddWithValue("@documento", objData.Documento);
                    cmd.Parameters.AddWithValue("@nombres", objData.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", objData.Apellidos);
                    cmd.Parameters.AddWithValue("@email", objData.Email);
                    cmd.Parameters.AddWithValue("@password", objData.Password);
                    cmd.Parameters.AddWithValue("@telefono", objData.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", objData.Direccion);
                    cmd.ExecuteNonQuery();
                }
                conex.MtdCerrarConexion();
                return objData;

        }
        private void enviarNotificacion(ClUsuarioE objData)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"),
                    EnableSsl = true,
                };

                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("pratuspies@gmail.com"); // Dirección del remitente
                mensaje.To.Add(objData.Email); // Dirección del destinatario
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
                            <p>Hola,</p>
                            <p>Queremos informarte que los datos de tu cuenta en <strong>A Tus Pies</strong> han sido modificados.</p>
                            <p>Si no realizaste esta modificación, por favor contáctanos inmediatamente.</p>
                            <p>Si deseas contactar con nosotros, por favor visita el siguiente enlace:</p>
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
                smtp.Port = 587; // Puerto para TLS
                smtp.Credentials = new System.Net.NetworkCredential("pratuspies@gmail.com", "zlre rota ykjk qkbq"); // Password de Aplicación
                smtp.EnableSsl = true; // Habilitar conexión segura (TLS)

                smtp.Send(mensaje); // Enviar el correo

            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo de notificación.", ex);
            }
        }
        public int MtdRegistrarCliente(ClUsuarioE cliente)
        {
            int idCliente = 0;
            SqlConnection con = null;
            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("spRegistrarCliente", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@email", cliente.Email);
                cmd.Parameters.AddWithValue("@password", cliente.Password);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

                idCliente = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar cliente: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }
            return idCliente;
        }

        public ClUsuarioE MtdIngreso(ClUsuarioE objDatos)
        {
            ClUsuarioE obDatosUsuario = new ClUsuarioE();
            SqlConnection con = null;

            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand comando = new SqlCommand("SpValidarLogin", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros de entrada
                comando.Parameters.AddWithValue("@documento", objDatos.Documento);
                comando.Parameters.AddWithValue("@password", objDatos.Password);

                // Configurar parámetros de salida
                SqlParameter rolesParameter = new SqlParameter("@roles", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(rolesParameter);

                SqlParameter nombresParameter = new SqlParameter("@nombres", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(nombresParameter);

                SqlParameter apellidosParameter = new SqlParameter("@apellidos", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(apellidosParameter);

                SqlParameter mensajeParameter = new SqlParameter("@mensaje", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(mensajeParameter);

                // Ejecutar el procedimiento almacenado
                comando.ExecuteNonQuery();

                // Procesar los resultados
                string roles = rolesParameter.Value.ToString();
                string mensaje = mensajeParameter.Value.ToString();

                if (string.IsNullOrEmpty(mensaje))
                {
                    // El usuario tiene múltiples roles posibles
                    obDatosUsuario = new ClUsuarioE
                    {
                        Documento = objDatos.Documento,
                        Nombres = nombresParameter.Value.ToString(),
                        Apellidos = apellidosParameter.Value.ToString(),
                        Roles = roles.Split(',')
                                    .Select(r =>
                                    {
                                        var parts = r.Split('-');
                                        return new ClRolE
                                        {
                                            RoleName = parts[0],
                                            IdUsuario = Convert.ToInt32(parts[1])
                                        };
                                    }).ToList()
                    };
                }
                else
                {
                    // Lanzar una excepción si hay un mensaje de error
                    throw new Exception(mensaje);
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return obDatosUsuario;
        }
        public bool IsEmailExist(string email)
        {
            bool emailExist = false;
            SqlConnection con = null;

            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("spVerificarEmail", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", email);
                emailExist = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return emailExist;
        }

        public void SaveTemporaryPassword(string email, string temporaryPassword)
        {
            SqlConnection con = null;

            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("spGuardarTemporalPassword", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Password", temporaryPassword);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }
        }

        public bool IsResetCodeValid(string resetCode)
        {
            bool isValid = false;
            SqlConnection con = null;

            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("spVerificarResetCode", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResetCode", resetCode);
                isValid = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return isValid;
        }

        public void UpdatePassword(string resetCode, string newPassword)
        {
            SqlConnection con = null;

            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand cmd = new SqlCommand("spActualizarPassword", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Password", newPassword);
                cmd.Parameters.AddWithValue("@ResetCode", resetCode);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }
        }
    }
}