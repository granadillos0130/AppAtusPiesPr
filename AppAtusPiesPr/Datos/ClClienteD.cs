using AppAtusPiesPr.Entidades;
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
            try
            {
                using (SqlConnection con = conexion.MtdAbrirConexion())
                using (SqlCommand cmd = new SqlCommand("spRegistrarCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                    cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@password", cliente.Password);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

                    idCliente = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar cliente: " + ex.Message);
            }
            return idCliente;
        }

        public string MtdValidarLogin(string documento, string password)
        {
            string rol = string.Empty;
            try
            {
                using (SqlConnection con = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ValidarLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@documento", documento);
                        cmd.Parameters.AddWithValue("@password", password);
                        SqlParameter rolParameter = new SqlParameter("@rol", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(rolParameter);

                        cmd.ExecuteNonQuery();
                        rol = rolParameter.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar login: " + ex.Message);
            }
            return rol;
        }
    }
}
