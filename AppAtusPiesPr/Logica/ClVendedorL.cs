using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClVendedorL
    {
        ClVendedorD oVendedor = new ClVendedorD();
        public int RegistrarVendedor(ClUsuarioE vendedor)
        {
            return oVendedor.MtdRegistrarVendedor(vendedor);
        }



    }
}