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

        public DataTable MtdListarVendedores()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection conn = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SpListarVendedores", conn))
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

        public DataTable MtdListarUsuarios()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection con = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SpListarUsuarios", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable Lista = new DataTable();
                        da.Fill(Lista);
                        return Lista;
                    }
                }
            }
            catch(Exception ex) 
            { throw new Exception("Errro al listar los Usuarios" + ex.Message); 
            }
        }

        public DataTable MtdListarSolicitudes()
        {
            try
            {
                ClConexion oConexion = new ClConexion();
                using (SqlConnection con = oConexion.MtdAbrirConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("spListarSolicitudes", con))
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
                throw new Exception("Errro al listar las Solicitudes" + ex.Message);
            }
        }

       

    }
}