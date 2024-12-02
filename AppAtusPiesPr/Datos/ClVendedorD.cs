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
        // Método para listar los productos mas vendidos  según el vendedor que inicie sesión 
        public List<ClProductoEmpresaE> MtdObtenerProductosMasVendidosPorVendedor(int idVendedor, DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClProductoEmpresaE> productos = new List<ClProductoEmpresaE>();
            ClConexion conexion = new ClConexion();

            try
            {
                using (SqlConnection conn = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spProductosMasVendidosPorVendedor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClProductoEmpresaE producto = new ClProductoEmpresaE
                                {
                                    idProdctoEmpresa = reader["IdProducto"] != DBNull.Value ? Convert.ToInt32(reader["IdProducto"]) : 0,
                                    nombreProducto = reader["NombreProducto"]?.ToString() ?? string.Empty,
                                    imagen = reader["imagen"]?.ToString() ?? string.Empty,
                                    CantidadVendida = reader["CantidadVendida"] != DBNull.Value ? Convert.ToInt32(reader["CantidadVendida"]) : 0,
                                    totalVentas = reader["TotalVentas"] != DBNull.Value ? Convert.ToDecimal(reader["TotalVentas"]) : 0,
                                    marca = reader["Marca"]?.ToString() ?? string.Empty
                                };

                                if (producto.CantidadVendida > 0)
                                {
                                    productos.Add(producto);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos más vendidos: " + ex.Message);
            }

            return productos;
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
                    cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
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
                                numProducto = Convert.ToInt32(reader["TotalProductos"])
                            };
                        }
                    }
                    reader.Close();
                }
            }
            return oUsuario;
        }

        public List<ClProductoE> MtdObtenerProductosPorVendedorYCategoria(int? idVendedor = null, int? idCategoria = null)
        {
            List<ClProductoE> productos = new List<ClProductoE>();
            ClConexion conexion = new ClConexion();

            try
            {
                using (SqlConnection conn = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SpListarProductosPorVendedorYCategoria", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor", idVendedor.HasValue ? (object)idVendedor.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria.HasValue ? (object)idCategoria.Value : DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClProductoE producto = new ClProductoE
                                {
                                    idProducto = reader["IdProducto"] != DBNull.Value ? Convert.ToInt32(reader["IdProducto"]) : 0,
                                    Nombre = reader["NombreProducto"]?.ToString() ?? string.Empty,
                                    CantidadStock = reader["CantidadStock"] != DBNull.Value ? Convert.ToInt32(reader["CantidadStock"]) : 0,
                                    Precio = reader["Precio"] != DBNull.Value ? Convert.ToInt32(reader["Precio"]) : 0, // Para int
                                    Descripcion = reader["DescripcionProducto"]?.ToString() ?? string.Empty,
                                    Categoria = reader["Categoria"]?.ToString() ?? "Sin Categoría",
                                    Marca = reader["Marca"]?.ToString() ?? "Sin Marca",
                                    NombreVendedor = reader["NombreVendedor"]?.ToString() ?? "Sin Vendedor"
                                };
                                productos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos por vendedor y categoría: " + ex.Message);
            }

            return productos;
        }



    }
}