using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClClienteL
    {
        private ClClienteD clienteDa = new ClClienteD();

        public ClUsuarioE mtdPerfilCliente(int idCliente)
        {
            return clienteDa.mtdPerfilCliente(idCliente);
        }
        public ClUsuarioE mtdActualizarInfoCliente(ClUsuarioE dataCliente)
        {
            return clienteDa.mtdActualiarInfoCliente(dataCliente);
        }
        public int RegistrarCliente(ClUsuarioE cliente, out string mensaje)
        {
            return clienteDa.MtdRegistrarCliente(cliente,out mensaje);
        }

        public ClUsuarioE MtdIngreso(ClUsuarioE usuario)
        {
            return clienteDa.MtdIngreso(usuario);
        }

        public bool IsEmailExist(string email)
        {
            return clienteDa.IsEmailExist(email);
        }

        public void SaveTemporaryPassword(string email, string temporaryPassword)
        {
            clienteDa.SaveTemporaryPassword(email, temporaryPassword);
        }

        public bool IsResetCodeValid(string resetCode)
        {
            return clienteDa.IsResetCodeValid(resetCode);
        }

        public void UpdatePassword(string resetCode, string newPassword)
        {
            clienteDa.UpdatePassword(resetCode, newPassword);
        }

        public bool mtdCancelarCuenta(int idCliente)
        {
            ClClienteD objPedidosD = new ClClienteD();
            bool tblDatos = objPedidosD.mtdCancelarCuenta(idCliente);
            return tblDatos;

        }

    }
}
