using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClAdminD
    {
        public ClUsuarioE MtdRegistrarVendedor(ClUsuarioE oDatos)
        {
        ClConexion oConexion = new ClConexion();
        SqlCommand cmd = new SqlCommand("spRegistrarVendedores", oConexion.MtdAbrirConexion());
        cmd.Parameters.AddWithValue("@nombres", oDatos.Nombres);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Apellidos);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Documento);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Email);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Password);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Telefono);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Direccion);
            cmd.Parameters.AddWithValue("@nombres", oDatos.Descripcion);

            cmd.ExecuteNonQuery();
            oConexion.MtdCerrarConexion();

            return oDatos;
        }
    }
}