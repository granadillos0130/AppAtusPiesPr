using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClPedidosL
    {
        private readonly ClPedidosD _pedidoDatos = new ClPedidosD(); // Instancia de la capa de datos

        // En la capa de lógica
        public int MtdGuardarPedido(ClPedidosE pedido, List<DetallePedido> detalles)
        {
            if (pedido == null)
                throw new ArgumentException("El objeto pedido no puede ser nulo.");

            if (pedido.IdCliente <= 0)
                throw new ArgumentException("El ID del cliente debe ser válido.");

            if (pedido.TotalPedido <= 0)
                throw new ArgumentException("El total del pedido debe ser mayor a cero.");

            // Llama al método de la capa de datos para guardar el pedido
            return _pedidoDatos.InsertarPedido(pedido, detalles);
        }

    }
}