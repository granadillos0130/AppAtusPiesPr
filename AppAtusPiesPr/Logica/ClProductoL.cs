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

        public ClProductoEmpresaE MtdEliminarProducto(int id)
        {
            ClProductoD objProD = new ClProductoD();
            ClProductoEmpresaE objData = objProD.MtdEliminarProducto(id);
            return objData; 
        }


        public DataTable mtdBuscarProducto(string busqueda)
        {
            ClProductoD oDatos = new ClProductoD();
            DataTable tblDatos = oDatos.buscarProductos(busqueda);
            return tblDatos;
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
       
        //Metodo para listar Producto

        public List<ClProductoEmpresaE> MtdListarProduc(int? idVendedor=null)
        {
            ClProductoD objPro = new ClProductoD();
            List<ClProductoEmpresaE> tblDatos = objPro.MtdListarProducto(idVendedor);
            return tblDatos;
        }

        //Metodo para listar producto por vendedor
        public List<ClProductoEmpresaE> ObtenerProductoPorVendedorYProducto(int? idVendedor = null,int? idProducto= null)
        {
            ClProductoD productoDatos = new ClProductoD();
            return productoDatos.MtdObtenerProductosPorVendedorYProducto(idVendedor,idProducto);
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
        public DataTable MtdListarPorVendedor(int idVendedor)
        {
            return productoD.MtdListarProductosVendedor(idVendedor);
        }

        // Método para obtener productos por categoría
        public List<ClProductoE> MtdObtenerProductosPorCategoria(string categoria = null)
        {
            ClProductoD productoD = new ClProductoD();
            return productoD.MtdListarProductosPorCategoria(categoria);

        }
    }
}