using System;
using System.IO;
using AppAtusPiesPr.Entidades;
using AppAtusPiesPr.Logica;

namespace AppAtusPiesPr.Vista
{
    public partial class EditarPerfilVendeyAdmin : System.Web.UI.Page
    {
        private ClUsuarioL usuarioLo = new ClUsuarioL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                {
                    // Verifica el rol del usuario y oculta el div para los administradores
                    bool esVendedor = Session["rol"].ToString() == "Vendedor";
                    gpDescripcion.Visible = esVendedor;
                    imgFotoPerfil.Visible = esVendedor;
                    gpDireccion.Visible = esVendedor;
                    gpTelefono.Visible = esVendedor;
                    gpCambiarFoto.Visible= esVendedor;

                    CargarDatosUsuario();

                }
            }
        }









        private void CargarDatosUsuario()
        {
            int idUsuario = (int)Session["idUsuario"];
            bool esVendedor = Session["rol"].ToString() == "Vendedor";

            ClUsuarioE usuario = usuarioLo.ObtenerUsuarioPorId(idUsuario, esVendedor);

            if (usuario != null)
            {
                txtNombres.Text = usuario.Nombres;
                txtApellidos.Text = usuario.Apellidos;
                txtEmail.Text = usuario.Email;
                if (esVendedor)
                {
                    txtTelefono.Text = usuario.Telefono;
                    txtDireccion.Text = usuario.Direccion;
                    txtDescripcion.Text = usuario.Descripcion;
                    imgFotoPerfil.ImageUrl = "/imagenes/fotoPerfil/" + usuario.foto;
                }
                else
                {
                    txtTelefono.Visible = false;
                    txtDireccion.Visible = false;
                    txtDescripcion.Visible = false;
                }
            }
            else
            {
                lblMensaje.Text = "Error: No se han podido obtener los datos del usuario.";
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar la contraseña actual
            string contrasenaActual = txtContrasenaActual.Text;
            string nuevaContrasena = txtNuevaContrasena.Text;
            bool esVendedor = Session["rol"].ToString() == "Vendedor";

            if (!usuarioLo.ValidarContrasena((int)Session["idUsuario"], contrasenaActual, esVendedor))
            {
                lblMensaje.Text = "La contraseña actual es incorrecta.";
                return;
            }

            // Actualizar los datos del usuario
            int idUsuario = (int)Session["idUsuario"];
            ClUsuarioE usuario = new ClUsuarioE
            {
                IdUsuario = idUsuario,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Email = txtEmail.Text,
                Password = string.IsNullOrEmpty(nuevaContrasena) ? contrasenaActual : nuevaContrasena // Si no se ingresó una nueva contraseña, mantener la actual
            };

            if (esVendedor)
            {
                usuario.Telefono = txtTelefono.Text;
                usuario.Direccion = txtDireccion.Text;
                usuario.Descripcion = txtDescripcion.Text;

                // Subir nueva foto si se seleccionó una
                if (fileUploadFoto.HasFile)
                {
                    // Construir el nombre del archivo de la foto
                    string nombreFoto = $"{usuario.Nombres}-{usuario.Documento}.png";
                    string rutaCarpeta = Server.MapPath("~/imagenes/fotoPerfil/");
                    string rutaFoto = Path.Combine(rutaCarpeta, nombreFoto);

                    // Verificar que la carpeta exista, si no, crearla
                    if (!Directory.Exists(rutaCarpeta))
                    {
                        Directory.CreateDirectory(rutaCarpeta);
                    }

                    // Borrar la foto existente si hay una con el mismo nombre
                    if (System.IO.File.Exists(rutaFoto))
                    {
                        System.IO.File.Delete(rutaFoto);
                    }

                    // Guardar la nueva foto
                    fileUploadFoto.SaveAs(rutaFoto);
                    usuario.foto = nombreFoto;
                }
                else
                {
                    usuario.foto = imgFotoPerfil.ImageUrl.Replace("~/imagenes/fotoPerfil/", ""); // Mantener la ruta actual de la foto si no se seleccionó una nueva
                }
            }

            bool exito = usuarioLo.ActualizarUsuario(usuario, esVendedor);

            if (exito)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Perfil actualizado correctamente.";

                // Redirigir a la misma página para recargar
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el perfil.";
            }
        }



    }
}
