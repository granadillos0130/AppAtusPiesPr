using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AppAtusPiesPr.Logica
{
    public class ClAdminL
    {
        public ClUsuarioE MtdRegistrarVendedor(ClUsuarioE oDatos)
        {
            oDatos.Password = HashPassword(oDatos.Password);

            ClAdminD oDatosD = new ClAdminD();
            return oDatosD.MtdRegistrarVendedor(oDatos);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public DataTable MtdListarVendedores()
        {
            ClAdminD Lista = new ClAdminD();
            return Lista.MtdListarVendedores();
        }

        public DataTable MtdListarUsuarios()
        {
            ClAdminD ListaUsuario = new ClAdminD();
            return ListaUsuario.MtdListarUsuarios();
        }
    }
}