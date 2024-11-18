using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Entidades
{
    public class ClProductoE
    {
        public int idProducto {  get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public int CantidadStock { get; set; }

        public int Precio { get; set; }

        public string Presentacion { get; set; }

        public string Estado { get; set; }

        public string Talla { get; set; }

        public int idVendedor { get; set; }
    }

    public class ClProductoEmpresaE
    {
        public int idProdctoEmpresa { get; set; }
        public int idVendedor { get; set; }
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidadStock { get; set; }
        public int precioVenta { get; set; }
        public string descripcionProducto { get; set; }
        public string referencia { get; set; }
        public string imagen { get; set; }
        public int descuento { get; set; }
        public string nombres { get; set; }
        public string nombreMarca { get; set; }

        // Nueva propiedad para las tallas asociadas
        public List<ClTallaE> TallasDisponibles { get; set; }
    }
}