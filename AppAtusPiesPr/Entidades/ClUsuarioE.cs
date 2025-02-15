﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Entidades
{
    [Serializable]
    public class ClUsuarioE
    {
        public int IdUsuario { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string foto { get; set; }


        public string Email { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string Rol { get; set; }
        public string Mensaje { get; set; }
        public int numProducto { get; set; }
        public string estado { get; set; }
        public List<ClRolE> Roles { get; set; }

        public class ClEstadisticaVendedorE : ClUsuarioE
        {
            public int Año { get; set; }
            public int Mes { get; set; }
            public int TotalClientes { get; set; }
        }
    }
}