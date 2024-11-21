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


    }
}