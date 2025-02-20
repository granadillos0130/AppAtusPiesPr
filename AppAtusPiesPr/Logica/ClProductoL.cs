﻿using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Logica
{
    public class ClProductoL
    {
        private const int PAGE_SIZE = 12;

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

        public DataTable MtdListarProductosMejorCalificados()
        {
            ClProductoD objProductoD = new ClProductoD();
            DataTable tblDatos = objProductoD.MtdListarProductosMejorCalificados();
            return tblDatos;
        }

        public DataTable MtdListarProductosMasRecientes()
        {
            ClProductoD objProductoD = new ClProductoD();
            DataTable tblDatos = objProductoD.MtdListarProductosMasRecientes();
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

        public List<ClMarcasE> MtdListarMarcas()
        {
            ClProductoD objMarca = new ClProductoD();
            List<ClMarcasE> tblDatos = objMarca.MtdListarMarcas();
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

        public DataTable MtdListarPorMarca(int idMarca)
        {
            return productoD.MtdListarProductosPorMarca(idMarca);
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

        public ClComentarioE MtdGuardarComentario(ClComentarioE objDataCom)
        {

            objDataCom.FechaComentario = DateTime.Now;

            ClProductoD objProdD = new ClProductoD();
            ClComentarioE objData = objProdD.mtdGuardarComentario(objDataCom);
            return objData;
        }

        public List<ClComentarioE> mtdListarComentarios(int idProducto)
        {
            ClProductoD objDatos = new ClProductoD();
            List<ClComentarioE> objData = objDatos.mtdListarComentario(idProducto);
            return objData;
        }


        public decimal ObtenerValoracionPromedio(int idProducto)
        {
            ClProductoD objD = new ClProductoD();
            return objD.ObtenerPromedioValoracion(idProducto);
        }

        public List <ClProductoE>MtdListarProductoDDL(int idVendedor)
        {
            ClProductoD objProducto = new ClProductoD();
            List<ClProductoE> objDatas = objProducto.mtdListarVendedordll(idVendedor);
            return objDatas;
        }

        public List<ClCategoriaE> MtdlistarCategoriasActua()
        {
            ClProductoD objproducto = new ClProductoD();
            List<ClCategoriaE> objData = objproducto.mtdListarCategoriaActu();
            return objData;
        }
        public List<ClMarcasE> MtdlistarMarcaActua()
        {
            ClProductoD objproducto = new ClProductoD();
            List<ClMarcasE> objData = objproducto.mtdListarMarcaActu();
            return objData;
        }


        public List<ClProductoEmpresaE> mtdNotificaciones(int idCliente)
        {
            ClProductoD objproducto = new ClProductoD();
            List<ClProductoEmpresaE> objData = objproducto.ObtenerNotificaciones(idCliente);
            return objData;
        } 

    }
}