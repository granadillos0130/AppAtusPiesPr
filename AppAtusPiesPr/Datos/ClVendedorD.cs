using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Datos
{
    public class ClVendedorD
    {
        public int MtdRegistrarVendedor(ClUsuarioE vendedor, out string mensaje)
        {
            int idVendedor = 0;
            mensaje = string.Empty;  // Inicializar el mensaje vacío

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

                        // Parámetro de salida para capturar el mensaje
                        SqlParameter mensajeParam = new SqlParameter("@mensaje", SqlDbType.VarChar, 200)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(mensajeParam);

                        // Ejecutar el procedimiento y obtener el ID del vendedor
                        idVendedor = Convert.ToInt32(cmd.ExecuteScalar());

                        // Capturar el mensaje de salida
                        mensaje = mensajeParam.Value.ToString();
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
            using (SqlConnection conexion = oConex.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("spPerfilVendedor", conexion))
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
                                Nombres = reader["NombreVendedor"].ToString(),
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


        // metodos para proveedores

        public ClProveedorE ObtenerProveedorPorId(int idProveedor)
        {
            ClProveedorE proveedor = new ClProveedorE();
            ClConexion conexion = new ClConexion();

            try
            {
                using (SqlConnection con = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spObtenerProveedorPorId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            proveedor.idProveedor = Convert.ToInt32(reader["idProveedor"]);
                            proveedor.Documento = reader["documento"].ToString();
                            proveedor.Nombres = reader["nombre"].ToString();
                            proveedor.Email = reader["email"].ToString();
                            proveedor.Telefono = reader["telefono"].ToString();
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener proveedor: " + ex.Message);
            }

            return proveedor;
        }

        public int RegistrarProveedor(ClProveedorE proveedor, int idVendedor, out string mensaje)
        {
            int idProveedor = 0;
            mensaje = string.Empty;

            try
            {
                ClConexion conexion = new ClConexion();

                using (SqlConnection con = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spRegistrarProveedorConVendedor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nombre", proveedor.Nombres);
                        cmd.Parameters.AddWithValue("@documento", proveedor.Documento);
                        cmd.Parameters.AddWithValue("@email", proveedor.Email);
                        cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                        cmd.Parameters.AddWithValue("@estado", proveedor.estado);
                        cmd.Parameters.AddWithValue("@idVendedor", idVendedor); 

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out idProveedor))
                        {
                            mensaje = "Proveedor registrado exitosamente.";
                        }
                        else
                        {
                            mensaje = "Proveedor registrado exitosamente.";
                        }
                       
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                mensaje = $"Error SQL: {sqlEx.Message}";
                throw;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al registrar proveedor: {ex.Message}";
                throw;
            }

            return idProveedor;
        }

        public List<ClProveedorE> ListarProveedoresPorVendedor(int idVendedor, string estado)
        {
            List<ClProveedorE> proveedores = new List<ClProveedorE>();
            ClConexion conexion = new ClConexion();

            try
            {
                using (SqlConnection con = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spListarProveedoresPorVendedor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
                        cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(estado) ? DBNull.Value : (object)estado);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClProveedorE proveedor = new ClProveedorE
                                {
                                    idProveedor = Convert.ToInt32(reader["idProveedor"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Documento = reader["Documento"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    estado = reader["Estado"].ToString()
                                };
                                proveedores.Add(proveedor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los proveedores: " + ex.Message);
            }

            return proveedores;
        }



        public bool ActualizarProveedor(ClProveedorE proveedor, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                ClConexion conexion = new ClConexion();

                using (SqlConnection con = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spActualizarProveedor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idProveedor", proveedor.idProveedor);
                        cmd.Parameters.AddWithValue("@nombre", proveedor.Nombres);
                        cmd.Parameters.AddWithValue("@documento", proveedor.Documento);
                        cmd.Parameters.AddWithValue("@email", proveedor.Email);
                        cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                        cmd.Parameters.AddWithValue("@estado", proveedor.estado);

                       
                        int rowsAffected = cmd.ExecuteNonQuery();


                        if (rowsAffected > 0)
                        {
                            mensaje = "Proveedor actualizado correctamente.";
                            return true;
                        }
                        else
                        {
                            
                            using (SqlCommand cmdValidar = new SqlCommand("SELECT COUNT(1) FROM Proveedor WHERE idProveedor = @idProveedor", con))
                            {
                                cmdValidar.Parameters.AddWithValue("@idProveedor", proveedor.idProveedor);
                                int existe = Convert.ToInt32(cmdValidar.ExecuteScalar());

                                if (existe > 0)
                                {
                                    mensaje = "No hubo cambios en los datos del proveedor.";
                                }
                                else
                                {
                                    mensaje = "El proveedor no existe.";
                                }
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar el proveedor: " + ex.Message;
                return false;
            }
        }



        public bool EliminarProveedor(int idProveedor, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                ClConexion conexion = new ClConexion();

                using (SqlConnection con = conexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spEliminarProveedor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);

                       
                        SqlParameter returnValue = new SqlParameter();
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnValue);

                        
                        cmd.ExecuteNonQuery();

                        
                        int result = (int)returnValue.Value;

                        if (result == 1)
                        {
                            mensaje = "Proveedor eliminado correctamente.";
                            return true;
                        }
                        else if (result == 0)
                        {
                            mensaje = "No se encontró el proveedor para eliminar.";
                            return false;
                        }
                        else
                        {
                            mensaje = "Error desconocido al eliminar el proveedor.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al eliminar el proveedor: {ex.Message}";
                return false;
            }
        }
        public List<ClProveedorE> ListarProveedorVendedor(int idVendedor)
        {
            List<ClProveedorE> listaProveedores = new List<ClProveedorE>();
            ClConexion oConexion = new ClConexion();

            using (SqlConnection con = oConexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SpProveedorVendedor", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idVendedor", idVendedor);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ClProveedorE proveedor = new ClProveedorE
                            {
                                idProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                Nombre = dr["Nombre"].ToString()
                            };
                            listaProveedores.Add(proveedor);
                        }
                    }
                }
            }
            return listaProveedores;
        }
        public bool RegistrarCompra(string numeroFactura, decimal totalCompra, int idProveedor, int idVendedor, DateTime fechaCompra)
        {
            ClConexion oConexion = new ClConexion();
            using (SqlConnection con = oConexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SpGuardarCompra", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@numeroFactura", numeroFactura);
                    cmd.Parameters.AddWithValue("@totalCompra", totalCompra);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
                    cmd.Parameters.AddWithValue("@fechaCompra", fechaCompra);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}