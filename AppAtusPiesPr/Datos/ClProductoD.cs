using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClProductoD
    {
        public ClProductoEmpresaE MtdRegistrarProducto(ClProductoEmpresaE objdata)
        {
            ClConexion objConexion = new ClConexion();
            SqlCommand cmd = new SqlCommand("spInsertarProductoEmpresa", objConexion.MtdAbrirConexion());

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombre", objdata.nombreProducto);
            cmd.Parameters.AddWithValue("@cantidadStock", Convert.ToInt32(objdata.cantidadStock));
            cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(objdata.precioVenta));
            cmd.Parameters.AddWithValue("@descripcionProducto",  objdata.descripcionProducto);
            cmd.Parameters.AddWithValue("@referencia", objdata.referencia);
            cmd.Parameters.AddWithValue("@imagen", objdata.imagen ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@descuento", Convert.ToInt32(objdata.descuento));
            cmd.Parameters.AddWithValue("@descripcionCategoria", objdata.descripcionCategoria);
            cmd.Parameters.AddWithValue("@nombreMarca", objdata.nombreMarca);
            

            cmd.ExecuteNonQuery();
            objConexion.MtdCerrarConexion();
            return objdata;
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
                                descuento = Convert.ToInt32(reader["descuento"]),
                                nombres = reader["nombres"].ToString(),
                                nombreMarca = reader["nombreMarca"].ToString(),
                                // Inicializamos la lista de tallas
                                TallasDisponibles = new List<ClTallaE>()
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
    }
}