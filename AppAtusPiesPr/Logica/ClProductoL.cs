using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClProductoL
    {
        public ClProductoEmpresaE MtdRegistroProd(ClProductoEmpresaE objDatosProdu)
        {
            ClProductoD objProdD = new ClProductoD();  
            ClProductoEmpresaE objData = objProdD.MtdRegistrarProducto(objDatosProdu);
            return objData;
        }


        public DataTable MtdListarProductos()
        {
            ClProductoD objProductoD = new ClProductoD();
            DataTable tblDatos = objProductoD.MtdListarProductos();
            return tblDatos;
        }

        private ClProductoD oDatos = new ClProductoD();

        public ClProductoEmpresaE MtdInfoProductos(int idProdctoEmpresa)
        {
            return oDatos.MtdInfoProducto(idProdctoEmpresa);
        }
        public List<ClCategoriaE> MtdListarCategorias()
        {
            ClProductoD objCategoria = new ClProductoD();
            List<ClCategoriaE> tblDatos = objCategoria.MtdListarCategorias();
            return tblDatos;
        }
       
        public List<ClProductoE> MtdObtenerProductosMasVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            ClProductoD productoD = new ClProductoD();
            return productoD.MtdListaProductosMasVendidos(fechaInicio, fechaFin);
        }



        public ClProductoEmpresaE MtdActualizacionProduc(ClProductoEmpresaE objProdActu)
        {
            ClProductoD objActuP = new ClProductoD();
            return objActuP.mtdActualizarProducto(objProdActu);
        }


        // Método para obtener productos por categoría
        private ClProductoD productoD = new ClProductoD();

        public DataTable MtdListarPorFiltro(int idCategoria)
        {
            return productoD.MtdListarProductosPorCategoria(idCategoria);
        }

        // Método para obtener productos por categoría
        public List<ClProductoE> MtdObtenerProductosPorCategoria(string categoria = null)
        {
            ClProductoD productoD = new ClProductoD();
            return productoD.MtdListarProductosPorCategoria(categoria);

        }
    }
}