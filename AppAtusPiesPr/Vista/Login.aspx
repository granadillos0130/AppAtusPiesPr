<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppAtusPiesPr.Vista.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            font-family: 'Arial', sans-serif;
            background: #f4f4f9;
        }

        .main-container {
            display: flex;
            background: #ffffff;
            border-radius: 15px;
            box-shadow: 0px 5px 20px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            max-width: 900px;
            width: 100%;
        }

        .login-image {
            flex: 1;
            background: url('https://media.giphy.com/media/l0MYzKvvW7ZyrSNgc/giphy.gif') no-repeat center center;
            background-size: cover;
        }

        .form-container {
            flex: 1;
            padding: 40px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }

        .form-container h2 {
            margin-bottom: 20px;
            color: #333;
            font-weight: bold;
        }

        .form-container .form-group {
            width: 100%;
            margin-bottom: 20px;
        }

        .form-container .form-control {
            border-radius: 5px;
            border: 1px solid #ccc;
            padding: 10px 15px;
            font-size: 1rem;
        }

        .form-container .btn {
            width: 100%;
            padding: 10px 15px;
            font-size: 1rem;
            border-radius: 5px;
        }

        .form-container .btn-primary {
            background-color: #007bff;
            border: none;
            transition: background-color 0.3s ease;
        }

        .form-container .btn-primary:hover {
            background-color: #0056b3;
        }

        .form-container .link {
            margin-top: 10px;
        }

        .form-container .link a {
            color: #007bff;
            text-decoration: none;
        }

        .form-container .link a:hover {
            text-decoration: underline;
        }

        .register-vendor {
            margin-top: 20px;
            text-align: center;
        }

        .register-vendor a {
            color: #28a745;
            font-weight: bold;
            text-decoration: none;
        }

        .register-vendor a:hover {
            color: #218838;
        }

        .modal-content {
            border-radius: 15px;
        }

        .modal-header, .modal-footer {
            border: none;
        }

        .modal-title {
            font-weight: bold;
            color: #333;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#btnRegistrar').click(function (event) {
                var isValid = true;
                $('.modal .form-control').each(function () {
                    if ($(this).val() === '') {
                        isValid = false;
                        $(this).addClass('is-invalid'); // Indicar error
                    } else {
                        $(this).removeClass('is-invalid'); // Quitar error
                    }
                });
                if (!isValid) {
                    event.preventDefault();
                    alert('Por favor, completa todos los campos obligatorios.');
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <div class="main-container">
            <!-- Imagen GIF alusiva al calzado -->
            <div class="login-image"></div>

            <!-- Contenedor del formulario -->
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
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>
                <div class="link">
                    <a href="#" data-toggle="modal" data-target="#registerModal">¿No tienes una cuenta? Regístrate aquí</a>
                </div>
                <div class="register-vendor">
                    <a href="RegisterVendor.aspx">¿Eres vendedor? Regístrate aquí</a>
                </div>
            </div>
        </div>

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
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ingresa tu documento" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombres:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresa tu nombre" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblApellido" runat="server" Text="Apellidos:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingresa tus apellidos" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblCorreo" runat="server" Text="Correo:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Ingresa tu correo" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblContrasenaReg" runat="server" Text="Contraseña:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtContrasenaReg" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingresa tu teléfono" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingresa tu dirección" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
