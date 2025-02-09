<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="carritoCompras.aspx.cs" Inherits="AppAtusPiesPr.Vista.carritoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />

    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
    <link rel='stylesheet' type='text/css' media='screen' href='css/carrito.css' />
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">


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



    <div class="container">
        <!-- Botón para vaciar el carrito, ahora al principio de la página -->
        <button id="vaciarCarritoButton" class="delete-carrito">Vaciar Carrito</button>
        <br>
        <br>
        <div id="carritoContainer"></div>
    </div>

    <!-- Mensaje para inicio de sesión -->
    <div id="mensajeInicioSesion" style="display: none; text-align: center; color: red; margin-top: 20px;">
        Debes iniciar sesión para proceder con la compra.
    </div>

    <script src="js/carrito.js"></script>
    <script src="https://www.paypal.com/sdk/js?client-id=AcJ4hvEnBv9MHxg3EggwPHF7XO7mdTT_G3N0wmqj0vieh-UOmSYC02lUOH_gAVONfGFwLvuWiGmIK1LZ&currency=USD"></script>

    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
</asp:Content>
