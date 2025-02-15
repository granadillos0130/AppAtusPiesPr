﻿using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
                    cargarProductos(idCliente);
                }
                
                cargarCategorias();
                cargarMarcas();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        private void cargarProductos(int idCliente)
        {
            ClPedidosL objPedidoL = new ClPedidosL();
            List<PedidoCliente> dt = objPedidoL.mtdPedidosCliente(idCliente);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        private void limpiarCampos()
        {
            txtDocumento.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
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
                txtPass.Text = oCliente.Password;
                telCliente.Text = oCliente.Telefono;
                direcCliente.Text = oCliente.Direccion;
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null)
            {
                string email = txtEmail.Text;

                if (!email.Contains("@") || !email.Contains("."))
                {
                    MostrarMensajeError("Por favor, ingrese un email válido.");
                    return; // Detener el proceso si el email no es válido
                }

                int idCliente = Convert.ToInt32(Session["idUsuario"]);  // Obtener el idCliente desde la sesión

                ClUsuarioE oUsuario = new ClUsuarioE
                {
                    IdUsuario = idCliente,
                    Documento = txtDocumento.Text,
                    Nombres = txtNombres.Text,
                    Apellidos = txtApellidos.Text,
                    Email = txtEmail.Text,
                    Password = txtPass.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text
                };

                ClClienteL oLogica = new ClClienteL();
                ClUsuarioE objData = oLogica.mtdActualizarInfoCliente(oUsuario);

                if (objData != null)
                {
                    MostrarMensajeExito("Se actualizó la información con éxito");
                    limpiarCampos();
                    Response.Redirect(Request.RawUrl + "?status=success", false);
                }
                else
                {
                    MostrarMensajeError("Error, no se pudo actualizar la información");
                    limpiarCampos();
                    Response.Redirect(Request.RawUrl + "?status=success", false);
                }

            }
        }

        private void MostrarMensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"Swal.fire('¡Éxito!', '{mensaje}', 'success');", true);
        }

        private void MostrarMensajeError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"Swal.fire('Error', '{mensaje}', 'error');", true);
        }

        protected void btnCancelarCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(Session["idCliente"]); // O como obtengas el ID del cliente
                ClClienteL clienteL = new ClClienteL();

                if (clienteL.mtdCancelarCuenta(idCliente))
                {
                    // Limpia la sesión
                    Session.Clear();
                    Session.Abandon();

                    // Redirige al inicio o página de confirmación
                    Response.Redirect("~/Vista/index.aspx", false);
                }
                else
                {
                    // Maneja el error
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: '¡Error!', text: 'Hubo un problema al cancelar tu cuenta. Intenta nuevamente.', showConfirmButton: false, timer: 2000, timerProgressBar: true, position: 'bottom-end', toast: true, background: '#ff6b6b', color: '#fff', iconColor: '#fff' });", true);
            }
            }
            catch (Exception ex)
            {
                // Maneja el error según tu implementación
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: '¡Error!', text: 'Hubo un problema al enviar tu solicitud. Intenta nuevamente.', showConfirmButton: false, timer: 2000, timerProgressBar: true, position: 'bottom-end', toast: true, background: '#ff6b6b', color: '#fff', iconColor: '#fff' });", true);
          
        }
        }

        private void cargarMarcas()
        {
            ClProductoL oLogica = new ClProductoL();
            RepeaterMarca.DataSource = oLogica.MtdListarMarcas();
            RepeaterMarca.DataBind();
        }

    }
}