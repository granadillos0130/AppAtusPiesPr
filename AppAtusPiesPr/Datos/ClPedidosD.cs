using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClPedidosD
    {

        public List<PedidoCliente> mtdListarPedidosCliente(int idCliente)
        {
            List<PedidoCliente> listaPedidos = new List<PedidoCliente>();

            ClConexion conexion = new ClConexion();
                using (SqlCommand cmd = new SqlCommand("spPedidosCliente", conexion.MtdAbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) // Verifica si hay filas devueltas
                        {
                            while (reader.Read())
                            {
                                PedidoCliente pedido = new PedidoCliente
                                {
                                    imagen = reader.IsDBNull(reader.GetOrdinal("imagen")) ? string.Empty : reader["imagen"].ToString(),
                                    Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? string.Empty : reader["estado"].ToString(),
                                    NombreProducto = reader.IsDBNull(reader.GetOrdinal("nombreProducto")) ? string.Empty : reader["nombreProducto"].ToString(),
                                    precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0 : Convert.ToDecimal(reader["precio"]),
                                    nombres = reader.IsDBNull(reader.GetOrdinal("nombres")) ? string.Empty : reader["nombres"].ToString(),
                                    apellidos = reader.IsDBNull(reader.GetOrdinal("apellidos")) ? string.Empty : reader["apellidos"].ToString(),
                                    idVendedor = Convert.ToInt32(reader["idVendedor"])
                                };

                                listaPedidos.Add(pedido);
                            }
                        }
                        else
                        {
                            // No hay datos, puedes registrar un mensaje o manejarlo según sea necesario
                            Console.WriteLine("No se encontraron datos para el idCliente proporcionado.");
                        }
                }
                }

            return listaPedidos;
        }
        public int InsertarPedido(ClPedidosE pedido, List<DetallePedido> detalles)
        {
            ClConexion oConex = new ClConexion();
            using (SqlConnection con = oConex.MtdAbrirConexion()) // Ya devuelve una conexión abierta
            {
                SqlTransaction transaccion = con.BeginTransaction();

                try
                {
                    // Insertar el pedido
                    string queryPedido = @"
            INSERT INTO Pedido (estado, fechaPedido, idCliente, idVendedor, totalPedido)
            VALUES (@estado, @fechaPedido, @idCliente, @idVendedor, @totalPedido);
            SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPedido = new SqlCommand(queryPedido, con, transaccion);
                    cmdPedido.Parameters.AddWithValue("@estado", pedido.Estado);
                    cmdPedido.Parameters.AddWithValue("@fechaPedido", pedido.FechaPedido);
                    cmdPedido.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
                    cmdPedido.Parameters.AddWithValue("@idVendedor", pedido.IdVendedor);
                    cmdPedido.Parameters.AddWithValue("@totalPedido", pedido.TotalPedido);

                    int idPedido = Convert.ToInt32(cmdPedido.ExecuteScalar());

                    // Insertar detalles del pedido
                    string queryDetalle = @"
            INSERT INTO DetallesPedido (idPedido, direccion, ciudad, direccionPrincipal)
            VALUES (@idPedido, @direccion, @ciudad, @direccionPrincipal)";

                    foreach (var detalle in pedido.Detalles)
                    {
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con, transaccion);
                        cmdDetalle.Parameters.AddWithValue("@idPedido", idPedido);
                        cmdDetalle.Parameters.AddWithValue("@direccion", detalle.Direccion);
                        cmdDetalle.Parameters.AddWithValue("@ciudad", detalle.Ciudad);
                        cmdDetalle.Parameters.AddWithValue("@direccionPrincipal", detalle.DireccionPrincipal);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    // Confirmar transacción
                    transaccion.Commit();
                    return idPedido;
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            } // Aquí se cerrará automáticamente la conexión
        }

    }
}