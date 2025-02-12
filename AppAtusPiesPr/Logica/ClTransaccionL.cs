using System.Collections.Generic;
using AppAtusPiesPr.Datos;

namespace AppAtusPiesPr.Logica
{
    public class ClTransaccionL
    {
        public string ProcesarTransaccion(List<int> idPedidos, List<int> idsVendedores, List<decimal> montos)
        {
            if (idPedidos == null || idsVendedores == null || montos == null)
                return "Datos inválidos";

            ClTransaccionD transaccionD = new ClTransaccionD();
            return transaccionD.GuardarTransacciones(idPedidos, idsVendedores, montos);
        }
    }
}
