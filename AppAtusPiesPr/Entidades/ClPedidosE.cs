using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Entidades
{
    public class ClPedidosE
    {
        public int IdPedido { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal TotalPedido { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }

    public class DetallePedido
    {
        public int IdDetallesPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal => Cantidad * Precio;
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public bool DireccionPrincipal { get; set; }
    }

}