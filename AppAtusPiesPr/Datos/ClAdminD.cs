using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClAdminD
    {
        public DataTable MtdListarPedidosEstado(int idVendedor, string estado)
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection conn = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarPedidosPorEstado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
                        cmd.Parameters.AddWithValue("@estado", estado);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable Lista = new DataTable();
                        da.Fill(Lista);

                        return Lista;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar los pedidos: " + ex.Message);
            }
        }


        public DataTable MtdListarVendedores()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection conn = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SpListarVendedores", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable Lista = new DataTable();
                        da.Fill(Lista);

                        return Lista;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar los vendedores: " + ex.Message);
            }
        }

        public DataTable MtdListarUsuarios()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection con = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SpListarUsuarios", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable Lista = new DataTable();
                        da.Fill(Lista);
                        return Lista;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errro al listar los Usuarios" + ex.Message);
            }
        }

        public DataTable MtdListarSolicitudes()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection con = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spListarSolicitudes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable Lista = new DataTable();
                        da.Fill(Lista);
                        return Lista;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errro al listar las Solicitudes" + ex.Message);
            }
        }

        public bool MtdAceptarSolicitud(int idVendedor)
        {
            ClConexion oConexion = new ClConexion();
            using (SqlConnection con = oConexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("spAceptarSolicitud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idVendedor", idVendedor);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool MtdInactivarVendedor(int idVendedor)
        {
            ClConexion oConexion = new ClConexion();
            using (SqlConnection con = oConexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SpInactivarVendedor", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idVendedor", idVendedor);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public List<ClUsuarioE> MtdBuscarVendedor(string documento, string estado)
        {
            List<ClUsuarioE> usuario = new List<ClUsuarioE>();

            ClConexion conexion = new ClConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SpBuscarVendedor", conexion.MtdAbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros, manejando nulos o vacíos para permitir filtros opcionales
                    cmd.Parameters.AddWithValue("@documento", string.IsNullOrWhiteSpace(documento) ? DBNull.Value : (object)documento);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrWhiteSpace(estado) ? DBNull.Value : (object)estado);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClUsuarioE oU = new ClUsuarioE()
                            {
                                IdUsuario = reader.GetInt32(reader.GetOrdinal("idVendedor")),
                                Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                Documento = reader.GetString(reader.GetOrdinal("documento")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                                estado = reader.GetString(reader.GetOrdinal("estado"))
                            };
                            usuario.Add(oU);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conexion.MtdCerrarConexion();

            }
            return usuario;
        }

        public bool MtdAgregarCategoria(ClCategoriaE oDatos)
        {
            bool validacion;

            ClConexion oConexion = new ClConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SpAgregarCategoria", oConexion.MtdAbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", oDatos.descripcion);
                    cmd.ExecuteNonQuery();

                    validacion = true;

                }
            }
            catch (Exception e)
            {

                validacion = false;

            }

            return validacion;
        }

        public List<ClUsuarioE> MtdObtenerVendedorEmail()
        {
            List<ClUsuarioE> listaVendedor = new List<ClUsuarioE>();

            ClConexion oConexion = new ClConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SpVendedoresEmail", oConexion.MtdAbrirConexion()))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClUsuarioE oU = new ClUsuarioE()
                            {
                                Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                            };
                            listaVendedor.Add(oU);
                        }
                    }

                }
            }
            catch (Exception e)
            {



            }

            return listaVendedor;
        }

    }
}