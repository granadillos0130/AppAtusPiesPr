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
            cmd.Parameters.AddWithValue("@presentacion",objdata.Presentacion.ToString());
            cmd.Parameters.AddWithValue("@talla",objdata.Talla);
            cmd.Parameters.AddWithValue("@idvendedor",Convert.ToInt32(objdata.idVendedor));

            cmd.ExecuteNonQuery();
            objConexion.MtdCerrarConexion();
            return objdata;
        }
    }
}