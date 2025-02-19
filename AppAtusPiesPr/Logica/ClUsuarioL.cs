using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using System.Collections.Generic;

namespace AppAtusPiesPr.Logica
{
    public class ClUsuarioL
    {
        private ClUsuarioD usuarioDa = new ClUsuarioD();

        public ClUsuarioE ObtenerUsuarioPorId(int idUsuario, bool esVendedor)
        {
            return esVendedor ? usuarioDa.ObtenerVendedorPorId(idUsuario) : usuarioDa.ObtenerAdminPorId(idUsuario);
        }

        public bool ActualizarUsuario(ClUsuarioE usuario, bool esVendedor)
        {
            return esVendedor ? usuarioDa.ActualizarVendedor(usuario) : usuarioDa.ActualizarAdmin(usuario);
        }

        public bool ValidarContrasena(int idUsuario, string contrasenaActual, bool esVendedor)
        {
            return usuarioDa.ValidarContrasena(idUsuario, contrasenaActual, esVendedor);
        }

        public ClUsuarioE datosVendedor(int idUsuario)
        {
            ClUsuarioD usuarioV = new ClUsuarioD();
            return usuarioV.DatosVendedor(idUsuario);
        }
    }
}
