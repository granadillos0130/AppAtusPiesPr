﻿using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
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

        public ClProductoE MtdActualizacionProduc(ClProductoE objProdActu)
        {
            ClProductoD objActuP = new ClProductoD();
            return objActuP.mtdActualizarProducto(objProdActu);
        }
    }
}