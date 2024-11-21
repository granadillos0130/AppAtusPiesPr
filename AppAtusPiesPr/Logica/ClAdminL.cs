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

        public DataTable MtdObtenerSolicitudes()
        {
            ClAdminD ListaSolicitudes = new ClAdminD();
            return ListaSolicitudes.MtdListarSolicitudes();
        }

        public bool MtdAceptarSolicitud(int idVendedor)
        {
            ClAdminD AceptarSoli = new ClAdminD();
            return AceptarSoli.MtdAceptarSolicitud(idVendedor);
        }
    }
}