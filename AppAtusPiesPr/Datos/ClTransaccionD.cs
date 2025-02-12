using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppAtusPiesPr.Datos
{
    public class ClTransaccionD
    {
        public string GuardarTransacciones(List<int> idPedidos, List<int> idsVendedores, List<decimal> montos)
        {
            try
            {
                ClConexion oConex = new ClConexion();
                using (SqlConnection con = oConex.MtdAbrirConexion()) // La conexión ya está abierta
                {
                    SqlTransaction transaccion = con.BeginTransaction();

                    try
                    {
                        for (int i = 0; i < idPedidos.Count; i++)
                        {
                            string query = @"
                    INSERT INTO Transaccion (idPedido, idMetodoPago, monto, fechaTransaccion, estado, idVendedor)
                    VALUES (@idPedido, 7, @monto, GETDATE(), 'Completada', @idVendedor)";

                            using (SqlCommand cmd = new SqlCommand(query, con, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@idPedido", idPedidos[i]);
                                cmd.Parameters.AddWithValue("@monto", montos[i]);
                                cmd.Parameters.AddWithValue("@idVendedor", idsVendedores[i]);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaccion.Commit();
                        return "OK";
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        return "Error en la base de datos: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error de conexión: " + ex.Message;
            }
        }


    }
}
