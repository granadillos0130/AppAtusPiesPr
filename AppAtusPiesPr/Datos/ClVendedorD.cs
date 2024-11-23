using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClVendedorD
    {

        public int MtdRegistrarVendedor(ClUsuarioE vendedor)
        {
            int idVendedor = 0;
            try
            {
                ClConexion objConexion = new ClConexion();

                using (SqlConnection con = objConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spRegistrarVendedor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@documento", vendedor.Documento);
                        cmd.Parameters.AddWithValue("@nombres", vendedor.Nombres);
                        cmd.Parameters.AddWithValue("@apellidos", vendedor.Apellidos);
                        cmd.Parameters.AddWithValue("@email", vendedor.Email);
                        cmd.Parameters.AddWithValue("@password", vendedor.Password);
                        cmd.Parameters.AddWithValue("@telefono", vendedor.Telefono);
                        cmd.Parameters.AddWithValue("@direccion", vendedor.Direccion);

                        idVendedor = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar vendedor: " + ex.Message);
            }
            return idVendedor;
        }

        public ClUsuarioE mtdPerfilVendedor(int idVendedor)
        {
            ClUsuarioE oUsuario = null;
            ClConexion oConex = new ClConexion();
            using(SqlConnection conexion = oConex.MtdAbrirConexion())
            {
                using(SqlCommand cmd = new SqlCommand("spPerfilVendedor", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idVendedor", oUsuario.IdUsuario);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (oUsuario == null)
                        {
                            oUsuario = new ClUsuarioE
                            {
                                IdUsuario = Convert.ToInt32(reader["idVendedor"]),
                                Nombres = reader["nombres"].ToString(),
                                Apellidos = reader["apellidos"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                            };
                        }
                    }
                    reader.Close();
                }
            }
            return oUsuario;
        }

    }
}