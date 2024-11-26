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

       public ClUsuarioE mtdPerfilVendedor(int idVendedor)
        {
            return oVendedor.mtdPerfilVendedor(idVendedor);
        }
        public List<ClProductoE> MtdObtenerProductosMasVendidosPorVendedor(int idVendedor, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVendedor.MtdObtenerProductosMasVendidosPorVendedor(idVendedor, fechaInicio, fechaFin);
        }
        public List<ClProductoE> ObtenerProductosPorVendedorYCategoria(int? idVendedor = null, int? idCategoria = null)
        {
            ClVendedorD vendedorDatos = new ClVendedorD();
            return vendedorDatos.MtdObtenerProductosPorVendedorYCategoria(idVendedor, idCategoria);
        }





    }
}