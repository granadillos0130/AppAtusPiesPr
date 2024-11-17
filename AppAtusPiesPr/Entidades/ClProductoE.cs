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
}