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
        public ClProductoE MtdRegistrarProducto(ClProductoE objdata)
        {
            ClConexion objConexion = new ClConexion();
            SqlCommand cmd = new SqlCommand("insertarProducto", objConexion.MtdAbrirConexion());

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codigo", objdata.Codigo);
            cmd.Parameters.AddWithValue("@nombre", objdata.Nombre);
            cmd.Parameters.AddWithValue("@cantidadStock", Convert.ToInt32(objdata.CantidadStock));
            cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(objdata.Precio));
            cmd.Parameters.AddWithValue("@presentacion", objdata.Presentacion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@talla", objdata.Talla);
            cmd.Parameters.AddWithValue("@idvendedor", Convert.ToInt32(objdata.idVendedor));

            cmd.ExecuteNonQuery();
            objConexion.MtdCerrarConexion();
            return objdata;
        }



        public ClProductoE mtdActualizarProducto(ClProductoE objData)
        {
            try
            {
                ClConexion oConex = new ClConexion();
                SqlConnection connection = oConex.MtdAbrirConexion();


                using (SqlCommand cmd = new SqlCommand("ActualizarProducto", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro obligatorio
                    cmd.Parameters.AddWithValue("@idProducto", objData.idProducto);

                    // Parámetros opcionales que pueden ser nulos
                    cmd.Parameters.AddWithValue("@codigo", (object)objData.Codigo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@nombre", (object)objData.Nombre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio", (object)objData.Precio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cantidadStock", (object)objData.CantidadStock ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@talla", (object)objData.Talla ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", (object)objData.Estado ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@presentacion", (object)objData.Presentacion ?? DBNull.Value);

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
            SqlCommand cmd = new SqlCommand("Sp_ListarProductos", conexion.MtdAbrirConec());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conexion.MtdCerrarConec();

            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable tblDatos = new DataTable();
            adaptador.Fill(tblDatos);

            return tblDatos;
        }

        public ClProductoEmpresaE MtdInfoProducto(int idProdctoEmpresa)
        {
            ClProductoEmpresaE prodInfo = null;
            ClConexion oConexion = new ClConexion();

            using (SqlConnection connection = oConexion.MtdAbrirConec())
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
                                precioVenta = Convert.ToInt32(reader["precioVenta"]),
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
            SqlCommand cmd = new SqlCommand("spListarCategorias", conexion.MtdAbrirConec());
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
            conexion.MtdCerrarConec();


            return oCategoria;


        }
    }
}
