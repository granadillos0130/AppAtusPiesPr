<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppAtusPiesPr.Vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <style>
        body {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            background: linear-gradient(135deg, #0f2027 0%, #203a43 50%, #2c5364 100%);
            font-family: 'Arial', sans-serif;
            position: relative;
            overflow: hidden;
            color: #fff;
        }

        @keyframes gradient {
            0% {
                background-position: 0% 50%;
            }

            50% {
                background-position: 100% 50%;
            }

            100% {
                background-position: 0% 50%;
            }
        }

        body {
            background: linear-gradient(-45deg, #000000, #333333, #555555, #777777);
            background-size: 400% 400%;
            animation: gradient 15s ease infinite;
        }

        @keyframes neon {
            0%, 100% {
                box-shadow: 0 0 5px #fff, 0 0 10px #fff, 0 0 20px #fff, 0 0 40px #203a43, 0 0 80px #203a43, 0 0 90px #203a43, 0 0 100px #203a43, 0 0 150px #203a43;
            }

            50% {
                box-shadow: 0 0 5px #fff, 0 0 10px #fff, 0 0 20px #fff, 0 0 40px #2c5364, 0 0 80px #2c5364, 0 0 90px #2c5364, 0 0 100px #2c5364, 0 0 150px #2c5364;
            }
        }

        .container {
            background: rgba(0, 0, 0, 0.7);
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.3);
            width: 350px;
            position: relative;
            z-index: 1;
            animation: neon 1.5s ease-in-out infinite alternate;
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #fff;
            font-family: 'Orbitron', sans-serif;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-control {
            border-radius: 5px;
            box-shadow: none;
            border: none;
            background: rgba(255, 255, 255, 0.1);
            color: #fff;
        }

            .form-control::placeholder {
                color: #ccc;
            }

        .btn-primary {
            background-color: #203a43;
            border: none;
            border-radius: 5px;
        }

            .btn-primary:hover {
                background-color: #2c5364;
            }

        .text-danger {
            font-size: 0.9em;
        }

        .link {
            text-align: center;
            margin-top: 10px;
        }

            .link a {
                color: #2c5364;
                text-decoration: none;
            }

                .link a:hover {
                    text-decoration: underline;
                }

        .is-invalid {
            border-color: #e74c3c;
        }

        /* Estilos para la modal */
        .modal-content {
            background: rgba(0, 0, 0, 0.9);
            border-radius: 10px;
            color: #fff;
        }

        .modal-header {
            border-bottom: none;
        }

        .modal-title {
            font-family: 'Orbitron', sans-serif;
        }

        .close {
            color: #fff;
            opacity: 1;
        }

            .close:hover {
                color: #e74c3c;
            }

        .modal-body .form-group {
            margin-bottom: 15px;
        }

        .modal-body .form-control {
            background: rgba(255, 255, 255, 0.1);
            color: #fff;
        }

            .modal-body .form-control::placeholder {
                color: #ccc;
            }

        .modal-footer {
            border-top: none;
        }

            .modal-footer .btn {
                border-radius: 5px;
            }

            .modal-footer .btn-secondary {
                background-color: #2c5364;
            }

                .modal-footer .btn-secondary:hover {
                    background-color: #3c6474;
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
                        $(this).addClass('is-invalid'); // Agregar clase para indicar error
                    } else {
                        $(this).removeClass('is-invalid'); // Eliminar clase si está correcto
                    }
                });
                if (!isValid) {
                    event.preventDefault(); // Detener el envío del formulario si hay errores
                    alert('Por favor, completa todos los campos obligatorios.');
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <div class="container">
            <h2>Iniciar Sesión</h2>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingresa tu correo" required></asp:TextBox>
                <span class="text-danger" id="emailError" runat="server" visible="false">Por favor, ingresa un correo válido.</span>
            </div>
            <div class="form-group">
                <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingresa tu contraseña" required></asp:TextBox>
                <span class="text-danger" id="passwordError" runat="server" visible="false">La contraseña es obligatoria.</span>
            </div>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" />
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>
            <div class="link">
                <a href="#" data-toggle="modal" data-target="#registerModal">¿No tienes una cuenta? Regístrate aquí</a>
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

    <!-- Burbujas en el fondo -->
    <div class="bubble"></div>
    <div class="bubble"></div>
    <div class="bubble"></div>
    <div class="bubble"></div>
</body>
</html>
