using System;
using System.Data;
using System.Data.SqlClient;
using AppAtusPiesPr.Entidades;

namespace AppAtusPiesPr.Datos
{
    public class ClUsuarioD
    {
        private ClConexion conexion = new ClConexion();

        public ClUsuarioE ObtenerAdminPorId(int idAdmin)
        {
            ClUsuarioE usuario = null;
            SqlConnection con = null;
            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand comando = new SqlCommand("SpObtenerAdminPorId", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comando.Parameters.AddWithValue("@idAdmin", idAdmin);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new ClUsuarioE
                    {
                        IdUsuario = (int)reader["IdUsuario"],
                        Documento = (string)reader["Documento"],
                        Nombres = (string)reader["Nombres"],
                        Apellidos = (string)reader["Apellidos"],
                        Email = (string)reader["Email"]
                    };
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return usuario;
        }

        public ClUsuarioE ObtenerVendedorPorId(int idVendedor)
        {
            ClUsuarioE usuario = null;
            SqlConnection con = null;
            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand comando = new SqlCommand("SpObtenerVendedorPorId", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comando.Parameters.AddWithValue("@idVendedor", idVendedor);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new ClUsuarioE
                    {
                        IdUsuario = (int)reader["IdUsuario"],
                        Documento = (string)reader["Documento"],
                        Nombres = (string)reader["Nombres"],
                        Apellidos = (string)reader["Apellidos"],
                        Email = (string)reader["Email"],
                        Telefono = reader["Telefono"] != DBNull.Value ? (string)reader["Telefono"] : null,
                        Direccion = reader["Direccion"] != DBNull.Value ? (string)reader["Direccion"] : null,
                        Descripcion = reader["Descripcion"] != DBNull.Value ? (string)reader["Descripcion"] : null,
                        foto = reader["foto"] != DBNull.Value ? (string)reader["foto"] : null
                    };
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return usuario;
        }


        public bool ActualizarAdmin(ClUsuarioE usuario)
        {
            SqlConnection con = null;
            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand comando = new SqlCommand("SpActualizarAdmin", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comando.Parameters.AddWithValue("@idAdmin", usuario.IdUsuario);
                comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                comando.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                comando.Parameters.AddWithValue("@Email", usuario.Email);
                comando.Parameters.AddWithValue("@Password", usuario.Password);

                int rowsAffected = comando.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }
        }

        public bool ActualizarVendedor(ClUsuarioE usuario)
        {
            SqlConnection con = null;
            try
            {
                con = conexion.MtdAbrirConexion();
                SqlCommand comando = new SqlCommand("SpActualizarVendedor", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comando.Parameters.AddWithValue("@idVendedor", usuario.IdUsuario);
                comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                comando.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                comando.Parameters.AddWithValue("@Email", usuario.Email);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                comando.Parameters.AddWithValue("@Descripcion", usuario.Descripcion);
                comando.Parameters.AddWithValue("@Password", usuario.Password);
                comando.Parameters.AddWithValue("@foto", usuario.foto);

                int rowsAffected = comando.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }
        }

        public bool ValidarContrasena(int idUsuario, string contrasenaActual, bool esVendedor)
        {
            SqlConnection con = null;
            try
            {
                con = conexion.MtdAbrirConexion();
                string procedimiento = esVendedor ? "SpValidarContrasenaVendedor" : "SpValidarContrasenaAdmin";
                SqlCommand comando = new SqlCommand(procedimiento, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                comando.Parameters.AddWithValue("@Password", contrasenaActual);

                SqlDataReader reader = comando.ExecuteReader();
                return reader.Read(); // Devuelve true si se encuentra una coincidencia
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    conexion.MtdCerrarConexion();
                }
            }
        }

        public ClUsuarioE DatosVendedor(int idUsuario)
        {
            ClUsuarioE vendedor = null;
            ClConexion conexion = new ClConexion();

            using (SqlConnection con = conexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("spDatosV", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            vendedor = new ClUsuarioE
                            {
                                Email = reader["email"].ToString(),
                                Telefono = reader["telefono"].ToString() 
                            };
                        }
                    }
                }
            }

            return vendedor;
        }





    }
}
