<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="perfilUsuario.aspx.cs" Inherits="AppAtusPiesPr.Vista.perfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
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

    <hr />
    <br />

    <center>
        <h2>Tu Perfil</h2>
        <br>
    </center>

    <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>
    <!-- Foto del Vendedor -->
    <div class="producto-detalle">
        <div class="producto-imagen">
            <asp:Image alt="Imagen 1" ID="ImgVendedor" runat="server" />
        </div>

        <!-- Información del Cliente -->

        <div class="producto-info">
            <ul>
                <li><strong>Documento:</strong>
                    <asp:Label ID="documentoCliente" runat="server"></asp:Label></li>
                <li><strong>Nombres:</strong>
                    <asp:Label ID="nombreCliente" runat="server"></asp:Label>
                    <asp:Label ID="apellidoCliente" runat="server"></asp:Label></li>
                <li><strong>Email:</strong>
                    <asp:Label ID="emailCliente" runat="server"></asp:Label></li>
                <li><strong>Password:</strong>
                    <asp:Label ID="passCliente" runat="server"></asp:Label></li>
                <li><strong>Telefono:</strong>
                    <asp:Label ID="telCliente" runat="server"></asp:Label></li>

                <li><strong>Direccion: </strong>
                    <asp:Label ID="direcCliente" runat="server"></asp:Label></li>
                <li><strong>Estado: </strong>
                    <asp:Label ID="estadoCliente" runat="server"></asp:Label></li>
            </ul>
        </div>

    </div>
    <hr>
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
