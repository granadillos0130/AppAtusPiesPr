<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppAtusPiesPr.Vista.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            font-family: 'Arial', sans-serif;
            background: #e8e8e8;
        }

        .main-container {
            display: flex;
            flex-direction: row;
            background: #1a1a1a;
            border-radius: 10px;
            box-shadow: 0px 5px 20px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            width: 800px;
            height: 500px;
            border: 1px solid #999;
            color: black;
        }

        .login-image {
            width: 45%;
            height: 100%;
            position: relative;
            overflow: hidden;
            background: #000;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .gif-container {
            width: 100%;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .gif-container img {
            width: 90%;
            height: auto;
            object-fit: contain;
        }

        .form-container {
            width: 55%;
            padding: 40px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            background: white;
            color: black;
        }

        .form-container h2 {
            margin-bottom: 30px;
            color: black;
            font-weight: bold;
            font-size: 2rem;
            text-align: center;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-container .control-label {
            margin-bottom: 8px;
            display: block;
            color: black;
        }

        .form-container .form-control {
            border-radius: 5px;
            border: 1px solid #444;
            padding: 12px;
            font-size: 1rem;
            background: white;
            color: black;
            width: 100%;
            transition: all 0.3s ease;
        }

        .form-container .form-control:focus {
            border-color: #999;
            box-shadow: 0 0 0 2px rgba(0,123,255,0.25);
            outline: none;
        }

        .form-container .btn {
            width: 100%;
            padding: 12px;
            font-size: 1.1rem;
            border-radius: 5px;
            margin-bottom: 20px;
            transition: all 0.3s ease;
        }

        .form-container .btn-primary {
            background-color: black;
            border: none;
            font-weight: bold;
            color: white;
        }

        .form-container .btn-primary:hover {
            background-color: #272727;
            transform: translateY(-1px);
        }

        .text-danger {
            color: #dc3545;
            margin-top: 10px;
            text-align: center;
        }

        .link, .register-vendor {
            margin-top: 15px;
            text-align: center;
        }

        .link a, .register-vendor a {
            color: #007bff;
            text-decoration: none;
            font-size: 0.9rem;
            transition: color 0.3s ease;
        }

        .link a:hover, .register-vendor a:hover {
            color: #0056b3;
            text-decoration: underline;
        }

        /* Modal Styles */
        .modal-content {
            background: #222;
            color: #fff;
            border-radius: 15px;
        }

        .modal-header {
            border-bottom: 1px solid #444;
            padding: 20px;
            background: #1a1a1a;
        }

        .modal-header .close {
            color: #fff;
            opacity: 0.8;
        }

        .modal-header .close:hover {
            opacity: 1;
        }

        .modal-body {
            padding: 20px;
            background: #222;
        }

        .modal-footer {
            border-top: 1px solid #444;
            padding: 20px;
            background: #1a1a1a;
        }

        .modal .btn-secondary {
            background-color: #6c757d;
            border: none;
        }

        .modal .btn-secondary:hover {
            background-color: #5a6268;
        }

        @media (max-width: 768px) {
            .main-container {
                flex-direction: column;
                width: 95%;
                height: auto;
                max-width: 400px;
                margin: 20px;
            }

            .login-image,
            .form-container {
                width: 100%;
            }

            .login-image {
                height: 200px;
            }

            .form-container {
                padding: 20px;
            }

            .form-container h2 {
                font-size: 1.5rem;
                margin-bottom: 20px;
            }
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <asp:ScriptManager runat="server" /> <!-- Necesario para AJAX -->
        
        <div class="main-container">
            <div class="login-image">
                <div class="gif-container">
                    <img src="https://i.gifer.com/4KDr.gif" alt="Login animation"/>
                </div>
            </div>

            <div class="form-container">
                <h2>Iniciar Sesión</h2>

                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server" Text="Documento:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingresa tu Documento" required></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" required></asp:TextBox>
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />

                <!-- Panel para manejar PostBacks correctamente -->
                <asp:UpdatePanel ID="updPanel" runat="server">
                    <ContentTemplate>
                        <!-- Botones ocultos para el Postback -->
                        <asp:Button ID="btnEnviarSolicitud" runat="server" Style="display:none;" OnClick="btnEnviarSolicitud_Click" />
                        <asp:Button ID="btnVolver" runat="server" Style="display:none;" OnClick="btnVolver_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnEnviarSolicitud" />
                        <asp:PostBackTrigger ControlID="btnVolver" />
                    </Triggers>
                </asp:UpdatePanel>

                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>

                <div class="link">
                    <a href="#" data-toggle="modal" data-target="#registerModal">¿No tienes una cuenta? Regístrate aquí</a>
                </div>
                <div class="register-vendor">
                    <a href="#" data-toggle="modal" data-target="#registerVendedorModal">¿Eres vendedor? Regístrate aquí</a>
                </div>
                <div class="link">
                    <a href="#" data-toggle="modal" data-target="#forgotPasswordModal">¿Olvidaste tu contraseña?</a>
                </div>
            </div>
        </div>
   

   


        

        <script>
    // Función para ocultar la alerta de error cuando el campo es válido
    function hideError(errorLabelId, inputField) {
        // Verificamos si el campo no está vacío y, en el caso del correo, contiene '@'
        if (inputField.value.trim() !== "" && 
            (errorLabelId !== 'lblCorreoError' || inputField.value.includes('@'))) {
            document.getElementById(errorLabelId).style.display = 'none';
        } else {
            document.getElementById(errorLabelId).style.display = 'inline';
        }
    }
        </script>


<!-- Modal de Registro -->
<div class="modal fade" id="registerModal" tabindex="-1" role="dialog" aria-labelledby="registerModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="registerModalLabel">Registrar Usuario</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ingresa tu documento" required onkeyup="hideError('lblDocumentoError', this)"></asp:TextBox>
                    <asp:Label ID="lblDocumentoError" runat="server" CssClass="text-danger" Visible="false">Campo obligatorio.</asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombres:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresa tu nombre" required onkeyup="hideError('lblNombreError', this)"></asp:TextBox>
                    <asp:Label ID="lblNombreError" runat="server" CssClass="text-danger" Visible="false">Campo obligatorio.</asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellidos:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingresa tus apellidos" required onkeyup="hideError('lblApellidoError', this)"></asp:TextBox>
                    <asp:Label ID="lblApellidoError" runat="server" CssClass="text-danger" Visible="false">Campo obligatorio.</asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCorreo" runat="server" Text="Correo:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Ingresa tu correo" required onkeyup="hideError('lblCorreoError', this)"></asp:TextBox>
                    <asp:Label ID="lblCorreoError" runat="server" CssClass="text-danger" Visible="false">El correo debe contener un '@'.</asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblContrasenaReg" runat="server" Text="Contraseña:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtContrasenaReg" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" required onkeyup="hideError('lblContrasenaError', this)"></asp:TextBox>
                    <asp:Label ID="lblContrasenaError" runat="server" CssClass="text-danger" Visible="false">Campo obligatorio.</asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingresa tu teléfono" required onkeyup="hideError('lblTelefonoError', this)"></asp:TextBox>
                    <asp:Label ID="lblTelefonoError" runat="server" CssClass="text-danger" Visible="false">Campo obligatorio.</asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingresa tu dirección" required onkeyup="hideError('lblDireccionError', this)"></asp:TextBox>
                    <asp:Label ID="lblDireccionError" runat="server" CssClass="text-danger" Visible="false">Campo obligatorio.</asp:Label>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
            </div>
        </div>
    </div>
</div>



        <!-- Modal de Olvidar Contraseña -->
<div class="modal fade" id="forgotPasswordModal" tabindex="-1" role="dialog" aria-labelledby="forgotPasswordModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="forgotPasswordModalLabel">Recuperar Contraseña</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <asp:Label ID="lblEmailRecuperar" runat="server" Text="Correo Electrónico:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtEmailRecuperar" runat="server" CssClass="form-control" Placeholder="Ingresa tu correo" required></asp:TextBox>
                </div>
                <asp:Label ID="lblMensajeRecuperar" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                <asp:Button ID="btnEnviarRecuperar" runat="server" Text="Enviar Enlace" CssClass="btn btn-primary" OnClick="btnEnviarRecuperar_Click" />
            </div>
        </div>
    </div>
</div>





<!-- Modal de Registro de Vendedor -->
<div class="modal fade" id="registerVendedorModal" tabindex="-1" role="dialog" aria-labelledby="registerVendedorModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="registerVendedorModalLabel">Registrar Vendedor</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>

            <div class="modal-body">
               <div class="form-group">
    <asp:Label ID="lblDocumentoVend" runat="server" Text="Documento:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">🪪</div>
        <asp:TextBox ID="txtDocumentoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu documento" required oninput="quitarErrorCampo(this)" />
    </div>
    <span id="errorDocumento" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>
<div class="form-group">
    <asp:Label ID="lblNombreVend" runat="server" Text="Nombre:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">🚹</div>
       <asp:TextBox ID="txtNombreVend" runat="server" CssClass="form-control" placeholder="Ingresa tu nombre" required oninput="quitarErrorCampo(this)" />

    </div>
    <span id="errorNombre" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>
<div class="form-group">
    <asp:Label ID="lblApellidoVend" runat="server" Text="Apellido:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">🚹</div>
     <asp:TextBox ID="txtApellidoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu apellido" required oninput="quitarErrorCampo(this)" />

    </div>
    <span id="errorApellido" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>
<div class="form-group">
    <asp:Label ID="lblCorreoVend" runat="server" Text="Correo:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">📘</div>
        <asp:TextBox ID="txtCorreoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu correo" required oninput="quitarErrorCampo(this)" />

    </div>
    <span id="errorCorreo" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>
<div class="form-group">
    <asp:Label ID="lblContrasenaVend" runat="server" Text="Contraseña:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">🛡</div>
<asp:TextBox ID="txtContrasenaVend" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" required oninput="quitarErrorCampo(this)" />
    </div>
    <span id="errorContrasena" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>
<div class="form-group">
    <asp:Label ID="lblTelefonoVend" runat="server" Text="Teléfono:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">📶</div>
<asp:TextBox ID="txtTelefonoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu teléfono" required oninput="quitarErrorCampo(this)" />
    </div>
    <span id="errorTelefono" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>
<div class="form-group">
    <asp:Label ID="lblDireccionVend" runat="server" Text="Dirección:" CssClass="control-label"></asp:Label>
    <div class="input-group">
        <div class="input-group-text">📬</div>
<asp:TextBox ID="txtDireccionVend" runat="server" CssClass="form-control" placeholder="Ingresa tu dirección" required oninput="quitarErrorCampo(this)" />
    </div>
    <span id="errorDireccion" class="text-danger"></span> <!-- Mensaje de error aquí -->
</div>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
  <asp:Button ID="btnRegistrarVendedor" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrarVendedor_Click" OnClientClick="return validarYRegistrar();" />            </div>
        </div>
    </div>
</div>
                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="myModalLabel">Seleccione el perfil</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="modal-body">
            <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSeleccionarRol" runat="server" CssClass="btn btn-primary" Text="Ingresar" OnClick="btnSeleccionarRol_Click" />
        </div>
    </div>
</div>

    </form>
 <script>
   
     $('#registerVendedorModal').on('show.bs.modal', function () {
        
         document.getElementById('<%= txtDocumentoVend.ClientID %>').value = '';
        document.getElementById('<%= txtNombreVend.ClientID %>').value = '';
        document.getElementById('<%= txtApellidoVend.ClientID %>').value = '';
        document.getElementById('<%= txtCorreoVend.ClientID %>').value = '';
        document.getElementById('<%= txtContrasenaVend.ClientID %>').value = '';
        document.getElementById('<%= txtTelefonoVend.ClientID %>').value = '';
        document.getElementById('<%= txtDireccionVend.ClientID %>').value = '';

       
        document.getElementById('errorDocumento').innerText = '';
        document.getElementById('errorNombre').innerText = '';
        document.getElementById('errorApellido').innerText = '';
        document.getElementById('errorCorreo').innerText = '';
        document.getElementById('errorContrasena').innerText = '';
        document.getElementById('errorTelefono').innerText = '';
        document.getElementById('errorDireccion').innerText = '';
    });

     
     function validarYRegistrar() {
         var documento = document.getElementById('<%= txtDocumentoVend.ClientID %>').value;
        var nombre = document.getElementById('<%= txtNombreVend.ClientID %>').value;
        var apellido = document.getElementById('<%= txtApellidoVend.ClientID %>').value;
        var correo = document.getElementById('<%= txtCorreoVend.ClientID %>').value;
        var contrasena = document.getElementById('<%= txtContrasenaVend.ClientID %>').value;
        var telefono = document.getElementById('<%= txtTelefonoVend.ClientID %>').value;
        var direccion = document.getElementById('<%= txtDireccionVend.ClientID %>').value;

        var fieldsValid = true;

       
        document.getElementById('errorDocumento').innerText = '';
        document.getElementById('errorNombre').innerText = '';
        document.getElementById('errorApellido').innerText = '';
        document.getElementById('errorCorreo').innerText = '';
        document.getElementById('errorContrasena').innerText = '';
        document.getElementById('errorTelefono').innerText = '';
        document.getElementById('errorDireccion').innerText = '';

        
        if (!documento) {
            document.getElementById('errorDocumento').innerText = "Documento es obligatorio.";
            fieldsValid = false;
        }
        if (!nombre) {
            document.getElementById('errorNombre').innerText = "Nombre es obligatorio.";
            fieldsValid = false;
        }
        if (!apellido) {
            document.getElementById('errorApellido').innerText = "Apellido es obligatorio.";
            fieldsValid = false;
        }
        if (!correo) {
            document.getElementById('errorCorreo').innerText = "Correo es obligatorio.";
            fieldsValid = false;
        } else if (!correo.includes('@')) { 
            document.getElementById('errorCorreo').innerText = "El correo debe contener un '@'.";
            fieldsValid = false;
        }
        if (!contrasena) {
            document.getElementById('errorContrasena').innerText = "Contraseña es obligatoria.";
            fieldsValid = false;
        }
        if (!telefono) {
            document.getElementById('errorTelefono').innerText = "Teléfono es obligatorio.";
            fieldsValid = false;
        }
        if (!direccion) {
            document.getElementById('errorDireccion').innerText = "Dirección es obligatoria.";
            fieldsValid = false;
        }

        if (!fieldsValid) {
            return false;  
        }

        
        return true;
    }

   
    function limpiarError(campo) {
        document.getElementById('error' + campo).innerText = '';
    }

    document.getElementById('<%= txtDocumentoVend.ClientID %>').oninput = function() { limpiarError('Documento'); };
    document.getElementById('<%= txtNombreVend.ClientID %>').oninput = function() { limpiarError('Nombre'); };
    document.getElementById('<%= txtApellidoVend.ClientID %>').oninput = function() { limpiarError('Apellido'); };
    document.getElementById('<%= txtCorreoVend.ClientID %>').oninput = function() { limpiarError('Correo'); };
    document.getElementById('<%= txtContrasenaVend.ClientID %>').oninput = function() { limpiarError('Contrasena'); };
    document.getElementById('<%= txtTelefonoVend.ClientID %>').oninput = function() { limpiarError('Telefono'); };
    document.getElementById('<%= txtDireccionVend.ClientID %>').oninput = function () { limpiarError('Direccion'); };
 </script>

    </form>
</body>
</html>