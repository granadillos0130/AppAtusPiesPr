﻿using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClClienteD
    {
        private ClConexion conexion = new ClConexion();

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
            ClUsuarioE obDatosUsuario = null;
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
                SqlParameter rolParameter = new SqlParameter("@rol", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(rolParameter);

                SqlParameter nombresParameter = new SqlParameter("@nombres", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(nombresParameter);

                SqlParameter apellidosParameter = new SqlParameter("@apellidos", SqlDbType.VarChar, 100)
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
                string mensaje = mensajeParameter.Value.ToString();

                if (string.IsNullOrEmpty(mensaje))
                {
                    // Crear el objeto usuario si no hay mensaje de error
                    obDatosUsuario = new ClUsuarioE
                    {
                        Documento = objDatos.Documento,
                        Nombres = nombresParameter.Value.ToString(),
                        Apellidos = apellidosParameter.Value.ToString(),
                        Email = objDatos.Email, // Suponiendo que el email se asigna igual al documento
                        Password = objDatos.Password,
                        Rol = rolParameter.Value.ToString()
                    };
                }
                else if (mensaje == "Su cuenta aún está en proceso de activación.")
                {
                    // Lanzar una excepción si la cuenta no está activada
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