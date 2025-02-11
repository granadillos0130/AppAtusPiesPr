<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="perfilUsuario.aspx.cs" Inherits="AppAtusPiesPr.Vista.perfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />

    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


    <style>
        .profile-container {
            max-width: 800px;
            margin: 30px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }

        .profile-image {
            max-width: 200px;
            border-radius: 50%;
            object-fit: cover;
        }

        .profile-info {
            padding-left: 30px;
        }

        .modal-content {
            border-radius: 15px;
        }

        .form-control {
            margin-bottom: 15px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <center>

        <div class="navbarFiltros">
            <nav>
                <ul class="menuFiltros">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
                                    <%# Eval("descripcion") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>

    </center>
    <center>
    <div class="navbarFiltros">
        <nav>

            <ul class="menuFiltros">
                <asp:Repeater ID="RepeaterMarca" runat="server">
                    <itemtemplate>
                        <li>
                                    <a href='<%# "moduloMarcaFiltrada.aspx?id=" + Eval("idMarca") %>'>
                                        <%# Eval("nombreMarca") %>
                            </a>
                        </li>
                    </itemtemplate>
                </asp:Repeater>
            </ul>
        </nav>
    </div>
</center>

    <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>
    <!-- Foto del Vendedor -->
    <div class="container profile-container">
        <div class="row">
            <div class="col-md-4 text-center">
                <asp:Image ID="ImgVendedor" runat="server" CssClass="profile-image" alt="Imagen de Perfil" />
            </div>
            <div class="col-md-8 profile-info">
                <h2 class="mb-4">Tu Perfil</h2>

                <div class="row">
                    <div class="col-md-6">
                        <strong>Documento:</strong>
                        <asp:Label ID="documentoCliente" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <strong>Nombres:</strong>
                        <asp:Label ID="nombreCliente" runat="server"></asp:Label>
                        <asp:Label ID="apellidoCliente" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row mt-3">
                    <div class="col-md-6">
                        <strong>Email:</strong>
                        <asp:Label ID="emailCliente" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <strong>Teléfono:</strong>
                        <asp:Label ID="telCliente" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row mt-3">
                    <div class="col-md-12">
                        <strong>Dirección:</strong>
                        <asp:Label ID="direcCliente" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <center>
        <div class="profile-update-container">
            <h2 class="mb-4">Actualizar Informacion</h2>

            <div class="profile-update-form">
                <div class="form-group">
                    <label class="form-label">Documento</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Número de documento" />
                </div>
                <div class="form-group">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Correo electrónico" />
                </div>
                <div class="form-group">
                    <label class="form-label">Nombres</label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" placeholder="Nombres" />
                </div>
                <div class="form-group">
                    <label class="form-label">Apellidos</label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" placeholder="Apellidos" />
                </div>
                <div class="form-group">
                    <label class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Número de teléfono" />
                </div>
                <div class="form-group">
                    <label class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Dirección" />
                </div>
                <div class="form-group" style="grid-column: span 2;">
                    <label class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña" />
                </div>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn-save" OnClick="btnGuardar_Click" />
            </div>
        </div>

        <h2 class="mb-4">Tus Productos</h2>

        <div id="cardsContainer" class="cards-container">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />

                        <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                        <%# Eval("estado") %>
                        <h4 class="card-title"></h4>
                        <div class="card-info">
                            <div class="card-details">

                                <a class="cardseller" href='<%# "perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>


                                    <%# Eval("nombres") %>
                                    <%# Eval("apellidos") %><br>
                                </a>
                                <div class="cardprice">
                                    <p>$<%# Eval("precio") %></p>

                                </div>

                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


    </center>
    <p class="txt">¿No estás satisfecho con nuestro servicio?</p>

    <asp:Button ID="btnCancelarCuenta" runat="server" Text="Cancelar Cuenta"
        CssClass="btn btn-danger btn-derecha" OnClick="btnCancelarCuenta_Click"
        OnClientClick="return confirm('¿Está seguro que desea cancelar su cuenta? Esta acción no se puede deshacer.');" />

    <style>
        .txt {
            right: 20px;
            margin-top: 30px; /* Ajusta según sea necesario */
            margin-left: 75px;
        }

        .btn-derecha {
            right: 10px;
            margin-top: 10px; /* Ajusta según sea necesario */
            margin-left: 75px;
        }


        .profile-update-container {
            width: 100%;
            max-width: 100%;
            padding: 20px;
        }

        .profile-update-form {
            width: 60%;
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
            background-color: white;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }

        .form-group {
            display: flex;
            flex-direction: column;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            transition: all 0.3s ease;
        }

            .form-control:focus {
                border-color: #007bff;
                outline: none;
                box-shadow: 0 0 0 3px rgba(0,123,255,0.1);
            }

        .form-label {
            margin-bottom: 8px;
            color: #000000;
            font-weight: 600;
        }

        .btn-save {
            grid-column: span 2;
            justify-self: end;
            background-color: #000000;
            color: white;
            border: none;
            border-radius: 8px;
            padding: 12px 25px;
            transition: all 0.3s ease;
        }

            .btn-save:hover {
                background-color: #4a4a4a;
                color: white;
            }

        @media (max-width: 768px) {
            .profile-update-form {
                grid-template-columns: 1fr;
            }

            .btn-save {
                grid-column: span 1;
            }
        }
    </style>



    <br />

    <!-- PIE DE PAGINA -->
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
</asp:Content>
