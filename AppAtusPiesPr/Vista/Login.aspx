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
            background: #e8e8e8;
            font-family: Arial, sans-serif; /* Define la fuente base para toda la página */
        }

        .main-container {
            display: flex;
            flex-direction: row;
            background: #1a1a1a;
            border-radius: 10px;
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
            color: #ffffff;
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
    <script>
        $(document).ready(function () {
            $('#btnRegistrar').click(function (event) {
                var isValid = true;
                $('.modal .form-control').each(function () {
                    if ($(this).val() === '') {
                        isValid = false;
                        $(this).addClass('is-invalid');
                    } else {
                        $(this).removeClass('is-invalid');
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
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>
                <div class="link">
                    <a href="#" data-toggle="modal" data-target="#registerModal">¿No tienes una cuenta? Regístrate aquí</a>
                </div>
                <div class="register-vendor">
                    <a href="#" data-toggle="modal" data-target="#registerVendedorModal">¿Eres vendedor? Regístrate aquí</a>
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
                    <asp:TextBox ID="txtDocumentoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu documento" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNombreVend" runat="server" Text="Nombre:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtNombreVend" runat="server" CssClass="form-control" placeholder="Ingresa tu nombre" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblApellidoVend" runat="server" Text="Apellido:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtApellidoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu apellido" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCorreoVend" runat="server" Text="Correo:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtCorreoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu correo" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblContrasenaVend" runat="server" Text="Contraseña:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtContrasenaVend" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblTelefonoVend" runat="server" Text="Teléfono:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtTelefonoVend" runat="server" CssClass="form-control" placeholder="Ingresa tu teléfono" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDireccionVend" runat="server" Text="Dirección:" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtDireccionVend" runat="server" CssClass="form-control" placeholder="Ingresa tu dirección" required></asp:TextBox>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                <asp:Button ID="btnRegistrarVendedor" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrarVendedor_Click" />
            </div>
        </div>
    </div>
</div>

    </form>
</body>
</html>