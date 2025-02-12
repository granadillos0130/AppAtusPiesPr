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
        public int RegistrarVendedor(ClUsuarioE usuario, HttpPostedFile foto, out string mensaje)
        {
            return oVendedor.MtdRegistrarVendedor(usuario, foto, out mensaje);
        }

        public ClUsuarioE mtdPerfilVendedor(int idVendedor)
        {
            return oVendedor.mtdPerfilVendedor(idVendedor);
        }
        public List<ClProductoEmpresaE> MtdObtenerProductosMasVendidosPorVendedor(int idVendedor, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVendedor.MtdObtenerProductosMasVendidosPorVendedor(idVendedor, fechaInicio, fechaFin);
        }
        public List<ClProductoE> ObtenerProductosPorVendedorYCategoria(int? idVendedor = null, int? idCategoria = null)
        {
            ClVendedorD vendedorDatos = new ClVendedorD();
            return vendedorDatos.MtdObtenerProductosPorVendedorYCategoria(idVendedor, idCategoria);
        }






        public int RegistrarProveedor(ClProveedorE proveedor, int idVendedor, out string mensaje)
        {
            
            ClVendedorD proveedorD = new ClVendedorD();
            return proveedorD.RegistrarProveedor(proveedor, idVendedor, out mensaje);
        }

        public List<ClProveedorE> ListarProveedores(int idVendedor, string estado = null)
        {
            ClVendedorD proveedorD = new ClVendedorD();
            return proveedorD.ListarProveedoresPorVendedor(idVendedor, estado);
        }

        public bool ActualizarProveedor (ClProveedorE proveedor , out string mensaje)
        {
            ClVendedorD proveedorD = new ClVendedorD ();
            return proveedorD.ActualizarProveedor(proveedor, out mensaje);
        }
        public bool EliminarProveedor(int idProveedor, out string mensaje)
        {
            ClVendedorD proveedorD = new ClVendedorD();
            return proveedorD.EliminarProveedor(idProveedor, out mensaje);
        }


    }
}