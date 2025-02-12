using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Entidades
{
    [Serializable]
    public class ClProveedorE : ClUsuarioE
    {
        public int idProveedor { get; set; }
        public int idVendedor { get; set; }

        public string Nombre {  get; set; } 

    }
}