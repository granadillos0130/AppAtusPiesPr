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
            SqlCommand cmd = new SqlCommand("insertarProducto",objConexion.MtdAbrirConexion());

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codigo",objdata.Codigo);
            cmd.Parameters.AddWithValue("@nombre",objdata.Nombre);
            cmd.Parameters.AddWithValue("@cantidadStock",Convert.ToInt32(objdata.CantidadStock));
            cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(objdata.Precio));
            cmd.Parameters.AddWithValue("@presentacion", objdata.Presentacion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@talla",objdata.Talla);
            cmd.Parameters.AddWithValue("@idvendedor",Convert.ToInt32(objdata.idVendedor));

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

    }
}