using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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