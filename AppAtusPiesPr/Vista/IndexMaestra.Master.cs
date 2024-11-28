using AppAtusPiesPr.Datos;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAtusPiesPr.Vista
{
    public partial class IndexMaestra : System.Web.UI.MasterPage
    {
        ClClienteL oClienteL = new ClClienteL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                menuConSesion.Visible = false;
                menuSinSesion.Visible = false;

                if (Session["usuario"] != null)
                {
                    string rol = Session["Rol"]?.ToString();
                    nombreCliente.Text = Session["usuario"].ToString();

                    if (!string.IsNullOrEmpty(rol) && rol == "Cliente")
                    {
                        menuConSesion.Visible = true;

                    }
                    else
                    {
                        menuSinSesion.Visible = true;
                    }
                }
                else
                {
                    menuSinSesion.Visible = true;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            HttpCookie cookies = Request.Cookies[".ASPXAUTH"];
            if (cookies != null)
            {
                cookies.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookies);
            }

            Response.Redirect("~/index.aspx");
        }

    }
}
