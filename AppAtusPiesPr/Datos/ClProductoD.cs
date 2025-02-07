using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClProductoD
    {
        public ClProductoEmpresaE MtdRegistrarProducto(ClProductoEmpresaE objdata)
        {


            try
            {
                int? idVendedor = HttpContext.Current.Session["idUsuario"] as int?;

                // Verificar si el idVendedor existe en la sesión
                if (!idVendedor.HasValue)
                {
                    // Si no existe el idVendedor, manejar el error o lanzar una excepción
                    throw new InvalidOperationException("El vendedor no está autenticado.");
                }

                ClConexion objConexion = new ClConexion();
                SqlCommand cmd = new SqlCommand("spInsertarProductoEmpresa", objConexion.MtdAbrirConexion());

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idVendedor", Convert.ToInt32(objdata.idVendedor));
                cmd.Parameters.AddWithValue("@nombre", objdata.nombreProducto);
                cmd.Parameters.AddWithValue("@cantidadStock", Convert.ToInt32(objdata.cantidadStock));
                cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(objdata.precioVenta));
                cmd.Parameters.AddWithValue("@descripcionProducto", objdata.descripcionProducto);
                cmd.Parameters.AddWithValue("@referencia", objdata.referencia);
                cmd.Parameters.AddWithValue("@imagen", objdata.imagen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@descuento", (object)objdata.descuento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@descripcionCategoria", objdata.descripcionCategoria);
                cmd.Parameters.AddWithValue("@nombreMarca", objdata.nombreMarca);


                cmd.ExecuteNonQuery();
                objConexion.MtdCerrarConexion();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return objdata;
        }


        public ClProductoEmpresaE MtdEliminarProducto(int id)
        {
            try
            {
                ClConexion objCone = new ClConexion();
                SqlCommand cmd = new SqlCommand("Sp_EliminarProductos", objCone.MtdAbrirConexion());

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProductoEmpresa", id);
                cmd.ExecuteNonQuery();
                objCone.MtdCerrarConexion();

                ClProductoEmpresaE objPro = new ClProductoEmpresaE();

                return objPro;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable buscarProductos(string busqueda)
        {
            try
            {
                ClConexion oCnx = new ClConexion();

                using (SqlConnection connection = oCnx.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spBarraBusquedaProductos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Busqueda", busqueda);

                        SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adaptador.Fill(dt);


                        cmd.ExecuteNonQuery();
                        return dt;

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar el producto: {ex.Message}");
                return null;
            }
        }
        public ClProductoEmpresaE mtdActualizarProducto(ClProductoEmpresaE objData)
        {
            try
            {
                ClConexion oConex = new ClConexion();
                SqlConnection connection = oConex.MtdAbrirConexion();


                using (SqlCommand cmd = new SqlCommand("SpActualizarProductoEmpresa", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro obligatorio
                    cmd.Parameters.AddWithValue("@idProductoEmpresa", objData.idProducto);

                    cmd.Parameters.AddWithValue("@nombre", (object)objData.nombreProducto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cantidadStock", (object)objData.cantidadStock ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio", (object)objData.precioVenta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@descripcionProducto", (object)objData.descripcionProducto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@referencia", (object)objData.referencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@imagen", (object)objData.imagen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@descuento", (object)objData.descuento ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", (object)objData.Estado ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@descripcionCategoria", (object)objData.descripcionCategoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@nombreMarca", (object)objData.nombreMarca ?? DBNull.Value);


                    cmd.ExecuteNonQuery();
                }


                oConex.MtdCerrarConexion();

                return objData; // Retornar el objeto actualizado
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el producto: {ex.Message}");
                return null;
            }

        }

        public DataTable MtdListarProductos()
        {
            ClConexion conexion = new ClConexion();
            SqlCommand cmd = new SqlCommand("Sp_ListarProductos", conexion.MtdAbrirConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conexion.MtdCerrarConexion();

            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable tblDatos = new DataTable();
            adaptador.Fill(tblDatos);

            return tblDatos;
        }

        public ClProductoEmpresaE MtdInfoProducto(int idProdctoEmpresa)
        {
            ClProductoEmpresaE prodInfo = null;
            ClConexion oConexion = new ClConexion();

            using (SqlConnection connection = oConexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InfoProducto", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProductoEmpresa", idProdctoEmpresa);

                    SqlDataReader reader = cmd.ExecuteReader();
                    var cont = reader.HasRows;

                    // Leer información del producto
                    while (reader.Read())
                    {
                        if (prodInfo == null)
                        {
                            prodInfo = new ClProductoEmpresaE
                            {
                                idProdctoEmpresa = Convert.ToInt32(reader["idProdctoEmpresa"]),
                                nombreProducto = reader["nombreProducto"].ToString(),
                                cantidadStock = Convert.ToInt32(reader["cantidadStock"]),
                                precioVenta = Convert.ToInt32(reader["precio"]),
                                descripcionProducto = reader["descripcionProducto"].ToString(),
                                referencia = reader["referencia"].ToString(),
                                imagen = reader["imagen"].ToString(),
                                descuento = reader["descuento"] != DBNull.Value ? Convert.ToInt32(reader["descuento"]) : (int?)null,
                                nombres = reader["nombres"].ToString(),
                                apellidoVendedor = reader["apellidos"].ToString(),
                                nombreMarca = reader["nombreMarca"].ToString(),
                                // Inicializamos la lista de tallas
                                TallasDisponibles = new List<ClTallaE>(),
                                idVendedor = Convert.ToInt32(reader["idVendedor"]),
                            };
                        }
                    }

                    // Cierra el primer lector para empezar uno nuevo
                    reader.Close();

                    // Leer tallas disponibles asociadas al producto
                    using (SqlCommand cmdTallas = new SqlCommand("spObtenerTallasDisponibles", connection))
                    {
                        cmdTallas.CommandType = CommandType.StoredProcedure;
                        cmdTallas.Parameters.AddWithValue("@ProductoID", idProdctoEmpresa);

                        using (SqlDataReader tallaReader = cmdTallas.ExecuteReader())
                        {
                            while (tallaReader.Read())
                            {
                                prodInfo.TallasDisponibles.Add(new ClTallaE
                                {
                                    idTalla = Convert.ToInt32(tallaReader["idTalla"]),
                                    descripcionTalla = tallaReader["descripcionTalla"].ToString(),
                                });
                            }
                        }
                    }
                }
            }

            return prodInfo;
        }



        public List<ClCategoriaE> MtdListarCategorias()
        {
            List<ClCategoriaE> oCategoria = new List<ClCategoriaE>();
            ClConexion conexion = new ClConexion();
            SqlCommand cmd = new SqlCommand("spListarCategorias", conexion.MtdAbrirConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                oCategoria.Add(new ClCategoriaE
                {
                    idCategoria = Convert.ToInt32(reader["idCategoria"]),
                    descripcion = reader["descripcion"].ToString()
                });
            }
            conexion.MtdCerrarConexion();


            return oCategoria;
        }

        //Metodo para listar productos

        public List<ClProductoEmpresaE> MtdListarProducto(int? idVendedor = null)
        {
            List<ClProductoEmpresaE> oProducto = new List<ClProductoEmpresaE>();
            try
            {
                ClConexion conex = new ClConexion();
                SqlCommand cmd = new SqlCommand("SpListarTodosLosProductos", conex.MtdAbrirConexion());

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idVendedor", (object)idVendedor ?? DBNull.Value);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    oProducto.Add(new ClProductoEmpresaE
                    {

                        nombreProducto = reader["nombreProducto"].ToString()
                    });
                }

                conex.MtdCerrarConexion();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
            return oProducto;
        }


        // método para listar los productos más vendidos
        public List<ClProductoE> MtdListaProductosMasVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClProductoE> productos = new List<ClProductoE>();

            ClConexion conexion = new ClConexion();
            SqlCommand sqlCommand = new SqlCommand("spProductosMasVendidos", conexion.MtdAbrirConexion());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            sqlCommand.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow fila in dataTable.Rows)
            {
                productos.Add(new ClProductoE
                {
                    idProducto = Convert.ToInt32(fila["IdProducto"]),
                    Nombre = fila["NombreProducto"].ToString(),
                    Descripcion = fila["DescripcionProducto"].ToString(),
                    CantidadVendida = Convert.ToInt32(fila["CantidadVendida"]),
                    TotalVentas = Convert.ToDecimal(fila["TotalVentas"]),
                    Marca = fila["Marca"].ToString()
                });
            }
            conexion.MtdCerrarConexion();
            return productos;
        }

        //Metodo para obtener los productos del vendedor

        public List<ClProductoEmpresaE> MtdObtenerProductosPorVendedorYProducto(int? idVendedor = null, int? idProducto = null)
        {
            List<ClProductoEmpresaE> productos = new List<ClProductoEmpresaE>();
            ClConexion conexx = new ClConexion();

            try
            {
                using (SqlConnection cone = conexx.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SpListarProductosPorVendedorYProducto", cone))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idProducto", idProducto.HasValue ? (object)idProducto.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@idVendedor", idVendedor.HasValue ? (object)idVendedor.Value : DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClProductoEmpresaE product = new ClProductoEmpresaE
                                {
                                    idProducto = reader["IdProducto"] != DBNull.Value ? Convert.ToInt32(reader["IdProducto"]) : 0,
                                    nombreProducto = reader["NombreProducto"]?.ToString() ?? string.Empty,
                                    cantidadStock = reader["CantidadStock"] != DBNull.Value ? Convert.ToInt32(reader["CantidadStock"]) : 0,
                                    precioVenta = reader["Precio"] != DBNull.Value ? Convert.ToInt32(reader["Precio"]) : 0,
                                    descripcionProducto = reader["DescripcionProducto"]?.ToString() ?? string.Empty,
                                    descripcionCategoria = reader["Categoria"]?.ToString() ?? "Sin Categoría",
                                    NombreVendedor = reader["NombreVendedor"]?.ToString() ?? "Sin Vendedor",
                                    nombreMarca = reader["Marca"]?.ToString() ?? "Sin Marca"
                                };

                                productos.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos por vendedor: " + ex.Message);
            }

            return productos;
        }


        // Método para listar productos por categoría
        public DataTable MtdListarProductosPorCategoria(int idCategoria)
        {
            ClConexion conexion = new ClConexion();
            DataTable dtProductos = new DataTable();

            using (SqlConnection connection = conexion.MtdAbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("spListarProductosConCategorias", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtProductos);
            }
            conexion.MtdCerrarConexion();
            return dtProductos;
        }

        // Método para listar productos por categoría
        public DataTable MtdListarProductosVendedor(int idVendedor)
        {
            ClConexion conexion = new ClConexion();
            DataTable dtProductos = new DataTable();

            using (SqlConnection connection = conexion.MtdAbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("spListarProductosVendedor", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idVendedor", idVendedor);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtProductos);
            }

            conexion.MtdCerrarConexion();
            return dtProductos;
        }

        // Método para listar productos por categoría
        public List<ClProductoE> MtdListarProductosPorCategoria(string categoria = null)
        {
            ClConexion conexion = new ClConexion();
            List<ClProductoE> productos = new List<ClProductoE>();

            using (SqlCommand cmd = new SqlCommand("SpListarProductosCategoria", conexion.MtdAbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoria", string.IsNullOrEmpty(categoria) ? (object)DBNull.Value : categoria);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    ClProductoE producto = new ClProductoE
                    {
                        Codigo = row["Codigo"].ToString(),
                        Nombre = row["NombreProducto"].ToString(),
                        CantidadStock = Convert.ToInt32(row["CantidadDisponible"]),
                        Precio = Convert.ToInt32(row["Precio"]),
                        Presentacion = row["Presentacion"].ToString(),
                        Estado = row["Estado"].ToString(),
                        Descripcion = row["Categoria"].ToString()
                    };

                    productos.Add(producto);
                }
            }
            conexion.MtdCerrarConexion();
            return productos;
        }

        public ClComentarioE mtdGuardarComentario(ClComentarioE objData)
        {
            try
            {

                ClConexion conex = new ClConexion();
                SqlConnection oConex = conex.MtdAbrirConexion();

                using (SqlCommand cmd = new SqlCommand("spRegistrarComentario", oConex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idProducto", Convert.ToInt32(objData.idProducto));
                    cmd.Parameters.AddWithValue("@idCliente", Convert.ToInt32(objData.idCliente));
                    cmd.Parameters.AddWithValue("@comentario", objData.comentario);
                    cmd.Parameters.AddWithValue("@fechaComentario", objData.FechaComentario);
                    cmd.Parameters.AddWithValue("@valoracion", Convert.ToInt32(objData.valoracion));

                    cmd.ExecuteNonQuery();
                }
                conex.MtdCerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar el comentario: {ex.Message}");
            }
            return objData;

        }

        public List<ClComentarioE> mtdListarComentario(int idProducto)
        {
            ClConexion oConex = new ClConexion();
            List<ClComentarioE> oComentario = new List<ClComentarioE>();

            try
            {
                using (SqlCommand cmd = new SqlCommand("spListarComentariosProductos", oConex.MtdAbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow fila in dataTable.Rows)
                    {
                        oComentario.Add(new ClComentarioE
                        {
                            FechaComentario = Convert.ToDateTime(fila["fechaComentario"]),
                            nombres = fila["nombres"].ToString(),
                            apellidos = fila["apellidos"].ToString(),
                            comentario = fila["comentario"].ToString(), // Se corrigió este campo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                oConex.MtdCerrarConexion(); // Asegurar el cierre de conexión en el finally
            }

            return oComentario;
        }

        public async Task<(decimal Promedio, int Total)> ObtenerValoracionPromedio(int productoId)
        {
            ClConexion oConex = new ClConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerValoracionPromedio", oConex.MtdAbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", productoId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return (
                                Promedio: reader.GetDecimal(2),
                                Total: reader.GetInt32(3)
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                oConex.MtdCerrarConexion(); // Asegurar el cierre de conexión en el finally
            }

            return (0, 0);
        }
    }

}
