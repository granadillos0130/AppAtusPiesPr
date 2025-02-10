using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClConexion
    {
        SqlConnection conex = null;

        public ClConexion()
        {

            //conex = new SqlConnection("Data Source=.;Initial Catalog=dbZapateria2.0;User ID=ZapateriaAdso;Password=ADSO1234;");
            conex = new SqlConnection("Data Source=DESKTOP-JUE434E\\SQLEXPRESS;Initial Catalog=dbZapateria2.0;Integrated Security=True;");

        }

        public SqlConnection MtdAbrirConexion()
        {
            conex.Open();
            return conex;
        }

        public void MtdCerrarConexion()
        {
            conex.Close();
        }
    }
}