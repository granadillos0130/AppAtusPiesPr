using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Entidades
{
    [Serializable]
    public class ClRolE
    {
        public string RoleName { get; set; }
        public int IdUsuario { get; set; }
       
            public int IdVendedor { get; set; }
            public int IdAdmin { get; set; }
            public int IdCliente { get; set; }
         
     

    }

}