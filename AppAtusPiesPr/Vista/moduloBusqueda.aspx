﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloBusqueda.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloBusqueda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='vista/css/main.css' />
    <script src="Vista/js/main.js"></script>
    <link rel="shortcut icon" href="vista/recursos/ATP.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
        <div class="container">
        <!-- Filtros -->
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
            <hr>
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
                                    <%# Eval("NombreVendedor") %><p></p>
                                    <%# Eval("apellidos") %><br>
                                </a>
                                <div class="cardprice">
                                    <p>$<%# Eval("precio") %></p>

                                </div>
                                <div class="cardButtons">
                                    <a class="buy-button" href='Vista/moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                    <a class="save-button" data-id='<%# Eval("idProdctoEmpresa") %>'>
                                        <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                    </a>

                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Pie de página -->
        <div class="pie-pagina">
            <br />
            <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
            <p>Diseñado con amor para brindar estilo y comodidad.</p>
            <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
            <br />
        </div>
    </div>
    <script src="Vista/js/carrito.js"></script>
</asp:Content>
