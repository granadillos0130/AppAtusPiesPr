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
        public DataTable MtdListarPedidosEstado(int idVendedor,string estado)
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection conn = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarPedidosPorEstado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor",idVendedor);
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
                using (SqlCommand cmd = new SqlCommand("spAceptarSolicitud",con))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
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
                using (SqlCommand cmd = new SqlCommand("SpInactivarVendedor",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idVendedor", idVendedor);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

    }
}