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
    public partial class perfilUsuario : System.Web.UI.Page
    {
        ClClienteL oClienteL = new ClClienteL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
        if (Session["idUsuario"] != null)
        {
            int idCliente = Convert.ToInt32(Session["idUsuario"]);  // Obtener el idCliente desde la sesión
            cargarDatosCliente(idCliente);
        }
                
                cargarCategorias();
            }
        }
        private void cargarCategorias()
        {
            ClProductoL oLogica = new ClProductoL();
            Repeater2.DataSource = oLogica.MtdListarCategorias();
            Repeater2.DataBind();
        }

        private void cargarDatosCliente(int idCliente)
        {
            ClUsuarioE oCliente = oClienteL.mtdPerfilCliente(idCliente);
            if (oCliente != null)
            {
                documentoCliente.Text = oCliente.Documento;
                nombreCliente.Text = oCliente.Nombres;
                apellidoCliente.Text = oCliente.Apellidos;
                emailCliente.Text = oCliente.Email;
                passCliente.Text = oCliente.Email;
                telCliente.Text = oCliente.Telefono;
                direcCliente.Text = oCliente.Direccion;
                estadoCliente.Text = oCliente.estado;
            }

        }
    }
}