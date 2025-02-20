﻿
using AppAtusPiesPr.Datos;

using AppAtusPiesPr.Entidades;

using AppAtusPiesPr.Logica;

using System;

using System.Collections.Generic;

using System.Net.Mail;

using System.Net;

using System.Web.UI;

using System.Web.UI.WebControls;
using System.Linq;

namespace AppAtusPiesPr.Vista

{

    public partial class AdministrarProveedores : System.Web.UI.Page

    {

        protected void Page_Load(object sender, EventArgs e)

        {

            if (!IsPostBack || ScriptManager.GetCurrent(this).IsInAsyncPostBack)

            {

                CargarProveedores();

            }

        }





        protected void AbrirModalEditar(int idProveedor)

        {



            ClProveedorE proveedor = new ClVendedorD().ObtenerProveedorPorId(idProveedor);

            if (proveedor != null)

            {

                hfIdProveedor.Value = proveedor.idProveedor.ToString();

                txtDocumentoEditar.Text = proveedor.Documento;

                txtNombresEditar.Text = proveedor.Nombres;

                txtEmailEditar.Text = proveedor.Email;

                txtTelefonoEditar.Text = proveedor.Telefono;





                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "$('#modalEditarProveedor').modal('show');", true);

            }

            else

            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "Swal.fire('Error', 'No se pudo cargar la información del proveedor.', 'error');", true);

            }

        }











        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)

        {

            CargarProveedores();

        }



        private void CargarProveedores()

        {

            int idVendedor = Convert.ToInt32(Session["idUsuario"]);

            string estado = "activo";





            List<ClProveedorE> proveedores = new ClVendedorD().ListarProveedoresPorVendedor(idVendedor, estado);





            rptProveedores.DataSource = proveedores;

            rptProveedores.DataBind();

        }

        protected void btnGuardarProveedor_Click(object sender, EventArgs e)
        {
            bool hasError = false;

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblTelefonoError.Text = "Por favor, ingresa el teléfono.";
                txtTelefono.CssClass = "form-control is-invalid";
                hasError = true;
            }
            else
            {

                string codigoPais = ddlPais.SelectedValue;
                string telefono = txtTelefono.Text.Trim();


                string telefonoSoloNumeros = telefono.Replace("+", "").Replace(codigoPais, "").Trim();

                if (!telefonoSoloNumeros.All(char.IsDigit))
                {
                    lblTelefonoError.Text = "El teléfono debe contener solo números.";
                    txtTelefono.CssClass = "form-control is-invalid";
                    hasError = true;
                }
                else
                {
                    lblTelefonoError.Text = "";
                    txtTelefono.CssClass = "form-control is-valid";
                }


                if (!telefono.StartsWith("+"))
                {
                    telefono = "+" + codigoPais + " " + telefonoSoloNumeros;
                }

                txtTelefono.Text = telefono;
            }

            if (hasError)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "$('#modalRegistrarProveedor').modal('show');", true);
                return;
            }


            ClProveedorE proveedor = new ClProveedorE
            {
                Documento = txtDocumento.Text.Trim(),
                Nombres = txtNombres.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                estado = "activo"
            };

            string mensaje;
            try
            {
                int idProveedor = new ClVendedorD().RegistrarProveedor(proveedor, Convert.ToInt32(Session["idUsuario"]), out mensaje);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"Swal.fire('Éxito', '{mensaje}', 'success');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "clearModal", "LimpiarCamposModal();", true);
                CargarProveedores();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"Swal.fire('Error', 'Hubo un problema al registrar el proveedor: {ex.Message}', 'error');", true);
            }
        }


        protected void btnActualizarProveedor_Click(object sender, EventArgs e)

        {



            int idProveedor;

            if (!int.TryParse(hfIdProveedor.Value, out idProveedor) || idProveedor <= 0)

            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "Swal.fire('Error', 'El ID del proveedor no es válido.', 'error');", true);

                return;

            }





            ClProveedorE proveedor = new ClProveedorE

            {

                idProveedor = idProveedor,

                Documento = txtDocumentoEditar.Text.Trim(),

                Nombres = txtNombresEditar.Text.Trim(),

                Email = txtEmailEditar.Text.Trim(),

                Telefono = txtTelefonoEditar.Text.Trim(),

                estado = "activo"

            };



            string mensaje;



            try

            {

                bool resultado = new ClVendedorD().ActualizarProveedor(proveedor, out mensaje);



                if (resultado)

                {



                    ScriptManager.RegisterStartupScript(this, GetType(), "Exito", "Swal.fire('Éxito', 'Proveedor actualizado correctamente.', 'success');", true);

                    CargarProveedores();

                }

                else

                {



                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", $"Swal.fire('Error', '{mensaje}', 'error');", true);

                }

            }

            catch (Exception ex)

            {



                ScriptManager.RegisterStartupScript(this, GetType(), "Error", $"Swal.fire('Error', 'Hubo un problema al actualizar el proveedor: {ex.Message}', 'error');", true);

            }

        }







        [System.Web.Services.WebMethod]

        public static ClProveedorE CargarProveedor(int idProveedor)

        {

            try

            {

                ClVendedorD proveedorD = new ClVendedorD();

                ClProveedorE proveedor = proveedorD.ObtenerProveedorPorId(idProveedor);



                if (proveedor == null)

                {

                    throw new Exception("Proveedor no encontrado.");

                }



                return proveedor;

            }

            catch (Exception ex)

            {

                throw new Exception("Error al cargar el proveedor: " + ex.Message);

            }

        }



        [System.Web.Services.WebMethod]

        public static string EliminarProveedor(int idProveedor)

        {

            string mensaje;

            ClVendedorL proveedorL = new ClVendedorL();



            bool resultado = proveedorL.EliminarProveedor(idProveedor, out mensaje);



            return resultado ? mensaje : $"Error al eliminar el proveedor: {mensaje}";

        }



        protected string ObtenerCorreoVendedor()

        {

            int idUsuario = Convert.ToInt32(Session["idUsuario"]);

            ClUsuarioE vendedor = new ClUsuarioD().DatosVendedor(idUsuario);



            if (vendedor != null && !string.IsNullOrEmpty(vendedor.Email))

            {

                return vendedor.Email;

            }

            return "";

        }



        protected string ObtenerTelefonoVendedor()

        {

            int idUsuario = Convert.ToInt32(Session["idUsuario"]);

            ClUsuarioE vendedor = new ClUsuarioD().DatosVendedor(idUsuario);



            if (vendedor != null && !string.IsNullOrEmpty(vendedor.Telefono))

            {

                return vendedor.Telefono;

            }

            return "";

        }
        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoPais = ddlPais.SelectedValue;
            string telefonoActual = txtTelefono.Text.Trim();

            if (!telefonoActual.StartsWith("+"))
            {
                txtTelefono.Text = $"+{codigoPais} {telefonoActual}";
            }
        }


    }

}

