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
        public ClProductoE MtdRegistroProd(ClProductoE objDatosProd)
        {
            ClProductoD objProdD = new ClProductoD();  
            ClProductoE objData = objProdD.MtdRegistrarProducto(objDatosProd);
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



        public ClProductoE MtdActualizacionProduc(ClProductoE objProdActu)
        {
            ClProductoD objActuP = new ClProductoD();
            return objActuP.mtdActualizarProducto(objProdActu);
        }

        public List<ClProductoEmpresaE> MtdListarPorFiltro(int idCategoria)
        {
            ClProductoL objProL = new ClProductoL();
            List<ClProductoEmpresaE> tblDatos = objProL.MtdListarPorFiltro(idCategoria);
            return tblDatos;
        }

    }
}