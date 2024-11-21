using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClClienteL
    {
        private ClClienteD clienteDa = new ClClienteD();

        public int RegistrarCliente(ClUsuarioE cliente)
        {
            return clienteDa.MtdRegistrarCliente(cliente);
        }

        public ClUsuarioE MtdIngreso(ClUsuarioE usuario)
        {
            return clienteDa.MtdIngreso(usuario);
        }
    }
}