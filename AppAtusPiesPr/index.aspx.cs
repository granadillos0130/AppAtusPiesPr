﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
                cargarCategorias();
            }
        }

        private void cargarProductos()
        {
            ClProductoL objProductoL = new ClProductoL();
            DataTable dt = objProductoL.MtdListarProductos();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }

    }
}