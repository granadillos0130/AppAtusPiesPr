<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloCatalogoFiltrado.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloCatalogoFiltrado2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">

    <link rel="shortcut icon" href="recursos/ATP.png" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div>

                    <!--Filtros-->
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

            <center>
                <p class="card-title"><%# Eval("descripcion")  %></p>
            </center>

            <br>
            <!-- Contenedor para las tarjetas -->
<div id="cardsContainer" class="cards-container">

    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <div class="card">
                <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                <h4 class="card-title"></h4>
                <div class="card-info">
                    <div class="card-details">
                        <a class="cardseller" href='<%# "vista/perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
                            <%# Eval("nombres") %>
                            <%# Eval("apellidos") %><br>
                        </a>
                        <div class="cardprice">
                            <p>$<%# Eval("precio") %></p>

                        </div>
                        <div class="cardButtons">
                            <a class="buy-button" href='moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                            <a class="save-button"
                                data-id='<%# Eval("idProdctoEmpresa") %>'
                                data-nombre='<%# HttpUtility.HtmlAttributeEncode(Eval("nombreProducto").ToString()) %>'
                                data-imagen='<%# ResolveUrl(Eval("imagen").ToString()) %>'
                                data-precio='<%# Eval("precio") %>'
                                data-vendedor='<%# HttpUtility.HtmlAttributeEncode(Eval("nombres").ToString()) %>'
                                data-apellidos='<%# HttpUtility.HtmlAttributeEncode(Eval("apellidos").ToString()) %>'
                                data-idvendedor='<%# Eval("idVendedor") %>'>
                                <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                            </a>

                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
            <script src="js/carrito.js"></script>
            

        </div>

        <!-- PIE DE PAGINA -->
<div class="pie-pagina">
    <br />
    <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
    <p>Diseñado con amor para brindar estilo y comodidad.</p>
    <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>

    <br />
</div>

    </div>
    <script src="js/main.js"></script>
</asp:Content>

