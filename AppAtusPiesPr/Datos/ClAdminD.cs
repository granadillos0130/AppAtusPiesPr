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
        public ClUsuarioE MtdRegistrarVendedor(ClUsuarioE oDatos)
        {
            // Validar que 'Nombres' no esté vacío
            if (string.IsNullOrEmpty(oDatos.Nombres))
            {
                throw new ArgumentException("El campo 'Nombres' no puede estar vacío.");
            }

            ClConexion oConexion = new ClConexion();
            using (SqlConnection conn = oConexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("spRegistrarVendedores", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Asignar todos los parámetros
                    cmd.Parameters.AddWithValue("@nombres", oDatos.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", oDatos.Apellidos);
                    cmd.Parameters.AddWithValue("@documento", oDatos.Documento);
                    cmd.Parameters.AddWithValue("@correo", oDatos.Email);
                    cmd.Parameters.AddWithValue("@password", oDatos.Password);
                    cmd.Parameters.AddWithValue("@telefono", oDatos.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", oDatos.Direccion);
                    cmd.Parameters.AddWithValue("@descripcion", oDatos.Descripcion);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en la base de datos: " + ex.Message);
                    }
                }
            }

            return oDatos;
        }

        public DataTable MtdListarVendedores()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection conn = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ListarVendedores", conn))
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

    }
}