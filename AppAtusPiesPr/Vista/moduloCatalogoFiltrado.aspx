<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloCatalogoFiltrado.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloCatalogoFiltrado2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />

    <link rel="shortcut icon" href="recursos/ATP.png" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

        <div>
            <div class="container">
                <header>

                    <!-- MENÚ -->
                    <div class="navbar">
                        <nav>
                            <ul class="menu">
                                <li><a href="#">WINTER 24</a></li>
                                <li><a href="#">WOMEN</a></li>
                                <li><a href="#">MEN</a></li>
                                <li><a href="#">GIFTS</a></li>
                                <li><a href="#">COUTURE</a></li>
                                <li><a href="#">EXPLORE</a></li>
                            </ul>
                        </nav>

                        <a href="../index.aspx" class="logo">A TUS PIES</a>

                        <div class="right-section">
                            <div class="search-bar">
                                <input type="text" placeholder="Search...">
                                <i class="fas fa-search"></i>
                            </div>
                            <a href="Vista/modulos/moduloCarrito.aspx" class="save-button2">
                                <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                            </a>
                            <a href="Login.aspx" class="login">LOGIN</a>

                            <a href="#" class="bookmark"><i class="fas fa-bookmark"></i></a>
                        </div>
                    </div>
                    <hr />

                </header>

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
                    <hr>
                </center>

                <center>
                    <p class="card-title"><%# Eval("descripcion")  %></p>
                </center>

                <br>
                <!-- Contenedor para las tarjetas -->
                <div class="cards-container">
                    <!-- Tarjeta 1 -->
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="card">
                                <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' class="card-image" />
                                <p class="card-title"><%# Eval("nombreProducto") %></p>

                                <div class="card-info">
                                    <div class="card-details">
                                        <a class="buy-button" href='moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                        <a href="#" class="save-button">
                                            <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                        </a>
                                        <center>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
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
        </div>

</asp:Content>
